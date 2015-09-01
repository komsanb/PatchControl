using System.Collections.Generic;

namespace Patch_Control.Models
{
    interface IPatchsRepository
    {
        IEnumerable<Patchs> getPatchInformations();
        IEnumerable<SoftwareVersion> getSoftwareVersion();
        IEnumerable<SoftwareType> getSoftwareType();
    }
}