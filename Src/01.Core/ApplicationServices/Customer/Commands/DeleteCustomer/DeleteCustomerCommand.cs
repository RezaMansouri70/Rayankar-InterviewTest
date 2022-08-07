using MediatR;


namespace ApplicationServices.Customer.Commands.DeleteCustomer
{
    public record DeleteCustomerCommand(int Id) : IRequest<bool>;

}
