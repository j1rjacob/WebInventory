using System.IO;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace SendMailwithAttachment.Controllers
{
    public class SendMailerController : Controller
    {
        // GET: SendMailer
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]

        public ActionResult Index(SendMailwithAttachment.Models.MailModel objModelMail, HttpPostedFileBase fileUploader)

        {

            if (ModelState.IsValid)

            {

                string from = "junar.etmt@kitloongholdings.com"; //example:- sourabh9303@gmail.com

                using (MailMessage mail = new MailMessage(from, objModelMail.To))

                {

                    mail.Subject = objModelMail.Subject;

                    mail.Body = objModelMail.Body;

                    if (fileUploader != null)

                    {

                        string fileName = Path.GetFileName(fileUploader.FileName);

                        mail.Attachments.Add(new Attachment(fileUploader.InputStream, fileName));

                    }

                    mail.IsBodyHtml = false;

                    SmtpClient smtp = new SmtpClient();

                    smtp.Host = "mail.kitloongholdings.com";

                    smtp.EnableSsl = true;

                    NetworkCredential networkCredential = new NetworkCredential(from, "junar1000");

                    smtp.UseDefaultCredentials = true;

                    smtp.Credentials = networkCredential;

                    smtp.Port = 25;

                    smtp.Send(mail);

                    ViewBag.Message = "Sent";

                    return View("Index", objModelMail);
                }
            }
            else
            {
                return View();
            }
        }
    }
}