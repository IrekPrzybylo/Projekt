using Lab06.Models;
using Lab06.Views;
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
    public class RegisterViewModel : BaseViewModel
    {
        public ObservableCollection<Student> Items { get; set; }
        public Command LoadItemsCommand { get; set; }
        public bool Notif { get; set; }
        public RegisterViewModel()
        {
            Title = "Rejestracja";
            Items = new ObservableCollection<Student>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
            LoadItemsCommand.Execute(ExecuteLoadItemsCommand());

            MessagingCenter.Subscribe<RegisterPage, Student>(this, "AddItem", async (obj, item) =>
            {
                var newItem = item as Student;
                
                if (Items.Where(x => x.Login == newItem.Login && x.Password == newItem.Password).Count() > 0)
                {
                    Notif = false;
                }
                else
                {
                    Notif = true;
                    Items.Add(newItem);
                    await DataStore.AddItemAsync(newItem);
                }        
            });
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
