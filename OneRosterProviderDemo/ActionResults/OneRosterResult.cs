﻿using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace OneRosterProviderDemo.ActionResults
{
    public class OneRosterResult : ContentResult
    {
        public int? count;
        public override Task ExecuteResultAsync(ActionContext context)
        {
            if (count != null)
            {
                context.HttpContext.Response.Headers.Add("X-Total-Count", $"{count}");
            }
            
            return base.ExecuteResultAsync(context);
        }
    }
}
