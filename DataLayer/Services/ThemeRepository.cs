using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class ThemeRepository : IThemeRepository
    {
        private MyCmsContext db;

        public ThemeRepository(MyCmsContext context)
        {
            this.db = context;
        }
        public IEnumerable<Theme> GetAllTheme()
        {
            return db.Theme;
        }

        public Theme GetThemeById(int pageId)
        {
            return db.Theme.Find(pageId);
        }

        public bool InsertTheme(Theme page)
        {
            try
            {
                db.Theme.Add(page);
                return true;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }
        public bool UpdateTheme(Theme page)
        {
            try
            {
                db.Entry(page).State=EntityState.Modified;
                return true;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }
        public bool DeleteTheme(Theme page)
        {
            try
            {
                db.Entry(page).State = EntityState.Deleted;
                return true;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }
        public bool DeleteTheme(int pageId)
        {
            try
            {
                var theme = GetThemeById(pageId);
                DeleteTheme(theme);
                return true;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }
        public void Save()
        {
            db.SaveChanges();
        }

        public IEnumerable<Theme> TopTheme(int take = 4)
        {
            return db.Theme.OrderByDescending(p => p.Visit).Take(take);
        }
    

        public IEnumerable<Theme> LastTheme(int take = 4)
        {
            return db.Theme.OrderByDescending(p => p.CreateDate).Take(take);
        }

        public IEnumerable<Theme> ShowThemeByGroupId(int groupId)
        {
            return db.Theme.Where(p => p.GroupID == groupId);
        }

        public IEnumerable<Theme> SearchTheme(string search)
        {
            return
                db.Theme.Where(
                    p =>
                        p.ThemeTitle.Contains(search) || p.ShortDescription.Contains(search) || p.Tags.Contains(search) ||
                        p.Text.Contains(search)).Distinct();
        }


        public void Dispose()
        {
          db.Dispose();
        }
    }
}
