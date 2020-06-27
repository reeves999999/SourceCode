using SourceCode.Web.Options;
using System.Collections.Generic;

namespace SourceCode.Web.Models
{
    public class ClientsViewModel : PageAndSortViewModel
    {
        public ClientsViewModel()
        {
            Items = new List<ClientViewModel>();
        }

        public ClientsViewModel(IEnumerable<ClientViewModel> items, PagingOptions pagingOptions)
        {
            Items = items;
            PageSize = pagingOptions.PageSize;
            PageLinkCount = pagingOptions.PageLinkCount;
        }

        public IEnumerable<ClientViewModel> Items { get; set; }

    }
}
