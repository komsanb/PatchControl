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

        //================== Upload File ===================
        [HttpPost]
        [ActionName("FileUpload")]
        public HttpResponseMessage PostFile()
            {
            HttpResponseMessage result = null;
            string fileName = "";
            string pathName = "";
            int staffID = 0;
            var httpRequest = HttpContext.Current.Request;
            var getStaffID = httpRequest.Form[0];
            if (httpRequest.Files.Count > 0)
            {
                var docfiles = new List<string>();
                foreach (string file in httpRequest.Files)
                {
                    try
                    {
                        var postedFile = httpRequest.Files[file];
                        var filePath = HttpContext.Current.Server.MapPath("~/Patchs/" + postedFile.FileName);
                        postedFile.SaveAs(filePath);
                        docfiles.Add(filePath);

                        //convert filename and pathname to string
                        fileName = postedFile.FileName.ToString();
                        pathName = filePath.ToString();

                    }
                    catch(Exception e)
                    {
                        Console.WriteLine(e);
                    }
                    
                }
                result = Request.CreateResponse(HttpStatusCode.Created, docfiles);

            }
            else
            {
                result = Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            staffID = Convert.ToInt32(getStaffID);
            repository.postFilesInformations(pathName, fileName, staffID);
            return result;
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