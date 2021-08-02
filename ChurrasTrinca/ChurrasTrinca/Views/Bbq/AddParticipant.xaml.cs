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
    public partial class AddParticipant : ContentPage
    {
        public AddParticipant()
        {
            InitializeComponent();
        }


        private async void BtnCadastrar(object sender, EventArgs e)
        {

            var convert = Convert.ToDouble(value_paid.Text);

            Boolean convert_boolean = Convert.ToBoolean(confirmed);

            //TODO: criar check box com o valor 1 e 2 true and false para ser passado
            ResponseService<Models.Participants> responseService = await Services.Service.ServiceClientInstance.PostUser(name.Text, convert_boolean, convert);
            await DisplayAlert("Sucesso", "Usuario cadastrado com sucesso", "OK");

            await Navigation.PopModalAsync();
        }
    }
}