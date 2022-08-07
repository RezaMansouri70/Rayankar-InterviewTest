using ApplicationServices.Customer.Commands.CreateCustomer;
using AutoMapper;
using MediatR;
using Moq;
using System;
using Xunit;
using DataLayer.SqlServer.Common;
using Microsoft.EntityFrameworkCore;
using Shouldly;
using ApplicationServices.Customer.Queries.GetCustomerById;
using ApplicationServices.Customer.Commands.EditCustomer;
using ApplicationServices.Customer.Commands.DeleteCustomer;
namespace Tests
{

    public class CustomerTests
    {
        private readonly Mock<ApplicationDbContext>? _applicationDbContext = new Mock<ApplicationDbContext>(new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase("sampleinmemeory").Options);
        private readonly Mock<IMediator> _mediator = new Mock<IMediator>();
        private readonly Mock<IMapper> _mapper = new Mock<IMapper>();

        [Fact]
        public void CreateCustomerValid_ReturnsSuccess()
        {

            //Arange
            CreateCustomerCommand createCustomerCommand = new CreateCustomerCommand("Reza", "Mansouri", DateTime.Now, 9127747016, "Reza.Mansouri70@gmail.com", "1234");
            CreateCustomerCommandHandler handler = new CreateCustomerCommandHandler(_applicationDbContext.Object, _mapper.Object, _mediator.Object);

            //Act
            var result = handler.Handle(createCustomerCommand, new System.Threading.CancellationToken());

            //Assert

            result.Result.ShouldNotBeNull();
            result.Result.FirstName.ShouldBe(createCustomerCommand.FirstName);
            result.Result.LastName.ShouldBe(createCustomerCommand.LastName);
            result.Result.PhoneNumber.ShouldBe(createCustomerCommand.PhoneNumber.ToString());
            result.Result.Email.ShouldBe(createCustomerCommand.Email);
            result.Result.BankAccountNumber.ShouldBe(createCustomerCommand.BankAccountNumber);
            result.Result.DateOfBirth.Date.ShouldBe(createCustomerCommand.DateOfBirth.Date);

        }



        [Fact]
        public void GetCustomerValid_ReturnsSuccess()
        {

            //Arange          
            GetCustomerByIdQuery getCustomerByIdQuery = new GetCustomerByIdQuery(1);
            GetCustomerByIdQueryHandler handler = new GetCustomerByIdQueryHandler(_applicationDbContext.Object, _mapper.Object);

            //Act
            var result = handler.Handle(getCustomerByIdQuery, new System.Threading.CancellationToken());

            //Assert

            result.Result.ShouldNotBeNull();
            result.Result.Id.ShouldBe(getCustomerByIdQuery.CustomerId);


        }

        [Fact]
        public void EditCustomerValid_ReturnsSuccess()
        {

            //Arange         
            EditCustomerCommand editCustomerCommand = new EditCustomerCommand(1, "Reza", "Mansouri", DateTime.Now, 9127747016, "Reza.Mansouri70@gmail.com", "1234");
            EditCustomerCommandHandler handler = new EditCustomerCommandHandler(_applicationDbContext.Object, _mapper.Object, _mediator.Object);

            //Act
            var result = handler.Handle(editCustomerCommand, new System.Threading.CancellationToken());

            //Assert

            result.Result.ShouldNotBeNull();
            result.Result.Id.ShouldBe(editCustomerCommand.Id);
        }

        [Fact]
        public void DelteCustomerValid_ReturnsSuccess()
        {

            //Arange         
            DeleteCustomerCommand deleteCustomerCommand = new DeleteCustomerCommand(1);
            DeleteCustomerCommandHandler handler = new DeleteCustomerCommandHandler(_applicationDbContext.Object, _mapper.Object, _mediator.Object);

            //Act
            var result = handler.Handle(deleteCustomerCommand, new System.Threading.CancellationToken());

            //Assert

            result.Result.Equals(true);

        }


    }

}
