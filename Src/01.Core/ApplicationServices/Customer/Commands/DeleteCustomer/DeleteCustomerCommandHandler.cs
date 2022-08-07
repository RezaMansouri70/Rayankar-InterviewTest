using AutoMapper;
using DataLayer.SqlServer.Common;
using MediatR;

namespace ApplicationServices.Customer.Commands.DeleteCustomer
{
    public class DeleteCustomerCommandHandler : IRequestHandler<DeleteCustomerCommand, bool>
    {
        private readonly ApplicationDbContext _context;
        public DeleteCustomerCommandHandler(ApplicationDbContext context,
            IMapper mapper,
            IMediator mediator)
        {
            _context = context;
        }

        public async Task<bool> Handle(DeleteCustomerCommand deleteCustomerCommand, CancellationToken cancellationToken)
        {
            var customerForDelete = _context.Customers.Where(x => x.Id == deleteCustomerCommand.Id).FirstOrDefault();
            if (customerForDelete == null)
            {
                throw new Exception("Customer with given ID is not found.");
            }
            _context.Customers.Remove(customerForDelete);
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
    }

}
