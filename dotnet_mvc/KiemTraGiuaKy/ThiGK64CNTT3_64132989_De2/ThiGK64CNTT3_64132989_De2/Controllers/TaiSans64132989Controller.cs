using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaTS,TenTS,DVT,XuatXu,DonGia,MaLTS,GhiChu")] TaiSan taiSan, HttpPostedFileBase AnhMH)
        {
            if (ModelState.IsValid)
            {
                if (AnhMH != null && AnhMH.ContentLength > 0)
                {
                    var directoryPath = Server.MapPath("~/Images");
                    if (!Directory.Exists(directoryPath))
                    {
                        Directory.CreateDirectory(directoryPath);
                    }

                    var fileName = Path.GetFileName(AnhMH.FileName);
                    var path = Path.Combine(directoryPath, fileName);
                    AnhMH.SaveAs(path);
                    taiSan.AnhMH = fileName;  // Save the file name in the database
                }

                db.TaiSans.Add(taiSan);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaLTS = new SelectList(db.LoaiTaiSans, "MaLTS", "TenLTS", taiSan.MaLTS);
            return View(taiSan);
        }

        public ActionResult TimKiemTS_64132989(string searchString, decimal? minPrice, decimal? maxPrice, int page = 1)
        {
            // Lấy danh sách tài sản từ cơ sở dữ liệu
            var taiSans = db.TaiSans.Include(t => t.LoaiTaiSan).AsQueryable();

            // Lọc theo giá (sử dụng LINQ)
            if (minPrice.HasValue)
            {
                taiSans = taiSans.Where(t => t.DonGia >= minPrice.Value);
            }

            if (maxPrice.HasValue)
            {
                taiSans = taiSans.Where(t => t.DonGia <= maxPrice.Value);
            }

            // Sử dụng SQL thô cho tìm kiếm không dấu
            if (!string.IsNullOrEmpty(searchString))
            {
                // Thực hiện tìm kiếm không dấu với SQL thô
                string sqlQuery = "SELECT * FROM TaiSan WHERE TenTS COLLATE SQL_Latin1_General_Cp1_CI_AI LIKE @p0";
                string normalizedSearchString = "%" + searchString + "%";
                var filteredTaiSans = db.TaiSans.SqlQuery(sqlQuery, normalizedSearchString).ToList();

                // Lọc kết quả SQL thô với các điều kiện LINQ đã áp dụng
                taiSans = filteredTaiSans.AsQueryable();
            }

            // Phân trang và trả về kết quả
            int pageSize = 2;
            int totalItems = taiSans.Count();
            var paginatedItems = taiSans.OrderBy(t => t.MaTS).Skip((page - 1) * pageSize).Take(pageSize).ToList();

            // Gán các biến cần thiết cho ViewBag
            ViewBag.TotalItems = totalItems;
            ViewBag.PageSize = pageSize;
            ViewBag.Page = page;

            return View(paginatedItems);
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
