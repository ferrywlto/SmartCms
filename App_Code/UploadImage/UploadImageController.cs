using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;

namespace SmartCms.App_Code
{
    public class UploadImageController : Umbraco.Web.Mvc.SurfaceController
    {
        //[HttpPost]
        //public ActionResult HandleUpload(UploadImageModel model)
        //{
        //    return View();
        //}
        public ActionResult UploadDocument()
        {
            return View();
        }

        //[HttpPost]
        //public ActionResult Upload(HttpPostedFileBase file)
        //{
        //    if (file != null && file.ContentLength > 0)
        //    {
        //        var fileName = System.IO.Path.GetFileName(file.FileName);
        //        var path = System.IO.Path.Combine(Server.MapPath("~/Images/"), fileName);
        //        file.SaveAs(path);
        //    }

        //    return RedirectToAction("UploadDocument");
        //}
        [HttpPost]
        public ActionResult HandleUpload()
        {
            if (Request.Files.Count > 0)
            {
                var file = Request.Files[0];

                //if (file != null && file.ContentLength > 0)
                //{
                //    var fileName = System.IO.Path.GetFileName(file.FileName);
                //    var path = System.IO.Path.Combine(Server.MapPath("~/Images/"), fileName);
                //    file.SaveAs(path);
                    
                //}
                var ms = Services.MediaService;
                var mediaFile = ms.CreateMedia("UploadTest", -1, global::Umbraco.Core.Constants.Conventions.MediaTypes.Folder);
                

                foreach (var propertyType in mediaFile.PropertyTypes)
                {
                    string alias = propertyType.Alias;
                    string name = propertyType.Name;
                    string description = propertyType.Description;
                    int dataTypeDefinitionId = propertyType.DataTypeDefinitionId;
                    bool mandatory = propertyType.Mandatory;
                    int sortOrder = propertyType.SortOrder;
                }
                mediaFile.SetValue("umbracoFile", file);
                ms.Save(mediaFile);
            }

            //var ms =  Services.MediaService;
            ////Use the MediaService to create a new Media object (-1 is Id of root Media object, "File" is the MediaType)
            //var mediaFile = ms.CreateMedia("UploadTest", 1364, "File");
            ////set the umbracofile property with upload file
            //FileStream s = new FileStream(Request.QueryString["uploadFile"], FileMode.Open);
            //mediaFile.SetValue("umbracoFile", Path.GetFileName(Request.QueryString["uploadFile"]), s);
            ////Use the MediaService to Save the new Media object
            
            return RedirectToAction("UploadDocument");
        }
    }


}
