using ChurrasTrinca.Helpers;
using ChurrasTrinca.Models.ResponseService;
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
        private ObservableCollection<Models.Bbq> MainList { get; set; }
        private SearchParams _searchParams;
        public AllBbqPage()
        {


            InitializeComponent();


            Device.BeginInvokeOnMainThread(() =>
            {
                BindingContext = this;
                carregaotudoAsync();

            });


        }



        private async void carregaotudoAsync()
        {

            _searchParams = new SearchParams() { pages = 1 };

            ResponseService<List<Models.Bbq>> responseService = await Services.Service.ServiceClientInstance.GetAllBbq(_searchParams.pages = 1);

            //if (responseService.isSucess)
           // {
                MainList = new ObservableCollection<Models.Bbq>(responseService.Data);
                CVListaDeTarefas.ItemsSource = MainList;
            CVListaDeTarefas.RemainingItemsThreshold = 0;

        //    }
        //   else
            ///   {
            //       await DisplayAlert("erro", "Erro", "ok");
            //   }



            //   MainList = new ObservableCollection<Models.Bbq>();
            //  var lst = await Services.Service.ServiceClientInstance.GetAllBbq();

            //    foreach (Models.Bbq its in lst.Data)
            //    {
            //        MainList.Add(its);
            //    };

            //   CVListaDeTarefas.ItemsSource = MainList;

        }



        private void BtnCadastrarChurras(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new Bbq.AddBbqPage());
        }

        private async void InfinitySearch(object sender, EventArgs e)
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
            }
          //  else
          //  {
          //      await DisplayAlert("erro", "Erro", "ok");
          //  }

        }
    }

