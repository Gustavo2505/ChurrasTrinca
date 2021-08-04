using ChurrasTrinca.ViewModel.Bbq;
using ChurrasTrinca.Views;
using ChurrasTrinca.Views.Bbq;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ChurrasTrinca
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
          
            MainPage = new NavigationPage(new Views.Login.LoginPage());


        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
