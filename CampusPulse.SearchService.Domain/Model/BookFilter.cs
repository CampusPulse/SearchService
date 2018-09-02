using System;
using System.Collections.Generic;
using System.Text;

namespace CampusPulse.SearchService.Domain.Model
{
    public class BookFilter
    {
        public string Acedamics { get; set; }
        public string Branch { get; set; }
        public int Semester { get; set; }
        public int Year { get; set; }
        public string Title { get; set; }      
        public string Author { get; set; }
    }
}
