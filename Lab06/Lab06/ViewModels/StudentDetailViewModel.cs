using System;
using System.Windows.Input;
using Lab06.Models;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Lab06.ViewModels
{
    public class StudentDetailViewModel : BaseViewModel
    {
        public Student Item { get; set; }
        public StudentDetailViewModel(Student student = null)
        {
            Title = student?.FirstName;
            Item = student;
            OpenWebCommand = new Command(async () => await Browser.OpenAsync(Item.Link));
            SendEmailCommand = new Command(async () => await Email.ComposeAsync("", "", Item.Email));
            var message = new SmsMessage("", new[] { Item.PhoneNumber });
            SendSmsCommand = new Command(async () => await Sms.ComposeAsync(message));
        }

        public ICommand OpenWebCommand { get; }
        public ICommand SendSmsCommand { get; }
        public ICommand SendEmailCommand { get; }

    }
}
