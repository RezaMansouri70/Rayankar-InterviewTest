using ApplicationServices.Customer.Models;
using AutoMapper;
using DataLayer.SqlServer.Common;
using MediatR;
using ApplicationServices.Customer.Events.CustomerCreated;

namespace ApplicationServices.Customer.Commands.CreateCustomer
{
    public class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, CustomerDto>
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public CreateCustomerCommandHandler(ApplicationDbContext context,
            IMapper mapper,
            IMediator mediator)
        {
            _context = context;
            _mapper = mapper;
            _mediator = mediator;
        }

        public async Task<CustomerDto> Handle(CreateCustomerCommand createCustomerCommand, CancellationToken cancellationToken)
        {
            DomainClass.Customer.Customer customer = _mapper.Map<DomainClass.Customer.Customer>(createCustomerCommand);

            //DomainClass.Customer.Customer customer = new DomainClass.Customer.Customer()
            //{
            //    FirstName = createCustomerCommand.FirstName,
            //    LastName = createCustomerCommand.LastName,
            //    BankAccountNumber = createCustomerCommand.BankAccountNumber,
            //    DateOfBirth = createCustomerCommand.DateOfBirth,
            //    Email = createCustomerCommand.Email,
            //    PhoneNumber = createCustomerCommand.PhoneNumber,

            //};

            await _context.Customers.AddAsync(customer, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            // Raising Event ...
            await _mediator.Publish(new CustomerCreatedEvent(customer.FirstName, customer.LastName, customer.DateOfBirth, customer.PhoneNumber, customer.Email, customer.BankAccountNumber), cancellationToken);

            return _mapper.Map<CustomerDto>(customer);
        }
    }
}
