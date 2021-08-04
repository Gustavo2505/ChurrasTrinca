using ChurrasTrinca.Views.Bbq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ChurrasTrinca.Views.Login
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
      
        }
        private async void BtnLogin(object sender, EventArgs e)
        {

            var content = await Services.Service.ServiceClientInstance.AuthenticateUserAsync(Username.Text, Password.Text);



            if (!string.IsNullOrEmpty(content.token))
            {

                await Navigation.PushAsync(new ListOfBbq());

            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Erro", "Usuário ou senha invalidos", "Ok");

            }
        }
    }
}