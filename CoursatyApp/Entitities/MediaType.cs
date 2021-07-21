using CoursatyApp.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CoursatyApp.Entitities
{
    public class MediaType : IPrimaryProperties
    {
        public int Id { get; set; }
        [Required]
        [StringLength(250, MinimumLength = 3)]
        public string Title { get; set; }
        [Required]
        public string ImagePath { get; set; }

        [ForeignKey("MediaTypeId")]
        public virtual ICollection<CategoryItem> CategoryItems { get; set; }
    }
}
