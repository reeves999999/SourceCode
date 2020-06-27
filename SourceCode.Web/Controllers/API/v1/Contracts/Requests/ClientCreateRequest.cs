using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SourceCode.Web.Controllers.API.v1.Contracts.Requests
{
    public class ClientCreateRequest
    {
        public string Name { get; set; }

        public string WebSite { get; set; }

        public string DirectorName { get; set; }

        public string EmailAddress { get; set; }
    }
}
