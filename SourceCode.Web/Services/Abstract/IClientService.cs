using SourceCode.Web.Domain.Entities;

namespace SourceCode.Web.Services
{
    public interface IClientService<T> : ICrudService<T>
        where T : Client
    {
    }
}
