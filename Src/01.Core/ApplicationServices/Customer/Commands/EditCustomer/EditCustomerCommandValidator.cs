using ApplicationServices.Services.PhoneService;
using DataLayer.SqlServer.Common;
using FluentValidation;

namespace ApplicationServices.Customer.Commands.EditCustomer
{
    public class EditCustomerCommandValidator : AbstractValidator<EditCustomerCommand>
    {
        private readonly ApplicationDbContext _context;


        public EditCustomerCommandValidator(ApplicationDbContext context)
        {
            _context = context;

            RuleFor(v => v.Id)
              .NotEmpty().WithMessage("Id is required.");
            RuleFor(v => v.FirstName)
                .NotEmpty().WithMessage("FirstName is required.")
                .MaximumLength(200).WithMessage("FirstName must not exceed 200 characters.");
            RuleFor(x => x.FirstName).Must((id, cancellation) =>
            {
                bool exists = _context.Customers.Where(x => x.FirstName == id.FirstName && x.LastName == id.LastName && x.DateOfBirth.Date == id.DateOfBirth.Date).Any();
                return !exists;
            }).WithMessage("FirstName With LastName With DateOfBirth Must be unique");

            RuleFor(v => v.LastName)
               .NotEmpty().WithMessage("LastName is required.")
               .MaximumLength(200).WithMessage("LastName must not exceed 200 characters.");

            RuleFor(v => v.Email)
               .NotEmpty().WithMessage("Email is required.")
               .MaximumLength(200).WithMessage("Email must not exceed 200 characters.")
               .Matches(@"^[a-zA-Z0-9+_.-]+@[a-zA-Z0-9.-]+$").WithMessage("Email must be valid.")
               .Must((id, cancellation) =>
               {
                   bool exists = _context.Customers.Where(x => x.Email.ToLower() == id.Email.ToLower()).Any();
                   return !exists;
               }).WithMessage("Email Must be a unique");


            RuleFor(v => v.BankAccountNumber)
           .NotEmpty().WithMessage("BankAccountNumber is required.")
           .MaximumLength(200).WithMessage("BankAccountNumber must not exceed 200 characters.")
           .Matches(@"^[0-9]*$").WithMessage("BankAccountNumber must be valid.");

            RuleFor(x => x.PhoneNumber)
             .Must((id, cancellation) =>
             {
                 var result = PhoneService.ValidatePhoneNumber(id.PhoneNumber.ToString(), "IR");
                 return result.IsMobile;
             }).WithMessage("PhoneNumber Must be a valid IR Mobile");
        }
    }


}
