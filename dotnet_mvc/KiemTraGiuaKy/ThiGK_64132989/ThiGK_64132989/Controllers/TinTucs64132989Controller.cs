using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ThiGK_64132989.Models;

namespace ThiGK_64132989.Controllers
{
    public class TinTucs64132989Controller : Controller
    {
        private Model_64132989 db = new Model_64132989();

        // GET: TinTucs64132989
        public ActionResult Index(int page = 1, int pageSize = 2)
        {
            var totalItems = db.TinTucs.Count(); // Tổng số mục
            var tinTucs = db.TinTucs.Include(t => t.LoaiTinTuc)
                                    .OrderBy(t => t.MaTT)
                                    .Skip((page - 1) * pageSize)
                                    .Take(pageSize)
                                    .ToList();

            ViewBag.CurrentPage = page;
            ViewBag.PageSize = pageSize;
            ViewBag.TotalItems = totalItems; // Tổng số mục
            ViewBag.TotalPages = (int)Math.Ceiling((double)totalItems / pageSize); // Tổng số trang

            return View(tinTucs);
        }


        // GET: TinTucs64132989/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TinTuc tinTuc = db.TinTucs.Find(id);
            if (tinTuc == null)
            {
                return HttpNotFound();
            }
            return View(tinTuc);
        }

        // GET: TinTucs64132989/Create
        public ActionResult Create()
        {
            ViewBag.MaLTT = new SelectList(db.LoaiTinTucs, "MaLTT", "TenLTT");
            return View();
        }

        // POST: TinTucs64132989/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaTT,TieuDe,NgonNgu,NguoiDang,NgayDang,AnhDaiDien,MaLTT,GhiChu")] TinTuc tinTuc, HttpPostedFileBase AnhDaiDien)
        {
            if (ModelState.IsValid)
            {
                if (AnhDaiDien != null && AnhDaiDien.ContentLength > 0)
                {
                    var directoryPath = Server.MapPath("~/Images");
                    if (!Directory.Exists(directoryPath))
                    {
                        Directory.CreateDirectory(directoryPath);
                    }

                    var fileName = Path.GetFileName(AnhDaiDien.FileName);
                    var path = Path.Combine(directoryPath, fileName);
                    AnhDaiDien.SaveAs(path);
                    tinTuc.AnhDaiDien = fileName;  // Save the file name in the database
                }

                db.TinTucs.Add(tinTuc);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaLTT = new SelectList(db.LoaiTinTucs, "MaLTT", "TenLTT", tinTuc.MaLTT);
            return View(tinTuc);
        }

        [HttpGet]
        public ActionResult TimKiem_64132989(string TieuDe, string NguoiDang, int page = 1, int pageSize = 2)
        {
            // Viết truy vấn SQL với tìm kiếm không dấu bằng COLLATE
            var sql = @"
        SELECT * 
        FROM TinTuc
        WHERE (@TieuDe IS NULL OR TieuDe COLLATE SQL_Latin1_General_CP1_CI_AI LIKE '%' + @TieuDe + '%')
        AND (@NguoiDang IS NULL OR NguoiDang COLLATE SQL_Latin1_General_CP1_CI_AI LIKE '%' + @NguoiDang + '%')
        ORDER BY MaTT
        OFFSET @Offset ROWS
        FETCH NEXT @PageSize ROWS ONLY;";

            // Tính toán giá trị Offset dựa trên số trang và số bản ghi mỗi trang
            int offset = (page - 1) * pageSize;

            // Tham số cho truy vấn dữ liệu
            var dataParameters = new[]
            {
                new SqlParameter("@TieuDe", string.IsNullOrEmpty(TieuDe) ? (object)DBNull.Value : TieuDe),
                new SqlParameter("@NguoiDang", string.IsNullOrEmpty(NguoiDang) ? (object)DBNull.Value : NguoiDang),
                new SqlParameter("@Offset", offset),
                new SqlParameter("@PageSize", pageSize)
            };

            // Thực hiện truy vấn lấy dữ liệu
            var tinTucs = db.Database.SqlQuery<TinTuc>(sql, dataParameters).ToList();

            // Truy vấn đếm tổng số bản ghi sau khi lọc
            var countSql = @"
                    SELECT COUNT(*)
                    FROM TinTuc
                    WHERE (@TieuDe IS NULL OR TieuDe COLLATE SQL_Latin1_General_CP1_CI_AI LIKE '%' + @TieuDe + '%')
                    AND (@NguoiDang IS NULL OR NguoiDang COLLATE SQL_Latin1_General_CP1_CI_AI LIKE '%' + @NguoiDang + '%');";

            // Tham số cho truy vấn đếm
            var countParameters = new[]
            {
                new SqlParameter("@TieuDe", string.IsNullOrEmpty(TieuDe) ? (object)DBNull.Value : TieuDe),
                new SqlParameter("@NguoiDang", string.IsNullOrEmpty(NguoiDang) ? (object)DBNull.Value : NguoiDang)
            };

            // Thực hiện truy vấn đếm
            int totalItems = db.Database.SqlQuery<int>(countSql, countParameters).Single();

            // Gán thông tin phân trang vào ViewBag
            ViewBag.TotalItems = totalItems;
            ViewBag.PageSize = pageSize;
            ViewBag.Page = page;
            ViewBag.TieuDe = TieuDe;
            ViewBag.NguoiDang = NguoiDang;

            return View(tinTucs);
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
