using System;
using System.Web.Mvc;
using Syncfusion.JavaScript;
using Umbraco.Web.WebApi;
using Umbraco.Web.Mvc;
using System.Collections;
using System.Collections.Generic;
namespace My.Controllers
{
    public class PathModel
    {
        public string skill { get; set; }
        public string category { get; set; }
        public uint value { get; set; }
    }
    [PluginController("My")]
    public partial class FileExplorerController : SurfaceController
    {
        [Authorize]
        public IEnumerable<PathModel> GetPathList()
        {
            List<PathModel> list = new List<PathModel>();
            list.Add(new PathModel { skill = "Cabbage", value = 1001, category = "Inspection" });
            list.Add(new PathModel { skill = "Pea", value = 1002, category = "Inspection" });
            list.Add(new PathModel { skill = "Spinach", value = 1003, category = "Inspection" });
            list.Add(new PathModel { skill = "Wheatgrass", value = 1004, category = "Inspection" });
            list.Add(new PathModel { skill = "Yarrow", value = 1005, category = "Inspection" });
            list.Add(new PathModel { skill = "Chickpea", value = 1006, category = "Safety" });
            list.Add(new PathModel { skill = "Green bean", value = 1007, category = "Safety" });
            list.Add(new PathModel { skill = "Horse gram", value = 1008, category = "Safety" });
            list.Add(new PathModel { skill = "Peanut", value = 1009, category = "ATMS" });
            list.Add(new PathModel { skill = "Pigeon pea", value = 1010, category = "ATMS" });
            list.Add(new PathModel { skill = "Garlic", value = 1011, category = "ePermit" });
            return list;
        }
        [Authorize]
        public ActionResult FileActionDefault(FileExplorerParams args)
        {
            switch (args.ActionType)
            {
                case "Read":
                    //need to check whether user have permission on project
                    //input: project ID, user ID
                    //if not authorized -> return Json("")
                    //umbraco.helper.GetCurrentUmbracoUser().Id -> project permission table
                    return Json(FileExplorerOperations.Read(args.Path, args.ExtensionsAllow));
                case "CreateFolder":
                    return Json(FileExplorerOperations.CreateFolder(args.Path, args.Name));
                case "Paste":
                    FileExplorerOperations.Paste(args.LocationFrom, args.LocationTo, args.Name.Split(','), args.Action, args.CommonFiles);
                    break;
                case "Delete":
                    FileExplorerOperations.Delete(args.Name.Split(','), args.Path);
                    break;
                case "Rename":
                    FileExplorerOperations.Rename(args.Path, args.PreviousName, args.NewName, args.Type, args.CommonFiles);
                    break;
                case "GetDetails":
                    return Json(FileExplorerOperations.GetDetails(args.Path, args.Name, args.Type));
                case "Download":
                    FileExplorerOperations.Download(args.Path);
                    break;
                case "Upload":
                    FileExplorerOperations.Upload(args.FileUpload, args.Path);
                    break;
            }
            return Json("");
        } 
    }
}
