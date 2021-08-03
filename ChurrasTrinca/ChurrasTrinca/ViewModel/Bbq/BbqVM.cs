using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.CommunityToolkit.ObjectModel;

namespace ChurrasTrinca.ViewModel.Bbq
{
   public class BbqVM : BaseViewModel
    {
            
        public int id { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public DateTime date { get; set; }
        public double value_per_person { get; set; }

        public int Pessoas
        {
            get
            {
                if (_participantVM == null)
                    return 0;
                else
                    return _participantVM.Count;
            }
        }
        public ObservableCollection<ParticipantVM> _participantVM { get; set; }

    }
}

