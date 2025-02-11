using FluentValidation;

namespace IntegrationDtos.Request;

public class PostValidator : AbstractValidator<PostRequest>
{
    public PostValidator()
    {
        CascadeMode = CascadeMode.Stop;
        RuleFor(x => x.Description).NotEmpty().WithMessage("Description is required");
        RuleFor(x => x.ShortDescription).NotEmpty().WithMessage("ShortDescription is required");
        RuleFor(x => x.Title).NotEmpty().WithMessage("Title is required");
    }
}