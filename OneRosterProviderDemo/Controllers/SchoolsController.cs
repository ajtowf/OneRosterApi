using Microsoft.AspNetCore.Mvc;
using OneRosterProviderDemo.Models;
using Microsoft.EntityFrameworkCore;
using OneRosterProviderDemo.Serializers;
using System.Linq;

namespace OneRosterProviderDemo.Controllers
{
    [Route("ims/oneroster/v1p1/schools")]
    public class SchoolsController : BaseController
    {
        public SchoolsController(ApiContext db) : base(db)
        {

        }

        // GET ims/oneroster/v1p1/schools
        [HttpGet]
        public IActionResult GetAllSchools()
        {
            IQueryable<Models.Org> orgsQuery = db.Orgs
                .Where(o => o.Type == Vocabulary.OrgType.school)
                .Include(o => o.Parent)
                .Include(o => o.Children);
            orgsQuery = ApplyBinding(orgsQuery);
            var orgs = orgsQuery.ToList();

            serializer = new OneRosterSerializer("orgs");
            serializer.Writer.WriteStartArray();
            foreach (var org in orgs)
            {
                org.AsJson(serializer.Writer, BaseUrl());
            }
            serializer.Writer.WriteEndArray();

            return JsonOk(FinishSerialization(), ResponseCount);
        }

        private Org LookupSchool(string id)
        {
            return db.Orgs
                .Where(o => o.Type == Vocabulary.OrgType.school)
                .Include(o => o.Parent)
                .Include(o => o.Children)
                .SingleOrDefault(o => o.Id == id);
        }

        // GET ims/oneroster/v1p1/schools/5
        [HttpGet("{id}")]
        public IActionResult GetSchool([FromRoute] string id)
        {
            var org = LookupSchool(id);

            if (org == null)
            {
                return NotFound();
            }

            serializer = new OneRosterSerializer("org");
            org.AsJson(serializer.Writer, BaseUrl());
            return JsonOk(serializer.Finish());
        }

        // GET schools/{id}/courses
        [HttpGet("{id}/courses")]
        public IActionResult GetCoursesForSchool([FromRoute] string id)
        {
            IQueryable<Course> courses = db.Courses
                .Include(c => c.SchoolYearAcademicSession)
                .Include(c => c.Org)
                .Where(c => c.OrgId == id);
            courses = ApplyBinding(courses);

            if(!courses.Any())
            {
                return NotFound();
            }
            serializer = new OneRosterSerializer("courses");
            serializer.Writer.WriteStartArray();
            foreach (var course in courses)
            {
                course.AsJson(serializer.Writer, BaseUrl());
            }
            serializer.Writer.WriteEndArray();

            return JsonOk(serializer.Finish(), ResponseCount);
        }

        // GET ims/oneroster/v1p1/schools/{id}/enrollments
        [HttpGet("{id}/enrollments")]
        public IActionResult GetEnrollmentsForSchool([FromRoute] string id)
        {
            IQueryable<Enrollment> enrollments = db.Enrollments
                .Include(e => e.User)
                .Include(e => e.IMSClass)
                .Include(e => e.School)
                .Where(e => e.IMSClass.SchoolOrgId == id);
            enrollments = ApplyBinding(enrollments);

            if(!enrollments.Any())
            {
                return NotFound();
            }

            serializer = new OneRosterSerializer("enrollments");
            serializer.Writer.WriteStartArray();
            foreach (var enrollment in enrollments)
            {
                enrollment.AsJson(serializer.Writer, BaseUrl());
            }
            serializer.Writer.WriteEndArray();

            return JsonOk(serializer.Finish(), ResponseCount);
        }

        // GET ims/oneroster/v1p1/schools/{id}/classes
        [HttpGet("{id}/classes")]
        public IActionResult GetClassesForSchool([FromRoute] string id)
        {
            var imsClasses = db.IMSClasses
                .Include(k => k.IMSClassAcademicSessions)
                    .ThenInclude(kas => kas.AcademicSession)
                .Include(k => k.Course)
                .Include(k => k.School)
                .Where(k => k.SchoolOrgId == id);

            imsClasses = ApplyBinding(imsClasses);

            if (!imsClasses.Any())
            {
                return NotFound();
            }

            serializer = new OneRosterSerializer("classes");
            serializer.Writer.WriteStartArray();
            foreach (var imsClass in imsClasses)
            {
                imsClass.AsJson(serializer.Writer, BaseUrl());
            }
            serializer.Writer.WriteEndArray();
            return JsonOk(serializer.Finish(), ResponseCount);
        }   

        // GET ims/oneroster/v1p1/schools/{id}/students
        [HttpGet("{id}/students")]
        public IActionResult GetStudentsForSchool([FromRoute] string id)
        {
            IQueryable<User> students = db.Users
                .Where(u => u.Role == Vocabulary.RoleType.student)
                .Include(u => u.UserOrgs).ThenInclude(uo => uo.Org)
                .Include(u => u.UserAgents).ThenInclude(ua => ua.Agent);

            students = ApplyBinding(students);

            if (!students.Any())
            {
                return NotFound();
            }
            
            serializer = new OneRosterSerializer("students");
            serializer.Writer.WriteStartArray();
            foreach(var student in students)
            {
                foreach(var org in student.UserOrgs)
                {
                    if(org.OrgId == id)
                    {
                        student.AsJson(serializer.Writer, BaseUrl());
                    }
                }
            }
            serializer.Writer.WriteEndArray();
            return JsonOk(serializer.Finish(), ResponseCount);
        }

