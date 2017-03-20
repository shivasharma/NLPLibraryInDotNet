using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StanfordNLPProject.Model;

namespace StanfordNLPProject.EntityExtractionService
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