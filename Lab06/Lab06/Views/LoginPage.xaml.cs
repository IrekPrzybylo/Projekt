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
    public partial class LoginPage : ContentPage
    {
        LoginViewModel viewModel;
        public LoginPage()
        {
            InitializeComponent();

            BindingContext = viewModel = new LoginViewModel();
        }

        private async void Register_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new RegisterPage()));    
        }
     
        //private bool CheckLogin()
        //{
        //    if (!string.IsNullOrEmpty(EntryLogin.Text) || !string.IsNullOrEmpty(EntryPassword.Text))
        //        return true;
        //    else
        //        return false;
        //}

        //async private void Login_Clicked(object sender, EventArgs e)
        //{
        //    Student stud = new Student() { Login = EntryLogin.Text, Password = EntryPassword.Text };
        //    if (CheckLogin())
        //    {

        //        if (viewModel.Items.Where(x => x.Login == stud.Login && x.Password == stud.Password).Count() == 0)
        //        {
        //            await DisplayAlert("Login", "Niepoprawny Login lub Hasło", "OK");
        //        }
        //        else
        //        {

        //            string logged = viewModel.Items.Where(x => x.Login == stud.Login && x.Password == stud.Password).First().Id;
        //            Application.Current.MainPage = new MainPage(logged);
        //        }
        //    }
        //    else
        //    {
        //        await DisplayAlert("Login", "Logowanie niepoprawne, pusty Login lub Hasło.", "OK");
        //    }

        //    // Application.Current.MainPage = new MainPage("sfsadfsfdaf");
        //}

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (viewModel.Items.Count == 0)
                viewModel.IsBusy = true;
        }
    }
}