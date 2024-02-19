using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DataLayer;
using System.IO;

namespace ThemeSeller.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ThemeGroupsController : Controller
    {
        private IThemeGroupRepository ThemeGroupRepository;
        MyCmsContext db=new MyCmsContext();
        public ThemeGroupsController()
        {
            ThemeGroupRepository=new ThemeGroupRepository(db);
        }

        // GET: Admin/PageGroups
        public ActionResult Index()
        {
            return View(ThemeGroupRepository.GetAllGroups());
        }

      //  [AllowAnonymous]
        // GET: Admin/PageGroups/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ThemeGroup pageGroup = ThemeGroupRepository.GetGroupById(id.Value);
            if (pageGroup == null)
            {
                return HttpNotFound();
            }
            return View(pageGroup);
        }

        // GET: Admin/PageGroups/Create
        public ActionResult Create()
        {
            return PartialView();
        }

        // POST: Admin/PageGroups/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "GroupID,GroupTitle,ImageTitleGroup")] ThemeGroup pageGroup, HttpPostedFileBase imgUp)
        {
            if (ModelState.IsValid)
            {
 
                if (imgUp != null)
                {
                    pageGroup.ImageTitleGroup = Guid.NewGuid() + Path.GetExtension(imgUp.FileName);
                    imgUp.SaveAs(Server.MapPath("/GroupImage/" + pageGroup.ImageTitleGroup));
                }
                ThemeGroupRepository.InsertGroup(pageGroup);
                ThemeGroupRepository.save();
                return RedirectToAction("Index");
            }

            return View(pageGroup);
        }

        // GET: Admin/PageGroups/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ThemeGroup pageGroup = ThemeGroupRepository.GetGroupById(id.Value);
            if (pageGroup == null)
            {
                return HttpNotFound();
            }
            return PartialView(pageGroup);
        }

        // POST: Admin/PageGroups/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "GroupID,GroupTitle,ImageTitleGroup")] ThemeGroup pageGroup, HttpPostedFileBase imgUp)
        {
            if (ModelState.IsValid)
            {
                if (imgUp != null)
                {
                    if (pageGroup.ImageTitleGroup != null)
                    {
                        System.IO.File.Delete(Server.MapPath("/GroupImage/" + pageGroup.ImageTitleGroup));
                    }


                    pageGroup.ImageTitleGroup = Guid.NewGuid() + Path.GetExtension(imgUp.FileName);
                    imgUp.SaveAs(Server.MapPath("/GroupImage/" + pageGroup.ImageTitleGroup));
                }

                ThemeGroupRepository.UpdateGroup(pageGroup);
                ThemeGroupRepository.save();
                return RedirectToAction("Index");
            }
            return View(pageGroup);
        }

        // GET: Admin/PageGroups/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ThemeGroup pageGroup = ThemeGroupRepository.GetGroupById(id.Value);
            if (pageGroup == null)
            {
                return HttpNotFound();
            }
            return PartialView(pageGroup);
        }

        // POST: Admin/PageGroups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var groupImage = ThemeGroupRepository.GetGroupById(id);
            if (groupImage.ImageTitleGroup != null)
            {
                System.IO.File.Delete(Server.MapPath("/GroupImage/" + groupImage.ImageTitleGroup));
            }
            ThemeGroupRepository.DeleteGroup(id);
            ThemeGroupRepository.save();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                ThemeGroupRepository.Dispose();
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
