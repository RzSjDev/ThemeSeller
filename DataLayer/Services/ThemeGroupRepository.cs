using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class ThemeGroupRepository : IThemeGroupRepository
    {

        private MyCmsContext db;

        public ThemeGroupRepository(MyCmsContext context)
        {
            this.db = context;
        }
        public IEnumerable<ThemeGroup> GetAllGroups()
        {
            return db.ThemeGroups;
        }

        public ThemeGroup GetGroupById(int groupId)
        {
            return db.ThemeGroups.Find(groupId);
        }

        public bool InsertGroup(ThemeGroup pageGroup)
        {
            try
            {
                db.ThemeGroups.Add(pageGroup);
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public bool UpdateGroup(ThemeGroup pageGroup)
        {
            try
            {
               db.Entry(pageGroup).State=EntityState.Modified;
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public bool DeleteGroup(int groupId)
        {
            try
            {
                var group = GetGroupById(groupId);
                DeleteGroup(group);
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public bool DeleteGroup(ThemeGroup pageGroup)
        {
            try
            {
                db.Entry(pageGroup).State=EntityState.Deleted;
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

     

        public void save()
        {
            db.SaveChanges();
        }

        public IEnumerable<ShowGroupViewModel> GetGroupsForView()
        {
            return db.ThemeGroups.Select(g => new ShowGroupViewModel()
            {
                GroupID = g.GroupID,
                GroupThemeTitle = g.GroupTitle,
                ImageGroup=g.ImageTitleGroup,
                ThemeCount = g.Themes.Count,
            });
        }


        public void Dispose()
        {
            db.Dispose();
        }
    }
}
