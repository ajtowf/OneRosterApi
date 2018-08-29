using Microsoft.AspNetCore.Mvc;
using OneRosterProviderDemo.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using OneRosterProviderDemo.Serializers;

namespace OneRosterProviderDemo.Controllers
{
    [Route("ims/oneroster/v1p1/courses")]
    public class CoursesController : BaseController
    {
        public CoursesController(ApiContext _db) : base(_db)
        {

        }

        // GET ims/oneroster/v1p1/courses
        [HttpGet]
        public IActionResult GetAllCourses()
        {
            IQueryable<Course> courseQuery = db.Courses
                .Include(c => c.SchoolYearAcademicSession)
                .Include(c => c.Org);
            courseQuery = ApplyBinding(courseQuery);
            var courses = courseQuery.ToList();

            serializer = new OneRosterSerializer("courses");
            serializer.Writer.WriteStartArray();
            foreach (var course in courses)
            {
                course.AsJson(serializer.Writer, BaseUrl());
            }
            serializer.Writer.WriteEndArray();

            return JsonOk(FinishSerialization(), ResponseCount);
        }

        // GET ims/oneroster/v1p1/courses/5
        [HttpGet("{id}")]
        public IActionResult GetCourse([FromRoute] string id)
        {
            var course = db.Courses
                .Include(c => c.SchoolYearAcademicSession)
                .Include(c => c.Org)
                .FirstOrDefault(c => c.Id == id);

            if (course == null)
            {
                return NotFound();
            }
            serializer = new OneRosterSerializer("course");
            course.AsJson(serializer.Writer, BaseUrl());
            return JsonOk(serializer.Finish());
        }

        // GET ims/oneroster/v1p1/courses/{id}/classes
        [HttpGet("{id}/classes")]
        public IActionResult GetClassesForCourse([FromRoute] string id)
        {
            var imsClasses = db.IMSClasses
                .Include(k => k.IMSClassAcademicSessions)
                    .ThenInclude(kas => kas.AcademicSession)
                .Include(k => k.Course)
                .Include(k => k.School)
                .Where(k => k.CourseId == id);

            if (!imsClasses.Any())
            {
                return NotFound();
            }

            serializer = new OneRosterSerializer("resources");
            serializer.Writer.WriteStartArray();
            foreach (var imsClass in imsClasses)
            {
                imsClass.AsJson(serializer.Writer, BaseUrl());
            }
            serializer.Writer.WriteEndArray();
            return JsonOk(serializer.Finish());
        }

        // GET ims/oneroster/v1p1/courses/5/resources
        [HttpGet("{courseId}/resources")]
        public IActionResult GetResourcesForCourse([FromRoute] string courseId)
        {
            var course = db.Courses
                .FirstOrDefault(c => c.Id == courseId);

            if (course == null || course.Resources == null)
            {
                return NotFound();
            }

            serializer = new OneRosterSerializer("resources");
            serializer.Writer.WriteStartArray();
            foreach (string resourceId in course.Resources)
            {
                var resource = db.Resources
                    .FirstOrDefault(r => r.Id == resourceId);

                resource.AsJson(serializer.Writer, BaseUrl());
            }
            serializer.Writer.WriteEndArray();
            return JsonOk(serializer.Finish());
        }
    }
}
