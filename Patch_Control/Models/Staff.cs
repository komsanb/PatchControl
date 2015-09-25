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
        public int StaffID { get; set; }

        public string StaffRoleName { get; set; }

        public string StaffPassword { get; set; }

        public int StaffRoleID { get; set; }

        public string StaffFirstname { get; set; }

        public string StaffLastname { get; set; }

        public string StaffName { get; set; }

        public string StaffCode { get; set; }

        public string Gender { get; set; }

        public int GenderID { get; set; }

        public string Address1 { get; set; }

        public string Address2 { get; set; }

        public string City { get; set; }

        public string Province { get; set; }

        public int ProvinceID { get; set; }

        public string Zipcode { get; set; }

        public string Telephone { get; set; }

        public string Mobile { get; set; }

        public string Picture { get; set; }

        public string Email { get; set; }

        public int Deleted { get; set; }
    }

    public class StaffRole
    {
        public int StaffRoleID { get; set; }

        public string StaffRoleName { get; set; }

        public int Deleted { get; set; }    

    }

    public class PermissionItemdata
    {
        public String GroupName { get; set; }

        public List<PermissionItemParent> GroupParent { get; set; }
    }

    public class PermissionItemParent
    {
        public string PermissionGroupID { get; set; }

        public string PermissionItemUrl { get; set; }

        public string PermissionItemID { get; set; }

        public string PermissionItemName { get; set; }
    }

    public class Province
    {
        public int ProvinceID { get; set; }

        public string ProvinceName { get; set; }
    }

    public class Gender
    {
        public int GenderID { get; set; }

        public string GenderName { get; set; }
    }

    public class StaffAccess
    {
        public int StaffAccessID { get; set; }

        public int StaffRoleID { get; set; }

        public List<int> PermissionItemID { get; set; }

        public string StaffRoleName { get; set; }
    }

    public class StaffRoleAccess
    {
        public int StaffRoleID { get; set; }

        public string StaffRoleName { get; set; }

        public string PermissionItemID { get; set; }

    }

    public class PermissionGroup
    {
        public int PermissionGroupID { get; set; }

        public string PermissionGroupName { get; set; }

        //public string PermissionItemName { get; set; }

        //public string PermissionItemUrl { get; set; }

        //public int PermissionItemID { get; set; }
    }
}