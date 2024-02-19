using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataLayer;

namespace ThemeSeller.Controllers
{
    public class ThemeController : Controller
    {
        MyCmsContext db=new MyCmsContext();
        private IThemeGroupRepository pageGroupRepository;
        private IThemeRepository pageRepository;
        private IPageCommentRepository pageCommentRepository;

        public ThemeController()
        {
            
            pageGroupRepository=new ThemeGroupRepository(db);
            pageRepository=new ThemeRepository(db);
            pageCommentRepository=new PageCommentRepository(db);
        }

        public ActionResult ShowGroups()
        {
            return PartialView(pageGroupRepository.GetGroupsForView());           
        }

        public ActionResult ShowGroupsInMenu()
        {
            return PartialView(pageGroupRepository.GetAllGroups());
        }

        public ActionResult mostSellingTheme()
        {
            return PartialView(pageRepository.TopTheme());
        }

        public ActionResult LatesThemeforSale()
        {
            return PartialView(pageRepository.LastTheme());
        }

        [Route("Archive")]
        public ActionResult ArchiveThemes()
        {
            return View(pageRepository.GetAllTheme());
        }

        [Route("Group/{id}/{title}")]
        public ActionResult ShowThemeByGroupId(int id, string title)
        {
            ViewBag.name = title;
            return View(pageRepository.ShowThemeByGroupId(id));
        }

        [Route("Theme/{id}")]
        public ActionResult ShowThemes(int id)
        {
            var theme = pageRepository.GetThemeById(id);

            if (theme == null)
            {
                return HttpNotFound();
            }

            theme.Visit += 1;
            pageRepository.UpdateTheme(theme);
            pageRepository.Save();

            return View(theme);
        }

        public ActionResult AddComment(int id, string name, string email, string comment)
        {
            PageComment addcomment=new PageComment()
            {
                CreateDate = DateTime.Now,
                PageID = id,
                Comment = comment,
                Email = email,
                Name = name
            };
            pageCommentRepository.AddComment(addcomment);

            return PartialView("ShowComments",pageCommentRepository.GetCommentByThemeId(id));
        }

        public ActionResult ShowComments(int id)
        {
            return PartialView(pageCommentRepository.GetCommentByThemeId(id));
        }
    }
}