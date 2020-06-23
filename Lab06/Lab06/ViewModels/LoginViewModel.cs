using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Lab06.Models;
using Lab06.Views;
using Xamarin.Forms;

namespace Lab06.ViewModels
{
    class LoginViewModel : BaseViewModel
    {
        public ObservableCollection<Student> Items { get; set; }
        public Student User { get; set; }
        public Command LoginCommand { get; }
        public LoginViewModel()
        {
            Title = "Logowanie";
            Items = new ObservableCollection<Student>();
            LoginCommand = new Command(VerifyLogin);
            //LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
            //LoadItemsCommand.Execute(ExecuteLoadItemsCommand());
        }

        private async void VerifyLogin()
        {
            IsBusy = true;
            if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(pass))
            {
                IsBusy = false;
                await Application.Current.MainPage.DisplayAlert("Błąd", "Nie wpisano danych", "OK");
            }
            else
            {
                try
                {
                    Items.Clear();
                    var items = await DataStore.GetItemsAsync(true);
                    foreach (var item in items)
                    {
                        Items.Add(item);
                    }
                    User = Items.FirstOrDefault(x => x.Login == login && x.Password == pass);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex);
                }
                finally
                {
                    IsBusy = false;
                }


                if (User == null)
                {
                    IsBusy = false;
                    await Application.Current.MainPage.DisplayAlert("Błąd", "Niepoprawny login lub hasło", "OK");

                }
                else
                {
                    IsBusy = false;
                    Application.Current.MainPage = new MainPage(User.Id);

                }

            }

        }
        string login = "";
        string pass = "";
        public string Login
        {
            get { return login; }
            set
            {
                login = value;
                OnPropertyChanged();
            }
        }
        public string Password
        {
            get { return pass; }
            set
            {
                pass = value;
                OnPropertyChanged();
            }
        }
    }
}
