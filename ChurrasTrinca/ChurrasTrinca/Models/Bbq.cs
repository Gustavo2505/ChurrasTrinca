using ChurrasTrinca.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChurrasTrinca.Models
{
   public class Bbq : BaseViewModel
    {
        public int id { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public DateTime date { get; set; }
        public double value_per_person { get; set; }
        
        public List<Participants> Participants { get; set; }

    }
}
