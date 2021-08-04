using ChurrasTrinca.Views.Bbq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
            ButtonChangeStatus();
            var content = await Services.Service.ServiceClientInstance.AuthenticateUserAsync(Username.Text, Password.Text);



            if (!string.IsNullOrEmpty(content.token))
            {

                await Navigation.PushAsync(new ListOfBbq());

            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Erro", "Usuário ou senha invalidos", "Ok");

            }

             async Task ButtonChangeStatus()
            {
                btLogin.IsEnabled = false;               
                btLogin.TextColor = Color.White;
                btLogin.Text = "Entrando..";
                await Task.Delay(1000);
               
                btLogin.IsEnabled = true;
                btLogin.TextColor = Color.White;
                await Task.Delay(3000);
                btLogin.Text = "Entrar";

            }
            }
        }
    }
