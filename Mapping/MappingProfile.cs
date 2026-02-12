using ASP.NEThwMain.DTO;
using AutoMapper;
using ASP.NEThwMain.Models;
using System.Reflection.PortableExecutable;

namespace ASP.NEThwMain.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductCreateDTO>();
            CreateMap<Product, CharacteristicsDTO>();
            CreateMap<Product, ProductReadDTO>();

            CreateMap<ProductCreateDTO, Product>()
                .ForMember(d => d.Id, opt => opt.Ignore())
                .ForMember(d => d.Characteristics,
                opt => opt.MapFrom(s => s.Characteristics ?? new CharacteristicsDTO()));

            CreateMap<ProductUpdateDTO, Product>()
                  .ForMember(d => d.Id, opt => opt.Ignore())
                 .ForMember(d => d.Characteristics,
                 opt => opt.MapFrom(s => s.Characteristics ?? new CharacteristicsDTO()));

            CreateMap<Product, ProductReadDTO>();
            CreateMap<Models.Characteristics, CharacteristicsDTO>();

            CreateMap<User, UserReadDTO>();
            CreateMap<UserCreateDTO, User>();
            CreateMap<UserUpdateDTO, User>()
                .ForMember(d => d.Id, opt => opt.Ignore());

            CreateMap<Order, OrderReadDTO>();
            CreateMap<OrderCreateDTO, Order>()
                .ForMember(d => d.Id, opt => opt.Ignore())
                .ForMember(d => d.CreatedAt, opt => opt.Ignore())
                .ForMember(d => d.Items, opt => opt.Ignore());

            CreateMap<OrderUpdateDTO, Order>()
                .ForMember(d => d.Id, opt => opt.Ignore());

            CreateMap<OrderItem, OrderItemReadDTO>();
            CreateMap<OrderItemCreateDTO, OrderItem>()
                .ForMember(d => d.Id, opt => opt.Ignore());

        }
    }
}
