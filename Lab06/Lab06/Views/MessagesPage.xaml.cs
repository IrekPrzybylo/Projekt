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
    public partial class MessagesPage : ContentPage
    {
        MessagesViewModel viewModel;
        readonly string LoggId;

        public MessagesPage(string id)
        {
            InitializeComponent();
            LoggId = id;

            BindingContext = viewModel = new MessagesViewModel(LoggId);
        }

        async void OnItemSelected(object sender, EventArgs args)
        {
            var layout = (BindableObject)sender;
            var item = (Student)layout.BindingContext;
            await Navigation.PushAsync(new ChatPage(new ChatViewModel(item,LoggId)));
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (viewModel.Items.Count == 0)
                viewModel.IsBusy = true;
        }
    }
}