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
using ThiGK64CNTT3_64132989_De2.Models;

namespace ThiGK64CNTT3_64132989_De2.Controllers
{
    public class TaiSans64132989Controller : Controller
    {
        private ModelQLTS_64132989 db = new ModelQLTS_64132989();

        // GET: TaiSans64132989
        public ActionResult Index(int page = 1, int pageSize = 2)
        {
            var taiSans = db.TaiSans.Include(t => t.LoaiTaiSan)
                                    .OrderBy(t => t.MaTS)
                                    .Skip((page - 1) * pageSize)
                                    .Take(pageSize)
                                    .ToList();

            // Pass the total count of items and current page to the view using ViewBag
            ViewBag.TotalItems = db.TaiSans.Count();
            ViewBag.Page = page;
            ViewBag.PageSize = pageSize;

            return View(taiSans);
        }

        // GET: TaiSans64132989/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TaiSan taiSan = db.TaiSans.Find(id);
            if (taiSan == null)
            {
                return HttpNotFound();
            }
            return View(taiSan);
        }

        // GET: TaiSans64132989/Create
        public ActionResult Create()
        {
            ViewBag.MaLTS = new SelectList(db.LoaiTaiSans, "MaLTS", "TenLTS");
            return View();
        }

        // POST: TaiSans64132989/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaTS,TenTS,DVT,XuatXu,DonGia,MaLTS,GhiChu")] TaiSan taiSan, HttpPostedFileBase AnhMH)
        {
            if (ModelState.IsValid)
            {
                if (AnhMH != null && AnhMH.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(AnhMH.FileName);
                    var path = Path.Combine(Server.MapPath("~/Images"), fileName);
                    AnhMH.SaveAs(path);
                    taiSan.AnhMH = fileName;  // Lưu tên tệp ảnh vào database
                }

                db.TaiSans.Add(taiSan);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaLTS = new SelectList(db.LoaiTaiSans, "MaLTS", "TenLTS", taiSan.MaLTS);
            return View(taiSan);
        }

        [HttpGet]
        public ActionResult TimKiemTS_64132989(string searchString, decimal? minPrice, decimal? maxPrice, int page = 1)
        {
            // Lấy danh sách tài sản từ cơ sở dữ liệu
            var taiSans = db.TaiSans.Include(t => t.LoaiTaiSan).AsQueryable();

            // Thêm điều kiện tìm kiếm theo giá
            if (minPrice.HasValue)
            {
                taiSans = taiSans.Where(t => t.DonGia >= minPrice.Value);
            }

            if (maxPrice.HasValue)
            {
                taiSans = taiSans.Where(t => t.DonGia <= maxPrice.Value);
            }

            // Thực hiện phân trang
            int pageSize = 2; // Số lượng tài sản mỗi trang
            int totalItems = taiSans.Count();
            var items = taiSans.OrderBy(t => t.MaTS).ToList(); // Retrieve all items to apply search in-memory

            // Normalize the search string to remove diacritics
            string normalizedSearchString = RemoveDiacritics(searchString ?? string.Empty).ToLower();

            // Filter the results based on the normalized search string
            if (!string.IsNullOrEmpty(normalizedSearchString))
            {
                items = items.Where(t => RemoveDiacritics(t.TenTS).ToLower().Contains(normalizedSearchString)).ToList();
            }

            // Perform pagination on the filtered items
            var paginatedItems = items.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            // Gán các biến cần thiết cho ViewBag
            ViewBag.TotalItems = totalItems;
            ViewBag.PageSize = pageSize;
            ViewBag.Page = page;

            return View(paginatedItems);
        }

        // Helper method to remove diacritics
        private string RemoveDiacritics(string text)
        {
            if (string.IsNullOrEmpty(text))
                return text;

            // Normalize the text to FormD (decomposed form)
            var normalizedString = text.Normalize(NormalizationForm.FormD);
            var stringBuilder = new StringBuilder();

            foreach (var c in normalizedString)
            {
                var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
                // Append only the characters that are not combining characters
                if (unicodeCategory != UnicodeCategory.NonSpacingMark)
                {
                    stringBuilder.Append(c);
                }
            }

            // Normalize back to FormC (composed form)
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
