using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lab06.Models;

namespace Lab06.Services
{
    public class MockDataStore : IStudentStore<Student>
    {
        readonly List<Student> items;

        public MockDataStore()
        {
            items = new List<Student>()
            {
                new Student() { Id = Guid.NewGuid().ToString(), FirstName = "Jan", LastName="Kowalski"},
                new Student() { Id = Guid.NewGuid().ToString(), FirstName = "Katarzyna", LastName="Hajto"},
                new Student() { Id = Guid.NewGuid().ToString(), FirstName = "Marek", LastName="Nowak" },
            };
        }

        public async Task<bool> AddItemAsync(Student item)
        {
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(Student item)
        {
            var oldItem = items.Where((Student arg) => arg.Id == item.Id).FirstOrDefault();
            items.Remove(oldItem);
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            var oldItem = items.Where((Student arg) => arg.Id == id).FirstOrDefault();
            items.Remove(oldItem);

            return await Task.FromResult(true);
        }

        public async Task<Student> GetItemAsync(string id)
        {
            return await Task.FromResult(items.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<Student>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(items);
        }
    }
}