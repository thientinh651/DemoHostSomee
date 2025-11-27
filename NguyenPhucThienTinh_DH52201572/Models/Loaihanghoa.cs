using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace NguyenPhucThienTinh_DH52201572.Models
{
    public partial class Loaihanghoa
    {
        public Loaihanghoa()
        {
            Hanghoas = new HashSet<Hanghoa>();
        }
        [StringLength(50)]
        [DisplayName("Mã loại hàng")]
        [Required(ErrorMessage = "Xin nhập mã loại hàng!")]

        public string Maloai { get; set; } = null!;
        [StringLength(50)]
        [DisplayName("Tên loại hàng")]
        [Required(ErrorMessage = "Xin nhập tên loại hàng!")]

        public string? Tenloai { get; set; }

        public virtual ICollection<Hanghoa> Hanghoas { get; set; }
    }
}
