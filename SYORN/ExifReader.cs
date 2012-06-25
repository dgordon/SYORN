using System.Collections.Generic;
using System.Drawing.Imaging;
using SYORN.Helpers;
using SYORN.Models;

namespace SYORN.Services
{
    public class ExifReader
    {
        readonly IPropertyTranslator _propertyItemTranslator;
        
        public ExifReader(IPropertyTranslator propertyTranslator)
        {
            _propertyItemTranslator = propertyTranslator;
        }

        public IEnumerable<ExifPropertyInfo> Read(IEnumerable<PropertyItem> propertyItems)
        {
            var list = new List<ExifPropertyInfo>();
            foreach (var propItem in propertyItems)
            {
                var exifProperty = _propertyItemTranslator.From(propItem.CastToExifPropertyInfo());
                if (exifProperty!=null)
                    list.Add(exifProperty);
            }

            return list;
        }
    }
}
