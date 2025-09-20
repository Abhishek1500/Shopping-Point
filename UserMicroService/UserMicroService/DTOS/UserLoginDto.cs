using FluentValidation;
using System.Text.RegularExpressions;

namespace UserMicroService.DTOS
{
    public class UserLoginDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }


    public class UserLoginDtoValidations : AbstractValidator<UserLoginDto>
    {
        public UserLoginDtoValidations()
        {
            RuleFor(x => x.Email).Cascade(CascadeMode.StopOnFirstFailure)
                .NotNull().WithMessage("{PropertyName} shouldn't be null")
                .NotEmpty().WithMessage("{PropertyName} Shouldnt be Empty")
                .Length(2, 30).WithMessage("{PropertyName} should have more than 2 character")
                .Must(x => new Regex("^[a-zA-Z0-9+_.-]+@[a-zA-Z0-9.-]+$").IsMatch(x))
                .WithMessage("The {PropertyName} is not in correct Format");

            RuleFor(x => x.Password).Cascade(CascadeMode.StopOnFirstFailure)
                .NotNull().WithMessage("{PropertyName} shouldn't be null")
                .NotEmpty().WithMessage("{PropertyName} Shouldnt be Empty")
                .Length(8, 30).WithMessage("{PropertyName} should have more than 8 character")
                .Must(validusername).WithMessage("there should not be space in {PropertyName} and it should start with character");

        }

        private bool validusername(string arg)
        {
            return Char.IsLetter(arg[0]) && !arg.Contains(" ");
        }
    }



}
