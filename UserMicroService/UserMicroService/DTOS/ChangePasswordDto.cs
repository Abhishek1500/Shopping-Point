using FluentValidation;

namespace UserMicroService.DTOS
{
    public class ChangePasswordDto
    {
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }

    }

    public class ChangePasswordDtoValidations : AbstractValidator<ChangePasswordDto>
    {
        public ChangePasswordDtoValidations()
        {
            
            RuleFor(x => x.CurrentPassword).Cascade(CascadeMode.StopOnFirstFailure)
                .NotNull().WithMessage("{PropertyName} shouldn't be null")
                .NotEmpty().WithMessage("{PropertyName} Shouldnt be Empty")
                .Length(8, 30).WithMessage("{PropertyName} should have more than 8 character")
                .Must(validusername).WithMessage("there should not be space in {PropertyName} and it should start with character");

            RuleFor(x => x.NewPassword).Cascade(CascadeMode.StopOnFirstFailure)
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
