using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab06.Models
{
    public interface IMessageRepository
    {
        void AddMess(Message item);
        Message RemoveMess(string key);
        Message GetMess(string id);
        IEnumerable<Message> GetAllMess();
    }
}
