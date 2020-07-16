using BookStoreOnline.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model.EF;
using Model.DAO;
using BookStoreOnline.Common;

namespace BookStoreOnline.Controllers
{
    public class CustomerController : Controller
    {
        // GET: Customer
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login(CustomerLoginViewModel model)
        {
            if(ModelState.IsValid)
            {
                var dao = new UserDAO();
                var result = dao.Login(model.Username, model.Password);
                if(result)
                {
                    var user = dao.GetByUsername(model.Username);
                    var userSession = new UserLogin();
                    userSession.UserID = user.UserID;
                    userSession.UserName = user.UserName;
                    userSession.Role = user.Role.RoleName;
                    Session.Add(CommonConstants.USER_SESSION, userSession);
                    return RedirectToAction("Index", "Home"); 
                } else
                {
                    ModelState.AddModelError("", "Đăng nhập thất bại");
                }
            }
            return View("Index");
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(CustomerRegisterViewModel model)
        {
            return View();
        }
    }
}