using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SourceCode.Web.Controllers.API.v1.Contracts.Responses;
using SourceCode.Web.Models;
using System.Linq;
using System.Threading.Tasks;

namespace SourceCode.Web.Middleware.Filters
{
    public class ValidationFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (!context.ModelState.IsValid)
            {
                var errorsInModel = context.ModelState
                    .Where(x => x.Value.Errors.Count > 0)
                    .ToDictionary(kvp => kvp.Key, kvp => kvp
                     .Value
                     .Errors
                     .Select(x => x.ErrorMessage))
                    .ToArray();

                var errorResponse = new ErrorResponse();

                foreach (var error in errorsInModel)
                {
                    foreach (var childError in error.Value)
                    {
                        var model = new ApiErrorModel
                        {
                            FieldName = error.Key,
                            Message = childError
                        };

                        errorResponse.Errors.Add(model);
                    }
                }

                context.Result = new BadRequestObjectResult(errorResponse);
                return;
            }

            await next();
        }
    }
}
