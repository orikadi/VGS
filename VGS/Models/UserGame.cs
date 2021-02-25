using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace VGS.Models
{
    public class UserGame
    {
        [Key]
        public int UserGameId { get; set; }
        [ForeignKey("User")]
        public int UserId { get; set; }
        public User User { get; set; }
        [ForeignKey("Game")]
        public int GameId { get; set; }
        public Game Game { get; set; }
    }
}