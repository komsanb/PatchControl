using Patch_Control.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Patch_Control.Controllers
{
    public class StaffController : ApiController
    {
        StaffRepository repository = new StaffRepository();
        // GET api/staff/staffall
        [HttpGet]
        [ActionName("StaffAll")]
        public IEnumerable<Staff> Get()
          {
            return repository.GetStaffAll();
        }

        [HttpGet]
        [ActionName("StaffRole")]
        public IEnumerable<StaffRole> GetStaffRole()
        {
            return repository.GetStaffRoleAll();
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