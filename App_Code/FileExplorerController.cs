using System;
using System.Web.Mvc;
using Syncfusion.JavaScript;
using Umbraco.Web.WebApi;
using Umbraco.Web.Mvc;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace My.Controllers
{
    public class PathModel
    {
        public string skill { get; set; }
        public string category { get; set; }
        public uint value { get; set; }
    }
    public class CustomFileExplorerEntry : FileExplorerEntry
    {
        public string stamp { get; set; }
        public CustomFileExplorerEntry(FileExplorerEntry fee)
        {
            this.dateModified = fee.dateModified;
            this.hasChild = fee.hasChild;
            this.isFile = fee.isFile;
            this.name = fee.name;
            this.size = fee.size;
            this.type = fee.type;
        }
    }
    public class CustomFileDetails : FileDetails
    {
        public CustomFileDetails(FileDetails fd) {
            this.Name = fd.Name;
            this.FullName = fd.FullName;
            this.Extension = fd.Extension;
            this.Format = fd.Format;
            this.LastAccessTime = fd.LastAccessTime;
            this.LastWriteTime = fd.LastWriteTime;
            this.CreationTime = fd.CreationTime;
            this.Length = fd.Length;
        }
        public string IsReadOnly { get; set; }
        public string Stamp { get; set; }
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
                    //return Json(FileExplorerOperations.Read(args.Path, args.ExtensionsAllow));
                    return Json(Read(args.Path, args.ExtensionsAllow));
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
                    return Json(GetDetails(args.Path, args.Name, args.Type));
                    //return Json(FileExplorerOperations.GetDetails(args.Path, args.Name, args.Type));
                case "Download":
                    FileExplorerOperations.Download(args.Path);
                    break;
                case "Upload":
                    foreach(var uf in args.FileUpload)
                    { 
                        var f = Services.MediaService.CreateMedia(args.Path, 1001, global::Umbraco.Core.Constants.Conventions.MediaTypes.File);
                        f.SetValue(global::Umbraco.Core.Constants.Conventions.Media.File, (Stream)uf.InputStream); 
                        Services.MediaService.Save(f);
                    }
                    FileExplorerOperations.Upload(args.FileUpload, args.Path);
                    break;
                case "Safety":
                    return Json(GetDetails(args.Path, args.Name, args.Type));
            }
            return Json("");
        }

        //Override and customize the Entry retrieve logic to enforce project/folder based permission
        public static IEnumerable<CustomFileExplorerEntry> Read(string path, string extensionAllow)
        {
            var readResult = FileExplorerOperations.Read(path, extensionAllow);
            List<CustomFileExplorerEntry> custResult = new List<CustomFileExplorerEntry>();
            var r = new Random();
            foreach(FileExplorerEntry fe in readResult)
            {
                CustomFileExplorerEntry cfe = new CustomFileExplorerEntry(fe);
                cfe.stamp = r.Next().ToString();
                custResult.Add(cfe);
            }
            return custResult;
        }

        public static CustomFileDetails GetDetails(string path, string name, string type)
        {
            //FileDetails fileDetail = FileExplorerOperations.GetDetails(path, name, type);
            CustomFileDetails fileDetail = new CustomFileDetails(FileExplorerOperations.GetDetails(path, name, type));
            path = System.Web.VirtualPathUtility.ToAbsolute(path);
            path = Path.Combine(path, name);
            try
            {
                var physicalPath = System.Web.HttpContext.Current.Server.MapPath(path);
                FileInfo info = new FileInfo(physicalPath);
                fileDetail.IsReadOnly = info.IsReadOnly.ToString();
                fileDetail.Stamp = new Random().Next().ToString();
                //CustomFileDetails custFileDetails = new CustomFileDetails();
                //custFileDetails.FullName = info.Name;
                //custFileDetails.Format = (info.Extension == "") ? "File Folder" : info.Extension; // Has period
                //custFileDetails.Location = info.FullName;             
                //fileDetail.Size = type == "File" ? info.Length : new DirectoryInfo(physicalPath).EnumerateFiles("*", SearchOption.AllDirectories).Sum(file => file.Length);
                //custFileDetails.Created = info.CreationTime.ToString();
                //custFileDetails.Modified = info.LastWriteTime.ToString();
                //custFileDetails.IsReadOnly = info.IsReadOnly.ToString();

                return fileDetail;
            }
            catch (Exception ex) { throw ex; }
        }
    }
}
