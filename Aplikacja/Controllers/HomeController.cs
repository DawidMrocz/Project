using Aplikacja.DTOS.JobDtos;
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

        [HttpGet]
        public async Task<ActionResult<List<Job>>> Index()
        {
            return View(await _jobRepository.GetJobs());
        }

        [HttpGet]
        public async Task<ActionResult<Job>> Details(int id)
        {
            return View(await _jobRepository.GetJob(id));
        }

        [HttpGet]
        public async Task<ActionResult<Job>> CreateOrEdit(int id = 0)
        {
            if (id == 0)
                return View(new Job());
            else
                return View(await _jobRepository.GetJob(id));
        }

        [HttpPost]
        public async Task<ActionResult<Job>> Create(Job job)
        {
            if (ModelState.IsValid)
            {
                if (job.JobId== 0)
                {
                    await _jobRepository.CreateJob(job);
                }
                else
                    await _jobRepository.UpdateJob(job,job.JobId);

                return RedirectToAction(nameof(Index));
            }
            return View(job);
        }

        [HttpPost]
        public async Task<ActionResult<Job>> Edit(int jobId, UpdateJobDto updateJob)
        {
            //return await _jobRepository.UpdateJob(updateJob, jobId);
            return View();
        }

        //[HttpDelete]
        //public async Task<ActionResult<bool>> Delete(int jobId)
        //{
        //    return await _jobRepository.DeleteJob(jobId);
        //}



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}