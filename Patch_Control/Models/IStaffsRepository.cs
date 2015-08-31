using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Patch_Control.Models
{
    interface IStaffsRepository
    {
        IEnumerable<Staffs> getStaffAll();
    }
}
