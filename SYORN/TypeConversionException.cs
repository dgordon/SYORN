using System;

namespace SYORN.Services
{
    public class TypeConversionException : Exception
    {
        public TypeConversionException(string message)
            : base(message)
        { }
    }
}