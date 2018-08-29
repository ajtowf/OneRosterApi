using Microsoft.AspNetCore.Mvc;
using OneRosterProviderDemo.Models;
using System.Linq;
using OneRosterProviderDemo.Serializers;
using Microsoft.EntityFrameworkCore;

namespace OneRosterProviderDemo.Controllers
{
    [Route("ims/oneroster/v1p1/terms")]
    public class TermsController : BaseController
    {
        public TermsController(ApiContext _db) : base(_db)
        {

        }

        // GET ims/oneroster/v1p1/terms
        [HttpGet]
        public IActionResult GetAllTerms()
        {
            IQueryable<AcademicSession> termsQuery = db.AcademicSessions
                .Where(ac => ac.Type == Vocabulary.SessionType.term);
            termsQuery = ApplyBinding(termsQuery);
            var terms = termsQuery.ToList();

            serializer = new OneRosterSerializer("terms");
            serializer.Writer.WriteStartArray();
            foreach (var tern in terms)
            {
                tern.AsJson(serializer.Writer, BaseUrl());
            }
            serializer.Writer.WriteEndArray();

            return JsonOk(FinishSerialization(), ResponseCount);
        }

        // GET ims/oneroster/v1p1/terms/5
        [HttpGet("{id}")]
        public IActionResult GetTerm([FromRoute] string id)
        {
            var term = db.AcademicSessions.SingleOrDefault(a => a.Id == id && a.Type == Vocabulary.SessionType.term);

            if (term == null)
            {
                return NotFound();
            }

            serializer = new OneRosterSerializer("term");
            term.AsJson(serializer.Writer, BaseUrl());
            return JsonOk(serializer.Finish());
        }

        // GET ims/oneroster/v1p1/terms/{id}/classes
        [HttpGet("{id}/classes")]
        public IActionResult GetClassesForTerm([FromRoute] string id)
        {
            var imsClassAcademicSessions = db.IMSClassAcademicSessions
                .Include(kas => kas.IMSClass)
                    .ThenInclude(k => k.Course)
                .Include(kas => kas.IMSClass)
                    .ThenInclude(k => k.School)
                .Include(kas => kas.AcademicSession)
                .Where(kas => kas.AcademicSessionId == id);

            if(!imsClassAcademicSessions.Any())
            {
                return NotFound();
            }

            serializer = new OneRosterSerializer("terms");
            serializer.Writer.WriteStartArray();
            foreach (var cas in imsClassAcademicSessions)
            {
                cas.IMSClass.AsJson(serializer.Writer, BaseUrl());
            }
            serializer.Writer.WriteEndArray();
            return JsonOk(serializer.Finish());
        }

        // GET ims/oneroster/v1p1/terms/{id}/gradingPeriods
        [HttpGet("{id}/gradingPeriods")]
        public IActionResult GetGradingPeriodsForTerm([FromRoute] string id)
        {
            var term = db.AcademicSessions
                .Include(s => s.Children)
                .SingleOrDefault(a => a.Id == id && a.Type == Vocabulary.SessionType.term);

            if(term == null)
            {
                return NotFound();
            }

            serializer = new OneRosterSerializer("gradingPeriods");
            serializer.Writer.WriteStartArray();
            foreach (var child in term.Children)
            {
                if(child.Type == Vocabulary.SessionType.gradingPeriod)
                {
                    child.AsJson(serializer.Writer, BaseUrl());
                }
            }
            serializer.Writer.WriteEndArray();
            return JsonOk(serializer.Finish());
        }
    }
}
