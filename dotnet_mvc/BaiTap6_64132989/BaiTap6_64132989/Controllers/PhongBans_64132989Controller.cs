using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BaiTap6_64132989.Models;

namespace BaiTap6_64132989.Controllers
{
    public class PhongBans_64132989Controller : Controller
    {
        private QLNVDbContext_64132989 db = new QLNVDbContext_64132989();

        // GET: PhongBans
        public ActionResult Index(int page = 1)
        {
            const int pageSize = 10;
            var phongBans = db.PhongBans.OrderBy(p => p.MaPhongBan);
            ViewBag.CurrentRecords = phongBans.Skip((page - 1) * pageSize).Take(pageSize).ToList().Count;
            ViewBag.TotalRecords = phongBans.Count();
            ViewBag.TotalPages = Math.Ceiling((double)ViewBag.TotalRecords / pageSize);
            ViewBag.CurrentPage = page;

            return View(phongBans.Skip((page - 1) * pageSize).Take(pageSize).ToList());
        }

        // GET: PhongBans/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PhongBan phongBan = db.PhongBans.Find(id);
            if (phongBan == null)
            {
                return HttpNotFound();
            }
            return View(phongBan);
        }

        // GET: PhongBans/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PhongBans/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaPhongBan,TenPhongBan")] PhongBan phongBan)
        {
            if (ModelState.IsValid)
            {
                db.PhongBans.Add(phongBan);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(phongBan);
        }

        // GET: PhongBans/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PhongBan phongBan = db.PhongBans.Find(id);
            if (phongBan == null)
            {
                return HttpNotFound();
            }
            return View(phongBan);
        }

        // POST: PhongBans/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaPhongBan,TenPhongBan")] PhongBan phongBan)
        {
            if (ModelState.IsValid)
            {
                db.Entry(phongBan).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(phongBan);
        }

        // GET: PhongBans/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PhongBan phongBan = db.PhongBans.Find(id);
            if (phongBan == null)
            {
                return HttpNotFound();
            }
            return View(phongBan);
        }

        // POST: PhongBans/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PhongBan phongBan = db.PhongBans.Find(id);
            if (phongBan != null)
            {
                // Logic to delete employees in this department
                var employees = db.NhanViens.Where(e => e.MaPhongBan == id).ToList();
                db.NhanViens.RemoveRange(employees);

                db.PhongBans.Remove(phongBan);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return HttpNotFound();
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
