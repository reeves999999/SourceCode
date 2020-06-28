using SourceCode.Web.Models;
using System.Collections.Generic;

namespace SourceCode.Web.Controllers.API.v1.Contracts.Responses
{
    public class ErrorResponse
    {
        public List<ApiErrorModel> Errors { get; set; } = new List<ApiErrorModel>();
    }
}
