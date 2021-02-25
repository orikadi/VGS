
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace VGS.Models
{
    public class Studio
    {
        [Key]
        public int StudioId { get; set; }
        [Display(Name = "Studio Name")]
        [Required]
        public string StudioName { get; set; }
        public ICollection<Game> Games { get; set; }
        [Required]
        public string Address { get; set; }
    }
}