using Microsoft.AspNetCore.Mvc;
using NguyenPhucThienTinh_DH52201572.Models;

namespace NguyenPhucThienTinh_DH52201572.Controllers
{
    
    public class NhaSanXuatController : Controller
    {
        QLBHContext db = new QLBHContext();
        public IActionResult Index()
        {
            var nsxList = db.Nhasanxuats.ToList();
            return View(nsxList);
        }
        [HttpGet]
        public ActionResult them()
        {
            return View();
        }

        [HttpPost]
        public ActionResult them(Nhasanxuat n)
        {
            if (ModelState.IsValid)
            {
                if (db.Nhasanxuats.Find(n.Mansx) != null)
                {
                    //ModelState.AddModelError("Manv", "Mã nhân viên bị trùng!");
                    ModelState.AddModelError("", "Mã nhà sản xuất bị trùng!");
                    return View(n);
                }
                else
                {
                    db.Nhasanxuats.Add(n);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            else
            {
                return View(n);
            }
        }


        [HttpGet]
        public IActionResult Sua(string id)
        {
            var nsx = db.Nhasanxuats.Find(id);
            if (nsx == null)
                return NotFound();
            return View(nsx);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Sua(Nhasanxuat n)
        {
            if (ModelState.IsValid)
            {
                var nsx = db.Nhasanxuats.Find(n.Mansx);
                if (nsx == null)
                {
                    return NotFound();
                }
                // cập nhật các trường
                nsx.Tennsx = n.Tennsx;
                nsx.Diachi = n.Diachi;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(n);
        }


        [HttpGet]
        public IActionResult Xoa(string id)   // đổi chữ hoa cho khớp
        {
            var nsx = db.Nhasanxuats.Find(id);
            if (nsx == null)
            {
                return NotFound();
            }

            bool check = db.Hanghoas.Any(p => p.Mansx == id);
            ViewBag.Cohang = check;
            return View(nsx);
        }

        [HttpPost, ActionName("Xoa")]
        [ValidateAntiForgeryToken]
        public IActionResult XoaConfirmed(string id)
        {
            var nsx = db.Nhasanxuats.Find(id);
            if (nsx == null)
            {
                return NotFound();
            }

            bool hasHang = db.Hanghoas.Any(p => p.Mansx == id);
            if (!hasHang)
            {
                db.Nhasanxuats.Remove(nsx);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Không thể xóa nhà sản xuất vì đã có hàng hóa liên quan.");
            ViewBag.Cohang = true;   // dùng cùng tên với GET
            return View(nsx);
        }
















    }
}
