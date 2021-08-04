using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.CommunityToolkit.ObjectModel;

namespace ChurrasTrinca.ViewModel.Bbq
{
   public class ParticipantVM : BaseViewModel
    {
        private bool _IsSelecionado;
        public int id { get; set; }
        public string name { get; set; }
        public bool confirmed
        {
            get
            {
                return _IsSelecionado;
            }
            set
            {
                SetProperty(ref _IsSelecionado, value);
            }
        }
        public int value_paid { get; set; }
        public int bbq_id { get; set; }


    }
}

