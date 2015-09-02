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
        // POST api/staff/staffall
        [HttpPost]
        [ActionName("StaffAll")]
        public IEnumerable<Staff> Post(Staff item)
        {
            return repository.PostStaffAll(item);
        }
        // GET api/staff/staffrole
        [HttpGet]
        [ActionName("StaffRole")]
        public IEnumerable<StaffRole> GetStaffRole()
        {
            return repository.GetStaffRoleAll();
        }
        // GET api/staff/province
        [HttpGet]
        [ActionName("Province")]
        public IEnumerable<Province> GetProvince()
        {
            return repository.GetProvinceAll();
        }
        // GET api/staff/gender
        [HttpGet]
        [ActionName("Gender")]
        public IEnumerable<Gender> GetGender()
        {
            return repository.GetGenderAll();
        }
        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        public string Post(int id)
        {
            return "value";
        }

        
    }
}