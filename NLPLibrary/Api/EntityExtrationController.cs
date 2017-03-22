using System.Threading.Tasks;
using System.Web.Http;
using NLPLibrary.EntityExtractionService;
using NLPLibrary.Helper;
using NLPLibrary.Model;

namespace NLPLibrary.Api
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

        [HttpGet]
        [Route("entities")]
        public async Task<IHttpActionResult> GetByUrl([FromUri] string url)
        {
            if (ModelState.IsValid)
            {
                var entityEtl = new EntityExtraction();
                var result = await Task.Factory.StartNew(() => Json(entityEtl.GetData(null, url)));
                return Ok(result.Content);
            }
            return BadRequest("Invalid request");
        }


        [HttpPost]
        [Route("entities")]
        public async Task<IHttpActionResult> PostByText([FromBody] Entity entity)
        {
            var entityEtl = new EntityExtraction();
            var result = await Task.Factory.StartNew(() => Json(entityEtl.GetData(entity, null)));
            return Ok(result.Content);
        }
    }
}