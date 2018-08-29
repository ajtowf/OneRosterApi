﻿using System.Linq;
using Microsoft.AspNetCore.Mvc;
using OneRosterProviderDemo.Models;
using Microsoft.EntityFrameworkCore;
using OneRosterProviderDemo.Serializers;

namespace OneRosterProviderDemo.Controllers
{
    [Route("ims/oneroster/v1p1/classes")]
    public class IMSClassesController : BaseController
    {
        public IMSClassesController(ApiContext db) : base(db)
        {
        }

        // GET ims/oneroster/v1p1/classes
        [HttpGet]
        public IActionResult GetAllClasses()
        {
            IQueryable<IMSClass> imsClassQuery = db.IMSClasses
                .Include(k => k.IMSClassAcademicSessions)
                    .ThenInclude(kas => kas.AcademicSession)
                .Include(k => k.Course)
                .Include(k => k.School);
            imsClassQuery = ApplyBinding(imsClassQuery);
            var imsClasses = imsClassQuery.ToList();

            serializer = new OneRosterSerializer("classes");
            serializer.Writer.WriteStartArray();
            foreach (var imsClass in imsClasses)
            {
                imsClass.AsJson(serializer.Writer, BaseUrl());
            }
            serializer.Writer.WriteEndArray();

            return JsonOk(FinishSerialization(), ResponseCount);
        }

        // GET ims/oneroster/v1p1/classes/5
        [HttpGet("{id}")]
        public IActionResult GetClass([FromRoute] string id)
        {
            var imsClasses = db.IMSClasses
                .Include(k => k.IMSClassAcademicSessions)
                    .ThenInclude(kas => kas.AcademicSession)
                .Include(k => k.Course)
                .Include(k => k.School)
                .SingleOrDefault(k => k.Id == id);

            if (imsClasses == null)
            {
                return NotFound();
            }
            serializer = new OneRosterSerializer("class");
            imsClasses.AsJson(serializer.Writer, BaseUrl());
            return JsonOk(serializer.Finish());
        }

        // GET ims/oneroster/v1p1/classes/5/students
        [HttpGet("{id}/students")]
        public IActionResult GetStudentsForClass([FromRoute] string id)
        {
            if (db.IMSClasses.SingleOrDefault(k => k.Id == id) == null)
            {
                return NotFound();
            }

            IQueryable<User> studentsQuery = db.Users
                .Include(u => u.UserOrgs)
                    .ThenInclude(uo => uo.Org)
                .Include(u => u.UserAgents)
                    .ThenInclude(ua => ua.Agent)
                .Include(u => u.Enrollments)
                .Where(u => u.Enrollments.Where(e => e.IMSClassId == id && e.Role == Vocabulary.RoleType.student).Count() > 0);
            studentsQuery = ApplyBinding(studentsQuery);
            var students = studentsQuery.ToList();

            serializer = new Serializers.OneRosterSerializer("users");
            serializer.Writer.WriteStartArray();
            foreach (var student in students)
            {
                student.AsJson(serializer.Writer, BaseUrl());
            }
            serializer.Writer.WriteEndArray();

            return JsonOk(FinishSerialization(), ResponseCount);
        }

        // GET ims/oneroster/v1p1/classes/5/teachers
        [HttpGet("{id}/teachers")]
        public IActionResult GetTeachersForClass([FromRoute] string id)
        {
            if (db.IMSClasses.SingleOrDefault(k => k.Id == id) == null)
            {
                return NotFound();
            }

            IQueryable<User> teachersQuery = db.Users
                .Include(u => u.UserOrgs)
                    .ThenInclude(uo => uo.Org)
                .Include(u => u.UserAgents)
                    .ThenInclude(ua => ua.Agent)
                .Include(u => u.Enrollments)
                .Where(u => u.Enrollments.Where(e => e.IMSClassId == id && e.Role == Vocabulary.RoleType.teacher).Count() > 0);
            teachersQuery = ApplyBinding(teachersQuery);
            var teachers = teachersQuery.ToList();

            serializer = new Serializers.OneRosterSerializer("users");
            serializer.Writer.WriteStartArray();
            foreach (var teacher in teachers)
            {
                teacher.AsJson(serializer.Writer, BaseUrl());
            }
            serializer.Writer.WriteEndArray();

            return JsonOk(FinishSerialization(), ResponseCount);
        }

        // GET ims/oneroster/v1p1/classes/5/lineItems
        [HttpGet("{id}/lineItems")]
        public IActionResult GetLineItemsForClass([FromRoute] string id)
        {
            if (db.IMSClasses.SingleOrDefault(k => k.Id == id) == null)
            {
                return NotFound();
            }

            IQueryable<LineItem> lineItemQuery = db.LineItems
                .Where(li => li.IMSClassId == id)
                .Include(li => li.LineItemCategory)
                .Include(li => li.AcademicSession);
            lineItemQuery = ApplyBinding(lineItemQuery);
            var lineItems = lineItemQuery.ToList();

            serializer = new Serializers.OneRosterSerializer("lineItems");
            serializer.Writer.WriteStartArray();
            foreach (var lineItem in lineItems)
            {
                lineItem.AsJson(serializer.Writer, BaseUrl());
            }
            serializer.Writer.WriteEndArray();

            return JsonOk(FinishSerialization(), ResponseCount);
        }

