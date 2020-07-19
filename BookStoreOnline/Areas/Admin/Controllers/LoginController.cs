using BookStoreOnline.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model.EF;
using Model.DAO;
using BookStoreOnline.Common;

namespace BookStoreOnline.Areas.Admin.Controllers
{
    public class LoginController : Controller
    {
        // GET: Admin/Login
        public ActionResult Index(String returnUrl)
        {
            if (string.IsNullOrEmpty(returnUrl) && Request.UrlReferrer != null)
                returnUrl = Server.UrlEncode(Request.UrlReferrer.PathAndQuery);

            if (Url.IsLocalUrl(returnUrl) && !string.IsNullOrEmpty(returnUrl))
            {
                ViewBag.ReturnURL = returnUrl;
            }

            return View();
        }

        public ActionResult Logout()
        {
            Session["USER_SESSION"] = null;
            return RedirectToAction("Index", "Login");
        }

        [HttpPost]
        public ActionResult Login(LoginModel model, String returnUrl)
        {
            string decodedUrl = "";
            if (!string.IsNullOrEmpty(returnUrl))
                decodedUrl = Server.UrlDecode(returnUrl);

            if (ModelState.IsValid)
            {
                var dao = new UserDAO();
                var result = dao.Login(model.Username, model.Password, true);
                if (result)
                {
                    var user = dao.GetByUsername(model.Username);
                    var userSession = new UserLogin();
                    userSession.UserID = user.UserID;
                    userSession.UserName = user.UserName;
                    userSession.Role = user.Role.RoleName;
                    Session.Add(CommonConstants.USER_SESSION, userSession);

                    if (Url.IsLocalUrl(decodedUrl))
                    {
                        return Redirect(decodedUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Đăng nhập thất bại");
                }
            }
            return View("Index");
        }
    }}