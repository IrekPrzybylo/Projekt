using Lab06.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Lab06.ViewModels
{
    class MessagesViewModel : BaseViewModel
    {

        public ObservableCollection<Student> Items { get; set; }
        public Command LoadItemsCommand { get; set; }

        public MessagesViewModel(string id)
        {
            string LoggedId = id;
            Title = "Wiadomości";
            Items = new ObservableCollection<Student>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand(LoggedId));  
        }

        async Task ExecuteLoadItemsCommand(string id)
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
                Items.Remove(Items.FirstOrDefault(s => s.Id == id));
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
