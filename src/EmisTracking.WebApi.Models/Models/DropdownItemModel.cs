namespace EmisTracking.WebApi.Models.Models
{
    public class DropdownItemModel
    {
        public DropdownItemModel()
        { }

        public DropdownItemModel(string value, string name)
        {
            Value = value;
            Name = name;
        }

        public string Value { get; set; }
        public string Name { get; set; }
    }
}
