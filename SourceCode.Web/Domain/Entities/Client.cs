using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SourceCode.Web.Domain.Entities
{
    public class Client
    {
        public Client()
        {
            ClientProjects = new List<ClientProject>();
        }

        [Key]
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string WebSite { get; set; }

        public string DirectorName { get; set; }

        public string EmailAddress { get; set; }

        public string UpdatedBy { get; set; }

        public DateTime? DateUpdated { get; set; }

        public string ImageUrl { get; set; }

        public virtual ICollection<ClientProject> ClientProjects { get; set; }
    }
}
