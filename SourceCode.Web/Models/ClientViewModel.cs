using SourceCode.Web.Domain.Entities;
using System;
using System.ComponentModel.DataAnnotations;

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
        }

        public Guid Id { get; set; }

        public string Name { get; set; }

        [Display(Name = "Website")]
        public string WebSite { get; set; }



        [Display(Name = "Director Name")]
        public string DirectorName { get; set; }


        [Display(Name = "Email Address")]
        public string EmailAddress { get; set; }

        /// <summary>
        /// Create new Client entity from ClientViewModel
        /// </summary>
        /// <param name="entity"></param>
        public Client MapToDomainObject(Client entity,ClientViewModel vm)
        {
            entity.Name = vm.Name;
            entity.WebSite = vm.WebSite;
            entity.DirectorName = vm.DirectorName;
            entity.EmailAddress = vm.EmailAddress;

            return entity;
        }
    }
}
