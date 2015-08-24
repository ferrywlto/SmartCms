using System;

using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ICSharpCode.SharpZipLib.Core;
using Syncfusion.JavaScript;
using Umbraco.Web.WebApi;
using Umbraco.Web.Mvc;
using Umbraco.Web.PropertyEditors;

namespace My.Controllers
{
    public class TreeViewModel
    {
        public string Id;
        public string Pid="";
        public string Name;
        public bool HasChild;
        public bool Expanded;
    }



    [PluginController("My")]
    public class TreeAndTileController : UmbracoApiController
    {
        //[Authorize]
        public IEnumerable<TreeViewModel> GetPathList()
        {
            var localData = new List<TreeViewModel>();
            localData.Add(new TreeViewModel { Id = "1", Name = "Discover Music", HasChild = true, Expanded = true });
            localData.Add(new TreeViewModel { Id = "2", Pid = "1", Name = "Hot Singles" });
            localData.Add(new TreeViewModel { Id = "3", Pid = "1", Name = "Rising Artists" });
            localData.Add(new TreeViewModel { Id = "4", Pid = "1", Name = "Live Music" });
            localData.Add(new TreeViewModel { Id = "6", Pid = "1", Name = "Best of 2013 So Far" });
            localData.Add(new TreeViewModel { Id = "7", Name = "Sales and Events", HasChild = true, Expanded = true });
            localData.Add(new TreeViewModel { Id = "8", Pid = "7", Name = "100 Albums - $5 Each" });
            localData.Add(new TreeViewModel { Id = "9", Pid = "7", Name = "Hip-Hop and R&B Sale" });
            localData.Add(new TreeViewModel { Id = "10", Pid = "7", Name = "CD Deals" });
            localData.Add(new TreeViewModel { Id = "11", Name = "Categories", HasChild = true });
            localData.Add(new TreeViewModel { Id = "12", Pid = "11", Name = "Songs" });
            localData.Add(new TreeViewModel { Id = "13", Pid = "11", Name = "Bestselling Albums" });
            localData.Add(new TreeViewModel { Id = "14", Pid = "11", Name = "New Releases" });
            localData.Add(new TreeViewModel { Id = "15", Pid = "11", Name = "Bestselling Songs" });
            localData.Add(new TreeViewModel { Id = "16", Name = "MP3 Albums", HasChild = true });
            localData.Add(new TreeViewModel { Id = "17", Pid = "16", Name = "Rock" });
            localData.Add(new TreeViewModel { Id = "18", Pid = "16", Name = "Gospel" });
            localData.Add(new TreeViewModel { Id = "19", Pid = "16", Name = "Latin Music" });
            localData.Add(new TreeViewModel { Id = "20", Pid = "16", Name = "Jazz" });
            localData.Add(new TreeViewModel { Id = "21", Name = "More in Music", HasChild = true });
            localData.Add(new TreeViewModel { Id = "22", Pid = "21", Name = "Music Trade-In" });
            localData.Add(new TreeViewModel { Id = "23", Pid = "21", Name = "Redeem a Gift Card" });
            localData.Add(new TreeViewModel { Id = "24", Pid = "21", Name = "Band T-Shirts" });
            localData.Add(new TreeViewModel { Id = "25", Pid = "21", Name = "Mobile MVC" });

            return localData;
            //return Json(FileExplorerOperations.Read(args.Path, args.ExtensionsAllow));
        }
        //protected override JsonResult Json(object data, string contentType, System.Text.Encoding contentEncoding, JsonRequestBehavior behavior)
        //{
        //    return new JsonResult()
        //    {
        //        Data = data,
        //        ContentType = contentType,
        //        ContentEncoding = contentEncoding,
        //        JsonRequestBehavior = behavior,
        //        MaxJsonLength = Int32.MaxValue
        //    };
        //}

        //public ActionResult GetDetail()
        //{
            
        //}
        public IEnumerable<TreeViewModel> GetTopLevelTreeStr()
        {
            var mediaRoot = VirtualPathUtility.ToAbsolute("~/media");
            var physicalRoot = HttpContext.Current.Server.MapPath(mediaRoot);
            var rootInfo = new DirectoryInfo(physicalRoot);

            List<TreeViewModel> folderList = new List<TreeViewModel>();

            var rootChild = rootInfo.GetDirectories("*", SearchOption.AllDirectories);
            foreach (var child in rootChild)
            {
                //uint childId;
                //if (uint.TryParse(child.Name, out childId))
                //{
                var model = new TreeViewModel
                {
                    Id = child.FullName,
                    Name = child.Name,
                    HasChild = (child.GetDirectories("*", SearchOption.TopDirectoryOnly).Length > 0)
                };
                if (child.Parent != null && child.Parent.FullName != rootInfo.FullName)
                {
                    model.Pid = child.Parent.FullName;
                }
                folderList.Add(model);
            }
            return folderList;

        //var model = new TreeViewModel
                //{
                //    Id = (uint)child.Name, Pid = uint.Parse(child.Parent.Name), Name = child.Name,
                //    HasChild = (child.GetDirectories("*", SearchOption.TopDirectoryOnly).Length > 0)
                //};
                //folderList.Add(model);
            
            //return folderList;
        }
        //public IEnumerable<TreeViewModel> GetTopLevelTree()
        //{
        //    var mediaRoot = VirtualPathUtility.ToAbsolute("~/media/");
        //    var physicalRoot = HttpContext.Current.Server.MapPath(mediaRoot);
        //    var rootInfo = new DirectoryInfo(physicalRoot);

