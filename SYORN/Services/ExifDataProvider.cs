using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Text;

namespace SYORN.Services
{
    internal class ExifDataProvider
    {
        public object Convert(PropertyItem propertyItem)
        {
            var method = PropertyValueConversions[propertyItem.Type];
            return method.Invoke(propertyItem.Value);
        }

        private static Dictionary<short, Converter<byte[], object>> PropertyValueConversions
        {
            get
            {
                return new Dictionary<short, Converter<byte[], object>>
                           {
                               //BYTE
                               {1, value =>  /*return null;*/  value[0]},
                               //ASCII
                               {2, value => Encoding.ASCII.GetString(value).Substring(0, value.Length - 1)},
                               //SHORT
                               {3, value => BitConverter.ToUInt16(value, 0)},
                               //LONG
                               {4, value => BitConverter.ToUInt32(value, 0)},
                               //RATIONAL
                               {
                                   5, value =>
                                          {
                                              var numer = BitConverter.ToInt32(value, 0);
                                              var denom = BitConverter.ToInt32(value, 4);
                                              return Fraction(numer, denom);
                                          }
                                   },
                               //UNDEFINED
                               {7, value => { return value; }},
                               //SLONG
                               {9, value => BitConverter.ToUInt32(value, 0)},
                               //SRATIONAL
                               {
                                   10, value =>
                                           {
                                               var numer = BitConverter.ToUInt32(value, 0);
                                               var denom = BitConverter.ToUInt32(value, 4);
                                               return Fraction(numer, denom);
                                           }
                                   }
                           };
            }
        }

        private static double Fraction(double numerator, double denominator)
        {
            return denominator > 0 ? numerator / denominator : 0.0;
        }
    }
}
