using Emirhan.App_Code;
using Emirhan.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Emirhan.Areas.admin.Controllers
{
    public class SporOutdoorController : Controller
    {
        // GET: admin/SporOutdoor
        public ActionResult Index()
        {
            using (eticaretEntities db = new eticaretEntities())
            {
                var model = db.sporoutdoor.ToList();

                return View(model);
            }
        }
        public ActionResult Yeni()
        {
            var model = new sporoutdoor();

            return View("SporOutdoorForm", model);
        }
        public ActionResult Guncelle(int sid)
        {
            using (eticaretEntities db = new eticaretEntities())
            {
                var model = db.sporoutdoor.Find(sid);
                if (model == null)
                {
                    return HttpNotFound();
                }
                return View("SporOutdoorForm", model);

            }

        }
        public ActionResult Kaydet(sporoutdoor gelenYazi)
        {
            if (!ModelState.IsValid)
            {
                return View("SporOutdoorForm", gelenYazi);
            }
            using (eticaretEntities db = new eticaretEntities())
            {

                if (gelenYazi.sid == 0)//kayıt
                {
                    if (gelenYazi.fotoFile == null)
                    {
                        ViewBag.HataFoto = "lütfen resim yükleyin";
                        return View("SporOutdoorForm", gelenYazi);
                    }

                    string resimAdi = Seo.DosyaAdiDuzenle(gelenYazi.fotoFile.FileName);
                    gelenYazi.resim = resimAdi;
                    db.sporoutdoor.Add(gelenYazi);
                    gelenYazi.fotoFile.SaveAs(Path.Combine(Server.MapPath("~/Content/img"), Path.GetFileName(gelenYazi.resim)));
                    TempData["yazi"] = "Ürün Başarıyla Eklendi";
                }
                else//güncelleme
                {
                    var guncellenecekVeri = db.sporoutdoor.Find(gelenYazi.sid);
                    if (gelenYazi.fotoFile != null)
                    {
                        string resimAdi = Seo.DosyaAdiDuzenle(gelenYazi.fotoFile.FileName);
                        gelenYazi.resim = resimAdi;
                        gelenYazi.fotoFile.SaveAs(Path.Combine(Server.MapPath("~/Content/img"), Path.GetFileName(gelenYazi.resim)));
                    }
                    db.Entry(guncellenecekVeri).CurrentValues.SetValues(gelenYazi);
                    TempData["yazi"] = "Ürün Başarıyla Eklendi";
                }

                db.SaveChanges();
                return RedirectToAction("Index");

            }
        }
        public ActionResult Sil(int sid)
        {

            using (eticaretEntities db = new eticaretEntities())
            {
                var silinecekUrun = db.sporoutdoor.Find(sid);
                if (silinecekUrun == null)
                {
                    return HttpNotFound();
                }
                db.sporoutdoor.Remove(silinecekUrun);
                db.SaveChanges();
                TempData["yazi"] = "Ürün başarılı bir şekilde silindi";
                return RedirectToAction("Index");
            }
        }
    }
}