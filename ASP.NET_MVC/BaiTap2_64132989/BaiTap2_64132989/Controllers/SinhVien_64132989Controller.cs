using System.Web.Mvc;

namespace BaiTap2_64132989.Controllers
{
    public class SinhVien_64132989Controller : Controller
    {
        // GET: SinhVien_64132989/Index
        public ActionResult Index()
        {
            return View();
        }

        // POST: SinhVien_64132989/Index
        [HttpPost]
        public ActionResult Index(Models.SinhVienModels sv)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("Result", sv);
            }

            return View(sv);
        }

        // GET: SinhVien_64132989/UseRequest
        public ActionResult UseRequest()
        {
            return View();
        }

        // POST: SinhVien_64132989/ProcessRequest
        [HttpPost]
        public ActionResult ProcessRequest()
        {
            Models.SinhVienModels sv = new Models.SinhVienModels();
            sv.studentId = Request["studentId"];
            sv.studentName = Request["studentName"];
            sv.studentMark = Request["studentMark"];
            return RedirectToAction("Result", sv);
        }

        // GET: SinhVien_64132989/UseArguments
        public ActionResult UseArguments()
        {
            return View();
        }

        // POST: SinhVien_64132989/UseArguments
        [HttpPost]
        public ActionResult UseArguments(string studentId, string studentName, string studentMark)
        {
            Models.SinhVienModels sv = new Models.SinhVienModels();
            sv.studentId = studentId;
            sv.studentName = studentName;
            sv.studentMark = studentMark;
            return RedirectToAction("Result", sv);
        }

        // GET: SinhVien_64132989/UseFormCollection
        public ActionResult UseFormCollection()
        {
            return View();
        }

        // POST: SinhVien_64132989/UseFormCollection
        [HttpPost]
        public ActionResult UseFormCollection(FormCollection form)
        {
            Models.SinhVienModels sv = new Models.SinhVienModels();
            sv.studentId = form["studentId"];
            sv.studentName = form["studentName"];
            sv.studentMark = form["studentMark"];
            return RedirectToAction("Result", sv);
        }

        // GET: SinhVien_64132989/Result
        public ActionResult Result(Models.SinhVienModels sv)
        {
            if (sv == null)
            {
                return RedirectToAction("Index");
            }

            return View(sv);
        }
    }
}
