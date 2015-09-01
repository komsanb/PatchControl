using Patch_Control.Models;
using System.Collections.Generic;
using System.Web.Http;

namespace Patch_Control.Controllers
{
    public class PatchsController : ApiController
    {
        PatchsRepository repository = new PatchsRepository();

        [HttpGet]
        [ActionName("PatchInformations")]

        // GET api/<controller>
        public IEnumerable<Patchs> Get()
        {
            List<Patchs> items = new List<Patchs>();
            return repository.getPatchInformations();
        }

        // GET api/<controller>/5
        //public string Get(int id)
        //{
        //    return "value";
        //}

        // POST api/<controller>
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}