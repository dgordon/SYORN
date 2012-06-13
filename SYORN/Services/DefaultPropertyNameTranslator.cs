using System.Configuration;
using System.Globalization;
using System.Reflection;
using System.Resources;

namespace SYORN.Services
{
    public class DefaultPropertyNameTranslator : IPropertyNameTranslator
    {
        public string From(int key)
        {
            ResourceManager rm = new ResourceManager("PropertyListResource", Assembly.GetExecutingAssembly());
            var value = rm.GetString(key.ToString(), CultureInfo.CurrentCulture);
            //var keys = ConfigurationManager.AppSettings.AllKeys;
            //var value = ConfigurationManager.AppSettings[key.ToString()];
            return value;
        }
    }

    public interface IPropertyNameTranslator
    {
        string From(int key);
    }
}
