using Aplikacja.Entities.RaportModels;
using AutoMapper;

namespace Aplikacja.DTOS.RaportDto
{
    public class UserRaportRecordDTOProfile:Profile
    {
        public UserRaportRecordDTOProfile()
        {
            CreateMap<UserRaportRecord, UserRaportRecordDTO>()
                .ForMember(m => m.Hours, c => c.MapFrom(s => s.InboxItem.Hours))
                .ForMember(m => m.Components, c => c.MapFrom(s => s.InboxItem.Hours))
                .ForMember(m => m.DrawingsComponents, c => c.MapFrom(s => s.InboxItem.DrawingsComponents))
                .ForMember(m => m.DrawingsAssembly, c => c.MapFrom(s => s.InboxItem.DrawingsAssembly))
                .ForMember(m => m.System, c => c.MapFrom(s => s.InboxItem.Job.System))
                .ForMember(m => m.Ecm, c => c.MapFrom(s => s.InboxItem.Job.Ecm))
                .ForMember(m => m.Gpdm, c => c.MapFrom(s => s.InboxItem.Job.Gpdm))
                .ForMember(m => m.Client, c => c.MapFrom(s => s.InboxItem.Job.Client))
                .ForMember(m => m.DueDate, c => c.MapFrom(s => s.InboxItem.Job.DueDate))
                .ForMember(m => m.Started, c => c.MapFrom(s => s.InboxItem.Job.Started))
                .ForMember(m => m.Finished, c => c.MapFrom(s => s.InboxItem.Job.Finished));
        }
    }
}