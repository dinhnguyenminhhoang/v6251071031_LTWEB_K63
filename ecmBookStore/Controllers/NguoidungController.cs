using ecmBookStore.Models;
using System;
using System.Linq;
using System.Web.Mvc;

namespace ecmBookStore.Controllers
{
    public class NguoidungController : Controller
    {
        private ecomBookStoreEntities db = new ecomBookStoreEntities();

        // GET: Nguoidung
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult DangKi()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DangKi(FormCollection collection, KHACHHANG kh)
        {
            var hoten = collection["HotenKH"];
            var tendn = collection["TenDN"];
            var matkhau = collection["Matkhau"];
            var matkhaunhaplai = collection["Matkhaunhaplai"];
            var diachi = collection["Diachi"];
            var email = collection["Email"];
            var dienthoai = collection["Dienthoai"];
            var ngaysinh = collection["Ngaysinh"];

            bool isValid = true;

            if (string.IsNullOrEmpty(hoten))
            {
                ViewData["Loi1"] = "Họ tên không được để trống";
                isValid = false;
            }
            if (string.IsNullOrEmpty(tendn))
            {
                ViewData["Loi2"] = "Phải nhập tên đăng nhập";
                isValid = false;
            }
            if (string.IsNullOrEmpty(matkhau))
            {
                ViewData["Loi3"] = "Phải nhập mật khẩu";
                isValid = false;
            }
            if (string.IsNullOrEmpty(matkhaunhaplai) || matkhau != matkhaunhaplai)
            {
                ViewData["Loi4"] = "Mật khẩu nhập lại không khớp";
                isValid = false;
            }
            if (string.IsNullOrEmpty(email))
            {
                ViewData["Loi5"] = "Phải nhập email";
                isValid = false;
            }
            if (string.IsNullOrEmpty(dienthoai))
            {
                ViewData["Loi6"] = "Phải nhập số điện thoại";
                isValid = false;
            }
            if (string.IsNullOrEmpty(diachi))
            {
                ViewData["Loi7"] = "Phải nhập địa chỉ";
                isValid = false;
            }
            if (isValid)
            {
                try
                {
                    var CheckMaKH = db.KHACHHANG.Count() + 1;
                    kh.HoTen = hoten;
                    kh.TaiKhoan = tendn;
                    kh.MatKhau = matkhau;
                    kh.DiachiKH = diachi;
                    kh.Email = email;
                    kh.DienThoaiKH = dienthoai;
                    kh.MaKH = "KH00" + CheckMaKH.ToString();

                    if (DateTime.TryParse(ngaysinh, out DateTime parsedNgaySinh))
                    {
                        kh.NgaySinh = parsedNgaySinh;
                    }

                    db.KHACHHANG.Add(kh);
                    db.SaveChanges();

                    return RedirectToAction("dangnhap");
                }
                catch (System.Data.Entity.Validation.DbEntityValidationException ex)
                {
                    foreach (var validationErrors in ex.EntityValidationErrors)
                    {
                        foreach (var validationError in validationErrors.ValidationErrors)
                        {
                            ModelState.AddModelError("", $"{validationError.PropertyName}: {validationError.ErrorMessage}");
                        }
                    }
                    return View();
                }
            }

            return View();
        }
        [HttpGet]
        public ActionResult DangNhap()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DangNhap(FormCollection collection, KHACHHANG kh)
        {
            var tendn = collection["TenDN"];
            var matkhau = collection["Matkhau"];
            bool isValid = true;

            if (string.IsNullOrEmpty(tendn))
            {
                ViewData["Loi1"] = "Phải nhập tên đăng nhập";
                isValid = false;
            }

            if (string.IsNullOrEmpty(matkhau))
            {
                ViewData["Loi2"] = "Phải nhập mật khẩu";
                isValid = false;
            }

            if (isValid)
            {
                try
                {
                    var CheckKH = db.KHACHHANG.FirstOrDefault(n => n.TaiKhoan == tendn && n.MatKhau == matkhau);
                    if (CheckKH != null)
                    {
                        ViewBag.ThongBao = "Chúc mừng đăng nhập thành công";
                        Session["TaiKhoan"] = CheckKH;
                    }
                    else
                    {
                        ViewBag.ThongBao = "Tên đăng nhập hoặc mật khẩu sai";
                    }
                    return View();
                }
                catch (System.Data.Entity.Validation.DbEntityValidationException ex)
                {
                    foreach (var validationErrors in ex.EntityValidationErrors)
                    {
                        foreach (var validationError in validationErrors.ValidationErrors)
                        {
                            ModelState.AddModelError("", $"{validationError.PropertyName}: {validationError.ErrorMessage}");
                        }
                    }
                    return View();
                }
            }

            return View();
        }
    }
}
