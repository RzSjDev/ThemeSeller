using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DataLayer;

namespace ThemeSeller.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ThemeController : Controller
    {
        private IThemeRepository ThemeRepository;
        private IThemeGroupRepository ThemeGroupRepository;
        private MyCmsContext db = new MyCmsContext();


        public ThemeController()
        {
            ThemeRepository=new ThemeRepository(db);
            ThemeGroupRepository=new ThemeGroupRepository(db);
        }

        // GET: Admin/Pages
        public ActionResult Index()
        {
            return View(ThemeRepository.GetAllTheme());
        }

        // GET: Admin/Pages/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Theme theme = db.Theme.Find(id);
            if (theme == null)
            {
                return HttpNotFound();
            }
            return View(theme);
        }

        // GET: Admin/Pages/Create
        public ActionResult Create()
        {
            ViewBag.GroupID = new SelectList(ThemeGroupRepository.GetAllGroups(), "GroupID", "GroupTitle");
            return View();
        }

        
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ThemeID,GroupID,ThemeTitle,ShortDescription,Text,Visit,ImageName,Price,CreateDate,Tags")] Theme theme,HttpPostedFileBase imgUp)
        {
            if (ModelState.IsValid)
            {
                theme.Visit = 0;
                theme.CreateDate=DateTime.Now;

                if (imgUp != null)
                {
                    theme.ImageName = Guid.NewGuid() + Path.GetExtension(imgUp.FileName);
                    imgUp.SaveAs(Server.MapPath("/ThemeImages/" + theme.ImageName));
                }

                ThemeRepository.InsertTheme(theme);
                ThemeRepository.Save();
                return RedirectToAction("Index");
            }

            ViewBag.GroupID = new SelectList(db.ThemeGroups, "GroupID", "GroupTitle", theme.GroupID);
            return View(theme);
        }

        // GET: Admin/Pages/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Theme theme = ThemeRepository.GetThemeById(id.Value);
            if (theme == null)
            {
                return HttpNotFound();
            }
            ViewBag.GroupID = new SelectList(ThemeGroupRepository.GetAllGroups(), "GroupID", "GroupTitle", theme.GroupID);
            return View(theme);
        }

        // POST: Admin/Pages/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ThemeID,GroupID,ThemeTitle,ShortDescription,Text,Visit,ImageName,Price,CreateDate,Tags")] Theme theme,HttpPostedFileBase imgUp)
        {
            if (ModelState.IsValid)
            {
                if (imgUp != null)
                {
                    if (theme.ImageName != null)
                    {
                        System.IO.File.Delete(Server.MapPath("/ThemeImages/" + theme.ImageName));
                    }


                    theme.ImageName = Guid.NewGuid() + Path.GetExtension(imgUp.FileName);
                    imgUp.SaveAs(Server.MapPath("/ThemeImages/" + theme.ImageName));
                }


                ThemeRepository.UpdateTheme(theme);
                ThemeRepository.Save();
                return RedirectToAction("Index");
            }
            ViewBag.GroupID = new SelectList(db.ThemeGroups, "GroupID", "GroupTitle", theme.GroupID);
            return View(theme);
        }

        // GET: Admin/Pages/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Theme theme = ThemeRepository.GetThemeById(id.Value);
            if (theme == null)
            {
                return HttpNotFound();
            }
            return View(theme);
        }

        // POST: Admin/Pages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var theme = ThemeRepository.GetThemeById(id);
            if (theme.ImageName != null)
            {
                System.IO.File.Delete(Server.MapPath("/ThemeImages/" + theme.ImageName));
            }
            ThemeRepository.DeleteTheme(theme);
            ThemeRepository.Save();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                ThemeGroupRepository.Dispose();
                ThemeRepository.Dispose();
                db.Dispose();
               
               
            }
            base.Dispose(disposing);
        }
    }
}
