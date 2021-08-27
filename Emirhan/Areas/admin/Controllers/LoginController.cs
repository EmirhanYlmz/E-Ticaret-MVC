using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Emirhan.Models;

namespace Emirhan.Areas.admin.Controllers
{
    public class LoginController : Controller
    {
        // GET: admin/Login
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Index2()
        {
            return Content(Sifrele.MD5Olustur("1234"));
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Alogin(kullanicilar kullanicilarForm, string ReturnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View("Index", kullanicilarForm);
            }
            string sifre1 = Sifrele.MD5Olustur(kullanicilarForm.sifre);            
            using (eticaretEntities db = new eticaretEntities())
            {
                var kullanicivarmi = db.kullanicilar.FirstOrDefault(
                    x => x.kad == kullanicilarForm.kad && x.sifre == sifre1);
                if (kullanicivarmi != null)
                {
                    FormsAuthentication.SetAuthCookie(kullanicivarmi.kad, kullanicilarForm.benihatirla);
                    if(!string.IsNullOrEmpty(ReturnUrl))
                    {
                        return Redirect(ReturnUrl);
                    }
                    else 
                    {
                        return RedirectToAction("/Index", "Elektronik");
                    }

                }
                ViewBag.hata = "Kullanıcı adı veya şifre hatalı!";
                return View("Index");
            }
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("Index");
        }
    }
}