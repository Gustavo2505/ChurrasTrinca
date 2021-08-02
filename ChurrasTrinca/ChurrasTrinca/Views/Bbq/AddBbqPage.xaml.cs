using ChurrasTrinca.Models.ResponseService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ChurrasTrinca.Views.Bbq
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddBbqPage : ContentPage
    {
        public AddBbqPage()
        {
            InitializeComponent();
            BindingContext = this;
        }

        private async void BtnCadastrar(object sender, EventArgs e)
        {

            var teste = Convert.ToDouble(valorpago.Text);           

            ResponseService<Models.Bbq> responseService = await Services.Service.ServiceClientInstance.PostBbq(titulo.Text, descricao.Text, date.Date, teste);

            await DisplayAlert("Sucesso", "Item inserido", "OK");

            await Navigation.PopModalAsync();


        }

        private void BtnAddUser(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new Views.Bbq.AddParticipant());
        }
    }
}