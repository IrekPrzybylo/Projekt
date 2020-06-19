using System;
using System.Collections.Generic;
using System.Text;

namespace Lab06.Models
{
    public enum MenuItemType
    {
        Users,
        Messages,
        About
    }
    public class HomeMenuItem
    {
        public MenuItemType Id { get; set; }

        public string Title { get; set; }
    }
}
