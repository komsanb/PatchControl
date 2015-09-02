using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Patch_Control.Models
{
    public class Patchs
    {
        public int PatchsID { get; set; }
        public int SoftwareTypeID { get; set; }
        public int SoftwareVersionID { get; set; }
        public string PatchsName { get; set; }
        public string PatchsDescription { get; set; }
        public string PatchsUpadateDate { get; set; }        
        public string PatchsInsertDate { get; set; }
        public string PatchsInsertBy { get; set; }
        public string PatchsUpdateBy { get; set; }
        public string SoftwareTypeName { get; set; }
        public string SoftwareVersionName { get; set; }
    }

    public class SoftwareType
    {
        public int SoftwareTypeID { get; set; }
        public string SoftwareTypeName { get; set; }
    }

    public class SoftwareVersion
    {
        public int SoftwareVersionID { get; set; }
        public string SoftwareVersionName { get; set; }
    }
}