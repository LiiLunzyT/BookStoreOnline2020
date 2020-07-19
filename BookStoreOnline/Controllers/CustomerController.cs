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
    [RoutePrefix("tai-khoan")]
    public class CustomerController : Controller
    {
        [Route]
        public ActionResult Index()
        {
            return View();
        }

        [Route("dang-nhap")]
        public ActionResult Login()
        {
            return View();
        }

        public ActionResult Logout()
        {
            Session["CUSTOMER_SESSION"] = null;
            return RedirectToAction("Index", "Home");
        }

        [Route("dang-nhap")]
        [HttpPost]
        public ActionResult SendLogin(CustomerLoginViewModel model)
        {
            if(ModelState.IsValid)
            {
                var dao = new UserDAO();
                var result = dao.Login(model.Username, model.Password);
                if(result)
                {
                    var user = dao.GetByUsername(model.Username);

                    var customerSession = new CustomerLogin();
                    customerSession.UserID = user.UserID;
                    customerSession.UserName = user.UserName;
                    Session.Add(CommonConstants.CUSTOMER_SESSION, customerSession);
                    return RedirectToAction("Index", "Home"); 
                } else
                {
                    ModelState.AddModelError("", "Đăng nhập thất bại");
                }
            }
            return View("Login");
        }

        [Route("dang-ky")]
        public ActionResult Register()
        {
            return View();
        }

        [Route("dang-ky")]
        [HttpPost]
        public ActionResult SendRegister(CustomerRegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var dao = new UserDAO();
                var cdao = new CustomerDAO();
                var result = dao.GetByUsername(model.Username);
                if (result != null)
                {
                    ModelState.AddModelError("", "Tên đăng nhập đã tồn tại");
                } else
                {
                    User newUser = new User();
                    Customer newCustomer = new Customer();

                    newUser.UserID = dao.getNewID();
                    newUser.UserName = model.Username;
                    newUser.Password = model.Password;
                    newUser.CreatedByDate = DateTime.Now;
                    newUser.RoleID = "RL-003";
                    dao.Insert(newUser);

                    newCustomer.CustomerID = cdao.getNewID();
                    newCustomer.CustomerName = model.CustomerName;
                    newCustomer.CustomerAddress = model.Address;
                    newCustomer.Birth = model.Birth;
                    newCustomer.PhoneNumber = model.PhoneNumber;
                    newCustomer.Email = model.Email;
                    newCustomer.Gender = model.Gender;
                    newCustomer.UserID = newUser.UserID;
                    cdao.Insert(newCustomer);

                    var customerSession = new CustomerLogin();
                    customerSession.UserID = newUser.UserID;
                    customerSession.UserName = newUser.UserName;
                    Session.Add(CommonConstants.CUSTOMER_SESSION, customerSession);
                    return RedirectToAction("Index", "Home");
                }
            }
            return View("Register");
        }
        
    }
}