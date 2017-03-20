using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Helpers;
using System.Web.Http;
using StanfordNLPProject.EntityExtractionService;
using StanfordNLPProject.Helper;
using StanfordNLPProject.Model;
namespace StanfordNLP.Web.Api
{
    [RoutePrefix("api")]
    public class EntityExtrationController : ApiController
    {
       
        [HttpGet]
        [Route("entity")]
        public async Task<IHttpActionResult> Get(string data)
        {
            var entity = new EntityExtraction();
            var result = await Task.Factory.StartNew(() => entity.GetEntity(data));
            return Ok(result);
        }
        [HttpGet]
        [Route("allentity")]
        public async Task<IHttpActionResult> GetAllEntity(string data)
        {
            var entity = new EntityExtraction();
            var result = await Task.Factory.StartNew(() => entity.GetAllEntity(data));
            return Ok(result);
        }

        [HttpGet]
        [Route("organization")]
        public async Task<IHttpActionResult> GetByOrganization(string data)
        {
            var entity = new EntityExtraction();
            var result =
                await Task.Factory.StartNew(
                    () => entity.GetByEntityType(data, EntityType.Organization.ToString()));
            return Ok(result);
        }

        [HttpGet]
        [Route("person")]
        public async Task<IHttpActionResult> GetByPerson(string data)
        {
            var entity = new EntityExtraction();
            var result =
                await Task.Factory.StartNew(() => entity.GetByEntityType(data, EntityType.Person.ToString()));
            return Ok(result);
        }

        [HttpGet]
        [Route("location")]
        public async Task<IHttpActionResult> GetByLocation(string data)
        {
            var entity = new EntityExtraction();
            var result =
                await Task.Factory.StartNew(() => entity.GetByEntityType(data, EntityType.Location.ToString()));
            return Ok(result);
        }


        [HttpPost]
        [Route("entities")]
        public async Task<IHttpActionResult> PostByEntities([FromBody]Entity entity)
        {
            var entityEtl = new EntityExtraction();
            var result = await Task.Factory.StartNew(() => entityEtl.GetBySelectedEntities(entity));
            return Ok(result);
        }



        [HttpPost]
        [Route("url")]
        public async Task<IHttpActionResult> PostByUrl([FromBody]Entity entity)
        {
            var entityEtl = new EntityExtraction();
            var result =
                await Task.Factory.StartNew(() => entityEtl.GetDataByUrl(entity));
            return Ok(result);
        }
    }
}