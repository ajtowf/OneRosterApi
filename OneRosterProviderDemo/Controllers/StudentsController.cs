using Microsoft.AspNetCore.Mvc;
using OneRosterProviderDemo.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using OneRosterProviderDemo.Serializers;

namespace OneRosterProviderDemo.Controllers
{
    [Route("ims/oneroster/v1p1/students")]
    public class StudentsController : BaseController
    {
        public StudentsController(ApiContext _db) : base(_db)
        {

        }

        // GET ims/oneroster/v1p1/students
        [HttpGet]
        public IActionResult GetAllStudents()
        {
            IQueryable<User> studentQuery = db.Users
                .Where(u => u.Role == Vocabulary.RoleType.student)
                .Include(u => u.UserOrgs).ThenInclude(uo => uo.Org)
                .Include(u => u.UserAgents).ThenInclude(ua => ua.Agent);
            studentQuery = ApplyBinding(studentQuery);
            var students = studentQuery.ToList();

            serializer = new OneRosterSerializer("users");
            serializer.Writer.WriteStartArray();
            foreach (var student in students)
            {
                student.AsJson(serializer.Writer, BaseUrl());
            }
            serializer.Writer.WriteEndArray();

            return JsonOk(FinishSerialization(), ResponseCount);
        }

        // GET ims/oneroster/v1p1/students/5
        [HttpGet("{id}")]
        public IActionResult GetStudent([FromRoute] string id)
        {
            var student = db.Users
                .Include(u => u.UserOrgs).ThenInclude(uo => uo.Org)
                .Include(u => u.UserAgents).ThenInclude(ua => ua.Agent)
                .SingleOrDefault(u => u.Id == id && u.Role == Vocabulary.RoleType.student);

            if (student == null)
            {
                return NotFound();
            }

            serializer = new OneRosterSerializer("user");
            student.AsJson(serializer.Writer, BaseUrl());

            return JsonOk(serializer.Finish());
        }

        // GET ims/oneroster/v1p1/students/{student_id}/classes
        [HttpGet("{id}/classes")]
        public IActionResult GetClassesForStudent([FromRoute] string id)
        {
            var student = db.Users
                .Include(u => u.UserOrgs).ThenInclude(uo => uo.Org)
                .Include(u => u.UserAgents).ThenInclude(ua => ua.Agent)
                .Include(u => u.Enrollments).ThenInclude(e => e.IMSClass)
                    .ThenInclude(k => k.IMSClassAcademicSessions)
                        .ThenInclude(kas => kas.AcademicSession)
                .Include(u => u.Enrollments).ThenInclude(e => e.IMSClass)
                    .ThenInclude(k => k.Course)
                .Include(u => u.Enrollments).ThenInclude(e => e.IMSClass)
                    .ThenInclude(k => k.School)
                .SingleOrDefault(u => u.Id == id && u.Role == Vocabulary.RoleType.student);

            if(student == null || !student.Enrollments.Any())
            {
                return NotFound();
            }

            serializer = new OneRosterSerializer("classes");
            serializer.Writer.WriteStartArray();
            foreach(var enrollment in student.Enrollments)
            {
                enrollment.IMSClass.AsJson(serializer.Writer, BaseUrl());
            }
            serializer.Writer.WriteEndArray();
            return JsonOk(serializer.Finish());
        }
    }
}
