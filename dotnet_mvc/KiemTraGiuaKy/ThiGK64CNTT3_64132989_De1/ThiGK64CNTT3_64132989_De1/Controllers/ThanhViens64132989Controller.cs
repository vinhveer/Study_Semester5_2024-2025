using System;
using System.Data.Entity;
using System.Data.SqlClient;
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

        [HttpGet]
        public ActionResult TimKiemTV_64132989(string HoTen, int? MaTinh, int page = 1, int pageSize = 2)
        {
            // Viết truy vấn SQL với tìm kiếm không dấu bằng COLLATE
            var sql = @"
                SELECT * 
                FROM ThanhVien
                WHERE (@MaTinh IS NULL OR MaTinh = @MaTinh)
                AND (@HoTen IS NULL OR (HoTV + ' ' + TenTV) COLLATE SQL_Latin1_General_CP1_CI_AI LIKE '%' + @HoTen + '%')
                ORDER BY MaTV
                OFFSET @Offset ROWS
                FETCH NEXT @PageSize ROWS ONLY;";

            // Tính toán giá trị Offset dựa trên số trang và số bản ghi mỗi trang
            int offset = (page - 1) * pageSize;

            // Tham số cho truy vấn dữ liệu
            var dataParameters = new[]
            {
        new SqlParameter("@MaTinh", MaTinh.HasValue ? (object)MaTinh.Value : DBNull.Value),
        new SqlParameter("@HoTen", string.IsNullOrEmpty(HoTen) ? (object)DBNull.Value : HoTen),
        new SqlParameter("@Offset", offset),
        new SqlParameter("@PageSize", pageSize)
    };

            // Thực hiện truy vấn lấy dữ liệu
            var thanhViens = db.Database.SqlQuery<ThanhVien>(sql, dataParameters).ToList();

            // Truy vấn đếm tổng số bản ghi sau khi lọc
            var countSql = @"
                SELECT COUNT(*)
                FROM ThanhVien
                WHERE (@MaTinh IS NULL OR MaTinh = @MaTinh)
                AND (@HoTen IS NULL OR (HoTV + ' ' + TenTV) COLLATE SQL_Latin1_General_CP1_CI_AI LIKE '%' + @HoTen + '%');";

            // Tham số cho truy vấn đếm
            var countParameters = new[]
            {
                new SqlParameter("@MaTinh", MaTinh.HasValue ? (object)MaTinh.Value : DBNull.Value),
                new SqlParameter("@HoTen", string.IsNullOrEmpty(HoTen) ? (object)DBNull.Value : HoTen)
            };

            // Thực hiện truy vấn đếm
            int totalItems = db.Database.SqlQuery<int>(countSql, countParameters).Single();

            // Gán thông tin phân trang vào ViewBag
            ViewBag.TotalItems = totalItems;
            ViewBag.PageSize = pageSize;
            ViewBag.Page = page;
            ViewBag.HoTen = HoTen;
            ViewBag.MaTinh = new SelectList(db.Tinhs, "MaTinh", "TenTinh", MaTinh);

            return View(thanhViens);
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
