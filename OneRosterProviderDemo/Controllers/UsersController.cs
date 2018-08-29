using System.Linq;
using Microsoft.AspNetCore.Mvc;
using OneRosterProviderDemo.Models;
using Microsoft.EntityFrameworkCore;

namespace OneRosterProviderDemo.Controllers
{
    [Route("ims/oneroster/v1p1/users")]
    public class UsersController : BaseController
    {
        public UsersController(ApiContext _db) : base(_db)
        {
        }

        // GET ims/oneroster/v1p1/users
        [HttpGet]
        public IActionResult GetAllUsers()
        {
            IQueryable<User> userQuery = db.Users
                .Include(u => u.UserOrgs)
                    .ThenInclude(uo => uo.Org)
                .Include(u => u.UserAgents)
                    .ThenInclude(ua => ua.Agent);
            userQuery = ApplyBinding(userQuery);
            var users = userQuery.ToList();

            serializer = new Serializers.OneRosterSerializer("users");
            serializer.Writer.WriteStartArray();
            foreach (var user in users)
            {
                user.AsJson(serializer.Writer, BaseUrl());
            }
            serializer.Writer.WriteEndArray();

            return JsonOk(FinishSerialization(), ResponseCount);
        }

        // GET ims/oneroster/v1p1/users/5
        [HttpGet("{id}")]
        public IActionResult GetUser([FromRoute] string id)
        {
            var user = db.Users
                .Include(u => u.UserOrgs)
                    .ThenInclude(uo => uo.Org)
                .Include(u => u.UserAgents)
                    .ThenInclude(ua => ua.Agent)
                .SingleOrDefault(u => u.Id == id);

            if (user == null)
            {
                return NotFound();
            }

            serializer = new Serializers.OneRosterSerializer("user");
            user.AsJson(serializer.Writer, BaseUrl());

            return JsonOk(serializer.Finish());
        }

        // GET ims/oneroster/v1p1/users/5/classes
        [HttpGet("{id}/classes")]
        public IActionResult GetClassesForUser([FromRoute] string id)
        {
            var user = db.Users
                .Include(u => u.UserOrgs)
                    .ThenInclude(uo => uo.Org)
                .Include(u => u.UserAgents)
                    .ThenInclude(ua => ua.Agent)
                .SingleOrDefault(u => u.Id == id);

            if (user == null)
            {
                return NotFound();
            }

            // get all Enrollments for the given userId
            var enrollments = db.Enrollments
                .Include(e => e.IMSClass)
                    .ThenInclude(k => k.IMSClassAcademicSessions)
                        .ThenInclude(kas => kas.AcademicSession)
                .Include(e => e.IMSClass)
                    .ThenInclude(k => k.Course)
                .Include(e => e.IMSClass)
                    .ThenInclude(k => k.School)
                .Where(e => e.UserId == id);
            serializer = new Serializers.OneRosterSerializer("classes");
            serializer.Writer.WriteStartArray();
            foreach (var enrollment in enrollments)
            {
                enrollment.IMSClass.AsJson(serializer.Writer, BaseUrl());
            }
            serializer.Writer.WriteEndArray();

            return JsonOk(serializer.Finish());
        }
    }
}
