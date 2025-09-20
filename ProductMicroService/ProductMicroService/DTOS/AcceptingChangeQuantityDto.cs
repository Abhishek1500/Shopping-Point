using FluentValidation;

namespace ProductMicroService.DTOS
{
    public class AcceptingChangeQuantityDto
    {
        public int AskedQuantity { get; set; }
    }


    public class AcceptingChangeQuantityDtoValidator : AbstractValidator<AcceptingChangeQuantityDto>
    {
        public AcceptingChangeQuantityDtoValidator()
        {
            RuleFor(x=>x.AskedQuantity).Cascade(CascadeMode.StopOnFirstFailure)
            .Must(x => x >= 0).WithMessage("The {PropertName} Should be greater than 0}");
        }
    }
}
