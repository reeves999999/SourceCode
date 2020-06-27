using System;

namespace SourceCode.Web.Controllers.API.v1.Contracts.Requests
{
    public class ClientUpdateRequest
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string WebSite { get; set; }

        public string DirectorName { get; set; }

        public string EmailAddress { get; set; }
    }
}
