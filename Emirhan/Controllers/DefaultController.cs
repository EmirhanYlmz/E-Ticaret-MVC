using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Emirhan.Models;

namespace Emirhan.Controllers
{
    public class DefaultController : Controller
    {
        // GET: Default
        public ActionResult Index()
        {
            using (eticaretEntities db = new eticaretEntities())
            {

                Viewmodel viewmodel = new Viewmodel();
                viewmodel.Elektroniklist = db.elektronik.ToList();
                viewmodel.Sporoutdoorlist = db.sporoutdoor.ToList();
                viewmodel.Saataksesuarslist = db.saataksesuar.ToList();
                viewmodel.Giyimlist = db.giyim.ToList();

                return View(viewmodel);
            }
        }

        [Route("Bilgisayar")]
        public ActionResult Bilgisayar()
        {
            using (eticaretEntities db = new eticaretEntities())
            {
                var model = db.elektronik.Where(x => x.kategori == "Bilgisayar").OrderByDescending(x => x.sira).ToList();
                return View(model);
            }
        }
        [Route("Laptop")]
        public ActionResult Laptop()
        {
            using (eticaretEntities db = new eticaretEntities())
            {
                var model = db.elektronik.Where(x => x.kategori == "Laptop").OrderByDescending(x => x.sira).ToList();
                return View(model);
            }
        }
        [Route("Telefon")]
        public ActionResult Telefon()
        {
            using (eticaretEntities db = new eticaretEntities())
            {
                var model = db.elektronik.Where(x => x.kategori == "Telefon").OrderByDescending(x => x.sira).ToList();
                return View(model);
            }
        }
        [Route("Gömlek")]
        public ActionResult Gömlek()
        {
            using (eticaretEntities db = new eticaretEntities())
            {
                var model = db.giyim.Where(x => x.kategori == "Gömlek").OrderByDescending(x => x.sira).ToList();
                return View(model);
            }
        }
        [Route("Tshirt")]
        public ActionResult Tshirt()
        {
            using (eticaretEntities db = new eticaretEntities())
            {
                var model = db.giyim.Where(x => x.kategori == "Tshirt").OrderByDescending(x => x.sira).ToList();
                return View(model);
            }
        }
        [Route("Pantalon")]
        public ActionResult Pantalon()
        {
            using (eticaretEntities db = new eticaretEntities())
            {
                var model = db.giyim.Where(x => x.kategori == "Pantalon").OrderByDescending(x => x.sira).ToList();
                return View(model);
            }
        }
        [Route("TakıKadın")]
        public ActionResult TakıKadın()
        {
            using (eticaretEntities db = new eticaretEntities())
            {
                var model = db.saataksesuar.Where(x => x.kategori == "Kadın").OrderByDescending(x => x.sira).ToList();
                return View(model);
            }
        }
        [Route("Takı")]
        public ActionResult Takı()
        {
            using (eticaretEntities db = new eticaretEntities())
            {
                var model = db.saataksesuar.Where(x => x.kategori == "TakıMücevher").OrderByDescending(x => x.sira).ToList();
                return View(model);
            }
        }
        [Route("TakıErkek")]
        public ActionResult TakıErkek()
        {
            using (eticaretEntities db = new eticaretEntities())
            {
                var model = db.saataksesuar.Where(x => x.kategori == "Erkek").OrderByDescending(x => x.sira).ToList();
                return View(model);
            }
        }
        [Route("Sporgiyim")]
        public ActionResult Sporgiyim()
        {
            using (eticaretEntities db = new eticaretEntities())
            {
                var model = db.sporoutdoor.Where(x => x.kategori == "SporGiyim").OrderByDescending(x => x.sira).ToList();
                return View(model);
            }
        }
        [Route("Sporbesin")]
        public ActionResult Sporbesin()
        {
            using (eticaretEntities db = new eticaretEntities())
            {
                var model = db.sporoutdoor.Where(x => x.kategori == "SporBesin").OrderByDescending(x => x.sira).ToList();
                return View(model);
            }
        }
        [Route("Spormalzeme")]
        public ActionResult Spormalzeme()
        {
            using (eticaretEntities db = new eticaretEntities())
            {
                var model = db.sporoutdoor.Where(x => x.kategori == "SporMalzeme").OrderByDescending(x => x.sira).ToList();
                return View(model);
            }
        }
        public ActionResult Sepetsayfasi()
        {
                return View();
            }
        }
    }
