using Lab06.Models;
using Lab06.ViewModels;
using Plugin.Media;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace Lab06.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegisterPage : ContentPage
    {
        private Plugin.Media.Abstractions.MediaFile img = null;
        public RegisterViewModel viewModel;

        public RegisterPage()
        {
            InitializeComponent();
            viewModel = new RegisterViewModel();
        }
        private bool CheckRegister()
        {
            if (!string.IsNullOrEmpty(E_Name.Text) || 
                !string.IsNullOrEmpty(E_Lname.Text) ||
                !string.IsNullOrEmpty(E_Login.Text) || 
                !string.IsNullOrEmpty(E_Pass.Text) || 
                !string.IsNullOrEmpty(E_Phone.Text) || 
                !string.IsNullOrEmpty(E_Email.Text))
                return true;
            else
                return false;
        }

        private async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        private async void Register_Clicked(object sender, EventArgs e)
        {
            if(CheckRegister())
            {
                Student Item = new Student()
                {
                    Id = Guid.NewGuid().ToString(), 
                    FirstName = E_Name.Text,
                    LastName = E_Lname.Text,
                    Login = E_Login.Text,
                    Password = E_Pass.Text,
                    PhoneNumber = E_Phone.Text,
                    Email = E_Email.Text,
                    Photo = ImageToBytes(),
                    Description = E_Desc.Text,
                    Link = E_Link.Text
                };
                MessagingCenter.Send(this, "AddItem", Item);

                if(viewModel.Notif==false){
                    await DisplayAlert("Rejestracja", "Konto o takim loginie już istnieje", "OK");
                }
                else
                {
                    await DisplayAlert("Rejestracja", "Konto Zostało utworzone", "OK");
                    await Navigation.PopModalAsync();
                }
            }
            else
            {
                await DisplayAlert("Rejestracja", "Jedno z pól jest puste. ", "OK");
            }

        }

        private async void Image_Clicked(object sender, EventArgs e)
        {
            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                await DisplayAlert("no upload", "picking a photo is not supported", "Ok");
                return;
            }

            img = await CrossMedia.Current.PickPhotoAsync();
            if (img == null)
                return;

            Image.Source = ImageSource.FromStream(() => img.GetStream());
        }
        private byte[] ImageToBytes()
        {
            if (img == null) return null;
            using (var memoryStream = new MemoryStream())
            {
                img.GetStream().CopyTo(memoryStream);
                img.Dispose();
                return memoryStream.ToArray();
            }
        }

    }
}