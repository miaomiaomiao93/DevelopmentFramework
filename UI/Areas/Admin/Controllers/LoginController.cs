using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Common;

namespace UI.Areas.Admin.Controllers
{
    public class LoginController : Controller
    {
        //
        // GET: /Admin/Login/

        public ActionResult Index()
        {
            return View();
        }

        public JsonResult getImgUrl()
        {
            var str = CreateQRCode.GetCode("https://www.baidu.com", "BYTE", "M", 8, 4);
            return Json(str, JsonRequestBehavior.AllowGet);
        }
    }
}
