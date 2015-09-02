using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Patch_Control.Models
{
    interface IPatchsRepository
    {
        IEnumerable<Patchs> getPatchInformations();
        IEnumerable<SoftwareType> getSoftwareType();
        IEnumerable<SoftwareVersion> getSoftwareVersion();
    }
}