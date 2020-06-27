using Microsoft.EntityFrameworkCore;
using SourceCode.Web.Data;
using SourceCode.Web.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SourceCode.Web.Services
{
    public class ClientService<T> : IClientService<T>
        where T : Client
    {
        private readonly ApplicationDbContext _dbContext;

        public ClientService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IList<T>> GetAsync(string sort = "", int skip = 0, int take = 10, string search = "")
        {
            var query = _dbContext.Clients
                .AsNoTracking();

            if (!string.IsNullOrWhiteSpace(search.Trim()))
            {
                query = query
                .Where(x => x.Name.Contains(search));
            }

            //use reflection here
            switch (sort)
            {
                case "Name":
                    query = query.OrderBy(x => x.Name);
                    break;

                case "NameDesc":
                    query = query.OrderByDescending(x => x.Name);
                    break;

                case "WebSite":
                    query = query.OrderBy(x => x.WebSite);
                    break;

                case "WebSiteDesc":
                    query = query.OrderByDescending(x => x.WebSite);
                    break;

                case "DirectorName":
                    query = query.OrderBy(x => x.DirectorName);
                    break;

                case "DirectorNameDesc":
                    query = query.OrderByDescending(x => x.DirectorName);
                    break;

                case "EmailAddress":
                    query = query.OrderBy(x => x.EmailAddress);
                    break;

                case "EmailAddressDesc":
                    query = query.OrderByDescending(x => x.EmailAddress);
                    break;

                default:
                    query = query.OrderBy(x => x.Name);
                    break;
            }

            query = query
                .Skip(skip)
                .Take(take);

            var output = await query.ToListAsync();

            return output as IList<T>;
        }

        public async Task<int> GetCountAsync()
        {
            return await _dbContext.Clients.CountAsync();
        }

        public async Task<int> GetFilteredCountAsync(string search)
        {
            return await _dbContext.Clients
                .Where(x => x.Name.Contains(search))
                .CountAsync();
        }

        public async Task<T> GetByIdAsync(Guid id)
        {
            var entity = await _dbContext.Clients
                .Include(x=>x.ClientProjects)
                .FirstOrDefaultAsync(x=>x.Id == id);

            return entity as T;
        }

        public bool Exists(Guid id)
        {
            return _dbContext.Clients.Any(x => x.Id == id);
        }

        public async Task<bool> UpdateAsync(T entity)
        {
            _dbContext.Clients.Update(entity);

            var updated = await _dbContext.SaveChangesAsync();

            return updated > 0;
        }

        public async Task<bool> CreateAsync(T entity)
        {
            _dbContext.Clients.Add(entity);
            var created = await _dbContext.SaveChangesAsync();
            return created > 0;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var entity = await _dbContext.Clients.FindAsync(id);

            if (entity == null)
            {
                return false;
            }

            _dbContext.Clients.Remove(entity);

            var deleted = await _dbContext.SaveChangesAsync();

            return deleted > 0;

        }

        public async Task<IList<T>> SearchAsync(string search)
        {
            var query = _dbContext.Clients
               .AsNoTracking()
                .Where(x => x.Name.Contains(search));

            var output = await query.ToListAsync();

            return output as IList<T>;
        }
    }
}
