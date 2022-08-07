namespace ApplicationServices.Customer.Models
{
    public record CustomerDto(int Id, string FirstName, string LastName, DateTime DateOfBirth, string PhoneNumber, string Email, string BankAccountNumber);

}
