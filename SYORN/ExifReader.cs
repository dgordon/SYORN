using System.Collections.Generic;
using System.Drawing.Imaging;

namespace SYORN.Services
{
    public class ExifReader
    {
        private readonly IEnumerable<PropertyItem> _propertyItems;

        public ExifReader(IEnumerable<PropertyItem> propertyItems)
        {
            _propertyItems = propertyItems;
        }

        public object Read()
        {
            var list = new Dictionary<int,object>();
            
            foreach(var propItem in _propertyItems)
            {
                list.Add(propItem.Id, PropertyItemValueConverter.ConvertValue(propItem));
            }

            return null;
        }
    }
}
