using ecmBookStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ecmBookStore.Controllers
{
    public class GiohangController : Controller
    {
        private ecomBookStoreEntities db = new ecomBookStoreEntities();
        public List<Giohang> LayGioHang()
        {
            List<Giohang> listGiohang = Session["Giohang"] as List<Giohang>;
            if (listGiohang == null)
            {
                listGiohang = new List<Giohang>();
                Session["Giohang"] = listGiohang;  
            }
            return listGiohang;
        }
        public ActionResult ThemGioHang(string MaSach, string strUrl)
        {
            List<Giohang> listGioHang = LayGioHang();
            if (listGioHang == null)
            {
                listGioHang = new List<Giohang>(); 
            }

            Giohang sanpham = listGioHang.Find(n => n.iMasach == MaSach);

            if (sanpham == null)
            {
                sanpham = new Giohang(MaSach);
                listGioHang.Add(sanpham);
            }
            else
            {
                sanpham.iSoluong++;
            }
            return Redirect(strUrl);
        }

        private double TongSoLuong()
        {
            double iTongSoLuong = 0;
            List<Giohang> listGioHang = Session["Giohang"] as List<Giohang>;
            if(listGioHang != null)
            {
                iTongSoLuong = listGioHang.Sum(n => n.iSoluong);
            }
            return iTongSoLuong;
        }
        private double Tongtien()
        {
            double iTongTien = 0;
            List<Giohang> listGioHang = Session["Giohang"] as List<Giohang>;
            if (listGioHang != null)
            {
                iTongTien = listGioHang.Sum(n => n.dThanhtien);
            }
            return iTongTien;
        }
        public ActionResult GioHang()
        {
            List<Giohang> listGioHang = LayGioHang();
            if(listGioHang == null)
            {
                return RedirectToAction("Index", "BookStore");
            }
            ViewBag.TongSoLuong = TongSoLuong();
            ViewBag.TongTien = Tongtien();
            return View(listGioHang);
        }
        public ActionResult GioHangPartial()
        {
            ViewBag.TongSoLuong = TongSoLuong();
            ViewBag.TongTien = Tongtien();
            return PartialView();
        }
        public ActionResult XoaGiohang(string Masp)
        {
            List<Giohang> listGioHang = Session["Giohang"] as List<Giohang>;

            if (listGioHang != null)
            {
                Giohang sanpham = listGioHang.Find(n => n.iMasach == Masp);

                if (sanpham != null)
                {
                    listGioHang.Remove(sanpham);
                }
                Session["Giohang"] = listGioHang;
            }
            return RedirectToAction("GioHang");
        }

        public ActionResult CapnhatGiohang(string iMaSP, FormCollection f)
        {
            List<Giohang> listGioHang = LayGioHang();
            Giohang sanphan = listGioHang.SingleOrDefault(n=>n.iMasach== iMaSP);
            if (sanphan != null) {
                sanphan.iSoluong = int.Parse(f["txtSoLuong"].ToString());
            }
            return RedirectToAction("GioHang");
        }
        public ActionResult XoaTatcaGiohang()
        {
            List<Giohang> listGioHang = LayGioHang();
            listGioHang.Clear();
            return RedirectToAction("Index", "BookStore");
        }
        public ActionResult DatHang()
        {
            if (Session["TaiKhoan"] == null || Session["TaiKhoan"].ToString() == "")
            {
                return RedirectToAction("DangNhap", "Nguoidung");
            }
            if (Session["Giohang"] == null)
            {
                return RedirectToAction("Index", "BookStore");
            }
            List<Giohang> listGioHang = LayGioHang();
            ViewBag.TongSoLuong = TongSoLuong();
            ViewBag.TongTien = Tongtien();
            return View(listGioHang);
        }
        [HttpPost]
        public ActionResult DatHang(FormCollection collection)
        {
            DONDATHANG ddh = new DONDATHANG();
            KHACHHANG kh = (KHACHHANG)Session["TaiKhoan"];
            List<Giohang>  listGioHang= LayGioHang();
            ddh.MaKH = kh.MaKH;
            ddh.NgayDat = DateTime.Now;
            var ngayGiao = String.Format("{0:MM/dd/yyyy}", collection["Ngaygiao"]);
            ddh.NgayDat = DateTime.Parse(ngayGiao);
            ddh.TinhTrangGiaoHang = "Chua giao hang";
            ddh.DaThanhToan = false;
            db.DONDATHANG.Add(ddh);
            if(listGioHang.Count > 0)
            {
            foreach (var item in listGioHang)
            {
                CHITIETDONDATHANG ctdh = new CHITIETDONDATHANG();
                ctdh.DonGia =(decimal) item.dDongia;
                ctdh.SoLuong = item.iSoluong;
                ctdh.MaDonHang = ddh.MaDonHang;
                ctdh.MaSach = item.iMasach;
                db.CHITIETDONDATHANG.Add(ctdh);
            }
            Session["Giohang"] = null;
            }
            return RedirectToAction("Xacnhandonhang", "GioHang");
        }
        public ActionResult Xacnhandonhang()
        {
            return View();
        }
        public ActionResult Index()
        {
            return View();
        }
    }
}