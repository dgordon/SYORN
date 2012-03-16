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
                return typeConverter(prop.Value);

                ////convert prop primitive value to original value
                //var valueConversions = GetPropertyValueOptions();
                //Func<object, object> valueConverter = valueConversions[prop.Id];
                //var value = valueConverter(primitiveValue);

                ////get prop name from id
                //var nameConversions = PropertyKeyTranslator();
                //var propName = nameConversions[prop.Id];

            }
            catch
            {
                string message = string.Format("Cannot convert property ID: {0}", prop.Id);
                throw new TypeConversionException(message);
            }
        }

        //rename to PropertyKeyTranslator
        public static Dictionary<int, string> PropertyKeyTranslator()
        {
            return new Dictionary<int, string>
                       {
                           //levels (1)
                           // A : image data structure
                            {256, "ImageWidth"},
                            {257, "ImageLength"},
                            {258, "BitsPerSample"},
                            {259, "Compression"},
                            {262, "PhotometricInterpretation"},
                            {274, "Orientation"},
                            {277, "SamplesPerPixel"},
                            {284, "PlanarConfiguration"},
                            {530, "YCbCrSubSampling"},
                            {531, "YCbCrPositioning"},
                            {282, "XResolution"},
                            {283, "YResolution"},
                            {296, "ResolutionUnit"},
                            // B : recording offset
                            {513, "JPEGInterchangeFormat"},
                            {514, "JPEGInterchangeFormatLength"},
                            // C : image data characteristics
                            {301, "TransferFunction"},
                            {318, "WhitePoint"},
                            {319, "PrimaryChromaticities"},
                            {529, "YCbCrCoefficients"},		 
                            {532, "ReferenceBlackWhite"},
                            // D : other
                            {306,"DateTime"},
                            {270,"ImageDescription"},
                            {271,"Make"},	
                            {272,"Model"},					
                            {305,"Software"},
                            {315,"Artist"},				
                            {3432,"Copyright"},

                            //EXIF IFD Attributes
                            // A : version
                            {36864, "ExifVersion"},
                            {40960, "FlashPixVersion"},
                            // B : image data characteristics
                            {40961, "ColorSpace"},
                            // C : image configuration
                            {37121, "ComponentsConfiguration"},
                            {37122, "CompressedBitsPerPixel"},
                            {40962, "PixelXDimension"},
                            {40963, "PixelYDimension"},
                            // D : user information
                            {37500, "MakerNote"},
                            {37510, "UserComment"},
                            // F : Date and Time
                            {36867, "DateTimeOriginal"},
                            {36868, "DateTimeDigitized"},
                            {37520, "SubSecTime"},
                            {37521, "SubSecTimeOriginal"},
                            {37522, "SubSecTimeDigitized"},

                            //Levels (2)
                            // G : picture taking conditions
                            {33434, "ExposureTime"},
                            {33437, "FNumber"},
                            {34850, "ExposureProgram"},
                            {34852, "SpectralSensitivity"},
                            {34855, "ISOSpeedRatings"},
                            {34856, "OECF"},
                            {37377, "ShutterSpeedValue"},
                            {37378, "ApertureValue"},
                            {37379, "BrightnessValue"},
                            {37380, "ExposureBiasValue"},
                            {37381, "MaxApertureValue"},
                            {37382, "SubjectDistance"},
                            {37383, "MeteringMode"},
                            {37384, "LightSource"},
                            {37385, "Flash"},
                            {37386, "FocalLength"},
                            {41483, "FlashEnergy"},
                            {41484, "SpatialFrequencyResponse"},
                            {41486, "FocalPlaneXResolution"},
                            {41487, "FocalPlaneYResolution"},
                            {41488, "FocalPlaneResolutionUnit"},
                            {41492, "SubjectLocation"},
                            {41493, "ExposureIndex"},
                            {41495, "SensingMethod"},
                            {41728, "FileSource"},
                            {41729, "SceneType"},
                            {41730, "CFAPattern"},

                            //levels (3)
                            // A : GPS
                            {0  ,"GPSVersionID"},
                            {1  ,"GPSLatitudeRef"},
                            {2  ,"GPSLatitude"},
                            {3  ,"GPSLongitudeRef"},
                            {4  ,"GPSLongitude"},
                            {5  ,"GPSAltitudeRef"},
                            {6  ,"GPSAltitude"},
                            {7  ,"GPSTimeStamp"},
                            {8  ,"GPSSatellites"},
                            {9  ,"GPSStatus"},
                            {10 ,"GPSMeasureMode"},
                            {11 ,"GPSDOP"},
                            {12 ,"GPSSpeedRef"},
                            {13 ,"GPSSpeed"},
                            {14 ,"GPSTrackRef"},
                            {15 ,"GPSTrack"},
                            {16 ,"GPSImgDirectionRef"},
                            {17 ,"GPSImgDirection"},
                            {18 ,"GPSMapDatum"},
                            {19 ,"GPSDestLatitudeRef"},
                            {20 ,"GPSDestLatitude"},
                            {21 ,"GPSDestLongitudeRef"},
                            {22 ,"GPSDestLongitude"},
                            {23 ,"GPSDestBearingRef"},
                            {24 ,"GPSDestBearing"},
                            {25 ,"GPSDestDistanceRef"},
                            {26 ,"GPSDestDistance"},
                       };
        }

        private static Dictionary<int, Func<object, object>> GetPropertyValueOptions()
        {
            throw new NotImplementedException();
            return new Dictionary<int, Func<object, object>>
                       {

                       };
        }

        //rename to PropertyItem Conversion Types
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
