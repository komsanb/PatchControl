using Patch_Control.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace Patch_Control.Controllers
{
    public class PatchsController : ApiController
    {
        PatchsRepository repository = new PatchsRepository();

        //================= Get PatchInformations ======================

        [HttpGet]
        [ActionName("PatchInformations")]
        public IEnumerable<Patchs> GetPatchInfos()
        {
            return repository.getPatchInformations();
        }

        //================= Get SoftwareType ======================

        [HttpGet]
        [ActionName("SoftwareType")]
        public IEnumerable<SoftwareType> GetSoftwareType()
        { 
            return repository.getSoftwareType();
        }

        //================= Get SoftwareVersion ======================

        [HttpGet]
        [ActionName("SoftwareVersion")]
        public IEnumerable<SoftwareVersion> GetSoftwareVersion()
        {
            return repository.getSoftwareVersion();
        }

        //================= Get MyPatch ======================

        [HttpGet]
        [ActionName("MyPatch")]
        public IEnumerable<MyPatch> GetMyPatch(int id)
        {
            return repository.getMyPatch(id);
        }

        //================= Get MyPatchDetails ======================

       
        [HttpGet]
        [ActionName("MyPatchDetails")]
        public MyPatch GetMyPatchInformationsById(int id)
        {
            return repository.getMyPatchInformations(id);
        }

        //================= Post UpdateMyPatch ======================

        [HttpPost]
        [ActionName("UpdateMyPatch")]
        public IEnumerable<Patchs> updateMyPatchInformations(Patchs update)
        {
            return repository.postUpdatePatchInformations(update);
        }


        //================= Post DeletedMyPatch ======================

        [HttpPost]
        [ActionName("DeletedMyPatch")]
        public IEnumerable<Patchs> deletedMyPatch(int id)
        { 
            return repository.postDeletePatchInformations(id);
        }

        //================= Post PatchInformations ======================

        [HttpPost]
        [ActionName("PatchInformations")]
        public IEnumerable<Patchs> PostPatchInfos(Patchs items)
        {

            return repository.postPatchInformations(items);
        }

        //================= Post UploadFiles ======================

        [HttpPost]
        [ActionName("UploadFiles")]
        public System.Web.Mvc.ActionResult PostFilesInfos(IEnumerable<HttpPostedFileBase> files)
        {
            string path = "";
            string fileName = "";

            foreach (var file in files)
            {
                if(file.ContentLength > 0)
                {
                    var filename = Path.GetFileName(file.FileName);
                    var filepath = Path.Combine(HttpContext.Current.Server.MapPath("~/files/Patchs"), filename);
                    file.SaveAs(filepath);

                    path = filepath.ToString();
                    fileName = fileName.ToString();
                }
            }

            return repository.postFilesInformations(path, fileName);
        }

        //====================== Sent E-mail ========================
        [HttpPost]
        [ActionName("SentEmail")]
        public IEnumerable<Email> SentEmail(Email items)
        {
            return repository.sentEmail(items);
        }

        //================= Post UploadFiles ======================
    }
}