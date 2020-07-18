using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookStoreOnline.Areas.Admin.Controllers
{
    public class GioiThieuController : BaseController
    {
        // GET: Admin/GioiThieu
        public ActionResult GioiThieu_ThongTin()
        {
            return View();
        }
    }
}