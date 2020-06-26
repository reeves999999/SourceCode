using System.Collections.Generic;

namespace SourceCode.Web.Models
{
    public class ClientsViewModel : PageAndSortViewModel
    {
        public ClientsViewModel()
        {
            Items = new List<ClientViewModel>();
        }

        public ClientsViewModel(IEnumerable<ClientViewModel> items)
        {
            Items = items;
        }

        public IEnumerable<ClientViewModel> Items { get; set; }

    }
}