        //    List<TreeViewModel> folderList = new List<TreeViewModel>();

        //    var rootChild = rootInfo.GetDirectories("*", SearchOption.AllDirectories);
        //    foreach (var child in rootChild)
        //    {
        //        uint childId;
        //        if (uint.TryParse(child.Name, out childId))
        //        {
        //            var model = new TreeViewModel
        //            {
        //                Id = uint.Parse(child.Name),
        //                Name = child.Name,
        //                HasChild = (child.GetDirectories("*", SearchOption.TopDirectoryOnly).Length > 0)
        //            };
        //            if (child.Parent != null)
        //            {
        //                uint parentId;
        //                if (uint.TryParse(child.Parent.Name, out parentId))
        //                    model.Pid = parentId;
        //            }
        //            folderList.Add(model);
        //        }
        //        //var model = new TreeViewModel
        //        //{
        //        //    Id = (uint)child.Name, Pid = uint.Parse(child.Parent.Name), Name = child.Name,
        //        //    HasChild = (child.GetDirectories("*", SearchOption.TopDirectoryOnly).Length > 0)
        //        //};
        //        //folderList.Add(model);
        //    }
        //    return folderList;
        //}
        //public IEnumerable<TreeViewModel> GetAllLevelTree()
        //{
        //    var mediaRoot = VirtualPathUtility.ToAbsolute("~/media/");
        //    var physicalRoot = HttpContext.Current.Server.MapPath(mediaRoot);
        //    var rootInfo = new DirectoryInfo(physicalRoot);

        //    List<TreeViewModel> folderList = new List<TreeViewModel>();
        //    //scan2(rootInfo, folderList);
        //    //var rootChild = rootInfo.GetDirectories("*", SearchOption.AllDirectories);
        //    //foreach (var child in rootChild)
        //    //{
        //        //var model = new TreeViewModel
        //        //{
        //        //    Id = child.Name,
        //        //    Pid = "",
        //        //    Name = child.Name,
        //        //    HasChild = (child.GetDirectories("*", SearchOption.AllDirectories).Length > 0)
        //        //};
        //        //folderList.Add(model);
        //    //}
        //    return folderList;
        //}
        //public void scan2(DirectoryInfo parent, List<TreeViewModel> list)
        //{
        //    var directories = parent.GetDirectories("*", SearchOption.AllDirectories);
        //    foreach (var child in directories)
        //    {
        //        var model = new TreeViewModel
        //        {
        //            Id = child.Name,
        //            Pid = child.Parent == null ? string.Empty : child.Parent.Name,
        //            Name = child.Name,
        //            HasChild = (child.GetDirectories("*", SearchOption.TopDirectoryOnly).Length > 0)
        //        };
        //        list.Add(model);
        //        //scan(child, list);
        //    }

        //}
        //public void scan(DirectoryInfo parent, List<TreeViewModel> list)
        //{
        //    var directories = parent.GetDirectories("*", SearchOption.TopDirectoryOnly);
        //    foreach (var child in directories)
        //    {
        //        var model = new TreeViewModel
        //        {
        //            Id = child.Name,
        //            Pid = parent.Name,
        //            Name = child.Name,
        //            HasChild = (child.GetDirectories("*", SearchOption.TopDirectoryOnly).Length > 0)
        //        };
        //        list.Add(model);
        //        scan(child, list);
        //    }
           
        //}
        //[Authorize]
        //public static CustomFileDetails GetDetails(string path, string name, string type)
        //{
        //    //FileDetails fileDetail = FileExplorerOperations.GetDetails(path, name, type);
        //    CustomFileDetails fileDetail = new CustomFileDetails(FileExplorerOperations.GetDetails(path, name, type));
        //    path = System.Web.VirtualPathUtility.ToAbsolute(path);
        //    path = Path.Combine(path, name);
        //    try
        //    {
        //        var physicalPath = System.Web.HttpContext.Current.Server.MapPath(path);
        //        var dirInfo = new DirectoryInfo(physicalPath);
        //        var dirInfo.GetDirectories("*", SearchOption.AllDirectories);
        //        FileInfo info = new FileInfo(physicalPath);
        //        fileDetail.IsReadOnly = info.IsReadOnly.ToString();
        //        fileDetail.Stamp = new Random().Next().ToString();
        //        //CustomFileDetails custFileDetails = new CustomFileDetails();
        //        //custFileDetails.FullName = info.Name;
        //        //custFileDetails.Format = (info.Extension == "") ? "File Folder" : info.Extension; // Has period
        //        //custFileDetails.Location = info.FullName;             
        //        //fileDetail.Size = type == "File" ? info.Length : new DirectoryInfo(physicalPath).EnumerateFiles("*", SearchOption.AllDirectories).Sum(file => file.Length);
        //        //custFileDetails.Created = info.CreationTime.ToString();
        //        //custFileDetails.Modified = info.LastWriteTime.ToString();
        //        //custFileDetails.IsReadOnly = info.IsReadOnly.ToString();

        //        return fileDetail;
        //    }
        //    catch (Exception ex) { throw ex; }
        //}
    }
}