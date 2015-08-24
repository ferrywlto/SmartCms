using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using Umbraco.Web.Mvc;

namespace My.Controllers
{
    public class TileViewModel
    {
        public string temp;
        public string name;
        public string path;
    }

    public class Sender
    {
        public string path;
    }

    [PluginController("My")]
    public class TileController : SurfaceController
    {
        public PartialViewResult PartialTiles(Sender args)
        {
            List<TileViewModel> list = new List<TileViewModel>();
            var model = new TileViewModel();
            model.temp = "hi";
            list.Add(model);
            return PartialView(list);
        }
    }
}