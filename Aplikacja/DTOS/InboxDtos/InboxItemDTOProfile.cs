using Aplikacja.Entities.InboxModel;
using AutoMapper;

namespace Aplikacja.DTOS.InboxDtos
{
    public class InboxItemDTOProfile:Profile
    {
        public InboxItemDTOProfile()
        {
            CreateMap<InboxItem, InboxItemDTO>()
                .ForMember(m => m.Ecm, c => c.MapFrom(s => s.Job.Ecm))
                .ForMember(m => m.Gpdm, c => c.MapFrom(s => s.Job.Gpdm))
                .ForMember(m => m.DueDate, c => c.MapFrom(s => s.Job.DueDate))
                .ForMember(m => m.Started, c => c.MapFrom(s => s.Job.Started))
                .ForMember(m => m.Finished, c => c.MapFrom(s => s.Job.Finished))
                .ForMember(m => m.Link, c => c.MapFrom(s => s.Job.Link))
                .ForMember(m => m.JobDescription, c => c.MapFrom(s => s.Job.JobDescription))
                .ForMember(m => m.Status, c => c.MapFrom(s => s.Job.Status))
                .ForMember(m => m.UserId, c => c.MapFrom(s => s.User.UserId))
                .ForMember(m => m.Name, c => c.MapFrom(s => s.User.Name))
                .ForMember(m => m.Email, c => c.MapFrom(s => s.User.Email))
                .ForMember(m => m.Role, c => c.MapFrom(s => s.User.Role))
                .ForMember(m => m.Photo, c => c.MapFrom(s => s.User.Photo));
        }
    }
}