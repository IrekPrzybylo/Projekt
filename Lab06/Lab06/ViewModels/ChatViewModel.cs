using Lab06.Models;
using Lab06.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Lab06.ViewModels
{
    public class ChatViewModel : BaseViewModel
    {
        public ObservableCollection<Message> Messages { get; set; }
        public Command LoadMessagesCommand { get; set; }
        public Student Recip;
        public string SenderId;
        public ChatViewModel(Student recip, string senderId)
        {
            Recip = recip;
            Title = Recip.FirstName;
            SenderId = senderId;
            Messages = new ObservableCollection<Message>();

            LoadMessagesCommand = new Command(async () => await ExecuteLoadMessages());
            MessagingCenter.Subscribe<ChatPage, Message>(this, "SendMessage", async (obj, message) =>
            {
                var newMessage = message as Message;
                await MessageDataStore.AddMessageAsync(newMessage);
                Messages.Add(newMessage);

            });
        }
        async Task ExecuteLoadMessages()
        {
            Messages.Clear();
            IsBusy = true;
            var messages = await MessageDataStore.GetMessagesAsync(true);
            var sendId = SenderId;
            var recipId = Recip.Id;
            messages = messages.OrderBy(s => s.SendDate);
            foreach (var msg in messages)
            {

                if (msg.ToId.Equals(recipId) && msg.FromId.Equals(sendId))
                {
                    msg.Text = "Wysłano: " + msg.Text;
                    Messages.Add(msg);
                }
                if (msg.FromId.Equals(recipId) && msg.ToId.Equals(sendId))
                {
                    msg.Text = "Odebrano: " + msg.Text;
                    Messages.Add(msg);
                }
            }
            IsBusy = false;
        }
    }
}
