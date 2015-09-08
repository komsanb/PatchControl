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

        // GET api/staff/permissonItemdata
        [HttpGet]
        [ActionName("PermissonItemdata")]
        public IEnumerable<PermissionItemdata> GetpermissionItemdata()
        {
            return repository.GetpermissionItemdataAll();
        }

        //[HttpGet]
        //[ActionName("StaffAccess")]
        //public IEnumerable<StaffAccess> GetstaffAccess()
        //{
        //    return repository.GetstaffAccessAll();
        //}

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/staff/staffall
        [HttpPost]
        [ActionName("StaffAll")]
        public IEnumerable<Staff> Post(Staff item)
        {
            return repository.PostStaffAll(item);
        }

        // POST api/staff/staffall
        //[HttpPost]
        //[ActionName("StaffEditAll")]
        //public IEnumerable<Staff> PostEditStaff(Staff item)
        //{
        //    return repository.PostEdStaffAll(item);
        //}

        // POST api/staff/staffrole
        //[HttpPost]
        //[ActionName("StaffRole")]
        //public IEnumerable<StaffRole> PostStaffRole(StaffRole staffRole, List<PermissionItemdata> permissionItemdata)
        //{
        //    return repository.PostStaffRoleAll(staffRole, permissionItemdata);
        //}

        // POST api/staff/staffaccess
        [HttpPost]
        [ActionName("StaffAccess")]
        public IEnumerable<StaffAccess> PostStaffAccess(StaffAccess staffAccess)
        {
            Console.WriteLine(staffAccess);

            return repository.PostStaffAccessAll(staffAccess );
        }

        // POST api/staff/permissonItemdata
        //[HttpPost]
        //[ActionName("PermissonItemdata")]
        //public IEnumerable<PermissionItemdata> PostPermissonItemdata(PermissionItemdata PermissionItemdata)
        //{
        //    return repository.PostPermissonItemdataAll(PermissionItemdata);
        //}

        public string Post(int id)
        {
            return "value";
        }

    }
}