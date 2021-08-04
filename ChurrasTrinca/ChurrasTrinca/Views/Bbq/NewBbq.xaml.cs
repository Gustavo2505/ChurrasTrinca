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
    public partial class NewBbq : ContentPage
    {
        BbqVM _vm;
        Action<Models.Bbq> _saveBbq;
        public NewBbq(BbqVM vm, Action<Models.Bbq> saveBbq)
        {          
            InitializeComponent();
            _vm = vm;
            _saveBbq = saveBbq;
            tituloV.Text = _vm.title;
            descricao.Text = _vm.description;
            ValorPorPessoa.Text = Convert.ToString(_vm.value_per_person);
            date.Date = _vm.date;
    

        } 

        private async void BtnNext_Clicked(object sender, EventArgs e)
        {
            var teste = tituloV;
            var rx = new System.Text.RegularExpressions.Regex("^[a-zA-Z0-9-]*$");


            if (teste.Text == null)
            {
                await DisplayAlert("Erro", "Digite um nome", "OK");
                tituloV.Focus();
                return;

            }

            if (!rx.IsMatch(tituloV.Text))
            {
                await DisplayAlert("Erro", "Nome inválido", "OK");
                tituloV.Focus();
                return;
            }

            _vm.title = tituloV.Text;
            _vm.description = descricao.Text;
            _vm.value_per_person = Convert.ToDouble(ValorPorPessoa.Text);
            _vm.date = date.Date;


            Models.Bbq ev = new Models.Bbq
            {
                title = _vm.title,
                description = _vm.description,
                date = _vm.date,
                Participants = new List<Models.Participants>(),
                value_per_person = _vm.value_per_person,
                id = _vm.id

            };

            if (_vm.id == 0)
            {
                ResponseService<Models.Bbq> responseService = await Services.Service.ServiceClientInstance.PostBbq(ev);
         
                await Navigation.PushAsync(new ListOfBbq());
            }

            else
            {
                ResponseService<Models.Bbq> responseService = await Services.Service.ServiceClientInstance.putBbq(ev);
                await Navigation.PushAsync(new ListOfBbq());
            }
           
        }

        private void BtnBack(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }
    }
    }

