using CampusPulse.SearchService.Filter;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CampusPulse.SearchService.Bootstrap
{
    public class MvcConfigurationManager
    {
        public static void ConfigureService(IServiceCollection services)
        {

            var mvcBuilder = services.AddMvc(options =>
            {
                options.Filters.Add(new SearchServiceExceptionFilterAtribute(null));
            }).AddJsonOptions(options =>
            {
                options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                options.SerializerSettings.ContractResolver = new DefaultContractResolver();
            });


        }
    }
}
