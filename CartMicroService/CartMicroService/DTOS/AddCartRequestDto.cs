using FluentValidation;

namespace CartMicroService.DTOS
{
    public class AddCartRequestDto
    {
        public int ProductId { get; set; }
        public int Count { get; set; }
       
    }

    public class AddCartRequestDtoValidator : AbstractValidator<AddCartRequestDto>
    {
        public AddCartRequestDtoValidator()
        {
            

            RuleFor(x => x.ProductId).Cascade(CascadeMode.StopOnFirstFailure)
            .Must(x => x > 0).WithMessage("The {PropertName} Should be greater than 0}");


            RuleFor(x => x.Count).Cascade(CascadeMode.StopOnFirstFailure)
            .Must(x => x > 0).WithMessage("The {PropertName} Should be greater than 0}");

        }
    }


}
