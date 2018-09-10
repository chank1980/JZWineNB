using JZWineNB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JZWineNB.viewmodels
{
    public class CommentWineVM
    {
        //Comment Model
        public int CommentID { get; set; }
        public Nullable<decimal> Rate { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
        public string Comment1 { get; set; }
        public int DrinkerID { get; set; }
        public int WineID { get; set; }

        public virtual Drinker Drinker { get; set; }
        public virtual Wine Wine { get; set; }

        //Wine Model
        //public int WineID { get; set; }
        public string Name { get; set; }
        public string WineType { get; set; }
        public string Grape { get; set; }
        public string Region { get; set; }
        public string Country { get; set; }
        public string Description { get; set; }
        public string Body { get; set; }
        public Nullable<decimal> AlcoholVol { get; set; }
        public Nullable<decimal> RateOverall { get; set; }
        public string WineImage { get; set; }
    }
}