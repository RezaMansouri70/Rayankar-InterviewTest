using ApplicationServices.Customer.Models;
using AutoMapper;
using DataLayer.SqlServer.Common;
using MediatR;

namespace ApplicationServices.Customer.Commands.EditCustomer
{
    public class EditCustomerCommandHandler : IRequestHandler<EditCustomerCommand, CustomerDto>
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public EditCustomerCommandHandler(ApplicationDbContext context,
            IMapper mapper,
            IMediator mediator)
        {
            _context = context;
            _mapper = mapper;
            _mediator = mediator;
        }

        public async Task<CustomerDto> Handle(EditCustomerCommand editCustomerCommand, CancellationToken cancellationToken)
        {
            DomainClass.Customer.Customer customer = _mapper.Map<DomainClass.Customer.Customer>(editCustomerCommand);
            var customerForEdit = _context.Customers.Where(x => x.Id == editCustomerCommand.Id).FirstOrDefault();
            if (customerForEdit == null)
            {
                throw new Exception("Customer with given ID is not found.");
            }

            customerForEdit.FirstName = customer.FirstName;
            customerForEdit.LastName = customer.LastName;
            customerForEdit.Email = customer.Email;
            customerForEdit.PhoneNumber = customer.PhoneNumber;
            customerForEdit.DateOfBirth = customer.DateOfBirth;
            customerForEdit.BankAccountNumber = customer.BankAccountNumber;


            await _context.SaveChangesAsync(cancellationToken);
            return _mapper.Map<CustomerDto>(customer);
        }
    }
}
