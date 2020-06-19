using System;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Lab06.Models;
using Lab06.ViewModels;

namespace Lab06.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class ItemDetailPage : ContentPage
    {
        StudentDetailViewModel viewModel;


        public ItemDetailPage(StudentDetailViewModel viewModel)
        {
            InitializeComponent();

            BindingContext = this.viewModel = viewModel;
        }

        public ItemDetailPage()
        {
            InitializeComponent();

            var item = new Student
            {
                FirstName = "Imie",
                LastName = "Nazwisko",

            };

            viewModel = new StudentDetailViewModel(item);
            BindingContext = viewModel;
            
        }
        //po tapnieciu SAVE powinno się zaktualizować i powrócić do strony głownej
        //async void Save_Clicked(object sender, EventArgs e)
        //{
        //    var stud = new Student() { FirstName = FName.Text, LastName = LName.Text, Id = viewModel.Item.Id };
        //    MessagingCenter.Send(this, "UpdateItem", stud);
        //    await Navigation.PopAsync();
        //}
    }
}