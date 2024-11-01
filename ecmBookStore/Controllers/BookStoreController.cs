using ecmBookStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace ecmBookStore.Controllers
{
    public class BookStoreController : Controller
    {
        private ecomBookStoreEntities db = new ecomBookStoreEntities();
        public ActionResult Index()
        {
            var books = db.SACH.ToList();
            ViewBag.Books = books;
            return View();
        }
        public ActionResult ChuDe()
        {
            var chuDe = db.CHUDE.ToList();
            return PartialView(chuDe);
        }
        public ActionResult NhaXuatBan()
        {
            var nhaXuatBan = db.NHAXUATBAN.ToList();
            return PartialView(nhaXuatBan); 
        }
        public ActionResult Details(string id)
        {
            var book = db.SACH.FirstOrDefault(b => b.MaSach == id); 
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book); 
        }
        public ActionResult SpTheoNSX(string id)
        {
            var books = db.SACH.Where(b => b.MaNXB == id).ToList();

            if (books == null || !books.Any())
            {
                return HttpNotFound(); 
            }
            ViewBag.Books = books;
            return View(); 
        }
        public ActionResult SpTheoChuDe(string id)
        {
            var books = db.SACH.Where(b => b.MaCD == id).ToList();

            if (books == null || !books.Any())
            {
                return HttpNotFound();
            }
            ViewBag.Books = books;
            return View();
        }
    }
}