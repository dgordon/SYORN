using System.Collections.Generic;
using System.Drawing.Imaging;
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
            _exifProperties = XElement.Load("Resources/ExifPropertyInformation.xml").Descendants("Property");
        }

        public ExifPropertyInfo From(PropertyItemEncoded propertyItem)
        {
            //use id to translate value with _propertyItemValueConverter
            var value = _propertyItemValueConverter.From(propertyItem.Type, propertyItem.Value);
            //once value has been converted
            //check if xelement has details to 'switch' (select matching translated value with a detail)
            var match = _exifProperties.FirstOrDefault(x => x.FirstAttribute.Value.Equals(propertyItem.Id.ToString()));
            //return match != null ? match.Value : null;
            //finally return a new property object with long name (and/or short name) along with the 
            //completed translated value.
            return new ExifPropertyInfo()
                       {
                           ShortName = match.Value,
                           Value = value.ToString()
                       };
        }
    }

    public interface IPropertyTranslator
    {
        ExifPropertyInfo From(PropertyItemEncoded propertyItem);
    }
}
