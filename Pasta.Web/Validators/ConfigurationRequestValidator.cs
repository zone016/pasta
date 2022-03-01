using FastEndpoints.Validation;

using Pasta.Shared.Requests;

namespace Pasta.Web.Validators;

public class ConfigurationRequestValidator : Validator<ConfigurationRequest>
{
    public ConfigurationRequestValidator()
    {
        RuleFor(request => request.Headers)
            .Must(h => h.Any())
            .WithMessage("Your header cannot be empty!")

            .Must(h => h.ContainsKey("User-Agent"))
            .WithMessage("You need at least \"User-Agent\" in your header!")
            
            .Must(h => h.Keys.Count != 1)
            .WithMessage("Headers must only have one name!");

        RuleFor(request => request.HttpProbingPorts)
            .Must(p => p.Any())
            .WithMessage("You need at least one probing port!");
        
        RuleForEach(request => request.HttpProbingPorts)
        .Must(p => p is > 0 and <= 65535)
        .WithMessage("Your ports need to be greater than 0 going up to 65535!");
    }
}