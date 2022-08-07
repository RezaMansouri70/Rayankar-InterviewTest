using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationServices.Customer.Events.CustomerCreated
{
    public record CustomerCreatedEvent(string FirstName, string LastName, DateTime DateOfBirth, long PhoneNumber, string Email, string BankAccountNumber) : INotification;

}
