using Patch_Control.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Patch_Control.Controllers
{
    public class StaffsController : ApiController
    {
        StaffsRepository repository = new StaffsRepository();


        [HttpGet]
        [ActionName("StaffAll")]

        // GET api/<controller>
        public IEnumerable<Staffs> Get()
        {
            List<Staffs> items = new List<Staffs>();
            return repository.getStaffAll();
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