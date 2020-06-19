using Lab06.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lab06.ViewModels
{
    public class StudentEditViewModel : BaseViewModel
    {
        public Student Item { get; set; }
        public StudentEditViewModel(Student student = null)
        {
            Title = student?.FirstName;
            Item = student;
        }

    }
}
