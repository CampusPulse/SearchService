using Microsoft.Extensions.DependencyInjection;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CampusPulse.SearchService.Bootstrap
{
    public class LoggingConfigurationManager
    {
        public static void ConfigureService(IServiceCollection services)
        {
            services.AddLogging(loggingBuilder => loggingBuilder.AddSerilog(dispose: true));
        }
    }
}
