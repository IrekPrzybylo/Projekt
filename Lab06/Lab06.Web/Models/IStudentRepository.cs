using System;
using System.Collections.Generic;

namespace Lab06.Models
{
    public interface IStudentRepository
    {
        void Add(Student item);
        void Update(Student item);
        Student Remove(string key);
        Student Get(string id);
        IEnumerable<Student> GetAll();
    }
}
