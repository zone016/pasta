using FastEndpoints.Validation;

using Pasta.Shared.Enums;
using Pasta.Shared.Requests;

namespace Pasta.Web.Validators;

public class WebhookRequestValidator : Validator<WebhookRequest>
{
    public WebhookRequestValidator()
    {
        RuleFor(request => request.Address)
            .Cascade(CascadeMode.Stop)

            .NotEmpty()
            .WithMessage("Address cannot be null or empty!")

            .MaximumLength(4096)
            .WithMessage("Address must have only 4096 characters length!");
        
        RuleFor(request => request.Type)
            .Cascade(CascadeMode.Stop)

            .NotEmpty()
            .WithMessage("Type cannot be null or empty!")

            .MaximumLength(10)
            .WithMessage("Type must have only 10 characters length!")

            .IsEnumName(typeof(WebhookType))
            .WithMessage("Type must be GoogleChat or Slack!");
    }
}