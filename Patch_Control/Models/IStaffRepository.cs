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
        IEnumerable<StaffRole> GetStaffRoleAll();
        IEnumerable<Province> GetProvinceAll();
        IEnumerable<Gender> GetGenderAll();
        IEnumerable<PermissionItemdata> GetpermissionItemdataAll();
        StaffRole GetStaffRoleAll(int id);
        IEnumerable<StaffRoleAccess> GetStaffRoleAccessAll(int id);
        IEnumerable<PermissionGroup> GetPermissionGroupAll(List<PermissionItemdata> permissionItem);
        IEnumerable<Staff> PostStaffAll(Staff item);
        IEnumerable<Staff> PostStaffEditAll(Staff item);
        IEnumerable<Staff> PostEditPasswordStaffAll(Staff item);
        IEnumerable<Staff> PostStaffDeleteAll(Staff item);
        IEnumerable<StaffAccess> PostStaffAccessAll(StaffAccess staffAccess);
        IEnumerable<StaffAccess> PostStaffAccessEditAll(StaffAccess staffAccess);
        IEnumerable<StaffRole> PostStaffRoleDeleteAll(StaffRole staffAccess);
    }

}