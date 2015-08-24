using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Umbraco.Web.Mvc;

namespace My.Controllers
{
    [PluginController("My")]
    public class TestPartialController : SurfaceController
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult IndexWithParam(string param)
        {
            return View(param);
        }
    }
}