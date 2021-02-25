using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VGS.ViewModels
{
    public class StudioGenreCountViewModel
    {
        public StudioGenreCountViewModel(string studioName, string genre, double count)
        {
            StudioName = studioName;
            Genre = genre;
            Count = count;
        }

        public string StudioName { get; set; }
        public string Genre { get; set; }
        public double Count { get; set; }
    }
}
