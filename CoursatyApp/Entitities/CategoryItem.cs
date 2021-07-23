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
        private DateTime _releaseDate = DateTime.MinValue;
        public int Id { get; set; }
        [Required]
        [StringLength(250, MinimumLength = 3)]
        public string Title { get; set; }
        [DisplayFormat(DataFormatString ="{0:yyyy-MM-dd}")]
        public DateTime ReleaseDate { get {
                return (_releaseDate == DateTime.MinValue) ? DateTime.Now : _releaseDate;
            } set {
                _releaseDate = value;
            } 
        }
        public string Description { get; set; }
        [NotMapped]
        public virtual ICollection<SelectListItem> MediaTypes { get; set; }
        public int CategoryId { get; set; }
        [Required(ErrorMessage ="Please select Media type from dropdown list")]
        [Display(Name ="Media Type")]
        public int MediaTypeId { get; set; }
        [NotMapped]
        public int ContentId { get; set; }
    }
}
