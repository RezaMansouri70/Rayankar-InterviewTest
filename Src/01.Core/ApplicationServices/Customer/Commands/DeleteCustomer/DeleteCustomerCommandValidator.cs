using FluentValidation;

namespace ApplicationServices.Customer.Commands.DeleteCustomer
{
    public class DeleteCustomerCommandValidator : AbstractValidator<DeleteCustomerCommand>
    {
        public DeleteCustomerCommandValidator()
        {
            RuleFor(v => v.Id)
              .NotEmpty().WithMessage("Id is required.");
        }
    }
}
