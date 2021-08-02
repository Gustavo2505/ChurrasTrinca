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

            ResponseService<List<Models.Bbq>> responseService = await Services.Service.ServiceClientInstance.GetAllBbq();

            if (responseService.isSucess)
            {
                MainList = new ObservableCollection<Models.Bbq>(responseService.Data);
                CVListaDeTarefas.ItemsSource = MainList;
            }
            else
            {
                await DisplayAlert("erro", "Erro", "ok");
            }



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
    }
}
