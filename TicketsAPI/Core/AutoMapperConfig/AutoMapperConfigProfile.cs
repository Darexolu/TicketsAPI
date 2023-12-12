using AutoMapper;
using TicketsAPI.Core.DTO;
using TicketsAPI.Core.Entities;

namespace TicketsAPI.Core.AutoMapperConfig
{
    public class AutoMapperConfigProfile : Profile
    {
        public AutoMapperConfigProfile()
        {
            //Tickets
            CreateMap<CreateTicketDTO, Ticket>();
            CreateMap<Ticket, GetTicketDTO>();
            CreateMap<UpdateTicketDTO, Ticket>();

        }
    }
}
