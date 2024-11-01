using System;
using System.Data.Entity;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using ThiGK64CNTT3_64132989_De1.Models;

namespace ThiGK64CNTT3_64132989_De1.Controllers
{
    public class ThanhViens64132989Controller : Controller
    {
        private Model_64132989 db = new Model_64132989();

        // GET: ThanhViens64132989
        public ActionResult Index(int page = 1, int pageSize = 2)
        {
            var thanhViens = db.ThanhViens.Include(t => t.Tinh)
                                          .OrderBy(t => t.MaTV)
                                          .Skip((page - 1) * pageSize)
                                          .Take(pageSize)
                                          .ToList();

            ViewBag.TotalItems = db.ThanhViens.Count();
            ViewBag.Page = page;
            ViewBag.PageSize = pageSize;
            return View(thanhViens);
        }

        // GET: ThanhViens64132989/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ThanhVien thanhVien = db.ThanhViens.Find(id);
            if (thanhVien == null)
            {
                return HttpNotFound();
            }
            return View(thanhVien);
        }

        // GET: ThanhViens64132989/Create
        public ActionResult Create()
        {
            ViewBag.MaTinh = new SelectList(db.Tinhs, "MaTinh", "TenTinh");
            return View();
        }

        // POST: ThanhViens64132989/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaTV,HoTV,TenTV,NgaySinh,GioiTinh,Email,DiaChi,MaTinh")] ThanhVien thanhVien, HttpPostedFileBase AnhDaiDien)
        {
            if (ModelState.IsValid)
            {
                if (AnhDaiDien != null && AnhDaiDien.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(AnhDaiDien.FileName);
                    var path = Path.Combine(Server.MapPath("~/Images"), fileName);
                    AnhDaiDien.SaveAs(path);
                    thanhVien.AnhDaiDien = fileName;  // Lưu tên tệp ảnh vào thuộc tính AnhDaiDien
                }

                db.ThanhViens.Add(thanhVien);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaTinh = new SelectList(db.Tinhs, "MaTinh", "TenTinh", thanhVien.MaTinh);
            return View(thanhVien);
        }

        // GET: ThanhViens64132989/TimKiemThanhVien_64132989
        // GET: ThanhViens64132989/TimKiemThanhVien_64132989
        [HttpGet]
        public ActionResult TimKiemTV_64132989(string HoTen, int? MaTinh, int page = 1, int pageSize = 2)
        {
            var thanhViens = db.ThanhViens.Include(t => t.Tinh).AsQueryable();

            // Lọc theo tỉnh trước nếu có
            if (MaTinh.HasValue)
            {
                thanhViens = thanhViens.Where(t => t.MaTinh == MaTinh.Value);
            }

            // Sử dụng AsEnumerable() để thực hiện phần còn lại của lọc trong bộ nhớ
            var filteredThanhViens = thanhViens.AsEnumerable();

            // Áp dụng lọc bằng RemoveDiacritics sau khi chuyển sang IEnumerable
            if (!string.IsNullOrEmpty(HoTen))
            {
                string normalizedHoTen = RemoveDiacritics(HoTen.ToLower());
                filteredThanhViens = filteredThanhViens
                                     .Where(t => RemoveDiacritics((t.HoTV + " " + t.TenTV).ToLower()).Contains(normalizedHoTen));
            }

            int totalItems = filteredThanhViens.Count(); // Đếm số bản ghi sau khi lọc
            var paginatedItems = filteredThanhViens.OrderBy(t => t.MaTV)
                                                   .Skip((page - 1) * pageSize)
                                                   .Take(pageSize)
                                                   .ToList(); // Phân trang

            ViewBag.TotalItems = totalItems;
            ViewBag.PageSize = pageSize;
            ViewBag.Page = page;
            ViewBag.HoTen = HoTen;
            ViewBag.MaTinh = new SelectList(db.Tinhs, "MaTinh", "TenTinh", MaTinh);

            return View(paginatedItems);
        }


        private string RemoveDiacritics(string text)
        {
            if (string.IsNullOrEmpty(text))
                return text;

            var normalizedString = text.Normalize(NormalizationForm.FormD);
            var stringBuilder = new StringBuilder();

            foreach (var c in normalizedString)
            {
                var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
                if (unicodeCategory != UnicodeCategory.NonSpacingMark)
                {
                    stringBuilder.Append(c);
                }
            }

            return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
