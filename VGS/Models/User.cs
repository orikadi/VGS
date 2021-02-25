using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VGS.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Display(Name = "User Name")]
        [Required]
        public string UserName { get; set; }

        public virtual ICollection<UserGame> UserGames { get; set; }

        [Required]
        public int Age { get; set; }

        [DataType(DataType.EmailAddress)]
        [Required]
        public string Email { get; set; }

        public double Balance { get; set; }

        [Required]
        public string Password { get; set; }

        public int UserType { get; set; } // 0 - user, 1 - admin
    }
}