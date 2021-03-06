using ChurrasTrinca.Models.ResponseService;
using ChurrasTrinca.ViewModel.Bbq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ChurrasTrinca.Views.Participant
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewParticipant : ContentPage
    {
        ParticipantVM _vm;
        Action<Models.Participants> _saveBbq;

        public NewParticipant(ParticipantVM vm, Action<Models.Participants> saveBbq)
        {
            InitializeComponent();
            _vm = vm;
            _saveBbq = saveBbq;
            name.Text = _vm.name;

            // chkTrue. = Convert.ToBoolean(_vm.confirmed());
           // name.Text = _vm.name;
           

          //  var t = Convert.ToInt32(value_paid)
       //     value_paid.Text = _vm.value_paid;

           
        }

        private void BtnCancel(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }

   

        private async void BtnInsert(object sender, EventArgs e)
        {
            _vm.name = name.Text;
            _vm.value_paid = Convert.ToInt32(value_paid.Text);
            _vm.confirmed = chkTrue.IsChecked;

            Models.Participants ev = new Models.Participants
            {
                
                id = _vm.id,
                bbq_id = _vm.bbq_id,
                name = _vm.name,
                value_paid = _vm.value_paid,
                confirmed = _vm.confirmed
                
            };

            _saveBbq(ev);
            if (ev.id == 0)
            {
                ResponseService<Models.Participants> responseService = await Services.Service.ServiceClientInstance.PostUser(ev);
             
                await Navigation.PopAsync();
            }

            else
            {
                ResponseService<Models.Participants> responseService = await Services.Service.ServiceClientInstance.putUser(ev);
                await Navigation.PopAsync();
           
            }
            
        }

        private void BtnBack(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }
    }
}