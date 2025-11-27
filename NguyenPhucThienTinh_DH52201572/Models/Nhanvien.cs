using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace NguyenPhucThienTinh_DH52201572.Models
{
    public partial class Nhanvien
    {
        public Nhanvien()
        {
            Phieugiaohangs = new HashSet<Phieugiaohang>();
        }
        [Key]
        [DisplayName("Mã nhân viên")]
        [Required(ErrorMessage ="Mã nhân viên ko đc để trống!")]
        public string Manv { get; set; } = null!;
        [DisplayName("Tên nhân viên")]
        [Required(ErrorMessage = "Tên nhân viên ko đc để trống!")]
        public string? Tennv { get; set; }
        [DisplayName("Ngày sinh")]
        [Required(ErrorMessage = "Ngày sinh ko đc để trống!")]
        public DateTime? Ngaysinh { get; set; }
		[DisplayName("Phái")]
		[Required(ErrorMessage = "Phái ko đc để trống!")]
		public bool? Phai { get; set; }
		[DisplayName("Địa chỉ")]
		[Required(ErrorMessage = "Địa chỉ nhân viên ko đc để trống!")]
		public string? Diachi { get; set; }
		[DisplayName("Password")]
		[Required(ErrorMessage = "Password ko đc để trống!")]
		[MinLength(6, ErrorMessage = "Mật khẩu tối thiểu 6 ký tự")]
		public string? Password { get; set; }


        public virtual ICollection<Phieugiaohang> Phieugiaohangs { get; set; }
    }
}
