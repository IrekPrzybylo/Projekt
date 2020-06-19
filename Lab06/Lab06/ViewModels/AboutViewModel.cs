using Lab06.Models;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Lab06.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {
        string Id { get; set; }
        public Student User { get; set; }

        public AboutViewModel(string id)
        {
            Id = id;
            ExecuteLoadUserCommand();
            Title = "O mnie";
            OpenWebCommand = new Command(async () => await Browser.OpenAsync(User.Link));
            SendEmailCommand = new Command(async () => await Email.ComposeAsync("", "", User.Email));
            var message = new SmsMessage("", new[] {User.PhoneNumber});
            SendSmsCommand = new Command(async () => await Sms.ComposeAsync(message));

        }

        void ExecuteLoadUserCommand()
        {
            IsBusy = true;

            try
            {
                var usr = DataStore.GetItemAsync(Id);
                User = usr.Result;
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

        public Command LoadUser { get; set; }
        public ICommand OpenWebCommand { get; }
        public ICommand SendSmsCommand { get; }
        public ICommand SendEmailCommand { get; }
    }
}