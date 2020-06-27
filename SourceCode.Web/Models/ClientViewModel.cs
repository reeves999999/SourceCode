using Microsoft.AspNetCore.Http;
using SourceCode.Web.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.InteropServices;

namespace SourceCode.Web.Models
{
    public class ClientViewModel
    {
        public ClientViewModel()
        {

        }

        public ClientViewModel(Client entity)
        {
            Id = entity.Id;
            Name = entity.Name;
            WebSite = entity.WebSite;
            DirectorName = entity.DirectorName;
            EmailAddress = entity.EmailAddress;
            ImageUrl = entity.ImageUrl;
            ClientProjects = entity.ClientProjects.Select(x => new ClientProjectViewModel
            {
                Id = x.Id,
                Name = x.Name
            });
        }

        public Guid Id { get; set; }

        public string Name { get; set; }

        [Display(Name = "Website")]
        public string WebSite { get; set; }

        [Display(Name = "Director Name")]
        public string DirectorName { get; set; }


        [Display(Name = "Email Address")]
        public string EmailAddress { get; set; }

        [Display(Name = "Projects")]
        public IEnumerable<ClientProjectViewModel> ClientProjects { get; set; }

        public string ImageUrl { get; set; }

        public string ImageUrlPath { get; set; }

        [Display(Name = "Image File")]
        public IFormFile ImageFile { get; set; }

        /// <summary>
        /// Create new Client entity from ClientViewModel
        /// </summary>
        /// <param name="entity"></param>
        public Client MapToDomainObject(Client entity, ClientViewModel vm)
        {
            entity.Name = vm.Name;
            entity.WebSite = vm.WebSite;
            entity.DirectorName = vm.DirectorName;
            entity.EmailAddress = vm.EmailAddress;
            entity.ImageUrl = vm.ImageUrl;

            return entity;
        }
    }
}
