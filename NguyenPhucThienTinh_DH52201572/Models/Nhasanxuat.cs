using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace NguyenPhucThienTinh_DH52201572.Models
{
    public partial class Nhasanxuat
    {
        public Nhasanxuat()
        {
            Hanghoas = new HashSet<Hanghoa>();
        }
        [Key]
        [DisplayName("Mã nhà sản xuất")]
        [Required(ErrorMessage = "Mã nhà sản xuất ko đc để trống!")]
        public string Mansx { get; set; } = null!;
        [DisplayName("Tên nhà sản xuất")]
        [Required(ErrorMessage = "Tên nhà sản xuất ko đc để trống!")]
        public string? Tennsx { get; set; }
        [DisplayName("Địa chỉ")]
        [Required(ErrorMessage = "Địa chỉ nhà sản xuất ko đc để trống!")]
        public string? Diachi { get; set; }

        public virtual ICollection<Hanghoa> Hanghoas { get; set; }
    }
}
