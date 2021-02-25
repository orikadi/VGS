using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VGS.ViewModels
{
    public class UserGameViewModel
    {
        public UserGameViewModel(string user, string game)
        {
            this.UserName = user;
            this.GameName = game;
        }

        public string UserName { get; set; }
        public string GameName { get; set; }
    }
}