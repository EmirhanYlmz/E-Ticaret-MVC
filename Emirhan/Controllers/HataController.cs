using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Emirhan.Controllers
{
    public class HataController : Controller
    {
        // GET: Hata
        public ActionResult NotFound()
        {
            return View();
        }
    }
}