        // GET ims/oneroster/v1p1/schools/{id}/teachers
        [HttpGet("{id}/teachers")]
        public IActionResult GetTeachersForSchool([FromRoute] string id)
        {
            IQueryable<User> teachers = db.Users
                .Where(u => u.Role == Vocabulary.RoleType.teacher)
                .Include(u => u.UserOrgs).ThenInclude(uo => uo.Org)
                .Include(u => u.UserAgents).ThenInclude(ua => ua.Agent);
            teachers = ApplyBinding(teachers); 
    
            if(!teachers.Any())
            {
                return NotFound();
            }

            serializer = new OneRosterSerializer("teachers");
            serializer.Writer.WriteStartArray();
            foreach (var teacher in teachers)
            {
                foreach(var org in teacher.UserOrgs)
                {
                    if(org.OrgId == id)
                    {
                        teacher.AsJson(serializer.Writer, BaseUrl());
                    }
                }
            }
            serializer.Writer.WriteEndArray();
            return JsonOk(serializer.Finish(), ResponseCount);
        }

        // GET ims/oneroster/v1p1/schools/{id}/terms
        [HttpGet("{id}/terms")]
        public IActionResult GetTermsForSchool([FromRoute] string id)
        {
            IQueryable<Course> courses = db.Courses
                .Include(c => c.SchoolYearAcademicSession)
                .Where(c => c.OrgId == id);
            courses = ApplyBinding(courses);

            if (!courses.Any())
            {
                return NotFound();
            }

            var terms = courses.Select(c => c.SchoolYearAcademicSession);

            serializer = new OneRosterSerializer("terms");
            serializer.Writer.WriteStartArray();
            foreach (var term in terms)
            {
                term.AsJson(serializer.Writer, BaseUrl());
            }
            serializer.Writer.WriteEndArray();
            return JsonOk(serializer.Finish(), ResponseCount);
        }

        // GET ims/oneroster/v1p1/schools/{school_id}/classes/class_id}/enrollments
        [HttpGet("{schoolId}/classes/{classId}/enrollments")]
        public IActionResult GetEnrollmentsForClassInSchool([FromRoute] string schoolId, string classId)
        {
            var imsClass = db.IMSClasses
                .Where(k => k.SchoolOrgId == schoolId)
                .Include(k => k.Enrollments)
                    .ThenInclude(e => e.User)
                .Include(k => k.Enrollments)
                    .ThenInclude(e => e.IMSClass)
                .Include(k => k.Enrollments)
                    .ThenInclude(e => e.School)
                .SingleOrDefault(k => k.Id == classId);

            if(imsClass == null)
            {
                return NotFound();
            }

            serializer = new OneRosterSerializer("enrollments");
            serializer.Writer.WriteStartArray();
            foreach (var enrollment in imsClass.Enrollments)
            {
                enrollment.AsJson(serializer.Writer, BaseUrl());
            }
            serializer.Writer.WriteEndArray();
            return JsonOk(serializer.Finish());
        }

        // GET ims/oneroster/v1p1/schools/{school_id}/classes/{class_id}/students
        [HttpGet("{schoolId}/classes/{classId}/students")]
        public IActionResult GetStudentsForClassInSchool([FromRoute] string schoolId, string classId)
        {
            var imsClass = db.IMSClasses
                .Where(k => k.SchoolOrgId == schoolId)
                .Include(k => k.Enrollments)
                    .ThenInclude(e => e.User)
                        .ThenInclude(u => u.UserOrgs)
                            .ThenInclude(uo => uo.Org)
                .Include(k => k.Enrollments)
                    .ThenInclude(e => e.User)
                        .ThenInclude(u => u.UserAgents)
                            .ThenInclude(ua => ua.Agent)
                .SingleOrDefault(k => k.Id == classId);

            if(imsClass == null)
            {
                return NotFound();
            }

            serializer = new OneRosterSerializer("students");
            serializer.Writer.WriteStartArray();
            foreach (var enrollment in imsClass.Enrollments)
            {
                var user = enrollment.User;
                if(user.Role == Vocabulary.RoleType.student)
                {
                    user.AsJson(serializer.Writer, BaseUrl());
                }
            }
            serializer.Writer.WriteEndArray();
            return JsonOk(serializer.Finish());
        }

        // GET ims/oneroster/v1p1/schools/{school_id}/classes/{class_id}/teachers
        [HttpGet("{schoolId}/classes/{classId}/teachers")]
        public IActionResult GetTeachersForClassInSchool([FromRoute] string schoolId, string classId)
        {
            var imsClass = db.IMSClasses
                .Where(k => k.SchoolOrgId == schoolId)
                .Include(k => k.Enrollments)
                    .ThenInclude(e => e.User)
                        .ThenInclude(u => u.UserOrgs)
                            .ThenInclude(uo => uo.Org)
                .Include(k => k.Enrollments)
                    .ThenInclude(e => e.User)
                        .ThenInclude(u => u.UserAgents)
                            .ThenInclude(ua => ua.Agent)
                .SingleOrDefault(k => k.Id == classId);

            if (imsClass == null)
            {
                return NotFound();
            }

            serializer = new OneRosterSerializer("teachers");
            serializer.Writer.WriteStartArray();
            foreach (var enrollment in imsClass.Enrollments)
            {
                var user = enrollment.User;
                if (user.Role == Vocabulary.RoleType.teacher)
                {
                    user.AsJson(serializer.Writer, BaseUrl());
                }
            }
            serializer.Writer.WriteEndArray();
            return JsonOk(serializer.Finish());
        }
    }
}
