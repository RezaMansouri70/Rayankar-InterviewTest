using ApplicationServices.Customer.Models;
using MediatR;

namespace ApplicationServices.Customer.Commands.CreateCustomer
{
    public record CreateCustomerCommand(string FirstName, string LastName, DateTime DateOfBirth, long PhoneNumber, string Email, string BankAccountNumber) : IRequest<CustomerDto>;

}
