using System;
using Machine.Specifications;
using SYORN.Services;

namespace SYORN.Specifications
{
    public class When_translating_byte_array_to_byte
    {
        Establish context = () =>
        {
            valueToConvert = new byte[] { 5 };
            propertyItemValueTranslator = new DefaultPropertyItemValueTranslator();
        };

        Because of = () => result = propertyItemValueTranslator.From(1, valueToConvert);

        It should_have_been_translated_to_byte = () => ((byte)result).ShouldEqual((byte)5);

        static DefaultPropertyItemValueTranslator propertyItemValueTranslator;
        static byte[] valueToConvert;
        static object result;
    }

    public class When_translating_byte_array_to_string
    {
        Establish context = () =>
        {
            var encoding = new System.Text.ASCIIEncoding();
            valueToConvert = encoding.GetBytes("test value");
            propertyItemValueTranslator = new DefaultPropertyItemValueTranslator();
        };

        Because of = () => result = propertyItemValueTranslator.From((short)2, valueToConvert);

        //NOTE: when translating ASCII values the last character is intentionally truncated.
        It should_have_been_translated = () => result.ShouldEqual("test valu");

        static DefaultPropertyItemValueTranslator propertyItemValueTranslator;
        static byte[] valueToConvert;
        static object result;
    }

    public class When_translating_byte_array_to_an_unsigned_short
    {
        Establish context = () =>
        {
            valueToConvert = BitConverter.GetBytes(234);
            propertyItemValueTranslator = new DefaultPropertyItemValueTranslator();
        };

        Because of = () => result = propertyItemValueTranslator.From(3, valueToConvert);

        It should_have_been_translated = () => ((ushort)result).ShouldEqual((ushort)234);

        static DefaultPropertyItemValueTranslator propertyItemValueTranslator;
        static byte[] valueToConvert;
        static object result;
    }

    public class When_translating_byte_array_to_an_unsigned_int
    {
        Establish context = () =>
        {
            valueToConvert = BitConverter.GetBytes(3463465);
            propertyItemValueTranslator = new DefaultPropertyItemValueTranslator();
        };

        Because of = () => result = propertyItemValueTranslator.From(4, valueToConvert);

        It should_have_been_translated = () => ((uint)result).ShouldEqual((uint)3463465);

        static DefaultPropertyItemValueTranslator propertyItemValueTranslator;
        static byte[] valueToConvert; 
        static object result;
    }

    public class When_translating_byte_array_to_rational
    {
        Establish context = () =>
        {
            propertyItemValueTranslator = new DefaultPropertyItemValueTranslator();
            var numerator = BitConverter.GetBytes(1u);
            var denominator = BitConverter.GetBytes(200u);
            valueToConvert = new byte[8];
            numerator.CopyTo(valueToConvert, 0);
            denominator.CopyTo(valueToConvert, 4);
        };

        Because of = () => result = propertyItemValueTranslator.From(5, valueToConvert);

        It should_have_been_translated = () => result.ShouldEqual(1d / 200d);

        static DefaultPropertyItemValueTranslator propertyItemValueTranslator;
        static byte[] valueToConvert;
        static object result;
    }

    //TODO: undefined tags are converted based on the tag name
    //public class When_translating_byte_array_to_undefined
    //{
    //    Establish context = () =>
    //    {
    //        valueToConvert = BitConverter.GetBytes(234234.34d);
    //        propertyItemValueTranslator = new DefaultPropertyItemValueTranslator();
    //    };

    //    Because of = () => result = propertyItemValueTranslator.From(7, valueToConvert);

    //    It should_have_been_translated = () => result.ShouldEqual(5);

    //    static DefaultPropertyItemValueTranslator propertyItemValueTranslator;
    //    static byte[] valueToConvert;
    //    static object result;
    //}

    public class When_translating_byte_array_to_a_signed_int
    {
        Establish context = () =>
        {
            valueToConvert = BitConverter.GetBytes(23487345);
            propertyItemValueTranslator = new DefaultPropertyItemValueTranslator();
        };

        Because of = () => result = propertyItemValueTranslator.From(9, valueToConvert);

        It should_have_been_translated = () => ((int)result).ShouldEqual(23487345);

        static DefaultPropertyItemValueTranslator propertyItemValueTranslator;
        static byte[] valueToConvert;
        static object result;
    }

    public class When_translating_byte_array_to_sRational
    {
        Establish context = () =>
        {
            propertyItemValueTranslator = new DefaultPropertyItemValueTranslator();
            var numerator = BitConverter.GetBytes(3);
            var denominator = BitConverter.GetBytes(5);
            valueToConvert = null;
            valueToConvert = new byte[8];
            numerator.CopyTo(valueToConvert, 0);
            denominator.CopyTo(valueToConvert, 4);
        };

        Because of = () => result = propertyItemValueTranslator.From(10, valueToConvert);

        It should_have_been_translated = () => result.ShouldEqual(3d / 5d);

        static DefaultPropertyItemValueTranslator propertyItemValueTranslator;
        static byte[] valueToConvert;
        static object result;
    }
}
