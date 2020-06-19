using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lab06.Services
{
    public interface IMessageStore<T>
    {
        Task<bool> AddMessageAsync(T item);
        Task<bool> DeleteMessageAsync(string id);
        Task<T> GetMessageAsync(string id);
        Task<IEnumerable<T>> GetMessagesAsync(bool forceRefresh = false);
    }
}
