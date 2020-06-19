using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Lab06.Models;
using Lab06.Views;
using Xamarin.Forms;

namespace Lab06.ViewModels
{
    class LoginViewModel : BaseViewModel
    {
        public ObservableCollection<Student> Items { get; set; }
        public Command LoadItemsCommand { get; set; }
        public LoginViewModel()
        {
            Title = "Logowanie";
            Items = new ObservableCollection<Student>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
            LoadItemsCommand.Execute(ExecuteLoadItemsCommand());
        }

        async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true;

            try
            {
                Items.Clear();
                var items = await DataStore.GetItemsAsync(true);
                foreach (var item in items)
                {
                    Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
