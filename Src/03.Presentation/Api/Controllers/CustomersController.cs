using ApplicationServices.Customer.Commands.CreateCustomer;
using ApplicationServices.Customer.Commands.DeleteCustomer;
using ApplicationServices.Customer.Commands.EditCustomer;
using ApplicationServices.Customer.Models;
using ApplicationServices.Customer.Queries.GetCustomerById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CustomersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Returns an specific customer.
        /// </summary>
        /// <param name="customerId">Customer ID</param>
        /// <returns>Customer Details</returns>
        [HttpGet("{customerId}")]
        public async Task<IActionResult> GetCustomerById([FromRoute] int customerId)
        {
            CustomerDto customer = await _mediator.Send(new GetCustomerByIdQuery(customerId));
            return Ok(customer);
        }

        /// <summary>
        /// Creates a new customer.
        /// </summary>  
        /// <param name="createCustomerCommand">New customer data .</param>
        /// <returns>New created customer</returns>
        [HttpPost]
        public async Task<IActionResult> CreateCustomer([FromBody] CreateCustomerCommand createCustomerCommand)
        {
            CustomerDto customer = await _mediator.Send(createCustomerCommand);
            return CreatedAtAction(nameof(GetCustomerById), new { customerId = customer.Id }, customer);
        }

        /// <summary>
        /// Edit a customer.
        /// </summary>  
        /// <param name="editCustomerCommand"> customer data .</param>
        /// <returns>edited customer</returns>
        [HttpPut]
        public async Task<IActionResult> EditCustomer([FromBody] EditCustomerCommand editCustomerCommand)
        {
            CustomerDto customer = await _mediator.Send(editCustomerCommand);
            return Ok(customer);
        }


        /// <summary>
        /// Delete a customer.
        /// </summary>  
        /// <param name="deleteCustomerCommand"> customer id .</param>
        /// <returns>Delete customer</returns>
        [HttpDelete]
        public async Task<IActionResult> DeleteCustomer([FromBody] DeleteCustomerCommand deleteCustomerCommand)
        {
            var result = await _mediator.Send(deleteCustomerCommand);
            return Ok(result);
        }

    }
}
