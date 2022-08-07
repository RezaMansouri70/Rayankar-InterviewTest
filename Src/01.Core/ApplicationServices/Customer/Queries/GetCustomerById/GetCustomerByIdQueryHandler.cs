using ApplicationServices.Customer.Models;
using AutoMapper;
using DataLayer.SqlServer.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationServices.Customer.Queries.GetCustomerById
{
    public class GetCustomerByIdQueryHandler : IRequestHandler<GetCustomerByIdQuery, CustomerDto>
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetCustomerByIdQueryHandler(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CustomerDto> Handle(GetCustomerByIdQuery request, CancellationToken cancellationToken)
        {
            DomainClass.Customer.Customer customer = await _context.Customers.FindAsync(request.CustomerId);

            if (customer == null)
            {
                throw new Exception("Customer with given ID is not found.");

            }

            return _mapper.Map<CustomerDto>(customer);
        }
    }
}
