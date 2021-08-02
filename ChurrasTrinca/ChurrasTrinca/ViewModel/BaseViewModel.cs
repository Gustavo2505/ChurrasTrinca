
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;

namespace ChurrasTrinca.ViewModel
{

    public class BaseViewModel : ObservableObject
    {
        public readonly INavigation Navigation;

        public BaseViewModel(INavigation navigation = null)
        {
            Navigation = navigation;
        }
    }
}
