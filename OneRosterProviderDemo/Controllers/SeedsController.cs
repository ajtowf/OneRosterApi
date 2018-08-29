using System;
using Microsoft.AspNetCore.Mvc;
using OneRosterProviderDemo.Models;

namespace OneRosterProviderDemo.Controllers
{
    [Route("seeds")]
    public class SeedsController : BaseController
    {
        public SeedsController(ApiContext _db) : base(_db)
        {

        }

        [HttpGet]
        public IActionResult Reset()
        {
            try
            {
                SeedData.Initialize(db);
            }
            catch (Exception e)
            {
                return Ok(e.ToString());
            }
            return Ok("Seeded");
        }
    }
}