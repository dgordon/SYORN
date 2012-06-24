using System.Drawing.Imaging;
using SYORN.Models;

namespace SYORN.Helpers
{
    public static class PropertyItemWrapper
    {
        public static PropertyItemEncoded CastToExifPropertyInfo(this PropertyItem propertyItem)
        {
            return new PropertyItemEncoded(propertyItem.Id, propertyItem.Type, propertyItem.Len, propertyItem.Value);
        }
    }
}
