using Aplikacja.DTOS.JobDtos;
using Aplikacja.Entities.InboxModel;
using Aplikacja.Entities.JobModel;
using Aplikacja.Extensions;
using Aplikacja.Models;
using Aplikacja.Repositories.UserRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;

namespace Aplikacja.Repositories.JobRepository
{
    public class JobRepository : IJobRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IDistributedCache _cache;
        public JobRepository(ApplicationDbContext context, IDistributedCache cache)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _cache = cache ?? throw new ArgumentNullException(nameof(cache));
        }

        
        public async Task<List<Job>> GetJobs()
        {
            return await _context.Jobs.ToListAsync();
        }
        public async Task<bool> AddToInbox(int jobId, int userId)
        {

            var itemToAdd = await _context.Jobs.SingleAsync(j => j.JobId == jobId);

            if (itemToAdd == null) throw new BadHttpRequestException("Job does not exist");

            var userInbox = await _context.Inboxs.SingleAsync(i => i.UserId == userId);

            var itemInUserInbox = await _context.InboxItems.AnyAsync(u => u.InboxId == userInbox.InboxId && u.JobId == itemToAdd.JobId);

            if(itemInUserInbox) throw new BadHttpRequestException("Job is already in inbox");

            InboxItem newInboxItem = new InboxItem()
            {
                Hours = 0,
                Components = 0,
                DrawingsComponents = 0,
                DrawingsAssembly = 0,
                WhenComplete = "IDK",
                JobId = itemToAdd.JobId,
                InboxId = userInbox.InboxId
            };

            await _context.InboxItems.AddAsync(newInboxItem);
            await _context.SaveChangesAsync();
   
            return true;
        }

        public async Task<Job> GetJob(int Id)
        {
            return await _context.Jobs.SingleAsync(r => r.JobId == Id);
        }

        public async Task<Job> CreateJob(Job command)
        {

            var newJob = new Job()
            {
                JobDescription = command.JobDescription,
                Type = command.Type,
                System = command.System,
                ProjectNumber = command.ProjectNumber,
                Link = command.Link,
                Engineer = command.Engineer,
                Ecm = command.Ecm,
                Gpdm = command.Gpdm,
                Received = command.Received,
                DueDate = command.DueDate,
                Status = command.Status,
                Client = command.Client,
                ProjectName = command.ProjectName,
            };
            await _context.Jobs.AddAsync(newJob);
            await _context.SaveChangesAsync();
            return newJob;
        }

        public async Task<bool> DeleteJob(int Id)
        {
            var TaskToDelete = await _context.Jobs.SingleAsync(r => r.JobId == Id);
            await _cache.DeleteRecordAsync<Task>($"Profile_{Id}");
            _context.Remove(TaskToDelete);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Job> UpdateJob(UpdateJobDto updatedJob,int jobId)
        {
            var currentTask = await _context.Jobs.SingleAsync(r => r.JobId == jobId);

            currentTask.JobDescription = updatedJob.JobDescription;
            currentTask.Type = updatedJob.Type;
            currentTask.System = updatedJob.System;
            currentTask.ProjectNumber = updatedJob.ProjectNumber;
            currentTask.Link = updatedJob.Link;
            currentTask.Engineer = updatedJob.Engineer;
            currentTask.Received = updatedJob.Received;
            currentTask.Ecm = updatedJob.Ecm;
            currentTask.Gpdm = updatedJob.Gpdm;
            currentTask.Client = updatedJob.Client;
            currentTask.DueDate = updatedJob.DueDate;

            _context.SaveChanges();
            return currentTask;
        }
    }
}
