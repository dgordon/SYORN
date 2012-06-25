using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using SYORN.Models;

namespace SYORN.Services
{
    public class DefaultPropertyItemTranslator : IPropertyTranslator
    {
        readonly IPropertyItemValueConverter _propertyItemValueConverter;
        private readonly IEnumerable<XElement> _exifProperties;

        public DefaultPropertyItemTranslator(IPropertyItemValueConverter propertyItemValueConverter)
        {
            _propertyItemValueConverter = propertyItemValueConverter;
            //TODO: decouple dependency
            _exifProperties = XDocument.Load("Resources/ExifPropertyInformation.xml").Descendants("Property");
        }

        public ExifPropertyInfo From(PropertyItemEncoded propertyItem)
        {
            string decodeValue = null;

            //find property
            var matchedProperty = FindPropertyMatch(propertyItem);
            
            //no match. no property. I'm done with you.
            if (matchedProperty == null) 
                return null;

            //need to explicitly convert values to a string to be a consistent type for all values
            var convertedValue = _propertyItemValueConverter.From(propertyItem.Type, propertyItem.Value);

            //does the value need to be translated from predefined codes?
            if (matchedProperty.HasElements)
                decodeValue = DecodeValue(matchedProperty, convertedValue);

            var propertyInfo = new ExifPropertyInfo()
                               {
                                   Name = matchedProperty.Attribute("name").Value,
                                   ShortName = matchedProperty.Attribute("shortname").Value,
                                   Value = string.IsNullOrEmpty(decodeValue) ? convertedValue.ToString() : decodeValue
                               };

            return propertyInfo;
        }

        private XElement FindPropertyMatch(PropertyItemEncoded propertyItem)
        {
           return _exifProperties.FirstOrDefault(x =>
                x.Attribute("dec").Value.Equals(propertyItem.Id.ToString())
                || x.Attribute("hex").Value.Equals(propertyItem.Id.ToString()));
        }
        private string DecodeValue(XElement propertyItem, object value)
        {
            string decodedValue = null;

            //get all possible codes
            var valueCodes = propertyItem.Elements("Values");
            //does the value match a predefined code?
            var matchedCode = valueCodes.Elements().FirstOrDefault(v => v.Attribute("code").Value == value.ToString());

            if (matchedCode == null)
            {
                //if value didn't match any codes then default it to the predefined default
                var predefinedDefault = valueCodes.Attributes("default").FirstOrDefault().Value;
                //get predefined decoded value
                var decodedMatch = valueCodes.Elements()
                    .FirstOrDefault(v => v.Attribute("code").Value == predefinedDefault.ToString());
                //it is possible that a default doesn't have a value (defined in property item resource file)
                decodedValue = decodedMatch != null ? decodedMatch.Value : null;
            }
            else
            {
                //use matched coded value
                decodedValue = matchedCode.Value;
            }

            return decodedValue;
        }
    }

    public interface IPropertyTranslator
    {
        ExifPropertyInfo From(PropertyItemEncoded propertyItem);
    }
}
