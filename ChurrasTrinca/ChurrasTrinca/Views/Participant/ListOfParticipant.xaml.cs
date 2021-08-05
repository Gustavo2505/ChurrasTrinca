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

namespace ChurrasTrinca.Views.Participant
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListOfParticipant : ContentPage
    {
        BbqVM _vm;
        int count { get; set; }
        Action<Models.Bbq> _saveBbq;
        public ObservableCollection<Models.Participants> MainList { get; set; }
        public ListOfParticipant(BbqVM vm, Action<Models.Bbq> saveBbq)
        {
            InitializeComponent();
            _vm = vm;  
            _saveBbq = saveBbq;
          
            LoadingAllHelper();
            BindingContext = this;
    


        }
        private async void LoadingAllHelper()
        {           
                MainList = new ObservableCollection<Models.Participants>();
                var lst = await Services.Service.ServiceClientInstance.GetAllUsers(_vm.id);
         
                  if (lst.Data != null) { 
                foreach (Models.Participants its in lst.Data)
                {
                    MainList.Add(its);             
                  
                };
             
            }            
         
        }

        private void Open(object sender, EventArgs e)
        {
              {
            var Event = (TappedEventArgs)e;
            var ev = (Models.Participants)Event.Parameter;        
            var lst = new ObservableCollection<ParticipantVM>();
            var vm = new ParticipantVM();
            vm.name = ev.name ;
            vm.value_paid = ev.value_paid;
            vm.confirmed = ev.confirmed;
         

            Navigation.PushAsync(new NewParticipant(vm, AddOrUpdateEvent));

        }
        }

        private async void BtnDeleteParticipant(object sender, EventArgs e)
        {
            bool Alerta = await DisplayAlert("Atenção", "Deseja mesmo excluir este Participante", "Sim", "Não");
            if (Alerta == true)
            {

                var t = (ImageButton)sender;
                Models.Participants ev = (Models.Participants)t.CommandParameter;

                ResponseService<Models.Participants> responseService = await Services.Service.ServiceClientInstance.DeleteParticipant(ev.id);
                MainList.Remove(ev);
            }

        }

        private void BtnInsertParticipant(object sender, EventArgs e)
        {
            var vm = new ParticipantVM();
            vm.bbq_id = _vm.id;

            Navigation.PushAsync(new NewParticipant(vm, AddOrUpdateEvent));

        }
       private void AddOrUpdateEvent(Models.Participants ev)
        {
            if (MainList.Count > 0 &&
                MainList.Any(X => X.id == ev.id))
            {
                var objList = MainList.FirstOrDefault(X => X.id == ev.id);
                var idx = MainList.IndexOf(objList);
                MainList[idx] = ev;
               

            }
            else
            {
                MainList.Add(ev);
            }

        }

        private void BtnBack(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }
    }
}