using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using BaiTap6_64132989.Models;

namespace BaiTap6_64132989.Controllers
{
    public class NhanViensController : Controller
    {
        private readonly Employee db = new Employee();

        // GET: NhanViens
        public async Task<ActionResult> Index(int? page)
        {
            int pageSize = 10;
            int pageNumber = page ?? 1;

            var nhanViens = await db.NhanViens.Include(n => n.PhongBan)
                                                .OrderBy(nv => nv.MaNV)
                                                .Skip((pageNumber - 1) * pageSize)
                                                .Take(pageSize)
                                                .ToListAsync();

            int totalRecords = await db.NhanViens.CountAsync();
            ViewBag.TotalPages = (int)Math.Ceiling((double)totalRecords / pageSize);
            ViewBag.CurrentRecords = nhanViens.Count;
            ViewBag.TotalRecords = totalRecords;
            ViewBag.CurrentPage = pageNumber;

            return View(nhanViens);
        }

        // GET: NhanViens/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var nhanVien = await db.NhanViens.FindAsync(id);
            if (nhanVien == null) return HttpNotFound();
            return View(nhanVien);
        }

        // GET: NhanViens/Create
        public ActionResult Create()
        {
            ViewBag.MaPhongBan = new SelectList(db.PhongBans, "MaPhongBan", "TenPhongBan");
            return View();
        }

        // POST: NhanViens/Create
        // POST: NhanViens/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "MaNV,HoNV,TenNV,GioiTinh,NgaySinh,Luong,AnhNV,DiaChi,MaPhongBan")] NhanVien nhanVien, HttpPostedFileBase AnhNV)
        {
            if (ModelState.IsValid)
            {
                // Handle file upload for the image
                if (AnhNV != null && AnhNV.ContentLength > 0)
                {
                    // Save the uploaded image
                    var fileName = Path.GetFileName(AnhNV.FileName);
                    var relativePath = "/Images/" + fileName; // Relative path to save the image
                    var absolutePath = Path.Combine(Server.MapPath(relativePath)); // Absolute path on the server

                    // Save the file to the server
                    AnhNV.SaveAs(absolutePath);

                    // Set the relative path in the model
                    nhanVien.AnhNV = relativePath;
                }

                // Add the new employee to the database
                db.NhanViens.Add(nhanVien);
                await db.SaveChangesAsync(); // Asynchronous save

                return RedirectToAction("Index");
            }

            // Populate the dropdown for PhongBan if the model state is not valid
            ViewBag.MaPhongBan = new SelectList(db.PhongBans, "MaPhongBan", "TenPhongBan", nhanVien.MaPhongBan);
            return View(nhanVien);
        }


        // GET: NhanViens/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var nhanVien = await db.NhanViens.FindAsync(id);
            if (nhanVien == null) return HttpNotFound();
            ViewBag.MaPhongBan = new SelectList(db.PhongBans, "MaPhongBan", "TenPhongBan", nhanVien.MaPhongBan);
            return View(nhanVien);
        }

        // POST: NhanViens/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "MaNV,HoNV,TenNV,GioiTinh,NgaySinh,Luong,AnhNV,DiaChi,MaPhongBan")] NhanVien nhanVien, HttpPostedFileBase AnhNV)
        {
            if (ModelState.IsValid)
            {
                if (AnhNV != null && AnhNV.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(AnhNV.FileName);
                    var relativePath = "/Images/" + fileName;
                    var absolutePath = Path.Combine(Server.MapPath(relativePath));
                    AnhNV.SaveAs(absolutePath);
                    nhanVien.AnhNV = relativePath;
                }

                db.Entry(nhanVien).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.MaPhongBan = new SelectList(db.PhongBans, "MaPhongBan", "TenPhongBan", nhanVien.MaPhongBan);
            return View(nhanVien);
        }

        // GET: NhanViens/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var nhanVien = await db.NhanViens.FindAsync(id);
            if (nhanVien == null) return HttpNotFound();
            return View(nhanVien);
        }

        // POST: NhanViens/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            var nhanVien = await db.NhanViens.FindAsync(id);
            if (nhanVien != null)
            {
                db.NhanViens.Remove(nhanVien);
                await db.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }

        // POST: NhanViens/DeleteSelected
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteSelected(List<int> selectedIds)
        {
            if (selectedIds == null || !selectedIds.Any())
            {
                return RedirectToAction("Index");
            }

            foreach (var id in selectedIds)
            {
                var nhanVien = await db.NhanViens.FindAsync(id);
                if (nhanVien != null)
                {
                    db.NhanViens.Remove(nhanVien);
                }
            }

            await db.SaveChangesAsync();
            return RedirectToAction("Index");
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

        [HttpGet]
        public ActionResult TimKiem(string maNV, string hoTen, bool? gioiTinh, decimal? luongMin, decimal? luongMax, string diaChi, string phongBan, int page = 1)
        {
            var nhanViens = db.NhanViens.Include(nv => nv.PhongBan);

            if (!string.IsNullOrEmpty(maNV))
            {
                // Chuyển đổi Mã nhân viên nhập vào thành int
                if (int.TryParse(maNV, out int maNVInt))
                {
                    nhanViens = nhanViens.Where(nv => nv.MaNV == maNVInt);
                }
            }

            if (!string.IsNullOrEmpty(hoTen))
            {
                string hoTenNoDau = RemoveDiacritics(hoTen);
                nhanViens = nhanViens.Where(nv => (RemoveDiacritics(nv.HoNV) + " " + RemoveDiacritics(nv.TenNV)).Contains(hoTenNoDau));
            }

            if (gioiTinh.HasValue)
            {
                nhanViens = nhanViens.Where(nv => nv.GioiTinh == gioiTinh.Value);
            }

            if (luongMin.HasValue)
            {
                nhanViens = nhanViens.Where(nv => nv.Luong >= luongMin.Value);
            }

            if (luongMax.HasValue)
            {
                nhanViens = nhanViens.Where(nv => nv.Luong <= luongMax.Value);
            }

            if (!string.IsNullOrEmpty(diaChi))
            {
                string diaChiNoDau = RemoveDiacritics(diaChi);
                nhanViens = nhanViens.Where(nv => RemoveDiacritics(nv.DiaChi).Contains(diaChiNoDau));
            }

            if (!string.IsNullOrEmpty(phongBan))
            {
                string phongBanNoDau = RemoveDiacritics(phongBan);
                nhanViens = nhanViens.Where(nv => RemoveDiacritics(nv.PhongBan.TenPhongBan).Contains(phongBanNoDau));
            }

            ViewBag.CurrentPage = page;
            ViewBag.TotalRecords = nhanViens.Count();
            ViewBag.TotalPages = (int)Math.Ceiling((double)ViewBag.TotalRecords / 5);
            ViewBag.CurrentRecords = Math.Min(5, ViewBag.TotalRecords - ((page - 1) * 5));

            var result = nhanViens.OrderBy(nv => nv.MaNV).Skip((page - 1) * 5).Take(5).ToList();

            return View(result);
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
