using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CoursatyApp.Entitities
{
    public class Category
    {
        public int Id { get; set; }
        [Required]
        [StringLength(250,MinimumLength =3)]
        public string Title { get; set; }
        public string Description{ get; set; }
        [Required]
        [Display(Name ="Thumbnail Image Path")]
        public string ImagePath { get; set; }

        [ForeignKey("CategoryId")]
        public virtual ICollection<CategoryItem> CategoryItems { get; set; }
        [ForeignKey("CategoryId")]
        public virtual ICollection<UserCategory> UserCategories { get; set; }
    }
}
