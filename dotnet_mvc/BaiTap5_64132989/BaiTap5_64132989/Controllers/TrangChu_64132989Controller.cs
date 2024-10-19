using System;
using System.IO;
using System.Web;
using System.Web.Mvc;

namespace BaiTap5_64132989.Controllers
{
    public class TrangChu_64132989Controller : Controller
    {
        // GET: Home/Index
        public ActionResult Index()
        {
            return View();
        }

        // GET: Home/ChangeBanner
        public ActionResult ChangeBanner()
        {
            return View();
        }

        // POST: Home/ChangeBanner
        [HttpPost]
        public ActionResult ChangeBanner(HttpPostedFileBase banner)
        {
            try
            {
                // Kiểm tra xem có tệp được tải lên hay không
                if (banner != null && banner.ContentLength > 0)
                {
                    // Lấy tên tệp
                    string postedFileName = Path.GetFileName(banner.FileName);

                    // Đường dẫn lưu ảnh banner
                    var path = Server.MapPath("/Images/" + postedFileName);

                    // Lưu ảnh banner vào thư mục Images
                    banner.SaveAs(path);

                    // Lưu tên banner vào file banner.txt
                    string bannerSavePath = Server.MapPath("~/App_Data/banner.txt");
                    System.IO.File.WriteAllText(bannerSavePath, postedFileName);

                    ViewBag.Message = "Banner đã được thay đổi thành công!";
                }
                else
                {
                    ViewBag.Error = "Vui lòng chọn một tệp ảnh để làm banner.";
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Có lỗi xảy ra: " + ex.Message;
            }

            return View();
        }
    }
}