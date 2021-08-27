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
    public class ElektronikController : Controller
    {
        [Authorize]
        // GET: admin/Haberler
        public ActionResult Index()
        {
            using (eticaretEntities db = new eticaretEntities())
            {
                var model = db.elektronik.ToList();

                return View(model);
            }
        }
            public ActionResult Yeni()
            {
                var model = new elektronik();

                return View("ElektronikForm", model);
            }
        public ActionResult Guncelle(int eid)
            {
            using (eticaretEntities db = new eticaretEntities())
            {
                var model = db.elektronik.Find(eid);
                if (model == null)
                {
                    return HttpNotFound();
                }
                return View("ElektronikForm", model);

            }

        }
        public ActionResult Kaydet(elektronik gelenYazi)
        {
            if (!ModelState.IsValid)
            {
                return View("ElektronikForm", gelenYazi);
            }
            using (eticaretEntities db = new eticaretEntities())
            {

                if (gelenYazi.eid == 0)//kayıt
                {
                    if (gelenYazi.fotoFile == null)
                    {
                        ViewBag.HataFoto = "lütfen resim yükleyin";
                        return View("ElektronikForm", gelenYazi);
                    }

                    string resimAdi = Seo.DosyaAdiDuzenle(gelenYazi.fotoFile.FileName);
                    gelenYazi.resim = resimAdi;
                    db.elektronik.Add(gelenYazi);
                    gelenYazi.fotoFile.SaveAs(Path.Combine(Server.MapPath("~/Content/img"), Path.GetFileName(gelenYazi.resim)));
                    TempData["yazi"] = "Ürün Başarıyla Eklendi";
                }
                else//güncelleme
                {
                    var guncellenecekVeri = db.elektronik.Find(gelenYazi.eid);
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
            public ActionResult Sil(int eid)
            {

                using (eticaretEntities db = new eticaretEntities())
                {
                    var silinecekUrun = db.elektronik.Find(eid);
                    if (silinecekUrun == null)
                    {
                        return HttpNotFound();
                    }
                    db.elektronik.Remove(silinecekUrun);
                    db.SaveChanges();
                    TempData["yazi"] = "Ürün başarılı bir şekilde silindi";
                    return RedirectToAction("Index");
                }

            }

        }
    }
    
