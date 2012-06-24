using FakeItEasy;
using Machine.Specifications;
using SYORN.Models;
using SYORN.Services;

namespace SYORN.Specifications
{
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

        It should_have_retrieved_property_name_from_configuration = () => _exifProperty.ShortName.ShouldEqual("ImageWidth");

        static ExifPropertyInfo _exifProperty;
        static PropertyItemEncoded _propertyItemEncoded;
        static DefaultPropertyItemTranslator _translateProperty;
    }
}