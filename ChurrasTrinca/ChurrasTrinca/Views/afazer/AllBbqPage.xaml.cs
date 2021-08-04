using ChurrasTrinca.Helpers;
using ChurrasTrinca.Models;
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
    public partial class AllBbqPage : ContentPage
    {
        public ObservableCollection<Models.Bbq> MainList { get; set; }
       // private SearchParams _searchParams;
        public AllBbqPage()
        {
            

            InitializeComponent();

            carregaotudoAsync();


            BindingContext = this;
              



        }



        public async void carregaotudoAsync()
        {

        //    _searchParams = new SearchParams() { pages = 1 };

        //    ResponseService<List<Models.Bbq>> responseService = await Services.Service.ServiceClientInstance.GetAllBbq(_searchParams.pages = 1);

            //if (responseService.isSucess)
           // {
         /*       MainList = new ObservableCollection<Models.Bbq>(responseService.Data);
                CVListaDeTarefas.ItemsSource = MainList;
               CVListaDeTarefas.RemainingItemsThreshold = 0;

        //    }
        //   else
            ///   {
            //       await DisplayAlert("erro", "Erro", "ok");
            //   }*/



              MainList = new ObservableCollection<Models.Bbq>();
              var lst = await Services.Service.ServiceClientInstance.GetAllBbq();

               foreach (Models.Bbq its in lst.Data)
                {
                   MainList.Add(its);
                };

          

          
        }

        private async void BtnDeleteBbq(object sender, EventArgs e)
        {
            bool Alerta = await DisplayAlert("Atenção", "Deseja mesmo excluir este evento", "Sim", "Não");
            if (Alerta == true)
            {

                var t = (Button)sender;
                Models.Bbq ev = (Models.Bbq)t.CommandParameter;

                ResponseService<Models.Bbq> responseService = await Services.Service.ServiceClientInstance.DeleteBbq(ev.id);
                MainList.Remove(ev);
            }
        
    }


        private void BtnCadastrarChurras(object sender, EventArgs e)
        {
            var vm = new BbqVM();
            vm._participantVM = new ObservableCollection<ParticipantVM>();

            Navigation.PushAsync(new Bbq.AddBbqPage(vm, AddOrUpdateEvent));
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


        private void Abrir(object sender, EventArgs e)
        {
            var evento = (TappedEventArgs)e;
            var ev = (Models.Bbq)evento.Parameter; 
            //   Metodo de pesquisa Visualizar(ev.Id);
            var lst = new ObservableCollection<ParticipantVM>();



           /*
            foreach (Participants act in ev.Participants)
            {
                lst.Add(new ParticipantVM
                {
                  name = act.name,
                  confirmed = act.confirmed,
                  value_paid = act.value_paid,
                  id = act.id

                });
            }
           */
            var vm = new BbqVM();
            vm.title = ev.title;
            vm.id = ev.id;
            vm.date = ev.date;
            vm.value_per_person = ev.value_per_person;
           // vm._participantVM = lst;

            Navigation.PushAsync(new Views.Bbq.AddBbqPage(vm, AddOrUpdateEvent));

        }


        /*    private async void InfinitySearch(object sender, EventArgs e)
            {
                _searchParams.pages++;
                ResponseService<List<Models.Bbq>> responseService = await Services.Service.ServiceClientInstance.GetAllBbq(_searchParams.pages);

             //   if (responseService.isSucess)
             //   {
                    var mainlistFromBbqService = responseService.Data;
                    foreach (Models.Bbq item in mainlistFromBbqService)
                    {
                        MainList.Add(item);
                    }
                    CVListaDeTarefas.ItemsSource = MainList;
                }*/
        //  else
        //  {
        //      await DisplayAlert("erro", "Erro", "ok");
        //  }

    }
}

