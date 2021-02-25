using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VGS.Models
{
    public class Game
    {

        public int GameId { get; set; }

        [Display(Name = "Game Name")]
        [Required]
        public string GameName { get; set; }
        [Required]
        public string Genre { get; set; }
        [ForeignKey("Studio")]
        public int StudioId { get; set; } //foreign key
        public Studio Studio { get; set; }

        [Display(Name = "Release Date")]
        [Required]
        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; } //change the underscore to nothing

        [Required]
        public double Price { get; set; }

        [Required]
        public string ImagePath { get; set; }

        public double Rating { get; set; }
        //game picture (datatype.imgurl)

        public virtual ICollection<UserGame> UserGames { get; set; }

    }
}