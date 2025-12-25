using AbdulAkinCengiz_222132128.Entity.Dtos.Customer;
using AbdulAkinCengiz_222132128.Entity.Dtos.Reservation;
using AbdulAkinCengiz_222132128.Entity.Dtos.Table;
using AbdulAkinCengiz_222132128.WebUI.Models.Customers;
using AbdulAkinCengiz_222132128.WebUI.Models.Reservations;
using AbdulAkinCengiz_222132128.WebUI.Models.Tables;
using AutoMapper;

namespace AbdulAkinCengiz_222132128.WebUI.Mappings;

public class WebUIMapProfile : Profile
{
    public WebUIMapProfile()
    {
        CreateMap<CustomerCreateViewModel, CustomerCreateRequestDto>();
        CreateMap<ReservationCreateViewModel, ReservationCreateRequestDto>();
        CreateMap<ReservationSearchTableViewModel, TableResponseDto>().ReverseMap();
        CreateMap<ReservationCreateViewModel, ReservationCreateWithCustomerRequestDto>().ForMember(dest => dest.TableId, opt => opt.MapFrom(src => src.TableId)).ForMember(dest => dest.StartAt, opt => opt.MapFrom(src => src.StartAt)).ForMember(dest => dest.EndAt, opt => opt.MapFrom(src => src.EndAt)).ForMember(dest => dest.GuestCount, opt => opt.MapFrom(src => src.GuestCount)).ForMember(dest => dest.Customer, opt => opt.MapFrom(src => src.Customer)); 
        // CustomerCreateViewModel → CustomerCreateRequestDto
        CreateMap<CustomerCreateViewModel, CustomerCreateRequestDto>() .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName)) .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName)) .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.Phone)) .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email));
    }
}
