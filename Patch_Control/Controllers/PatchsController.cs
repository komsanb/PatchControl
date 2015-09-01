using Patch_Control.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Patch_Control.Controllers
{
    public class PatchsController : ApiController
    {
        PatchsRepository repository = new PatchsRepository();

        [HttpGet]
        [ActionName("PatchInformations")]

        // GET api/<controller>
        public IEnumerable<Patchs> GetPatchInfos()
        {
            List<Patchs> items = new List<Patchs>();
            return repository.getPatchInformations();
        }

        [HttpGet]
        [ActionName("SoftwareType")]

        // GET api/<controller>
        public IEnumerable<SoftwareType> GetSoftwareType()
        {
            List<SoftwareType> items = new List<SoftwareType>();
            return repository.getSoftwareType();
        }

        [HttpGet]
        [ActionName("SoftwareVersion")]

        // GET api/<controller>
        public IEnumerable<SoftwareVersion> GetSoftwareVersion()
        {
            List<SoftwareVersion> items = new List<SoftwareVersion>();
            return repository.getSoftwareVersion();
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

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