using ChurrasTrinca.Models.ResponseService;
using ChurrasTrinca.ViewModel.Bbq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ChurrasTrinca.Views.Bbq
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AllParticipantsPage : ContentPage
    {
        BbqVM _vm;
        Action<Models.Bbq> _saveBbq;
        public ObservableCollection<Models.Participants> MainList { get; set; }
        public AllParticipantsPage(BbqVM vm, Action<Models.Bbq> saveBbq)
        {
            InitializeComponent();
            _vm = vm;
            _saveBbq = saveBbq;
            BindingContext = _vm;
            carregatudo();
        }

        private async void BtnCancel(object sender, EventArgs e)
        {
            bool Alerta = await DisplayAlert("Atenção", "Caso cancele, perderá todos os campos preenchidos", "Sim", "Não");
            if (Alerta == true)
            {
                await Navigation.PopToRootAsync();
            }

        }


        private async void carregatudo()
        {
            MainList = new ObservableCollection<Models.Participants>();

            if(_vm.id != null)
            { 
            var lst = Services.Service.ServiceClientInstance.GetAllUsers(_vm.id);
            }
            else
            {

            }
        }


        private async void BtnAddParticipant(object sender, EventArgs e)
        {
            var vModel = new ParticipantVM();

            var v = new AddParticipant(vModel, AddParticipant);

            await Navigation.PushModalAsync(new NavigationPage(v));

           
        }



        public void AddParticipant(ParticipantVM Pvm)
        {
           _vm._participantVM.Add(Pvm);
        }




        private async void BtnAddBbq(object sender, EventArgs e)
        {
            Models.Bbq ev = new Models.Bbq
            {
                title = _vm.title,
                description = _vm.description,
                date = _vm.date,
                Participants = new List<Models.Participants>(),
                value_per_person = _vm.value_per_person,
                id = _vm.id
              
            };

            /*  foreach (ParticipantVM de in _vm._participantVM)
              {
                  ev.Participants.Add(new Models.Participants
                  {
                      BbqId = ev.id,
                      name = de.name,
                      value_paid = de.value_paid,
                      confirmed = de.confirmed,                    
                      id = de.id
                  }) ;*/

            if (_vm.id == 0)
            {


                ResponseService<Models.Bbq> responseService = await Services.Service.ServiceClientInstance.PostBbq(ev);

                var t = responseService.Data.id;
                await Navigation.PushAsync(new AllBbqPage());
            }
                
                else
                {
                ResponseService<Models.Bbq> responseService = await Services.Service.ServiceClientInstance.putBbq(ev);
            }
                ///TODO: CHAMAR METODO ATUALIZA OU UPDATE


                /*         var teste = Convert.ToDouble(valorpago.Text);

                         ResponseService<Models.Bbq> responseService = await Services.Service.ServiceClientInstance.PostBbq(titulo.Text, descricao.Text, date.Date, teste);

                         await DisplayAlert("Sucesso", "Item inserido", "OK");

                         await Navigation.PopModalAsync();
                */

            }            

        private void Button_Clicked(object sender, EventArgs e)
        {

        }

        private async void BtnDelete(object sender, EventArgs e)
        {
            
                bool Alerta = await DisplayAlert("Atenção", "Deseja mesmo excluir este evento", "Sim", "Não");
                if (Alerta == true)
                {
                    var t = (Button)sender;

                    ParticipantVM teste = (ParticipantVM)t.CommandParameter;
                    // await methods.ExcluirEventAndAction(ev.Id);
                    _vm._participantVM.Remove(teste);
                    // _vm.EventActions.Remove(teste);

                
            }
    }
   
    }
}
