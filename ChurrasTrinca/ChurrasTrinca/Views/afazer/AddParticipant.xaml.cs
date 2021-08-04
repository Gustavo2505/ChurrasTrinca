using ChurrasTrinca.Models.ResponseService;
using ChurrasTrinca.ViewModel.Bbq;
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
        ParticipantVM _vm;
        Action<ParticipantVM> _Include;
        public AddParticipant(ParticipantVM vm, Action<ParticipantVM> include)
        {
            InitializeComponent();
            _vm = vm;
            _Include = include;
        }


        private async void BtnCadastrar(object sender, EventArgs e)
        {
            var conversor = Convert.ToInt32(value_paid.Text);
       

            


            _vm.name = name.Text;
            _vm.value_paid = conversor;
            _vm.confirmed = false;

            _Include(_vm);
            if (name.Text != null)
            {
                await DisplayAlert("Sucesso", "Participante inserido com sucesso", "OK");
            }
            else
            {
                await DisplayAlert("Erro", "insira um participante", "OK");
                return;
            }


            await Navigation.PopAsync();


            /*     var convert = Convert.ToDouble(value_paid.Text);

                 Boolean convert_boolean = Convert.ToBoolean(confirmed);

                 //TODO: criar check box com o valor 1 e 2 true and false para ser passado
                 ResponseService<Models.Participants> responseService = await Services.Service.ServiceClientInstance.PostUser(name.Text, convert_boolean, convert);
                 await DisplayAlert("Sucesso", "Usuario cadastrado com sucesso", "OK");

                 await Navigation.PopModalAsync();*/
        }

        private async void BtnCancel(object sender, EventArgs e)
        {


            bool Alerta = await DisplayAlert("Atenção", "Caso cancele, perderá todos os campos preenchidos", "Sim", "Não");
            if (Alerta == true)
            {
                await Navigation.PopModalAsync();

            }
        }
    }
}