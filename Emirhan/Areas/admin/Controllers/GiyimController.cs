using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Emirhan.App_Code;
using Emirhan.Models;

namespace Emirhan.Areas.admin.Controllers
{
    public class GiyimController : Controller
    {
        // GET: admin/Giyim
        public ActionResult Index()
        {
            using (eticaretEntities db = new eticaretEntities())
            {
                var model = db.giyim.ToList();

                return View(model);
            }
        }
        public ActionResult Yeni()
        {
            var model = new giyim();

            return View("GiyimForm", model);
        }
        public ActionResult Guncelle(int gid)
        {
            using (eticaretEntities db = new eticaretEntities())
            {
                var model = db.giyim.Find(gid);
                if (model == null)
                {
                    return HttpNotFound();
                }
                return View("GiyimForm", model);

            }

        }
        public ActionResult Kaydet(giyim gelenYazi)
        {
            if (!ModelState.IsValid)
            {
                return View("GiyimForm", gelenYazi);
            }
            using (eticaretEntities db = new eticaretEntities())
            {

                if (gelenYazi.gid == 0)//kayıt
                {
                    if (gelenYazi.fotoFile == null)
                    {
                        ViewBag.HataFoto = "lütfen resim yükleyin";
                        return View("GiyimForm", gelenYazi);
                    }

                    string resimAdi = Seo.DosyaAdiDuzenle(gelenYazi.fotoFile.FileName);
                    gelenYazi.resim = resimAdi;
                    db.giyim.Add(gelenYazi);
                    gelenYazi.fotoFile.SaveAs(Path.Combine(Server.MapPath("~/Content/img"), Path.GetFileName(gelenYazi.resim)));
                    TempData["yazi"] = "Ürün Başarıyla Eklendi";
                }
                else//güncelleme
                {
                    var guncellenecekVeri = db.giyim.Find(gelenYazi.gid);
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
        public ActionResult Sil(int gid)
        {

            using (eticaretEntities db = new eticaretEntities())
            {
                var silinecekUrun = db.giyim.Find(gid);
                if (silinecekUrun == null)
                {
                    return HttpNotFound();
                }
                db.giyim.Remove(silinecekUrun);
                db.SaveChanges();
                TempData["yazi"] = "Ürün başarılı bir şekilde silindi";
                return RedirectToAction("Index");
            }

        }
    }
}