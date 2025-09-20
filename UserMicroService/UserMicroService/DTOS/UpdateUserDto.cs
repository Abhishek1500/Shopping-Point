using FluentValidation;
using System.Text.RegularExpressions;

namespace UserMicroService.DTOS
{
    public class UpdateUserDto
    {
        public string Name { get; set; }
        public DateTime DOB { get; set; }
        public string Gender { get; set; }
        public string PhotoUrl { get; set; }
    }

    public class UpdateUserDtoValidations : AbstractValidator<UpdateUserDto>
    {
        public UpdateUserDtoValidations()
        {
            

            
            RuleFor(x => x.Name).Cascade(CascadeMode.StopOnFirstFailure)
                .NotNull().WithMessage("{PropertyName} shouldn't be null")
                .NotEmpty().WithMessage("{PropertyName} Shouldnt be Empty")
                .Length(2, 30).WithMessage("{PropertyName} should have more than 2 character")
                .Must(x => new Regex("^[a-zA-Z][a-zA-Z\\s]+$").IsMatch(x)).WithMessage("The {PropertyName} is not valid format");

            RuleFor(x => x.Gender).Cascade(CascadeMode.StopOnFirstFailure)
                .NotNull().WithMessage("{PropertyName} shouldn't be null")
                .NotEmpty().WithMessage("{PropertyName} Shouldnt be Empty")
                .Must(x => x.ToLower().Trim() == "male" || x.ToLower().Trim() == "female" || x.ToLower().Trim() == "others")
                .WithMessage("The {PropertyName} should be either male,female or others");

            RuleFor(x => x.DOB).Cascade(CascadeMode.StopOnFirstFailure)
                .NotNull().WithMessage("{PropertyName} is reqired")
                .NotEmpty().WithMessage("{PropertyName} is reqired")
                .Must(x => x <= DateTime.Today).WithMessage("{PropertyName} should be date in past");
        }

        private bool validusername(string arg)
        {
            return Char.IsLetter(arg[0]) && !arg.Contains(" ");
        }
    }
}
