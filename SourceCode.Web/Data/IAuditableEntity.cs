using System;

namespace SourceCode.Web.Data
{
    public interface IAuditableEntity
    {
        public string UpdatedBy { get; set; }

        public DateTime? DateUpdated { get; }
    }
}
