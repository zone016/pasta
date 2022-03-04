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

        RuleFor(request => request.Timeout)
            .Must(t => t is >= 1000 and <= 10000)
            .WithMessage("You must be in range 1000 to 10000 milliseconds!");

        RuleFor(request => request.ScreenshotResolution)
            .Cascade(CascadeMode.Stop)
            
            .NotEmpty()
            .WithMessage("You must provide the resolution!")
            
            .MaximumLength(256)
            .WithMessage("Resolution must have a maximum length of 128 characters!")
            
            .Must(r => r.Contains('x'))
            .WithMessage("Invalid resolution format! Your input do not have an \"x\" as valid separator!")

            .Must(r => r.ToCharArray().Count(c => c == 'x') == 1)
            .WithMessage("Invalid resolution format! Your input must contains only one separator!")
            
            .Must(r => r.Split('x', StringSplitOptions.RemoveEmptyEntries).Length == 2)
            .WithMessage("Invalid resolution format! You must provide X and Y pixels size!")
            
            .DependentRules(() =>
            {
                RuleFor(request => request.ScreenshotResolution)
                    .Cascade(CascadeMode.Stop)
                    
                    .Must(r => int.TryParse(r.Split('x', StringSplitOptions.RemoveEmptyEntries)[0], out _))
                    .WithMessage("Invalid resolution format! X must be a unsigned integer!")

                    .Must(r => int.TryParse(r.Split('x', StringSplitOptions.RemoveEmptyEntries)[1], out _))
                    .WithMessage("Invalid resolution format! Y must be a unsigned integer!")

                    .Must(r =>
                        int.TryParse(r.Split('x', StringSplitOptions.RemoveEmptyEntries)[0],
                            out var xPixels) && xPixels >= 512)
                    .WithMessage("Invalid resolution, you must set X at least 512 pixels!")

                    .Must(r => int.Parse(r.Split('x', StringSplitOptions.RemoveEmptyEntries)[1]) >= 512)
                    .WithMessage("Invalid resolution, you must set Y at least 512 pixels!");
            });

        RuleFor(request => request.Title)
            .Cascade(CascadeMode.Stop)

            .MaximumLength(256)
            .WithMessage("Title must have a maximum length of 256 characters!")
            
            .MinimumLength(3)
            .WithMessage("Title must have a minimum length of 3 characters!");

        RuleForEach(request => request.HttpProbingPorts)
            .Must(p => p is > 0 and <= 65535)
            .WithMessage("Your ports need to be greater than 0 going up to 65535!");
    }
}