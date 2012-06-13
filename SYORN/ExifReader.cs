using System.Collections.Generic;
using System.Drawing.Imaging;

namespace SYORN.Services
{
    public class ExifReader
    {
        readonly IPropertyItemValueConverter _propertyItemValueConverter;
        public ExifReader(DefaultPropertyItemValueConverter propertyItemValueConverter)
        {
            _propertyItemValueConverter = propertyItemValueConverter;
        }

        public object Read(IEnumerable<PropertyItem> propertyItems)
        {
            var list = new List<object>();
            foreach (var propItem in propertyItems)
            {
                var obj = _propertyItemValueConverter.ConvertValue(propItem);
                if (obj != null)
                    list.Add(obj);
            }

            return null;
        }
    }
}
