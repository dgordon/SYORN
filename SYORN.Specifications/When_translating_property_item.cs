using FakeItEasy;
using Machine.Specifications;
using SYORN.Models;
using SYORN.Services;

namespace SYORN.Specifications
{
    /// <remarks>
    /// this is a simple property item that has no value codes to decode
    /// </remarks>
    public class When_translating_property_item_Image_Width
    {
        Establish context = () =>
                                {
                                    _propertyItemEncoded = A.Fake<PropertyItemEncoded>();
                                    A.CallTo(() => _propertyItemEncoded.Id).Returns(256);
                                    A.CallTo(() => _propertyItemEncoded.Type).Returns((short)3);

                                    var converter = A.Fake<IPropertyItemValueConverter>();
                                    A.CallTo(() => converter.From(A<short>.Ignored, A<byte[]>.Ignored))
                                        .Returns(350);

                                    _translateProperty = new DefaultPropertyItemTranslator(converter);
                                };

        Because of = () => _exifProperty = _translateProperty.From(_propertyItemEncoded);

        It should_have_short_name = () => _exifProperty.ShortName.ShouldEqual("ImageWidth");
        It should_have_long_name = () => _exifProperty.Name.ShouldEqual("Image width");
        It should_have_value = () => _exifProperty.Value.ShouldEqual("350");

        static ExifPropertyInfo _exifProperty;
        static PropertyItemEncoded _propertyItemEncoded;
        static DefaultPropertyItemTranslator _translateProperty;
    }
    //todo: need to write specs for a property item that doesn't have a match (returns null)
    //todo: need to write specs for a property item that need the value to be decoded and match is found
    //todo: need to write specs for a property item that need the value to be decoded and no match is found and defaulted to predefined
    //todo: need to write specs for a property item that need the value to be decoded and no match is found and can't default to predefined
}