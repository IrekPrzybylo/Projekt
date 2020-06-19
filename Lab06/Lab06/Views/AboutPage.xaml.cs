using Lab06.Models;
using Lab06.ViewModels;
using System;
using System.ComponentModel;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Lab06.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class AboutPage : ContentPage
    {
        AboutViewModel viewModel;

        readonly string LoggId;
        public AboutPage(string id)
        {
            InitializeComponent();
            LoggId = id;
            BindingContext = viewModel = new AboutViewModel(LoggId);

            EFname.Text = viewModel.User.FirstName;
            ELname.Text = viewModel.User.LastName;
            ELogin.Text = viewModel.User.Login;
            EDesc.Text = viewModel.User.Description;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (viewModel.User == null)
                viewModel.IsBusy = true;
        }

        private async void Edit_Clicked(object sender, EventArgs e)
        {
           
            await Navigation.PushAsync(new ItemEditPage(new StudentEditViewModel(viewModel.User)));
        }
    }
    }
