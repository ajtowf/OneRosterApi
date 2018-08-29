﻿using Microsoft.AspNetCore.Mvc;
using OneRosterProviderDemo.Models;
using System.Linq;
using OneRosterProviderDemo.Serializers;
using Microsoft.EntityFrameworkCore;

namespace OneRosterProviderDemo.Controllers
{
    [Route("ims/oneroster/v1p1/enrollments")]
    public class EnrollmentsController : BaseController
    {
        public EnrollmentsController(ApiContext _db) : base(_db)
        {

        }

        // GET ims/oneroster/v1p1/enrollments
        [HttpGet]
        public IActionResult GetAllEnrollments()
        {
            IQueryable<Enrollment> enrollmentsQuery = db.Enrollments
                .Include(e => e.User)
                .Include(e => e.IMSClass)
                .Include(e => e.School);
            enrollmentsQuery = ApplyBinding(enrollmentsQuery);
            var enrollments = enrollmentsQuery.ToList();

            serializer = new OneRosterSerializer("enrollments");
            serializer.Writer.WriteStartArray();
            foreach (var enrollment in enrollments)
            {
                enrollment.AsJson(serializer.Writer, BaseUrl());
            }
            serializer.Writer.WriteEndArray();

            return JsonOk(FinishSerialization(), ResponseCount);
        }

        // GET ims/oneroster/v1p1/enrollments/5
        [HttpGet("{id}")]
        public IActionResult GetEnrollment([FromRoute] string id)
        {
            var enrollment = db.Enrollments
                .Include(e => e.User)
                .Include(e => e.IMSClass)
                .Include(e => e.School)
                .SingleOrDefault(a => a.Id == id);

            if (enrollment == null)
            {
                return NotFound();
            }
            serializer = new OneRosterSerializer("enrollment");
            enrollment.AsJson(serializer.Writer, BaseUrl());
            return JsonOk(serializer.Finish());
        }
    }
}
