using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SourceCode.Web.Services
{
    public interface ICrudService<T>
        where T : class
    {
        Task<IList<T>> GetAsync(string sort = "", int skip = 0, int pageSize = 10, string search = "");

        Task<IList<T>> SearchAsync(string search);

        Task<T> GetByIdAsync(Guid id);

        Task<bool> CreateAsync(T entity);

        Task<bool> DeleteAsync(Guid id);

        Task<bool> UpdateAsync(T entity);

        bool Exists(Guid id);

        Task<int> GetCountAsync();

        Task<int> GetFilteredCountAsync(string search);
    }
}
