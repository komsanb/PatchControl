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
        Staff GetStaff(int id);
        Staff PostStaffImageAll(string imageName, int staffid);
        IEnumerable<StaffRole> GetStaffRoleAll();
        IEnumerable<Province> GetProvinceAll();
        IEnumerable<Gender> GetGenderAll();
        StaffRole GetStaffRoleAll(int id);
        IEnumerable<StaffRoleAccess> GetStaffRoleAccessAll(int id);
        //IEnumerable<PermissionItemdata> GetPermissionGroupAll();
        IEnumerable<PermissionItemdata> PostPermissionGroupAll(PermissionItemdata item);
        IEnumerable<PermissionItem> PostPermissionItemAll(PermissionItem item);
        IEnumerable<Staff> PostStaffAll(Staff item);
        Staff PostStaffIndexAll(Staff item);
        Staff PostLoginAll(Staff item);
        IEnumerable<Staff> PostStaffEditAll(Staff item);
        IEnumerable<Staff> PostEditPasswordStaffAll(Staff item);
        IEnumerable<Staff> PostStaffDeleteAll(Staff item);
        IEnumerable<StaffAccess> PostStaffAccessAll(StaffAccess staffAccess);
        IEnumerable<StaffAccess> PostStaffAccessEditAll(StaffAccess staffAccess);
        IEnumerable<StaffRole> PostStaffRoleDeleteAll(StaffRole staffAccess);
    }

}