        // GET ims/oneroster/v1p1/classes/5/results
        [HttpGet("{id}/results")]
        public IActionResult GetResultsForClass([FromRoute] string id)
        {
            if (db.IMSClasses.SingleOrDefault(k => k.Id == id) == null)
            {
                return NotFound();
            }

            IQueryable<Result> resultQuery = db.Results
                .Include(r => r.Student)
                .Include(r => r.LineItem)
                .Where(r => r.LineItem.IMSClassId == id);
            resultQuery = ApplyBinding(resultQuery);
            var results = resultQuery.ToList();

            serializer = new Serializers.OneRosterSerializer("results");
            serializer.Writer.WriteStartArray();
            foreach (var result in results)
            {
                result.AsJson(serializer.Writer, BaseUrl());
            }
            serializer.Writer.WriteEndArray();

            return JsonOk(FinishSerialization(), ResponseCount);
        }

        // GET ims/oneroster/v1p1/classes/5/lineItems/5/results
        [HttpGet("{id}/lineItems/{lineItemId}/results")]
        public IActionResult GetResultsForLineItemForClass([FromRoute] string id, [FromRoute] string lineItemId)
        {
            if (db.IMSClasses.SingleOrDefault(k => k.Id == id) == null ||
                db.LineItems.SingleOrDefault(li => li.Id == lineItemId) == null)
            {
                return NotFound();
            }

            IQueryable<Result> resultQuery = db.Results
                .Where(r => r.LineItemId == lineItemId)
                .Include(r => r.Student)
                .Include(r => r.LineItem);
            resultQuery = ApplyBinding(resultQuery);
            var results = resultQuery.ToList();

            serializer = new Serializers.OneRosterSerializer("results");
            serializer.Writer.WriteStartArray();
            foreach (var result in results)
            {
                result.AsJson(serializer.Writer, BaseUrl());
            }
            serializer.Writer.WriteEndArray();

            return JsonOk(FinishSerialization(), ResponseCount);
        }

        // GET ims/oneroster/v1p1/classes/5/students/5/results
        [HttpGet("{id}/students/{studentId}/results")]
        public IActionResult GetResultsForStudentForClass([FromRoute] string id, [FromRoute] string studentId)
        {
            if (db.IMSClasses.SingleOrDefault(k => k.Id == id) == null ||
                db.Users.SingleOrDefault(u => u.Id == studentId) == null ||
                db.Enrollments.SingleOrDefault(e => e.IMSClassId == id && e.UserId == studentId && e.Role == Vocabulary.RoleType.student) == null)
            {
                return NotFound();
            }
            IQueryable<Result> resultQuery = db.Results
                .Where(r => r.StudentUserId == studentId && r.LineItem.IMSClassId == id)
                .Include(r => r.Student)
                .Include(r => r.LineItem);
            resultQuery = ApplyBinding(resultQuery);
            var results = resultQuery.ToList();

            serializer = new Serializers.OneRosterSerializer("results");
            serializer.Writer.WriteStartArray();
            foreach (var result in results)
            {
                result.AsJson(serializer.Writer, BaseUrl());
            }
            serializer.Writer.WriteEndArray();

            return JsonOk(FinishSerialization(), ResponseCount);
        }

        // GET ims/oneroster/v1p1/classes/5/resources
        [HttpGet("{id}/resources")]
        public IActionResult GetResourcesForClass([FromRoute] string id)
        {
            var imsClass = db.IMSClasses
                .Include(k => k.Course)
                .SingleOrDefault(k => k.Id == id);

            if (imsClass == null)
            {
                return NotFound();
            }

            serializer = new Serializers.OneRosterSerializer("resources");
            serializer.Writer.WriteStartArray();

            if (imsClass.Course != null && imsClass.Course.Resources != null)
            {
                foreach (var resourceId in imsClass.Course.Resources)
                {
                    var resource = db.Resources
                        .SingleOrDefault(r => r.Id == resourceId);
                    resource.AsJson(serializer.Writer, BaseUrl());
                }
            }

            if (imsClass.Resources != null)
            {
                foreach (var resourceId in imsClass.Resources)
                {
                    var resource = db.Resources
                        .SingleOrDefault(r => r.Id == resourceId);
                    resource.AsJson(serializer.Writer, BaseUrl());
                }
            }

            serializer.Writer.WriteEndArray();
            return JsonOk(FinishSerialization(), ResponseCount);
        }
    }
}
