using System;
using Xamarin.Forms;

namespace Lab06.Models
{
    public class Student
    {
        public string Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public byte[] Photo { get; set; }
        public string Description { get; set; }
        public string Link { get; set; }
    }

  
}