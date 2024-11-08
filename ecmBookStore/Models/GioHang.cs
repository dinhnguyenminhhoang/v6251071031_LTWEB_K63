using ecmBookStore.Models;
using System;
using System.Linq;

public class Giohang
{
    ecomBookStoreEntities data = new ecomBookStoreEntities();

    public string iMasach { get; set; }
    public string sTensach { get; set; }
    public string sAnhbia { get; set; }
    public Double dDongia { get; set; }
    public int iSoluong { get; set; }
    public Double dThanhtien
    {
        get { return iSoluong * dDongia; }
    }

    public Giohang(string Masach)
    {
        iMasach = Masach;
        SACH sach = data.SACH.Single(n => n.MaSach == iMasach);
        sTensach = sach.TenSach;
        sAnhbia = sach.AnhBia;
        dDongia = double.Parse(sach.GiaBan.ToString());
        iSoluong = 1;
    }
}
