using AutoMapper;
using db_cp.DTO;
using db_cp.ModelsBL;
using db_cp.Models;

namespace db_cp.Utils
{
    public class AutoMappingProfile : Profile
    {
        public AutoMappingProfile()
        {
            CreateMap<ButtonsEvents, ButtonsEventsBL>().ReverseMap();
            CreateMap<ButtonsPosts, ButtonsPostsBL>().ReverseMap();
            CreateMap<Events, EventsBL>().ReverseMap();
            CreateMap<EventsTypes, EventsTypesBL>().ReverseMap();
            CreateMap<Users, UsersBL>().ReverseMap();

            CreateMap<ButtonsEventsBaseDTO, ButtonsEventsBL>().ReverseMap();
            CreateMap<ButtonsEventsDTO, ButtonsEventsBL>().ReverseMap();
            CreateMap<ButtonsPostsDTO, ButtonsPostsBL>().ReverseMap();
            CreateMap<EventsBaseDTO, EventsBL>().ReverseMap();
            CreateMap<EventsDTO, EventsBL>().ReverseMap();
            CreateMap<EventsUpdateDTO, EventsBL>().ReverseMap();
            CreateMap<EventsTypesBaseDTO, EventsTypesBL>().ReverseMap();
            CreateMap<EventsTypesDTO, EventsTypesBL>().ReverseMap();
            CreateMap<UsersDTO, UsersBL>().ReverseMap();
            CreateMap<UsersLoginDTO, UsersBL>().ReverseMap();
            CreateMap<UsersUpdatePermDTO, UsersBL>().ReverseMap();
            CreateMap<NewUsersDTO, UsersBL>().ReverseMap();
        }
    }
}