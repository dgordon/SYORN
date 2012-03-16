using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Text;

namespace SYORN.Services
{
    public static class PropertyItemValueConverter
    {
        public static object ConvertValue(PropertyItem prop)
        {
            try
            {
                //get prop conversion types
                var typesOfConversions = GetPropertyTypeOptions();
                //get prop converter
                Func<byte[], object> typeConverter = typesOfConversions[prop.Type];
                //convert prop byte[] to primitive value
                var primitiveValue = typeConverter(prop.Value);

                ////convert prop primitive value to original value
                //var valueConversions = GetPropertyValueOptions();
                //Func<object, object> valueConverter = valueConversions[prop.Id];
                //var value = valueConverter(primitiveValue);

                //get prop name from id
                var nameConversions = GetPropertyNameOptions();
                Func<int,object, string> idConverter = nameConversions[prop.Id];
                //var name = idConverter();

                return null;
            }
            catch
            {
                string message = string.Format("Cannot convert property ID: {0}", prop.Id);
                throw new TypeConversionException(message);
            }
        }

        private static Dictionary<int, Func<int,object, string>> GetPropertyNameOptions()
        {
            return new Dictionary<int, Func<int,object, string>>
                       {
                            //{256, value => "ImageWidth"},
                            //{257, value => "ImageLength"},
                            //{258, value => "BitsPerSample"},
                            //{259, value => "Compression"},
                            //{262, value => "PhotometricInterpretation"},
                            //{274, value => "Orientation"},
                            //{277, value => "SamplesPerPixel"},
                            //{284, value => "PlanarConfiguration"},
                            //{530, value => "YCbCrSubSampling"},
                            //{531, value => "YCbCrPositioning"},
                            //{282, value => "XResolution"},
                            //{283, value => "YResolution"},
                            //{296, value => "ResolutionUnit"}
                       };
        }

        private static Dictionary<int, Func<object, object>> GetPropertyValueOptions()
        {
            throw new NotImplementedException();
            return new Dictionary<int, Func<object, object>>
                       {

                       };
        }

        private static Dictionary<short, Func<byte[], object>> GetPropertyTypeOptions()
        {
            return new Dictionary<short, Func<byte[], object>>
                                         {
                                            //BYTE
                                            {1, value =>  { return "NOT IMPLEMENTED"; }},
                                            //ASCII
                                            {2, value => Encoding.ASCII.GetString(value).Substring(0, value.Length-2)},
                                            //SHORT
                                            {3, value => BitConverter.ToUInt16(value, 0)},
                                            //LONG
                                            {4, value => BitConverter.ToUInt32(value,0)},
                                            //RATIONAL
                                            {5, value =>
                                                    {
                                                        var numer = BitConverter.ToInt32(value, 0);
                                                        var denom = BitConverter.ToInt32(value, 4);
                                                        return Fraction(numer, denom);
                                                    }
                                            },
                                            //UNDEFINED
                                            {7, value => { return "NOT IMPLEMENTED"; }},
                                            //SLONG
                                            {9, value => BitConverter.ToUInt32(value,0)},
                                            //SRATIONAL
                                            {10, value =>
                                                {
                                                    var numer = BitConverter.ToUInt32(value, 0);
                                                    var denom = BitConverter.ToUInt32(value, 4);
                                                    return Fraction(numer, denom);
                                                    }
                                            }
                                        };
        }

        private static double Fraction(double numerator, double denominator)
        {
            return denominator > 0 ? numerator / denominator : 0.0;
        }
    }

    public class TypeConversionException : Exception
    {
        public TypeConversionException(string message)
            : base(message)
        { }
    }
}
