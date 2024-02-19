using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataLayer;

namespace ThemeSeller.Controllers
{
    public class SearchController : Controller
    {
        private IThemeRepository themeRepository;
        MyCmsContext db=new MyCmsContext();

        public SearchController()
        {
            themeRepository=new ThemeRepository(db);
        }
        // GET: Search
        public ActionResult Index(string q)
        {
            ViewBag.Name = q;
            return View(themeRepository.SearchTheme(q));
        }
    }
}