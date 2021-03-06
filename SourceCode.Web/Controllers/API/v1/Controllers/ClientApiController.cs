﻿using Microsoft.AspNetCore.Mvc;
using Serilog;
using SourceCode.Web.Controllers.API.v1.Contracts.Requests;
using SourceCode.Web.Controllers.API.v1.Contracts.Responses;
using SourceCode.Web.Domain.Entities;
using SourceCode.Web.Models;
using SourceCode.Web.Services;
using System;
using System.Collections.Generic;
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
        public async Task<IActionResult> GetAll([FromRoute] string search)
        {
            var items = await _clientService.GetAsync(search:search);

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

            var response = new ClientCreateResponse
            {
                Success = true,
                Name = request.Name,
                URL = locationUri
            };

            return Created(locationUri, response);
        }

        [HttpPut(ApiRoutes.Clients.Update)]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] ClientUpdateRequest request)
        {
            var client = await _clientService.GetByIdAsync(id);

            if (client == null)
            {
                return NotFound();
            }

            client.Name = request.Name;
            client.WebSite = request.WebSite;
            client.DirectorName = request.DirectorName;
            client.EmailAddress = request.EmailAddress;

            var updated = await _clientService.UpdateAsync(client);

            if (updated)
            {
                return Ok(new ClientUpdateResponse
                {
                    Id = client.Id,
                    Name = client.Name,
                    WebSite = client.WebSite,
                    DirectorName = client.DirectorName,
                    EmailAddress = client.EmailAddress,

                });
            }

            return NotFound();
        }

        [HttpDelete(ApiRoutes.Clients.Delete)]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var deleted = await _clientService.DeleteAsync(id);

            if (deleted)
            {
                return NoContent();
            }

            return NotFound(new ErrorResponse
            {
                Errors = new List<ApiErrorModel>()
                    {
                        new ApiErrorModel
                        {
                            FieldName = string.Empty,
                            Message = "Unable to delete client."
                        }
                    }
            });
        }

    }
}
