using FluentValidation;
using System.Text.RegularExpressions;

namespace ProductMicroService.DTOS
{
    public class AddUpdateCategoryDto
    {
        public string CategoryName { get; set; }

    }

    public class AddUpdateCategoryDtoValidator : AbstractValidator<AddUpdateCategoryDto>
    { 
        public AddUpdateCategoryDtoValidator()
        {
            RuleFor(x => x.CategoryName).Cascade(CascadeMode.StopOnFirstFailure)
                .NotNull().WithMessage("{PropertyName} shouldn't be null")
                .NotEmpty().WithMessage("{PropertyName} Shouldnt be Empty")
                .Length(2, 15).WithMessage("{PropertyName} should have more than 2 character")
                .Must(x => new Regex("^[a-zA-Z][a-zA-Z\\s]+$").IsMatch(x)).WithMessage("The {PropertyName} is not valid format");
        }
    }

}
