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
    public partial class ItemEditPage : ContentPage
    {
        StudentEditViewModel viewModel;
        private Plugin.Media.Abstractions.MediaFile img = null;


        public ItemEditPage(StudentEditViewModel viewModel)
        {
            InitializeComponent();

            BindingContext = this.viewModel = viewModel;
        }

        public ItemEditPage()
        {
            InitializeComponent();

            var item = new Student
            {
                FirstName = "Imie",
                LastName = "Nazwisko",

            };

            viewModel = new StudentEditViewModel(item);
            BindingContext = viewModel;

        }
        //po tapnieciu SAVE powinno się zaktualizować i powrócić do strony głownej
        async void Save_Clicked(object sender, EventArgs e)
        {

            var stud = new Student() { FirstName = FName.Text, LastName = LName.Text, PhoneNumber = PNr.Text, Description = Des.Text, Email = Em.Text, Link = viewModel.Item.Link, Login = viewModel.Item.Login, Password = viewModel.Item.Password, Photo = viewModel.Item.Photo, Id = viewModel.Item.Id };
            MessagingCenter.Send(this, "UpdateItem", stud);
            await Navigation.PopAsync();
        }
    }
}