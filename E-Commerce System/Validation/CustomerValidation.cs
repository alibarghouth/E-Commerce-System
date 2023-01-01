using E_Commerce_System.DTO.CustomerDto;
using FluentValidation;

namespace E_Commerce_System.Validation
{
    public class CustomerValidation : AbstractValidator<RegisterUser>
    {
        public CustomerValidation()
        {
            RuleFor(x => x.Address).NotEmpty();

            RuleFor(x => x.Email).EmailAddress()
                .NotEmpty();

            RuleFor(x => x.Picture).NotEmpty();

            RuleFor(x => x.City).NotEmpty();

            RuleFor(x => x.Phone).NotEmpty();

            RuleFor(x => x.City).NotEmpty();

            RuleFor(x => x.Country).NotEmpty();

            RuleFor(x => x.FirstName).NotEmpty()
                .WithMessage("Please write name");

            RuleFor(x => x.LastName).NotEmpty()
                .WithMessage("Please write name");

            RuleFor(x => x.Password).NotEmpty()
                .Matches("A-Z");

            RuleFor(x => x.PostalCode).NotEmpty();

            RuleFor(x => x.Region).NotEmpty();
        }
    }
}
