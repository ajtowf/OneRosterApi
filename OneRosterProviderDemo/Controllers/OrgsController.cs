﻿using Microsoft.AspNetCore.Mvc;
using OneRosterProviderDemo.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using OneRosterProviderDemo.Serializers;

namespace OneRosterProviderDemo.Controllers
{
    [Route("ims/oneroster/v1p1/orgs")]
    public class OrgsController : BaseController
    {
        public OrgsController(ApiContext _db) : base(_db)
        {

        }

        // GET ims/oneroster/v1p1/orgs
        [HttpGet]
        public IActionResult GetAllOrgs()
        {
            IQueryable<Models.Org> orgsQuery = db.Orgs
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

        // GET ims/oneroster/v1p1/orgs/5
        [HttpGet("{id}")]
        public IActionResult GetOrg([FromRoute] string id)
        {
            var org = db.Orgs
                .Include(o => o.Parent)
                .Include(o => o.Children)
                .SingleOrDefault(a => a.Id == id);

            if (org == null)
            {
                return NotFound();
            }

            serializer = new OneRosterSerializer("org");
            org.AsJson(serializer.Writer, BaseUrl());
            return JsonOk(serializer.Finish());
        }
    }
}
