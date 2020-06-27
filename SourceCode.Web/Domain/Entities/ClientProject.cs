using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SourceCode.Web.Domain.Entities
{
    public class ClientProject
    {
        [Key]
        public Guid Id { get; set; }

        public string Name { get; set; }

        [ForeignKey(nameof(Client))]
        public Guid ClientId { get; set; }

        public virtual Client Client { get; set; }

    }
}
