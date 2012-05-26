using System.Collections.Generic;
using System.Drawing.Imaging;

namespace SYORN.Services
{
    public class ExifReader
    {
        public ExifReader()
        {
            
        }

        public object Read(IEnumerable<PropertyItem> propertyItems)
        {
            var list = new List<object>();
            foreach(var propItem in propertyItems)
            {
                var obj = PropertyItemValueConverter.ConvertValue(propItem);
                if(obj !=null)
                    list.Add(obj);
            }

            return null;
        }
    }
}
