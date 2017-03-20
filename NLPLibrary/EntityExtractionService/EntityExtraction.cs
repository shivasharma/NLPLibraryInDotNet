using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime.Remoting.Channels;
using System.Threading.Tasks;
using StanfordNLPProject.Helper;
using StanfordNLPProject.Model;
namespace StanfordNLPProject.EntityExtractionService
{
    public class EntityExtraction : IEntityExtraction
    {
        public string GetEntity(string entity)
        {
            return Startup.Classifier.classifyWithInlineXML(entity);
        }

        public IEnumerable<string> GetAllEntity(string entity)
        {
            var classifierResult = Startup.Classifier.classifyWithInlineXML(entity);

            var entityTypeOrganization = classifierResult.FilterByEntityType(EntityType.Organization.ToEnumDescription());
            var entities = entityTypeOrganization.ToList();

            var entityTypePerson = classifierResult.FilterByEntityType(EntityType.Person.ToEnumDescription());
            entities.AddRange(entityTypePerson);

            var entityTypelocation = classifierResult.FilterByEntityType(EntityType.Location.ToEnumDescription());
            entities.AddRange(entityTypelocation);

            return entities.Distinct();
        }
        public IEnumerable<string> GetByEntityType(string data, string entityType)
        {
            var classifierResult = Startup.Classifier.classifyWithInlineXML(data);
            switch (entityType)
            {
                case "Organization":
                    var entityTypeOrganization = classifierResult.FilterByEntityType(EntityType.Organization.ToEnumDescription());
                    var organization = entityTypeOrganization.ToList();
                    return organization.Distinct();

                case "Person":
                    var entityTypePerson = classifierResult.FilterByEntityType(EntityType.Person.ToEnumDescription());
                    var person = entityTypePerson.ToList();
                    return person.Distinct();

                case "Location":
                    var entityTypelocation = classifierResult.FilterByEntityType(EntityType.Location.ToEnumDescription());
                    var location = entityTypelocation.ToList();
                    return location.Distinct();
            }
            return null;
        }
        public async Task<string> GetAsync(string myUrl)
        {
            using (var client = new HttpClient())
            {
                return await client.GetStringAsync(myUrl);
            }
        }
      
        public IEnumerable<string> GetBySelectedEntities(Entity entity)
        {
            var classifierResult = Startup.Classifier.classifyWithInlineXML(entity.Rawtext);
            var allEntites = new List<string>();
            foreach (var entityType in entity.Entities)
            {
                GetEnitiesByType(entityType, classifierResult, allEntites);

            }
            return allEntites;
        }

        public IEnumerable<string> GetDataByUrl(Entity entity)
        {
            var result = (GetAsync(entity.Url));
            var extractText = result.Result.StripHtml();
            var classifierResult =  Startup.Classifier.classifyWithInlineXML(extractText);
            var allEntites = new List<string>();
            foreach (var entityType in entity.Entities)
            {
                GetEnitiesByType(entityType, classifierResult, allEntites);

            }
            return allEntites;
        }

        private static void GetEnitiesByType(string entityType, string classifierResult, List<string> allEntites)
        {
            switch (entityType)
            {
                case "Organization":
                    var entityOrganization = classifierResult.FilterByEntityType(EntityType.Organization.ToEnumDescription());
                    var organization = entityOrganization.ToList().Distinct();
                    allEntites.AddRange(organization);
                    break;

                case "Person":
                    var entityPerson = classifierResult.FilterByEntityType(EntityType.Person.ToEnumDescription());
                    var person = entityPerson.ToList().Distinct();
                    allEntites.AddRange(person);
                    break;

                case "Location":
                    var entityLocation = classifierResult.FilterByEntityType(EntityType.Location.ToEnumDescription());
                    var location = entityLocation.ToList().Distinct();
                    allEntites.AddRange(location);
                    break;
            }
        }
    }
}