using System;
using System.Collections.Generic;
using System.Text;

namespace ChurrasTrinca.Models
{
   public class Participants
    {
       public int id { get; set; }        
        public string name { get; set; }
        public bool confirmed { get; set; }
        public double value_paid { get; set; }
        public List<Bbq> BbqId { get; set; }
    }
}
