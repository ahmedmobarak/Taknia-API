using FluentValidation;
using MyHealthProfile.Models.Dtos;

namespace MyHealthProfile.Validators
{
    public class UserAllergyValidator : AbstractValidator<UserAllergyDto>
    {
        public UserAllergyValidator() 
        {
            RuleFor(p => p.AllergyId)
                .NotEmpty();
        }
    }
}
