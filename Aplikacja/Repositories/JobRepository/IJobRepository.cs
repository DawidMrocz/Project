

using Aplikacja.DTOS.JobDtos;
using Aplikacja.Entities.JobModel;

namespace Aplikacja.Repositories.JobRepository
{
    public interface IJobRepository
    {
        public Task<List<Job>> GetJobs();
        public Task<Job> GetJob(int jobId);

        //public Task<bool> CreateInboxItem(int jobId, int userId);

        public Task<Job> CreateJob(CreateJobDto command);
        public Task<bool> DeleteJob(int jobId);
        public Task<Job> UpdateJob(UpdateJobDto command,int jobId);
    }
}
