using System;
using System.Linq;
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
            var list = new Dictionary<string,object>();
            var keyTranslation = PropertyItemValueConverter.PropertyKeyTranslator();
            foreach(var propItem in _propertyItems)
            {
                if(keyTranslation.Any(x=>x.Key == propItem.Id))
                    list.Add(keyTranslation[propItem.Id], PropertyItemValueConverter.ConvertValue(propItem));
            }

            return null;
        }
    }
}
