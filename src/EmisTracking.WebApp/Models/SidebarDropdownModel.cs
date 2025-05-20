using System.Collections.Generic;

namespace EmisTracking.WebApp.Models
{
    public class SidebarDropdownModel
    {
        public SidebarDropdownModel(string dropdownId, string iconName, string text, List<(string, string, string)> items)
        {
            DropdownId = dropdownId;
            IconName = iconName;
            Text = text;
            Items = items;
        }
        public string DropdownId { get; set; }

        public string IconName { get; set; }

        public string Text { get; set; }

        public List<(string,string,string)> Items { get; set; }
    }
}
