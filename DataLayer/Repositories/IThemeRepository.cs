using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public interface IThemeRepository:IDisposable
    {
        IEnumerable<Theme> GetAllTheme();
        Theme GetThemeById(int pageId);
        bool InsertTheme(Theme page);
        bool UpdateTheme(Theme page);
        bool DeleteTheme(Theme page);
        bool DeleteTheme(int pageId);
        void Save();

        IEnumerable<Theme> TopTheme(int take = 4);
        IEnumerable<Theme> LastTheme(int take = 4);
        IEnumerable<Theme> ShowThemeByGroupId(int groupId);
        IEnumerable<Theme> SearchTheme(string search);

    }
}
