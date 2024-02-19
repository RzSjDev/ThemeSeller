using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class ThemeGroup
    {
        [Key]
        public int GroupID { get; set; }

        [Display(Name = "عنوان گروه")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(150)]
        public string GroupTitle { get; set; }

        [Display(Name ="تصویر گروه قالب")]
        public string ImageTitleGroup { get; set; }

        
        public virtual List<Theme> Themes { get; set; }

        public ThemeGroup()
        {
            
        }

    }
}
