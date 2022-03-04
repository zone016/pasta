using FastEndpoints.Validation;

using Pasta.Shared.Enums;
using Pasta.Shared.Requests;

namespace Pasta.Web.Validators;

public class JobRequestValidator : Validator<JobRequest>
{
    public JobRequestValidator()
    {
        RuleFor(request => request.Priority)
            .Cascade(CascadeMode.Stop)

            .NotEmpty()
            .WithMessage("Priority cannot be null or empty!")

            .MaximumLength(6)
            .WithMessage("Priority must have only 6 characters length!")

            .IsEnumName(typeof(Priority))
            .WithMessage("Priority must be low, normal or high!");

        RuleFor(request => request.Target)
            .Cascade(CascadeMode.Stop)

            .NotEmpty()
            .WithMessage("Target cannot be null or empty!")

            .MaximumLength(2048)
            .WithMessage("Target must have only 2048 characters length!");
        
        RuleFor(request => request.Title)
            .Cascade(CascadeMode.Stop)

            .NotEmpty()
            .WithMessage("Title cannot be null or empty!")

            .MaximumLength(256)
            .WithMessage("Title must have only 256 characters length!");
        
        RuleFor(request => request.ConfigurationGuid)
            .Cascade(CascadeMode.Stop)
            
            .Length(36)
            .WithMessage("ConfigurationGuid must have only 36 characters length!")
            
            .Must(guid => Guid.TryParse(guid, out _))
            .WithMessage("ConfigurationGuid  must be a valid GUID!");
    }
}