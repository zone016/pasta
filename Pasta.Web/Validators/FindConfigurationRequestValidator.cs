using FastEndpoints.Validation;

using Pasta.Shared.Requests;

namespace Pasta.Web.Validators;

public class FindConfigurationRequestValidator : Validator<FindRequest>
{
    public FindConfigurationRequestValidator()
    {
        RuleFor(request => request.Guid)
            .Cascade(CascadeMode.Stop)
            
            .Length(36)
            .WithMessage("GUID must have only 36 characters length!")
            
            .Must(guid => Guid.TryParse(guid, out _))
            .WithMessage("You must provide a valid GUID!");
    }
}