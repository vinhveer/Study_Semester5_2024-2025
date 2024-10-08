using System;
using System.Dynamic;
using System.Net.Mail;
using System.Web.Mvc;
using BaiTap5_64132989.Models;

namespace BaiTap5_64132989.Controllers
{
    public class GuiEmailController : Controller
    {
        // GET: GuiEmail_64132989
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(MailInfo model)
        {
            try
            {
                // Gửi email
                SendEmail(model);

                // Sử dụng TempData để lưu thông báo giữa các yêu cầu
                TempData["SuccessMessage"] = "Email đã được gửi thành công!";
            }
            catch (Exception ex)
            {
                // Xử lý lỗi và gửi thông báo
                TempData["ErrorMessage"] = $"Có lỗi xảy ra: {ex.Message}";
            }

            return RedirectToAction("Index");
        }

        private void SendEmail(MailInfo model)
        {
            using (var mail = new MailMessage())
            {
                mail.From = new MailAddress(model.From);
                mail.To.Add(model.To);
                mail.Subject = model.Subject;
                mail.Body = model.Body;
                mail.IsBodyHtml = true;

                using (var smtp = new SmtpClient("smtp.gmail.com", 587))
                {
                    smtp.Credentials = new System.Net.NetworkCredential(model.From, model.Password);
                    smtp.EnableSsl = true;
                    smtp.Send(mail);
                }
            }
        }
    }
}
