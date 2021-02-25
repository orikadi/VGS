using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VGS.ViewModels
{
    public class ChartsViewModel
    {
        public ChartsViewModel()
        {
            GenreTotalPrice = new List<GenreCount>();
            StudiosProfit = new List<StudioProfit>();
        }

        public List<GenreCount> GenreTotalPrice { get; set; }
        public List<StudioProfit> StudiosProfit { get; set; }
    }

    public class GenreCount
    {
        public string Genre { get; set; }
        public double TotalPrice { get; set; }
    }
    public class StudioProfit
    {
        public string StudioName { get; set; }
        public double TotalProfit { get; set; }
    }
}