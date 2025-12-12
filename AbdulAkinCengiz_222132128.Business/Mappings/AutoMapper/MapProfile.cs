using AbdulAkinCengiz_222132128.Entity.Concrete;
using AbdulAkinCengiz_222132128.Entity.Dtos.Category;
using AbdulAkinCengiz_222132128.Entity.Dtos.Users;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AbdulAkinCengiz_222132128.Entity.Dtos.Customer;
using AbdulAkinCengiz_222132128.Entity.Dtos.Order;
using AbdulAkinCengiz_222132128.Entity.Dtos.OrderItem;
using AbdulAkinCengiz_222132128.Entity.Dtos.Payment;
using AbdulAkinCengiz_222132128.Entity.Dtos.Product;
using AbdulAkinCengiz_222132128.Entity.Dtos.Reservation;
using AbdulAkinCengiz_222132128.Entity.Dtos.Table;

namespace AbdulAkinCengiz_222132128.Business.Mappings.AutoMapper;
public class MapProfile : Profile
{
    public MapProfile()
    {
        CreateMap<AppUser, AppUserResponseDto>();
        CreateMap<AppUser, AppUserDetailResponseDto>();
        CreateMap<AppUserUpdateRequestDto, AppUser>();
        CreateMap<AppUserRegisterRequestDto, AppUser>();

        CreateMap<Category, CategoryResponseDto>();
        CreateMap<Category, CategoryDetailResponseDto>();
        CreateMap<CategoryCreateRequestDto, Category>();
        CreateMap<CategoryUpdateRequestDto, Category>();

        CreateMap<Customer, CustomerResponseDto>();
        CreateMap<Customer, CustomerDetailResponseDto>();
        CreateMap<CustomerCreateRequestDto, Customer>();
        CreateMap<CustomerUpdateRequestDto, Customer>();

        CreateMap<Order, OrderResponseDto>();
        CreateMap<Order, OrderDetailResponseDto>();
        CreateMap<OrderCreateRequestDto, Order>();
        CreateMap<OrderUpdateRequestDto, Order>();

        CreateMap<OrderItem, OrderItemResponseDto>();
        CreateMap<OrderItem, OrderItemDetailResponseDto>();
        CreateMap<OrderItemCreateRequestDto, OrderItem>();
        CreateMap<OrderItemUpdateRequestDto, OrderItem>();

        CreateMap<Payment, PaymentResponseDto>();
        CreateMap<Payment, PaymentDetailResponseDto>();
        CreateMap<PaymentCreateRequestDto, Payment>();
        CreateMap<PaymentUpdateRequestDto, Payment>();

        CreateMap<Product, ProductResponseDto>();
        CreateMap<Product, ProductDetailResponseDto>();
        CreateMap<ProductCreateRequestDto, Product>();
        CreateMap<ProductUpdateRequestDto, Product>();

        CreateMap<Reservation, ReservationResponseDto>();
        CreateMap<Reservation, ReservationDetailResponseDto>();
        CreateMap<ReservationCreateRequestDto, Reservation>();
        CreateMap<ReservationUpdateRequestDto, Reservation>();

        CreateMap<Table, TableResponseDto>();
        CreateMap<Table, TableDetailResponseDto>();
        CreateMap<TableCreateRequestDto, Table>();
        CreateMap<TableUpdateRequestDto, Table>();
    }
}
