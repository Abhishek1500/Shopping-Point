using FluentValidation;
using ProductMicroService.Models;
using System.Text.RegularExpressions;

namespace ProductMicroService.DTOS
{
    public class AddUpdateProductDto
    {

        public string ProductName { get; set; }
        public string ProductCompany { get; set; }
        public DateTime DateOfManuFacture { get; set; }
        public int Quantity { get; set; }
        public string Description { get; set; }
        public string Imageurl { get; set; }
        public int Price { get; set; }
        public int CategoryId { get; set; }

    }

    public class AddUpdateProductDtoValidator : AbstractValidator<AddUpdateProductDto>
    {
        public AddUpdateProductDtoValidator()
        {
            RuleFor(x => x.ProductName).Cascade(CascadeMode.StopOnFirstFailure)
                .NotNull().WithMessage("{PropertyName} shouldn't be null")
                .NotEmpty().WithMessage("{PropertyName} Shouldnt be Empty")
                .Length(2, 10).WithMessage("{PropertyName} should have more than 2 character")
                .Must(x => new Regex("^[a-zA-Z][a-zA-Z\\s]+$").IsMatch(x)).WithMessage("The {PropertyName} is not valid format");


            RuleFor(x => x.ProductCompany).Cascade(CascadeMode.StopOnFirstFailure)
                .NotNull().WithMessage("{PropertyName} shouldn't be null")
                .NotEmpty().WithMessage("{PropertyName} Shouldnt be Empty")
                .Length(2, 10).WithMessage("{PropertyName} should have more than 2 character")
                .Must(x => new Regex("^[a-zA-Z][a-zA-Z\\s]+$").IsMatch(x)).WithMessage("The {PropertyName} is not valid format");

            RuleFor(x => x.DateOfManuFacture).Cascade(CascadeMode.StopOnFirstFailure)
                .NotNull().WithMessage("{PropertyName} is reqired")
                .NotEmpty().WithMessage("{PropertyName} is reqired")
                .Must(x => x <=DateTime.Today).WithMessage("{PropertyName} should be date in past");


            RuleFor(x => x.Description).Cascade(CascadeMode.StopOnFirstFailure)
                .NotNull().WithMessage("{PropertyName} shouldn't be null")
                .NotEmpty().WithMessage("{PropertyName} Shouldnt be Empty")
                .Length(50,100).WithMessage("{PropertyName} should have min length of 50 and max 100 char");

                RuleFor(x => x.Quantity).Cascade(CascadeMode.StopOnFirstFailure)
                .Must(x => x >= 0).WithMessage("The {PropertName} Should be greater than 0}");


            RuleFor(x => x.Price).Cascade(CascadeMode.StopOnFirstFailure)
            .Must(x => x >= 0).WithMessage("The {PropertName} Should be greater than 0}");


            RuleFor(x => x.CategoryId).Cascade(CascadeMode.StopOnFirstFailure)
            .Must(x => x >= 0).WithMessage("The {PropertName} Should be greater than 0}");
        }
    }


}
