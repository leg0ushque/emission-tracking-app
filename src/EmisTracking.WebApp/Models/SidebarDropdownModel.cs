using System.Collections.Generic;

namespace EmisTracking.WebApp.Models
{
    public class SidebarDropdownModel(string dropdownId, string iconName, string text, List<(string, string, string)> items)
    {
        public string DropdownId { get; set; } = dropdownId;

        public string IconName { get; set; } = iconName;

        public string Text { get; set; } = text;

        public List<(string, string, string)> Items { get; set; } = items;
    }
}
