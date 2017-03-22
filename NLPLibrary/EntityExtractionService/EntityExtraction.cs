using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using NLPLibrary.Helper;
using NLPLibrary.Model;

namespace NLPLibrary.EntityExtractionService
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
                    var entityTypeOrganization =
                        classifierResult.FilterByEntityType(EntityType.Organization.ToEnumDescription());
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
            var client = new HttpClient();

            var GetDetails = client.GetStringAsync(myUrl);
            return await GetDetails;
        }

        public IEnumerable<string> GetBySelectedEntities(Entity entity)
        {
            var classifierResult = Startup.Classifier.classifyWithInlineXML(entity.Rawtext);
            var allEntites = new List<string>();
            var output = new List<string>();
            foreach (var entityType in entity.Entities)
                GetEnitiesByType(entityType, classifierResult, allEntites, output);
            return allEntites;
        }

        public IEnumerable<string> GetData(Entity entity, string url)
        {
            var output = new List<string>();
            if (entity != null)
            {
                var classifierResult = Startup.Classifier.classifyWithInlineXML(entity.Rawtext);
                var allEntites = new List<string>();
                var keyValueListDict = new Dictionary<string, List<string>>();
                var checkentities = IncludeAllEnityTypes(entity);
                foreach (var entityType in checkentities.Entities)
                    output = GetEnitiesByType(entityType, classifierResult, allEntites, output);
            }
            else

            {
                var passedurl = url.IsValidUrl();
                if (passedurl)
                {
                    var result = GetAsync(url);
                }
                // var extractText = result.Result.StripHtml();
                //var classifierResult = Startup.Classifier.classifyWithInlineXML(extractText);
                var allEntites = new List<string>();
                var keyValueListDict = new Dictionary<string, List<string>>();
                var checkentities = IncludeAllEnityTypes(entity);
                foreach (var entityType in checkentities.Entities)
                {
                    //output = GetEnitiesByType(entityType, classifierResult, allEntites, output);
                }
            }
            return output.ToList();
        }

        private static Entity IncludeAllEnityTypes(Entity entity)
        {
            if (entity == null)
            {
                var entityInitialize = new Entity();
                entityInitialize.Entities = new List<string>
                {
                    EntityType.Organization.ToString(),
                    EntityType.Person.ToString(),
                    EntityType.Location.ToString()
                };
                return entityInitialize;
            }
            return entity;
        }

        private static List<string> GetEnitiesByType(string entityType, string classifierResult, List<string> allEntites,
            List<string> output)
        {
            var keyValueListDict = new Dictionary<string, List<string>>();
            switch (entityType)
            {
                case "Organization":
                    var entityOrganization =
                        classifierResult.FilterByEntityType(EntityType.Organization.ToEnumDescription());
                    var organization = entityOrganization.ToList().Distinct();
                    keyValueListDict.Add("company_ss", organization.ToList());
                    output.Add(
                        new KeyValuePair<string, List<string>>("company_ss", keyValueListDict["company_ss"])
                            .GetOutputString());
                    break;

                case "Person":
                    var entityPerson = classifierResult.FilterByEntityType(EntityType.Person.ToEnumDescription());
                    var personEntity = entityPerson.ToList().Distinct();
                    keyValueListDict.Add("person_ss", personEntity.ToList());
                    output.Add(
                        new KeyValuePair<string, List<string>>("person_ss", keyValueListDict["person_ss"])
                            .GetOutputString());
                    break;

                case "Location":
                    var entityLocation = classifierResult.FilterByEntityType(EntityType.Location.ToEnumDescription());
                    var location = entityLocation.ToList().Distinct();
                    keyValueListDict.Add("location_ss", location.ToList());
                    output.Add(
                        new KeyValuePair<string, List<string>>("location_ss", keyValueListDict["location_ss"])
                            .GetOutputString());
                    break;
            }

            return output;
        }
    }
}