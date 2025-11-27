using Microsoft.AspNetCore.Mvc;
using NguyenPhucThienTinh_DH52201572.Models;

namespace NguyenPhucThienTinh_DH52201572.Controllers
{
    public class NhanVienController : Controller
    {   QLBHContext db=new QLBHContext();
		public IActionResult Index()
		{
			var nvList = db.Nhanviens.ToList();
			return View(nvList);
		}


		[HttpGet]
        public ActionResult them()
        {
            return View();
        }

        [HttpPost]
        public ActionResult them(Nhanvien n)
        {
            if (ModelState.IsValid)
            {
                if (db.Nhanviens.Find(n.Manv) != null)
                {
                    //ModelState.AddModelError("Manv", "Mã nhân viên bị trùng!");
                    ModelState.AddModelError("", "Mã nhân viên bị trùng!");
                    return View(n);
                }
                else
                {
                    db.Nhanviens.Add(n);
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
		public IActionResult xoa(string id)
		{
			var nv = db.Nhanviens.Find(id);
			if (nv == null)
			{
				return NotFound();
			}

			// kiểm tra ràng buộc khóa ngoại
			bool hasPhieu = db.Phieugiaohangs.Any(p => p.Manv == id);

			ViewBag.HasPhieu = hasPhieu;
			return View(nv);
		}

		
		[HttpPost, ActionName("Xoa")]
		[ValidateAntiForgeryToken]
		public IActionResult XoaConfirmed(string id)
		{
			var nv = db.Nhanviens.Find(id);
			if (nv == null)
			{
				return NotFound();
			}

			// chỉ xóa khi không có phiếu giao hàng liên quan
			bool hasPhieu = db.Phieugiaohangs.Any(p => p.Manv == id);
			if (!hasPhieu)
			{
				db.Nhanviens.Remove(nv);
				db.SaveChanges();
				return RedirectToAction("Index");
			}

			// nếu có quan hệ thì quay lại view thông báo
			ModelState.AddModelError("", "Không thể xóa nhân viên vì đã có phiếu giao hàng liên quan.");
			ViewBag.HasPhieu = true;
			return View(nv);
		}


        [HttpGet]
        public IActionResult Sua(string id)
        {
            var nv = db.Nhanviens.Find(id);
            if (nv == null)
                return NotFound();
            return View(nv);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Sua(Nhanvien n)
        {
            if (ModelState.IsValid)
            {
                var nv = db.Nhanviens.Find(n.Manv);
                if (nv == null)
                {
                    return NotFound();
                }

                // cập nhật các trường
                nv.Tennv = n.Tennv;
                nv.Ngaysinh = n.Ngaysinh;
                nv.Phai = n.Phai;
                nv.Diachi = n.Diachi;
                nv.Password = n.Password;

                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(n);
        }











    }
}
