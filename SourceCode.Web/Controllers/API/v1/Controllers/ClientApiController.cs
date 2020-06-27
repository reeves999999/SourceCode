using Microsoft.AspNetCore.Mvc;
using Serilog;
using SourceCode.Web.Controllers.API.v1.Contracts.Requests;
using SourceCode.Web.Controllers.API.v1.Contracts.Responses;
using SourceCode.Web.Domain.Entities;
using SourceCode.Web.Services;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SourceCode.Web.Controllers.API.v1.Controllers
{
    public class ClientApiController : ControllerBase
    {
            private readonly IClientService<Client> _clientService;

            public ClientApiController(IClientService<Client> clientService)
            {
            _clientService = clientService;
            }

            [HttpGet(ApiRoutes.Clients.GetAll)]
            public async Task<IActionResult> GetAll()
            {
                var items = await _clientService.GetAsync();

                if (items == null || !items.Any())
                {
                    Log.Information($"{nameof(ClientApiController)} - {nameof(GetAll)} - No results returned.");
                }

                return Ok(items.Select(x => new ClientResponse(x)));
            }

            [HttpGet(ApiRoutes.Clients.Get)]
            public async Task<IActionResult> Get([FromRoute] Guid id)
            {
                var item = await _clientService.GetByIdAsync(id);

                if (item == null)
                {
                    Log.Information($"{nameof(ClientApiController)} - {nameof(Get)} - 404 returned for ID: {id}.");

                    return NotFound();
                }

                return Ok(new ClientResponse(item));
            }

            [HttpGet(ApiRoutes.Clients.Search)]
            public async Task<IActionResult> Search([FromRoute] string search)
            {
                var items = await _clientService.SearchAsync(search);

                return Ok(items.Select(x => new ClientResponse(x)));
            }


        [HttpPost(ApiRoutes.Clients.Create)]
        public async Task<IActionResult> Create([FromBody] ClientCreateRequest request)
        {
            var newId = Guid.NewGuid();
            var client = new Client
            {
                Id = newId,
                Name = request.Name,
                WebSite = request.WebSite,
                DirectorName = request.DirectorName,
                EmailAddress = request.EmailAddress
            };

            await _clientService.CreateAsync(client);

            var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToUriComponent()}";

            var locationUri = $"{baseUrl}/{ApiRoutes.Clients.Get.Replace("{id}", newId.ToString())}";

            var response = new ClientCreateResponse { 
                Success = true,
                Name = request.Name,
                URL = locationUri
            };

            return Created(locationUri, response);
        }

    }
}
