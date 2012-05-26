using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using FakeItEasy;
using Machine.Specifications;
using SYORN.Services;

namespace SYORN.Specifications
{
    public class When_converting_property_with_type_code_1
    {
        Establish context = () =>
                                {
                                    propertyItem = A.Fake<PropertyItem>();
                                    //A.CallTo(() => propertyItem.Type).Returns((short)1);
                                    //A.CallTo(() => propertyItem.Value).Returns(new byte[] {5});
                                    //propertyItem = prop
                                };
        Because of = () => result = exifDataProvider.Convert(propertyItem);
        It should_have_been_converted = () => result.Equals(5);

        static ExifDataProvider exifDataProvider;
        static PropertyItem propertyItem;
        static object result;
    }

    public class When_converting_property_with_type_code_2
    {

    }
    public class When_converting_property_with_type_code_3
    {

    }
    public class When_converting_property_with_type_code_4
    {

    }
    public class When_converting_property_with_type_code_5
    {

    }
    public class When_converting_property_with_type_code_7
    {

    }
    public class When_converting_property_with_type_code_8 
    {

    }
}
