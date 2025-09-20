using FluentValidation;

namespace CartMicroService.DTOS
{
    public class RespondToRequestDto
    {
        public string NewStatus { get; set; }
        public string? Remark { get; set; } = "";
    }

    public class RespondToRequestDtoValidator : AbstractValidator<RespondToRequestDto>
    {
        public RespondToRequestDtoValidator()
        {
            RuleFor(x => x.NewStatus).Cascade(CascadeMode.StopOnFirstFailure)
                .NotNull().WithMessage("{PropertyName} shouldn't be null")
                .NotEmpty().WithMessage("{PropertyName} Shouldnt be Empty")
                .Length(2, 30).WithMessage("{PropertyName} should have more than 2 character")
                .Must(x => x.Trim().ToLower() == "approved" || x.Trim().ToLower() == "rejected").WithMessage("The {PropertyName} is could be either approved or rejected");

        }
    }
}
