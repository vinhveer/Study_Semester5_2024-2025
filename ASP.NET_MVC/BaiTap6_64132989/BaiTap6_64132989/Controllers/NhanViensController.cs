﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
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
