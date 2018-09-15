using CampusPulse.Core.Service;
using Microsoft.AspNetCore.Hosting;

namespace CampusPulse.SearchService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            ServiceWebHost<Startup>.BuildWebHost(args).Run();
        }
    }
}
