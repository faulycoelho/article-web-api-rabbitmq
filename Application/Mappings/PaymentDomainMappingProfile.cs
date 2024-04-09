using Application.DTOs;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappings
{
    public class PaymentDomainMappingProfile : Profile
    {
        public PaymentDomainMappingProfile()
        {
            CreateMap<Payment, PaymentDTO>().ReverseMap();
        }
    }
}
