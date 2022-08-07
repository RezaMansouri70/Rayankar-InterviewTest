using ApplicationServices.Customer.Models;
using MediatR;
namespace ApplicationServices.Customer.Commands.EditCustomer
{
    public record EditCustomerCommand(int Id, string FirstName, string LastName, DateTime DateOfBirth, long PhoneNumber, string Email, string BankAccountNumber) : IRequest<CustomerDto>;

}
