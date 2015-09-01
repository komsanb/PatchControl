using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Patch_Control.Models
{
    public class Staff
    {

        public string StaffRole { get; set; }

        public int StaffRoleID { get; set; }

        public string StaffFirstname { get; set; }

        public string StaffLastname { get; set; }

        public string StaffName { get; set; }

        public int StaffCode { get; set; }

        public string Gender { get; set; }

        public string Address1 { get; set; }

        public string Address2 { get; set; }

        public string City { get; set; }

        public string Province { get; set; }

        public string Zipcode { get; set; }

        public string Telephone { get; set; }

        public string Mobile { get; set; }

        public string Picture { get; set; }

        public string Email { get; set; }

    }

    public class StaffRole
    {
        public int StaffRoleID { get; set; }

        public string StaffRoleName { get; set; }
    }
}