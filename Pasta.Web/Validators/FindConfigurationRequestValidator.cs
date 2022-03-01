using FastEndpoints.Validation;

using Pasta.Shared.Requests;

namespace Pasta.Web.Validators;

public class FindConfigurationRequestValidator : Validator<FindConfigurationRequest>
{
    public FindConfigurationRequestValidator()
    {
        RuleFor(request => request.Guid)
            .Must(guid => Guid.TryParse(guid, out _))
            .WithMessage("You must provide a valid GUID!");
    }
}