using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xamarin.Essentials;
using Lab06.Models;
using Xamarin.Forms;

namespace Lab06.Services
{
    public class AzureDataStore : IStudentStore<Student>, IMessageStore<Message>
    {
        HttpClient client;
        IEnumerable<Student> items;
        IEnumerable<Message> messages;

        public AzureDataStore()
        {
#if DEBUG
            var insecHandler = GetInsecureHandler();
            client = new HttpClient(insecHandler);
#else
            client = new HttpClient();
#endif
            

            client.BaseAddress = new Uri($"{App.AzureBackendUrl}/");

            items = new List<Student>();
            messages = new List<Message>();
        }

        // This method must be in a class in a platform project, even if
        // the HttpClient object is constructed in a shared project.
        public HttpClientHandler GetInsecureHandler()
        {
            HttpClientHandler handler = new HttpClientHandler();
            handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) =>
            {
                return true;
            };
            return handler;
        }

        bool IsConnected => Connectivity.NetworkAccess == NetworkAccess.Internet;
        public async Task<IEnumerable<Student>> GetItemsAsync(bool forceRefresh = false)
        {
            if (forceRefresh && IsConnected)
            {
                var json = await client.GetStringAsync($"api/item");
                items = await Task.Run(() => JsonConvert.DeserializeObject<IEnumerable<Student>>(json));
            }

            return items;
        }

        public Task<Student> GetItemAsync(string id)
        {
            if (id != null && IsConnected)
            {
                var json = client.GetStringAsync($"api/item/{id}");
                return Task.Run(() => JsonConvert.DeserializeObject<Student>(json.Result));
            }

            return null;
        }

        public async Task<bool> AddItemAsync(Student item)
        {
            if (item == null || !IsConnected)
                return false;

            var serializedItem = JsonConvert.SerializeObject(item);

            var response = await client.PostAsync($"api/item", new StringContent(serializedItem, Encoding.UTF8, "application/json"));

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateItemAsync(Student item)
        {
            if (item == null || item.Id == null || !IsConnected)
                return false;

            //poprawiony kod
            var serializedItem = JsonConvert.SerializeObject(item);
            var response = await client.PutAsync($"api/item/{item.Id}", new StringContent(serializedItem, Encoding.UTF8, "application/json"));

                return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            if (string.IsNullOrEmpty(id) && !IsConnected)
                return false;

            var response = await client.DeleteAsync($"api/item/{id}");

            return response.IsSuccessStatusCode;
        }


        public async Task<bool> AddMessageAsync(Message item)
        {
            if (item == null || !IsConnected)
                return false;

            var serializedItem = JsonConvert.SerializeObject(item);

            var response = await client.PostAsync($"api/message", new StringContent(serializedItem, Encoding.UTF8, "application/json"));

            return response.IsSuccessStatusCode;
        }


        public async Task<Message> GetMessageAsync(string id)
        {
            if (id != null && IsConnected)
            {
                var json = await client.GetStringAsync($"api/message/{id}");
                return await Task.Run(() => JsonConvert.DeserializeObject<Message>(json));
            }

            return null;
        }

        public async Task<IEnumerable<Message>> GetMessagesAsync(bool forceRefresh)
        {
            if (forceRefresh && IsConnected)
            {
                var json = await client.GetStringAsync($"api/message");
                messages = await Task.Run(() => JsonConvert.DeserializeObject<IEnumerable<Message>>(json));
            }

            return messages;
        }

        public async Task<bool> DeleteMessageAsync(string id)
        {
            if (string.IsNullOrEmpty(id) && !IsConnected)
                return false;

            var response = await client.DeleteAsync($"api/message/{id}");

            return response.IsSuccessStatusCode;
        }
    }
}
