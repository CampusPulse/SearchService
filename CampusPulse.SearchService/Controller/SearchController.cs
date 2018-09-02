using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CampusPulse.Core.Domain;
using CampusPulse.SearchService.Domain.Model;
using CampusPulse.SearchService.Manager;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Service.Controller
{

    public class SearchController : ControllerBase
    {
        private readonly ISearchManager searchManager;
        private readonly ILogger logger;
        public SearchController(ISearchManager searchManager, ILogger logger)
        {
            this.searchManager = searchManager;
        }
        [Produces(typeof(ICollection<Book>))]
        public IActionResult Get(BookFilter filter)
        {
            if (filter == null)
            {
                //logger.
            }
            return Ok(searchManager.GetBooks(filter));
        }
    }
}