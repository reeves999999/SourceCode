using FluentValidation;
using SourceCode.Web.Models;

namespace SourceCode.Web.Validators
{
    public class ClientValidator : AbstractValidator<ClientViewModel>
    {
        public ClientValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty();

            RuleFor(x => x.WebSite)
                .NotEmpty()
                .Matches(@"^http(s)?://([\w-]+.)+[\w-]+(/[\w- ./?%&=])?$");


            RuleFor(x => x.EmailAddress)
                .NotEmpty()
                .EmailAddress();
        }

    }
}
