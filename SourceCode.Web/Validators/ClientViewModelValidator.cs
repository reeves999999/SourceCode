using FluentValidation;
using SourceCode.Web.Models;

namespace SourceCode.Web.Validators
{
    public class ClientViewModelValidator : AbstractValidator<ClientViewModel>
    {
        public ClientViewModelValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty();

            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(255);

            RuleFor(x => x.DirectorName)
                .NotEmpty()
                .MaximumLength(255);

            RuleFor(x => x.WebSite)
                .NotEmpty()
                .Matches(@"^(http(s)?://)?([\w-]+.)+[\w-]+(/[\w- ./?%&=])?$");

            RuleFor(x => x.EmailAddress)
                .NotEmpty()
                .EmailAddress()
                .MaximumLength(255);
        }

    }
}
