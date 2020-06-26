using SourceCode.Web.Domain.Entities;
using System;

namespace SourceCode.Web.Controllers.API.v1.Contracts.Responses
{
    public class ClientResponse
    {
        public ClientResponse(Client entity)
        {
            Id = entity.Id;
            Name = entity.Name;
            WebSite = entity.WebSite;
            DirectorName = entity.DirectorName;
            EmailAddress = entity.EmailAddress;
        }

        public Guid Id { get; set; }

        public string Name { get; set; }

        public string WebSite { get; set; }

        public string DirectorName { get; set; }

        public string EmailAddress { get; set; }
    }
}
