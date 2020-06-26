using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Serilog;
using SourceCode.Web.Domain.Entities;
using SourceCode.Web.Models;
using SourceCode.Web.Options;
using SourceCode.Web.Services;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SourceCode.Web.Controllers
{

    //NOTE: Naturally would add Authentication/Authorization/Roles etc. here
    public class HomeController : Controller
    {
        private readonly IClientService<Client> _clientService;
        private readonly IConfiguration _config;
        private readonly PagingOptions _pagingOptions;

        public HomeController(IClientService<Client> clientService, IConfiguration config, PagingOptions pagingOptions)
        {
            _clientService = clientService;
            _config = config;
            _pagingOptions = pagingOptions;
        }

        public async Task<IActionResult> Index(string sort = "", int page = 1, string search = "")
        {
            ViewData["NameSort"] = String.IsNullOrEmpty(sort) ? "NameDesc" : "";
            ViewData["WebSiteSort"] = sort == "WebSite" ? "WebSiteDesc" : "WebSite";
            ViewData["DirectorNameSort"] = sort == "DirectorName" ? "DirectorNameDesc" : "DirectorName";
            ViewData["EmailAddressSort"] = sort == "EmailAddress" ? "EmailAddressDesc" : "EmailAddress";

            int pageSize = _pagingOptions.PageSize;

            int skip = page > 1 ? (page - 1) * pageSize : 0;

            var items = await _clientService.GetAsync(sort: sort, skip: skip, pageSize: pageSize, search: search);

            var model = new ClientsViewModel(items.Select(x => new ClientViewModel(x)));

            int? filteredItemsCount = null;

            int totalItemsCount = await _clientService.GetCountAsync();
            if (!string.IsNullOrWhiteSpace(search))
            {
                filteredItemsCount = await _clientService.GetFilteredCountAsync(search);
            }


            model.SetPagingState(items.Count(), totalItemsCount, pageSize, page, sort, filteredItemsCount: filteredItemsCount, search: search);

            return View(model);
        }

        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var client = await _clientService.GetByIdAsync(id.Value);
            if (client == null)
            {
                return NotFound();
            }

            return View(new ClientViewModel(client));
        }

        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var client = await _clientService.GetByIdAsync(id.Value);
            if (client == null)
            {
                return NotFound();
            }

            return View(new ClientViewModel(client));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, ClientViewModel model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var client = await _clientService.GetByIdAsync(model.Id);

                if (client == null)
                {
                    Log.Warning($"{nameof(Edit)} for {nameof(HomeController)} failed to Update for ID: {model.Id} - no such item found in DB.");

                    return BadRequest();
                }

                client = model.MapToDomainObject(client, model);

                bool updated = await _clientService.UpdateAsync(client);

                if (updated)
                {
                    Log.Information($"{nameof(Edit)} for {nameof(HomeController)} successfully updated ID: {model.Id}");

                    return RedirectToAction(nameof(Details), new { id = model.Id });
                }
                return View(model);

            }
            return View(model);
        }

        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ClientViewModel model)
        {
            if (ModelState.IsValid)
            {
                var client = model.MapToDomainObject(new Client(), model);

                bool created = await _clientService.CreateAsync(client);

                if (created)
                {
                    Log.Information($"{nameof(Create)} for {nameof(HomeController)} successfully created with ID: {client.Id}");

                    return RedirectToAction(nameof(Details), new { id = client.Id });
                }
                return View(model);

            }
            return View(model);
        }


        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var client = await _clientService.GetByIdAsync(id.Value);
            if (client == null)
            {
                return NotFound();
            }

            return View(new ClientViewModel(client));
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var deleted = await _clientService.DeleteAsync(id);
            if (!deleted)
            {
                Log.Warning($"{nameof(Delete)} for {nameof(HomeController)} failed for ID: {id}");
            }

            return RedirectToAction(nameof(Index));
        }

        private bool ClientExists(Guid id)
        {
            return _clientService.Exists(id);
        }
    }
}
