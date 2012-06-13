using Machine.Specifications;
using SYORN.Services;

namespace SYORN.Specifications
{
    public class When_translating_property_name
    {
        Establish context = () => _translatePropertyName = new DefaultPropertyNameTranslator();

        Because of = () => _propertyName =_translatePropertyName.From(258);

        It should_have_retrieved_property_name_from_configuration = () => _propertyName.ShouldEqual("BitsPerSample");

        static string _propertyName;
        static DefaultPropertyNameTranslator _translatePropertyName;
    }
}