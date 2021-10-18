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

        public FileResult DownloadResumeDocx()
        {
            //Name the File
            string fileName = "ogundero_micheal_ayodeji.docx";

            //Build the File Path.
            string path = Server.MapPath("~/documents/ogundero_micheal_ayodeji.docx");

            //Read the File data into Byte Array.
            byte[] bytes = System.IO.File.ReadAllBytes(path);

            //Send the File to Download.
            return File(bytes, "application/octet-stream", fileName);
        }

        public FileResult DownloadResumePdf()
        {
            //Name the File
            string fileName = "ogundero_micheal_ayodeji.pdf";

            //Build the File Path.
            string path = Server.MapPath("~/documents/ogundero_micheal_ayodeji_CV.pdf");

            //Read the File data into Byte Array.
            byte[] bytes = System.IO.File.ReadAllBytes(path);

            //Send the File to Download.
            return File(bytes, "application/octet-stream", fileName);
        }
    }
}
