using System;

namespace SourceCode.Web.Domain.Entities
{
    public class Client
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string WebSite { get; set; }

        public string DirectorName { get; set; }

        public string EmailAddress { get; set; }

        public string UpdatedBy { get; set; }

        public DateTime? DateUpdated { get; set; }
    }
}
