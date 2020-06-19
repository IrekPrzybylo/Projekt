using Lab06.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab06.Models
{
    public class DbMessageRepository : IMessageRepository
    {
        private AppDbContext _dbContext;

        public DbMessageRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void AddMess(Message item)
        {
            _dbContext.Messages.Add(item);
            _dbContext.SaveChanges();
        }

        public Message GetMess(string id)
        {
            return _dbContext.Messages.Find(id);
        }

        public IEnumerable<Message> GetAllMess()
        {
            return _dbContext.Messages.ToList();
        }

        public Message RemoveMess(string key)
        {
            Message message = _dbContext.Messages.Find(key);
            _dbContext.Messages.Remove(message);
            _dbContext.SaveChanges();
            return message;
        }

    }
}
