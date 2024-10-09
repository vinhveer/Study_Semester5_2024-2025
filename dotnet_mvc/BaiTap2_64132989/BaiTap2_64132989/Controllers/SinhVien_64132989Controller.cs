using BaiTap2_64132989.Models;
using System.Web.Mvc;

namespace BaiTap2_64132989.Controllers
{
    public class SinhVien_64132989Controller : Controller
    {
        // Use Model Binding
        // GET: SinhVien_64132989/Index
        public ActionResult Index()
        {
            return View();
        }

        // POST: SinhVien_64132989/Index
        [HttpPost]
        public ActionResult Index(SinhVienModels sv)
        {
            if (ModelState.IsValid)
            {
                TempData["SinhVien"] = sv; // Lưu trữ model trong TempData để sử dụng sau
                return RedirectToAction("Result");
            }

            return View(sv);
        }

        // Use Request
        // GET: SinhVien_64132989/UseRequest
        public ActionResult UseRequest()
        {
            return View();
        }

        // POST: SinhVien_64132989/ProcessRequest
        [HttpPost]
        public ActionResult ProcessRequest()
        {
            var sv = new SinhVienModels
            {
                studentId = Request["studentId"],
                studentName = Request["studentName"],
                studentMark = Request["studentMark"]
            };

            TempData["SinhVien"] = sv;
            return RedirectToAction("Result");
        }

        // Use Arguments
        // GET: SinhVien_64132989/UseArguments
        public ActionResult UseArguments()
        {
            return View();
        }

        // POST: SinhVien_64132989/UseArguments
        [HttpPost]
        public ActionResult UseArguments(string studentId, string studentName, string studentMark)
        {
            var sv = new SinhVienModels
            {
                studentId = studentId,
                studentName = studentName,
                studentMark = studentMark
            };

            TempData["SinhVien"] = sv;
            return RedirectToAction("Result");
        }

        // Use FormCollection
        // GET: SinhVien_64132989/UseFormCollection
        public ActionResult UseFormCollection()
        {
            return View();
        }

        // POST: SinhVien_64132989/UseFormCollection
        [HttpPost]
        public ActionResult UseFormCollection(FormCollection form)
        {
            var sv = new SinhVienModels
            {
                studentId = form["studentId"],
                studentName = form["studentName"],
                studentMark = form["studentMark"]
            };

            TempData["SinhVien"] = sv;
            return RedirectToAction("Result");
        }

        // GET: SinhVien_64132989/Result
        public ActionResult Result()
        {
            if (TempData["SinhVien"] is SinhVienModels sv)
            {
                return View(sv);
            }

            return RedirectToAction("Index");
        }
    }
}
