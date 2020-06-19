using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;
using Lab06.Models;
using Lab06.Views;

namespace Lab06.ViewModels
{
    public class StudentsViewModel : BaseViewModel
    {
        public ObservableCollection<Student> Items { get; set; }
        public Command LoadItemsCommand { get; set; }

        public StudentsViewModel()
        {
            Title = "Użytkownicy";
            Items = new ObservableCollection<Student>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());


            MessagingCenter.Subscribe<ItemEditPage, Student>(this, "UpdateItem", async (obj, item) =>
            {
                var updItem = item as Student;
                await DataStore.UpdateItemAsync(updItem);
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