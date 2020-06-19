using Lab06.Data;
using Lab06.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab06.Models
{
    public class DbStudentRepository : IStudentRepository //, IMessageRepository
    {
        private AppDbContext _dbContext;

        public DbStudentRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Add(Student item)
        {
            item.Id = Guid.NewGuid().ToString();
            _dbContext.Students.Add(item);
            _dbContext.SaveChanges();
        }

        public Student Get(string id)
        {
            return _dbContext.Students.Find(id);
        }

        public IEnumerable<Student> GetAll()
        {
            return _dbContext.Students.ToList();
        }

        public Student Remove(string key)
        {
            Student student = _dbContext.Students.Find(key);
            _dbContext.Students.Remove(student);
            _dbContext.SaveChanges();
            return student;
        }

        public void Update(Student item)
        {
            _dbContext.Entry(item).State = EntityState.Modified;
            _dbContext.SaveChanges();
            
        }

        //public void AddMess(Message item)
        //{
        //    _dbContext.Messages.Add(item);
        //    _dbContext.SaveChanges();
        //}

        //public IEnumerable<Message> GetAllMess()
        //{
        //    return _dbContext.Messages.ToList();
        //}

        //public Message GetMess(string id)
        //{
        //    throw new NotImplementedException();
        //}

        //public Message RemoveMess(string key)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
