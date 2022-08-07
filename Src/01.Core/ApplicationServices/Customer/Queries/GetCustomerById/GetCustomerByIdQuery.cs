using ApplicationServices.Customer.Models;
using MediatR;

namespace ApplicationServices.Customer.Queries.GetCustomerById
{
    public record GetCustomerByIdQuery(int CustomerId) : IRequest<CustomerDto>;

}
