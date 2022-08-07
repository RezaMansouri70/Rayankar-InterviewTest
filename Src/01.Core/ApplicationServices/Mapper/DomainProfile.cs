using ApplicationServices.Customer.Commands.CreateCustomer;
using ApplicationServices.Customer.Commands.EditCustomer;
using ApplicationServices.Customer.Models;
using AutoMapper;

namespace ApplicationServices.Mapper
{
    public class DomainProfile : Profile
    {
        public DomainProfile()
        {
            CreateMap<CreateCustomerCommand, DomainClass.Customer.Customer>();
            CreateMap<DomainClass.Customer.Customer?, DomainClass.Customer.Customer>();
            CreateMap<EditCustomerCommand, DomainClass.Customer.Customer>();
            CreateMap<DomainClass.Customer.Customer, CustomerDto>()
                .ForMember(cd => cd.DateOfBirth, opt =>
                    opt.MapFrom(c => c.DateOfBirth.ToShortDateString()));
        }
    }
}
