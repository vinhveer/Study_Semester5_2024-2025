using BaiTap6_64132989_NoAuth.Models;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace BaiTap6_64132989_NoAuth.Controllers
{
    public class TimKiemController : Controller
    {
        private QLNV_64132989Entities db = new QLNV_64132989Entities();

        // GET: NhanViens
        public ActionResult Index()
        {
            var nhanViens = db.NhanViens.Include(n => n.PhongBan);
            return View(nhanViens.ToList());
        }
    }
}