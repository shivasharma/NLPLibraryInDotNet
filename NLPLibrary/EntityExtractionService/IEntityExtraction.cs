using System.Collections.Generic;
using System.Threading.Tasks;
using NLPLibrary.Model;

namespace NLPLibrary.EntityExtractionService
{
    public interface IEntityExtraction
    {
        string GetEntity(string data);
        IEnumerable<string> GetByEntityType(string data, string entityType);
        IEnumerable<string> GetAllEntity(string data);

        Task<string> GetAsync(string data);
        IEnumerable<string> GetBySelectedEntities(Entity entity);
    }
}