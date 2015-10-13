using Patch_Control.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
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

        // GET api/<controller>/5
        [HttpGet]
        [ActionName("StaffAll")]
        public Staff Get(int id)
        {
            return repository.GetStaff(id);
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

        // GET api/staff/staffrole
        [HttpGet]
        [ActionName("StaffRole")]
        public StaffRole GetStaffRole(int id)
        {
            return repository.GetStaffRoleAll(id);
        }

        // GET api/staff/staffroleaccess
        [HttpGet]
        [ActionName("StaffRoleAccess")]
        public IEnumerable<StaffRoleAccess> GetStaffRoleAccess(int id)
        {
            return repository.GetStaffRoleAccessAll(id);
        }       

        // POST api/staff/permissionitem
        [HttpPost]
        [ActionName("PermissionItem")]
        public IEnumerable<PermissionItem> PostPermissionItem(PermissionItem item)
        {
            return repository.PostPermissionItemAll(item);
        }

        // POST api/staff/staffall
        [HttpPost]
        [ActionName("StaffAll")]
        public IEnumerable<Staff> Post(Staff item)
        {

            return repository.PostStaffAll(item);
        }

        // POST api/staff/staffpageindex
        [HttpPost]
        [ActionName("StaffPageIndex")]
        public Staff PostStaffIndex(Staff item)
        {
            return repository.PostStaffIndexAll(item);
        }

        // POST api/staff/permissiongroup
        [HttpPost]
        [ActionName("PermissionGroup")]
        public IEnumerable<PermissionItemdata> PostPermissionGroup(PermissionItemdata item)
        {
            return repository.PostPermissionGroupAll(item);
        }

        // POST api/staff/login
        [HttpPost]
        [ActionName("Login")]
        public Staff PostLogin(Staff item)
        {
            return repository.PostLoginAll(item);
        }


        // POST api/staff/staffedit
        [HttpPost]
        [ActionName("StaffEdit")]
        public IEnumerable<Staff> PostStaffEdit(Staff item)
        {
            Console.WriteLine(item);
            return repository.PostStaffEditAll(item);
        }

        // POST api/staff/editpasswordstaff
        [HttpPost]
        [ActionName("EditPasswordStaff")]
        public IEnumerable<Staff> PostEditPasswordStaff(Staff item)
        {
            return repository.PostEditPasswordStaffAll(item);
        }

        // POST api/staff/staffdelete
        [HttpPost]
        [ActionName("StaffDelete")]
        public IEnumerable<Staff> PostStaffDelete(Staff item)
        {
            return repository.PostStaffDeleteAll(item);
        }

        // POST api/staff/staffaccess
        [HttpPost]
        [ActionName("StaffAccess")]
        public IEnumerable<StaffAccess> PostStaffAccess(StaffAccess staffAccess)
        {
            Console.WriteLine(staffAccess);

            return repository.PostStaffAccessAll(staffAccess );
        }

        // POST api/staff/staffaccessedit
        [HttpPost]
        [ActionName("StaffAccessEdit")]
        public IEnumerable<StaffAccess> PostStaffAccessEdit(StaffAccess staffAccess)
        {

            return repository.PostStaffAccessEditAll(staffAccess);
        }

        // POST api/staff/staffroledelete
        [HttpPost]
        [ActionName("StaffRoleDelete")]
        public IEnumerable<StaffRole> PostStaffRoleDelete(StaffRole item)
        {
            return repository.PostStaffRoleDeleteAll(item);
        }

        // POST api/staff/image
        [HttpPost]
        [ActionName("Image")]
        public HttpResponseMessage Post()
        {
            HttpResponseMessage result = null;
            string imageName = "";
            var httpRequest = HttpContext.Current.Request;
            var staffid = httpRequest.Form[0];
            
            if (httpRequest.Files.Count > 0)
            {
                var docfiles = new List<string>();
                foreach (string file in httpRequest.Files)
                {
                    var postedFile = httpRequest.Files[file];
                    var filePath = HttpContext.Current.Server.MapPath("~/images/" + postedFile.FileName);
                    postedFile.SaveAs(filePath);

                    docfiles.Add(filePath);
                    imageName = postedFile.FileName.ToString();
                }
                result = Request.CreateResponse(HttpStatusCode.Created, docfiles);
            }
            else
            {
                result = Request.CreateResponse(HttpStatusCode.BadRequest);
            }
            
            repository.PostStaffImageAll(imageName, Convert.ToInt32(staffid));
            return result;
        }
    }
}