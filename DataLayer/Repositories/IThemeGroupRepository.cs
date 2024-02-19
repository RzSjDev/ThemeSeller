using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public interface IThemeGroupRepository:IDisposable
    {
        IEnumerable<ThemeGroup> GetAllGroups();
        ThemeGroup GetGroupById(int groupId);
        bool InsertGroup(ThemeGroup pageGroup);
        bool UpdateGroup(ThemeGroup pageGroup);
        bool DeleteGroup(ThemeGroup pageGroup);
        bool DeleteGroup(int groupId);
        void save();

        IEnumerable<ShowGroupViewModel> GetGroupsForView();
    }
}
