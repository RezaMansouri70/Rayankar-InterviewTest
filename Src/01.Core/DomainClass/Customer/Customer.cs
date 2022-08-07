using DomainClass.Common;

namespace DomainClass.Customer
{
    public class Customer : BaseEntity
    {
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public DateTime DateOfBirth { get; set; }

        // if use string convert to varchar or char in database and for 11 digit get minimum 11 byte but with long datatype get in use 8 byte of storage
        public long PhoneNumber { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string BankAccountNumber { get; set; } = default!;

    }
}
