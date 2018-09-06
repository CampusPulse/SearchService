﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Extensions.Logging;
using System;
using System.Net;

namespace CampusPulse.SearchService.Filter
{
    public class SearchServiceExceptionFilterAtribute : ExceptionFilterAttribute
    {
        private readonly ILoggerFactory loggerFactory;
        public SearchServiceExceptionFilterAtribute(ILoggerFactory loggerFactory)
        {
            this.loggerFactory = loggerFactory;
        }
        public override void OnException(ExceptionContext context)
        {
            //add structured seri logging for kibana search
            //loggerFactory.CreateLogger<SearchServiceExceptionFilterAtribute>().LogError(Guid.NewGuid().ToString(), context.Exception, "hello", null);
            Log.Error(context.Exception, "Exception occured");
            Microsoft.Extensions.Logging.ILogger logger = loggerFactory.CreateLogger<SearchServiceExceptionFilterAtribute>();
            logger.LogError(Guid.NewGuid().ToString(), context.Exception, "hello", null);


            context.Result = new JsonResult("Unable to process the request")
            {
                StatusCode = (int)HttpStatusCode.InternalServerError
            };

            base.OnException(context);
        }
    }
}
