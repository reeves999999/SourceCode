using FluentValidation;
using SourceCode.Web.Controllers.API.v1.Contracts.Requests;

namespace SourceCode.Web.Controllers.API.v1.Validators
{
    public class ClientCreateRequestValidator : AbstractValidator<ClientCreateRequest>
    {
        public ClientCreateRequestValidator()
        {
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
