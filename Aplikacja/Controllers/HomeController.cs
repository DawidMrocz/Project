using Aplikacja.Entities.JobModel;
using Aplikacja.Models;
using Aplikacja.Repositories.JobRepository;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Aplikacja.Controllers
{
    public class HomeController : Controller
    {
        private readonly IJobRepository _jobRepository;

        public HomeController(IJobRepository jobRepository)
        {
            _jobRepository = jobRepository;
        }

        public async Task<ActionResult<List<Job>>> Index()
        {
            return View(await _jobRepository.GetJobs());
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}