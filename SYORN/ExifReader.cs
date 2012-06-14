using System.Collections.Generic;
using System.Drawing.Imaging;

namespace SYORN.Services
{
    public class ExifReader
    {
        readonly IPropertyIdTranslator _propertyItemIdTranslator;
        readonly IPropertyItemValueTranslator _propertyItemValueConverter;
        
        public ExifReader(IPropertyIdTranslator propertyIdTranslator, IPropertyItemValueTranslator propertyItemValueConverter)
        {
            _propertyItemIdTranslator = propertyIdTranslator;
            _propertyItemValueConverter = propertyItemValueConverter;
        }

        public IEnumerable<Property> Read(IEnumerable<PropertyItem> propertyItems)
        {
            var list = new List<Property>();
            foreach (var propItem in propertyItems)
            {
                var name = _propertyItemIdTranslator.From(propItem.Id);
                var value = _propertyItemValueConverter.From(propItem.Type, propItem.Value);
                
                if (!string.IsNullOrEmpty(name))
                    list.Add(new Property{Name = name, Value = value});
            }

            return list;
        }
    }

    public class Property
    {
        public string Name { get; set; }
        public object Value { get; set; }
    }
}
