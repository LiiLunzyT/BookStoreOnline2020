using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookStoreOnline.Areas.Admin.Controllers
{
    public class ChartController : BaseController
    {
        BookStore db = new BookStore();
        // GET: Admin/Chart
        public ActionResult Index()
        {
            DateTime date = DateTime.Now.Date;

            // Tính số tiền và số sách bán trong ngày
            var listOrder = db.Orders.Where(o => o.Status == "Đã duyệt").Where(o => o.OrderByDate == date).ToList();
            int moneycount = 0;
            int sellcount = 0;
            foreach(var item in listOrder)
            {
                moneycount += item.Total;

                foreach (var detail in item.OrderDetails)
                {
                    sellcount += detail.Quantity;
                }
            }
            ViewData["moneycount"] = "" + moneycount;
            ViewData["sellcount"] = sellcount;

            // Tính số tài khoản tạo trong vòng 7 ngày
            var listUser = db.Users.Where(u => u.Role.RoleName.Equals("Khách hàng")).Where(c => date.CompareTo((DateTime)c.CreatedByDate) < 7).ToList();
            ViewData["cuscount"] = listUser.Count();

            // Tính đơn hàng chưa duyệt
            listOrder = db.Orders.Where(o => o.Status == "Chờ duyệt").ToList();
            ViewData["ordercount"] = listOrder.Count;

            return View();
        }
    }
}