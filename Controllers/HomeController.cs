using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using MichealOgundero.Models;

namespace MichealOgundero.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View ();
        }

        [HttpPost]
        public async Task<ActionResult> SendEmail(SendEmailModel model)
        {
            try
            {
                var data = await MailSender.SendEmail(model.Name, model.Message, model.Subject, model.Email);
                return Json(new { StatusCode = "00", Message = "success", Data = data }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { StatusCode = "01", Message = "fail", Data = "" }, JsonRequestBehavior.AllowGet);
            }

        }
    }
}
