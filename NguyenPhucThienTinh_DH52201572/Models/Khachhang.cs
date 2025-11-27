using System;
using System.Collections.Generic;

namespace NguyenPhucThienTinh_DH52201572.Models
{
    public partial class Khachhang
    {
        public Khachhang()
        {
            Phieudathangs = new HashSet<Phieudathang>();
        }

        public string Makh { get; set; } = null!;
        public string? Tenkh { get; set; }
        public int? Namsinh { get; set; }
        public bool? Phai { get; set; }
        public string? Dienthoai { get; set; }
        public string? Diachi { get; set; }
        public string? Password { get; set; }

        public virtual ICollection<Phieudathang> Phieudathangs { get; set; }
    }
}
