using SourceCode.Web.Data;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SourceCode.Web.Domain.Entities
{
    public class ClientProject : IAuditableEntity
    {
        [Key]
        public Guid Id { get; set; }

        public string Name { get; set; }

        [ForeignKey(nameof(Client))]
        public Guid ClientId { get; set; }

        public virtual Client Client { get; set; }


        public string UpdatedBy { get; set; }

        public DateTime? DateUpdated { get; set; }

    }
}
