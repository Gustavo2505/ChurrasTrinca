using ChurrasTrinca.Models.ResponseService;
using ChurrasTrinca.ViewModel.Bbq;
using ChurrasTrinca.Views.Participant;
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
 
    public partial class ListOfBbq : ContentPage
    {
        public ObservableCollection<Models.Bbq> MainList { get; set; }
        public ListOfBbq()
        {
            InitializeComponent();

            LoadingHelper();


            BindingContext = this;

        }


        public async void LoadingHelper()
        {

            MainList = new ObservableCollection<Models.Bbq>();
            var lst = await Services.Service.ServiceClientInstance.GetAllBbq();

            foreach (Models.Bbq its in lst.Data)
            {
                MainList.Add(its);
            };




        }

        private async void BtnDeleteBbq(object sender, EventArgs e)
        {
            bool Alerta = await DisplayAlert("Atenção", "Deseja mesmo excluir este Bbq", "Sim", "Não");
            if (Alerta == true)
            {

                var t = (ImageButton)sender;
                Models.Bbq ev = (Models.Bbq)t.CommandParameter;

                ResponseService<Models.Bbq> responseService = await Services.Service.ServiceClientInstance.DeleteBbq(ev.id);
                MainList.Remove(ev);
            }

        }


        private void BtnAddBbq(object sender, EventArgs e)
        {
            var vm = new BbqVM();
            vm._participantVM = new ObservableCollection<ParticipantVM>();

            Navigation.PushAsync(new NewBbq(vm, AddOrUpdateEvent));
        }

        private void AddOrUpdateEvent(Models.Bbq ev)
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


        private void Open(object sender, EventArgs e)
        {
            var Event = (TappedEventArgs)e;
            var ev = (Models.Bbq)Event.Parameter;        
            var lst = new ObservableCollection<ParticipantVM>();
            var vm = new BbqVM();
            vm.title = ev.title;
            vm.id = ev.id;
            vm.date = ev.date;
            vm.value_per_person = ev.value_per_person;   

            Navigation.PushAsync(new NewBbq(vm, AddOrUpdateEvent));

        }

        private void BtnAddParticipant(object sender, EventArgs e)
        {
            var t = (ImageButton)sender;
            Models.Bbq ev = (Models.Bbq)t.CommandParameter;
            var lst = new ObservableCollection<ParticipantVM>();
            var vm = new BbqVM();
       
            vm.id = ev.id;
            
            Navigation.PushAsync(new ListOfParticipant(vm, AddOrUpdateEvent));

        }

        private void BtnExit(object sender, EventArgs e)
        {
            Navigation.PopToRootAsync();
        }
    }
}

