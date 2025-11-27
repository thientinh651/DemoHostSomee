using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NguyenPhucThienTinh_DH52201572.Models;

namespace NguyenPhucThienTinh_DH52201572.Controllers
{
    public class HangHoaController : Controller
    {
        QLBHContext db = new QLBHContext();
        public IActionResult Index()
        {
            var hanghoa = db.Hanghoas
                .Include(h => h.MaloaiNavigation)
                .Include(h => h.MansxNavigation)
                .ToList();

            return View(hanghoa);
        }

        public IActionResult XemHH(string id)
        {
            var hh = db.Hanghoas
                .Include(h => h.MaloaiNavigation)
                .Include(h => h.MansxNavigation)
                .FirstOrDefault(h => h.Mahang == id);

            if (hh == null)
            {
                return NotFound();
            }

            return View(hh);
        }

        [HttpGet]
        public IActionResult Them()
        {
            ViewBag.LoaiList = db.Loaihanghoas
                .Select(l => new SelectListItem { Value = l.Maloai, Text = $"{l.Maloai} - {l.Tenloai}" })
                .ToList();

            ViewBag.NsxList = db.Nhasanxuats
                .Select(n => new SelectListItem { Value = n.Mansx, Text = $"{n.Mansx} - {n.Tennsx}" })
                .ToList();

            return View();
        }

        [HttpPost]
        public IActionResult Them(Hanghoa hh, IFormFile HinhFile)
        {
            if (ModelState.IsValid)
            {
                if (HinhFile != null)
                {
                    var fileName = Path.GetFileName(HinhFile.FileName);
                    var imageFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images");

                    if (!Directory.Exists(imageFolder))
                    {
                        Directory.CreateDirectory(imageFolder);
                    }

                    var path = Path.Combine(imageFolder, fileName);
                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        HinhFile.CopyTo(stream);
                    }

                    hh.Hinh = fileName;
                }

                db.Hanghoas.Add(hh);
                db.SaveChanges();
                return RedirectToAction("Index");
            }


            // Nếu lỗi, load lại danh sách dropdown
            ViewBag.LoaiList = db.Loaihanghoas
                .Select(l => new SelectListItem { Value = l.Maloai, Text = $"{l.Maloai} - {l.Tenloai}" })
                .ToList();

            ViewBag.NsxList = db.Nhasanxuats
                .Select(n => new SelectListItem { Value = n.Mansx, Text = $"{n.Mansx} - {n.Tennsx}" })
                .ToList();

            return View(hh);
        }


    }
}
