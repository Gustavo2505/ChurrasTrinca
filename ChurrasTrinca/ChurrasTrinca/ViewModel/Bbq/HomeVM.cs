using ChurrasTrinca.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChurrasTrinca.ViewModel.Bbq
{
    public class AllBbqVM : BaseViewModel
    {
        public int id { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public DateTime date { get; set; }
        public double value_per_person { get; set; }

        public virtual List<Participants> Participants { get; set; }

    }
}
