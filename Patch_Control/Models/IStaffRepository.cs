using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Patch_Control.Models
{
    interface IStaffRepository
    {
        IEnumerable<Staff> GetStaffAll();
        IEnumerable<StaffRole> GetStaffRoleAll();
        IEnumerable<Province> GetProvinceAll();
        IEnumerable<Gender> GetGenderAll();
        IEnumerable<PermissionItemdata> GetpermissionItemdataAll();
        IEnumerable<StaffAccess> GetstaffAccessAll();
        IEnumerable<Staff> PostStaffAll(Staff item);
        //IEnumerable<Staff> PostEdStaffAll(Staff item);
        IEnumerable<StaffAccess> PostStaffAccessAll(StaffAccess staffAccess);
        //IEnumerable<StaffRole> PostStaffRoleAll(StaffRole staffRole, List<PermissionItemdata> permissionItemdata);
    }

}