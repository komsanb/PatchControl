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
        IEnumerable<MyPatch> getMyPatch(int staffID);
        MyPatch getMyPatchInformations(int staffID);
        IEnumerable<Patchs> postUpdatePatchInformations(Patchs update);
        IEnumerable<Patchs> postDeletePatchInformations(int patchID);
        IEnumerable<Patchs> postPatchInformations(Patchs items);
        System.Web.Mvc.ActionResult postFilesInformations(string path, string fileName);
        IEnumerable<Email> sentEmail(Email items);
    }
}