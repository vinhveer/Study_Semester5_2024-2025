using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using KT0720Vinh_64132989.Models;

namespace KT0720Vinh_64132989.Controllers
{
    public class SinhViens64132989Controller : Controller
    {
        private readonly KT0720VinhDbContext db;

        // GET: SinhViens64132989
        public ActionResult Index(int page = 1, int pageSize = 2)
        {
            var thanhViens = db.SinhViens.Include(t => t.Lop)
                                          .OrderBy(t => t.MaSV)
                                          .Skip((page - 1) * pageSize)
                                          .Take(pageSize)
                                          .ToList();

            ViewBag.TotalItems = db.SinhViens.Count();
            ViewBag.Page = page;
            ViewBag.PageSize = pageSize;
            return View(thanhViens);
        }

        // GET: SinhViens64132989/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SinhVien sinhVien = db.SinhViens.Find(id);
            if (sinhVien == null)
            {
                return HttpNotFound();
            }
            return View(sinhVien);
        }

        // GET: SinhViens64132989/Create
        public ActionResult Create()
        {
            ViewBag.MaLop = new SelectList(db.Lops, "MaLop", "TenLop");
            return View();
        }

        // POST: SinhViens64132989/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaSV,HoSV,TenSV,NgaySinh,GioiTinh,AnhSV,DiaChi,MaLop")] SinhVien sinhVien, HttpPostedFileBase AnhSV)
        {
            // Thiết lập ViewBag.Lop nếu chưa thiết lập
            ViewBag.Lop = new SelectList(db.Lops, "MaLop", "TenLop", sinhVien.MaLop);

            if (ModelState.IsValid)
            {
                if (AnhSV != null && AnhSV.ContentLength > 0)
                {
                    // Lưu ảnh vào thư mục Images
                    var fileName = Path.GetFileName(AnhSV.FileName);
                    var path = Path.Combine(Server.MapPath("~/Images"), fileName);
                    AnhSV.SaveAs(path);
                    sinhVien.AnhSV = fileName;  // Lưu tên tệp ảnh vào thuộc tính AnhSV
                }

                db.SinhViens.Add(sinhVien);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(sinhVien);
        }

        public static string RemoveDiacritics(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
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

        public ActionResult TimKiem_64132989(string HoTen, string MaSV, int page = 1, int pageSize = 2)
        {
            ViewBag.HoTen = HoTen; // Lưu giá trị nhập vào để hiển thị lại
            ViewBag.MaSV = MaSV;

            // Lấy toàn bộ danh sách từ cơ sở dữ liệu
            var sinhViens = db.SinhViens.Include(s => s.Lop).ToList();

            // Áp dụng tìm kiếm trong bộ nhớ
            if (!string.IsNullOrEmpty(HoTen))
            {
                var hoTenSearch = RemoveDiacritics(HoTen).ToLower();
                sinhViens = sinhViens.Where(s =>
                    RemoveDiacritics(s.HoSV + " " + s.TenSV).ToLower().Contains(hoTenSearch)).ToList();
            }

            if (!string.IsNullOrEmpty(MaSV))
            {
                sinhViens = sinhViens.Where(s => s.MaSV.Contains(MaSV)).ToList();
            }

            // Phân trang
            int totalItems = sinhViens.Count();
            var thanhViensPaged = sinhViens.OrderBy(s => s.MaSV)
                                           .Skip((page - 1) * pageSize)
                                           .Take(pageSize)
                                           .ToList();

            // Truyền các giá trị cần thiết sang View
            ViewBag.TotalItems = totalItems;
            ViewBag.Page = page;
            ViewBag.PageSize = pageSize;

            return View(thanhViensPaged);
        }

        public ActionResult TimKiemAjax_64132989()
        {
            return View();
        }

        [HttpGet]
        public JsonResult TimKiemSinhVien(string keyword)
        {
            if (string.IsNullOrEmpty(keyword))
            {
                return Json(new { success = false, message = "Không có từ khóa tìm kiếm." }, JsonRequestBehavior.AllowGet);
            }

            var sinhViens = db.SinhViens
                              .Where(s => s.HoSV.Contains(keyword) || s.TenSV.Contains(keyword))
                              .Select(s => new
                              {
                                  s.MaSV,
                                  s.HoSV,
                                  s.TenSV,
                                  s.NgaySinh,
                                  s.GioiTinh,
                                  Lop = s.Lop.TenLop
                              })
                              .ToList();

            return Json(new { success = true, data = sinhViens }, JsonRequestBehavior.AllowGet);
        }
    }
}
