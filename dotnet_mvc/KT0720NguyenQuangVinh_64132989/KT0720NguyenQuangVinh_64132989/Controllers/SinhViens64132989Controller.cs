using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using KT0720NguyenQuangVinh_64132989.Models;

namespace KT0720NguyenQuangVinh_64132989.Controllers
{
    public class SinhViens64132989Controller : Controller
    {
        private KT0720_64132989Entities db = new KT0720_64132989Entities();

        // GET: SinhViens64132989
        public ActionResult Index()
        {
            var sinhViens = db.SinhViens.Include(s => s.Lop);
            return View(sinhViens.ToList());
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
            if (ModelState.IsValid)
            {
                if (AnhSV != null && AnhSV.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(AnhSV.FileName);
                    var relativePath = "~/Images/" + fileName;
                    var physicalPath = Server.MapPath(relativePath);
                    AnhSV.SaveAs(physicalPath);

                    sinhVien.AnhSV = relativePath;
                }

                db.SinhViens.Add(sinhVien);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaLop = new SelectList(db.Lops, "MaLop", "TenLop", sinhVien.MaLop);
            return View(sinhVien);
        }

        // Get: SinhViens64132989/TimKiem
        public ActionResult TimKiem()
        {
            var sinhViens = db.SinhViens.Include(s => s.Lop);
            return View(sinhViens.ToList());
        }

        [HttpGet]
        public ActionResult TimKiem(string maSV, string hoTen, int page = 1)
        {
            // Lấy danh sách sinh viên từ database và áp dụng bộ lọc
            var sinhViens = db.SinhViens.AsQueryable();

            if (!string.IsNullOrEmpty(maSV))
            {
                sinhViens = sinhViens.Where(sv => sv.MaSV == maSV);
            }

            if (!string.IsNullOrEmpty(hoTen))
            {
                sinhViens = sinhViens.Where(sv => sv.HoSV.Contains(hoTen) || sv.TenSV.Contains(hoTen));
            }

            // Thêm OrderBy để tránh lỗi khi sử dụng Skip
            sinhViens = sinhViens.OrderBy(sv => sv.MaSV);

            // Phân trang
            var totalRecords = sinhViens.Count();
            var totalPages = (int)Math.Ceiling((double)totalRecords / 5);
            var currentRecords = sinhViens.Skip((page - 1) * 5).Take(5).ToList();

            // Truyền dữ liệu phân trang sang View
            ViewBag.CurrentPage = page;
            ViewBag.TotalRecords = totalRecords;
            ViewBag.TotalPages = totalPages;

            return View(currentRecords);
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
