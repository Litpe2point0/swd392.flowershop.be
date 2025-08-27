using AutoMapper;
using BusinessObject.DTO.OrderDTO;
using BusinessObject.Model;

namespace WebAPI.Mapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            //Budget
            CreateMap<OrderCreateRequestDTO, Order>().ReverseMap();
            CreateMap<Order, OrderResultDTO>().ReverseMap();
        }
    }
}
