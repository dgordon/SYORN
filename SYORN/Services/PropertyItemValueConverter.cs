using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;



namespace SYORN.Services
{
    public static class PropertyItemValueConverter
    {
        public static object ConvertValue(PropertyItem prop)
        {
            
            //if (PropertyKeys().Any(x => x.Key == prop.Id))
            //{
            //    var del = ConverterPropertyItemValue.GetBy(prop.Type);
            //    var propValue = typeConverter(prop.Value);
                
            //    var propField = PropertyKeys()[prop.Id];
            //    return new { Field = propField, Value = propValue };
            //}
            //else
            //{
            //    return null;
            //}
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

    public class TypeConversionException : Exception
    {
        public TypeConversionException(string message)
            : base(message)
        { }
    }
}
