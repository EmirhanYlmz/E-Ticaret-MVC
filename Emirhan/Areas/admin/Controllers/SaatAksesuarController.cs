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
    public class SaatAksesuarController : Controller
    {
        // GET: admin/SaatAksesuar
        public ActionResult Index()
        {
            using (eticaretEntities db = new eticaretEntities())
            {
                var model = db.saataksesuar.ToList();

                return View(model);
            }
        }
        public ActionResult Yeni()
        {
            var model = new saataksesuar();

            return View("SaatAksesuarForm", model);
        }
        public ActionResult Guncelle(int sid)
        {
            using (eticaretEntities db = new eticaretEntities())
            {
                var model = db.saataksesuar.Find(sid);
                if (model == null)
                {
                    return HttpNotFound();
                }
                return View("SaatAksesuarForm", model);

            }

        }
        public ActionResult Kaydet(saataksesuar gelenYazi)
        {
            if (!ModelState.IsValid)
            {
                return View("SaatAksesuarForm", gelenYazi);
            }
            using (eticaretEntities db = new eticaretEntities())
            {

                if (gelenYazi.sid == 0)//kayıt
                {
                    if (gelenYazi.fotoFile == null)
                    {
                        ViewBag.HataFoto = "lütfen resim yükleyin";
                        return View("SaatAksesuarForm", gelenYazi);
                    }

                    string resimAdi = Seo.DosyaAdiDuzenle(gelenYazi.fotoFile.FileName);
                    gelenYazi.resim = resimAdi;
                    db.saataksesuar.Add(gelenYazi);
                    gelenYazi.fotoFile.SaveAs(Path.Combine(Server.MapPath("~/Content/img"), Path.GetFileName(gelenYazi.resim)));
                    TempData["yazi"] = "Ürün Başarıyla Eklendi";
                }
                else//güncelleme
                {
                    var guncellenecekVeri = db.saataksesuar.Find(gelenYazi.sid);
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
                var silinecekUrun = db.saataksesuar.Find(sid);
                if (silinecekUrun == null)
                {
                    return HttpNotFound();
                }
                db.saataksesuar.Remove(silinecekUrun);
                db.SaveChanges();
                TempData["yazi"] = "Ürün başarılı bir şekilde silindi";
                return RedirectToAction("Index");
            }
        }
    }
}