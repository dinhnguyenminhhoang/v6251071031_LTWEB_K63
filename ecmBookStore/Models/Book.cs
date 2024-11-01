using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ecmBookStore.Models
{
    public class Book
    {
        [Key]
        public int Masach { get; set; }
        public string Tensach { get; set; }
        public decimal Giaban { get; set; }
        public string Mota { get; set; }
        public string Anhbia { get; set; }
        public DateTime? Ngaycapnhat { get; set; }
        public int Soluongton { get; set; }
        public int MaCD { get; set; }
        public int MaNXB { get; set; }
    }
}