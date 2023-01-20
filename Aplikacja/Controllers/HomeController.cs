using Aplikacja.DTOS.JobDtos;
using Aplikacja.Entities.JobModel;
using Aplikacja.Models;
using Aplikacja.Repositories.JobRepository;
using Aplikacja.Repositories.UserRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;

namespace Aplikacja.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IJobRepository _jobRepository;
        private readonly IUserRepository _userRepository;

        public HomeController(IJobRepository jobRepository, IUserRepository userRepository)
        {
            _jobRepository = jobRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        }

        [HttpGet]
        public async Task<ActionResult<List<Job>>> Index()
        {
            return View(await _jobRepository.GetJobs());
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
           
               
                    await _jobRepository.CreateJob(job);
              
                    //await _jobRepository.UpdateJob(job,job.JobId);

                return RedirectToAction("Index", "Home");

        }

        [HttpPost]
        public async Task<ActionResult<Job>> Edit(int jobId, UpdateJobDto updateJob)
        {
            return await _jobRepository.UpdateJob(updateJob, jobId);
            return View();
        }

        [HttpDelete]
        public async Task<ActionResult<bool>> Delete(int jobId)
        {
            return await _jobRepository.DeleteJob(jobId);
        }

        [HttpPost]
        public async Task<ActionResult<bool>> AddToInbox(int jobId)
        {
            
            await _jobRepository.AddToInbox(jobId, int.Parse(User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value));
          
            return RedirectToAction("Index", "Home");
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}