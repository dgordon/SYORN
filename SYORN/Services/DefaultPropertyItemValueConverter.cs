using System;
using System.Collections.Generic;
using System.Drawing.Imaging;


namespace SYORN.Services
{
    public class DefaultPropertyItemValueConverter : IPropertyItemValueConverter
    {
        readonly IPropertyNameTranslator _findExifProperty;

        public DefaultPropertyItemValueConverter(IPropertyNameTranslator exifPropertyList)
        {
            _findExifProperty = exifPropertyList;
        }

        public object ConvertValue(PropertyItem prop)
        {
            var propertyName = _findExifProperty.From(prop.Id);

            if (propertyName != null)
            {
                //var del = ConverterPropertyItemValue.GetBy(prop.Type);
                //var propValue = typeConverter(prop.Value);

                //var propField = PropertyKeys()[prop.Id];
                //return new { Field = propField, Value = propValue };
            }
            else
            {
                return null;
            }
            return null;
        }

        private static Dictionary<int, Func<object, object>> GetPropertyValueOptions()
        {
            throw new NotImplementedException();
            return new Dictionary<int, Func<object, object>>
                       {

                       };
        }
    }

    public interface IPropertyItemValueConverter
    {
        object ConvertValue(PropertyItem properiItem);
    }
}
