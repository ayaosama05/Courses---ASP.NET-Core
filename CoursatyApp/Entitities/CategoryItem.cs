using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations.Schema;
using CoursatyApp.Interfaces;

namespace CoursatyApp.Entitities
{
    public class CategoryItem
    {
        public int Id { get; set; }
        [Required]
        [StringLength(250, MinimumLength = 3)]
        public string Title { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string Description { get; set; }
        [NotMapped]
        public virtual ICollection<SelectListItem> MediaTypes { get; set; }
        public int CategoryId { get; set; }
        public int MediaTypeId { get; set; }
        [NotMapped]
        public int ContentId { get; set; }
    }
}
