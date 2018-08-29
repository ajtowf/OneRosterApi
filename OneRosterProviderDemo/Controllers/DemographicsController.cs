using System.Linq;
using Microsoft.AspNetCore.Mvc;
using OneRosterProviderDemo.Models;
using OneRosterProviderDemo.Serializers;

namespace OneRosterProviderDemo.Controllers
{
    [Route("ims/oneroster/v1p1/demographics")]
    public class DemographicsController : BaseController
    {
        public DemographicsController(ApiContext _db) : base(_db)
        {
        }

        // GET ims/oneroster/v1p1/demographics
        [HttpGet]
        public IActionResult GetAllDemographics()
        {
            IQueryable<Demographic> demographicsQuery = db.Demographics;
            demographicsQuery = ApplyBinding(demographicsQuery);
            var demographics = demographicsQuery.ToList();

            serializer = new OneRosterSerializer("demographics");
            serializer.Writer.WriteStartArray();
            foreach (var demographic in demographics)
            {
                demographic.AsJson(serializer.Writer, BaseUrl());
            }
            serializer.Writer.WriteEndArray();

            return JsonOk(FinishSerialization(), ResponseCount);
        }

        // GET ims/oneroster/v1p1/demographics/5
        [HttpGet("{user_id}")]
        public IActionResult GetDemographic([FromRoute] string user_id)
        {
            var user = db.Demographics
                .SingleOrDefault(u => u.Id == user_id);

            if (user == null)
            {
                return NotFound();
            }

            serializer = new Serializers.OneRosterSerializer("demographic");
            user.AsJson(serializer.Writer, BaseUrl());

            return JsonOk(serializer.Finish());
        }
    }
}