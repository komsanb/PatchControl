using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Patch_Control.Models
{
    public class Patchs
    {
        public int patchsID { get; set; }
        public int staffID { get; set; }
        public int softwareTypeID { get; set; }
        public int softwareVersionID { get; set; }
        public string patchsName { get; set; }
        public string patchsDescription { get; set; }
        public string patchsUpdateDate { get; set; }        
        public string patchsInsertDate { get; set; }
        public string patchsInsertBy { get; set; }
        public string patchsUpdateBy { get; set; }
        public string softwareTypeName { get; set; }
        public string softwareVersionName { get; set; }
        public string patchsPathName { get; set; }
        public string patchsVersionNumber { get; set; }
        public string deleted { get; set; }
    }

    public class SoftwareType
    {
        public int softwareTypeID { get; set; }
        public string softwareTypeName { get; set; }
    }

    public class SoftwareVersion
    {
        public int softwareVersionID { get; set; }
        public string softwareVersionName { get; set; }
        public int softwareTypeID { get; set; }
    }

    public class Files
    {
        public int filesID { get; set; }
        public HttpPostedFileBase filesName { get; set; }
        public HttpPostedFileBase filesPath { get; set; }
    }

    public class MyPatch
    {
        public int staffID { get; set; }
        public int patchsID { get; set; }
        public string patchsName { get; set; }
        public string patchsInsertDate { get; set; }
        public string softwareTypeName { get; set; }
        public string softwareVersionName { get; set; }
        public string patchsVersionNumber { get; set; }
        public string patchsDescription { get; set; }
        public string staffFirstname { get; set; }
        public int softwareTypeID { get; set; }
        public int softwareVersionID { get; set; }
    }

    public class Email
    {
        public int staffID { get; set; }
        public int staffRoleID { get; set; }
        public string myEmail { get; set; }
    }
}