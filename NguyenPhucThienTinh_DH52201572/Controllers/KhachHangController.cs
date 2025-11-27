using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NguyenPhucThienTinh_DH52201572.Models;
using System.Linq;

namespace NguyenPhucThienTinh_DH52201572.Controllers
{
    public class KhachHangController : Controller
    {
        QLBHContext db = new QLBHContext();

        public IActionResult Index()
        {
            var khList = db.Khachhangs.ToList();
            return View(khList);
        }

        public IActionResult IndexDangNhap()
        {
            ViewBag.kiemtraDangnhap = null;
            return View();
        }

        [HttpPost]
        public IActionResult DangNhap(Khachhang kh)
        {
            ViewBag.loginCheck = false;
            Khachhang x = db.Khachhangs.Find(kh.Makh);
            if (x != null)
            {
                if (x.Password == kh.Password)
                {
                    string json = JsonConvert.SerializeObject(x);
                    HttpContext.Session.SetString("Khachhang", json);
                    ViewBag.loginCheck = true;
                    return RedirectToAction("Index", "Home");
                }
            }
            ViewBag.kiemtraDangnhap = "Sai mã khách hàng hoặc mật khẩu!";
            return View("IndexDangNhap");
        }

        // Hiển thị form đăng ký
        public IActionResult IndexDangKy()
        {
            return View();
        }

        // Xử lý form đăng ký
        [HttpPost]
        public IActionResult DangKy(Khachhang kh)
        {
            if (ModelState.IsValid)
            {
                var existing = db.Khachhangs.FirstOrDefault(x => x.Makh == kh.Makh || x.Dienthoai == kh.Dienthoai);
                if (existing != null)
                {
                    ViewBag.kiemtraDangKy = "Khách hàng đã tồn tại!";
                    return View("IndexDangKy", kh);
                }

                db.Khachhangs.Add(kh);
                db.SaveChanges();
                return RedirectToAction("IndexDangNhap", "KhachHang");
            }
            return View("IndexDangKy", kh);
        }


        public IActionResult DangXuat()
        {
            HttpContext.Session.Remove("Khachhang");
            return RedirectToAction("IndexDangNhap", "KhachHang");
        }
    }
}
