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
            var list = new List<object>();
            foreach(var propItem in _propertyItems)
            {
                var obj = PropertyItemValueConverter.ConvertValue(propItem);
                if(obj !=null)
                    list.Add(obj);
            }

            return null;
        }
    }
}
