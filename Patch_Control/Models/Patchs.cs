using System;

namespace Patch_Control.Models
{
    public class Patchs
    {
        public int PatchsID { get; set; }
        public string PatchsName { get; set; }
        public string PatchsDescription { get; set; }
        public DateTime PatchsInsertDate { get; set; }
        public string PatchsInsertBy { get; set; }
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
        public string SoftwareVerionsName { get; set; }
    }
}