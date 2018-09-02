using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace CampusPulse.SearchService.Filter
{
    public class SearchServiceExceptionFilterAtribute : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            //add structured seri logging for kibana search
            context.Result = new JsonResult("Unable to process the request")
            {
                StatusCode = (int)HttpStatusCode.Forbidden
            };

            base.OnException(context);
        }
    }
}
