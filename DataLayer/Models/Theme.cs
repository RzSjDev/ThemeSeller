using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace DataLayer
{
    public class Theme
    {
        [Key]
        public int ThemeID { get; set; }

        [Display(Name = "گروه قالب")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int GroupID { get; set; }

        [Display(Name = "عنوان قالب")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(250)]
        public string ThemeTitle { get; set; }
        [Display(Name = "توضیح مختصر")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(350)]
        [DataType(DataType.MultilineText)]
        public string ShortDescription { get; set; }

        [Display(Name = "متن")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [DataType(DataType.MultilineText)]
        [AllowHtml]
        public string Text { get; set; }

        [Display(Name = "بازدید")]
        public int Visit { get; set; }

        [Display(Name = "تصویر قالب")]
        public string ImageName { get; set; }


        [Display(Name = "تاریخ ایجاد")]
        [DisplayFormat(DataFormatString = "{0: yyyy/MM/dd}")]
        public DateTime CreateDate { get; set; }

        [Display(Name = "کلمات کلیدی")]
        public string Tags { get; set; }

        [Display(Name = "قیمت قالب")]
        public int Price { get; set; }
        public virtual ThemeGroup PageGroup { get; set; }

        public virtual List<PageComment> PageComments { get; set; }


        public Theme()
        {
            
        }

    }
}
