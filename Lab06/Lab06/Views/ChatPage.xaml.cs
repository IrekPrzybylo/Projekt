using Lab06.Models;
using Lab06.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Lab06.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChatPage : ContentPage
    {
        ChatViewModel viewModel;

        public ChatPage(ChatViewModel recipent)
        {
            InitializeComponent();
            BindingContext = viewModel = recipent;
        }



        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (viewModel.Messages.Count == 0)
                viewModel.IsBusy = true;
        }

        private void SendMsg_Clicked(object sender, EventArgs e)
        {
            var message = new Message() { MessageId = Guid.NewGuid().ToString(), Text = Message.Text, ToId= viewModel.Recip.Id, FromId = viewModel.SenderId, SendDate = DateTime.Now };
            MessagingCenter.Send(this, "SendMessage", message);
            Message.Text = "";
        }
    }
}