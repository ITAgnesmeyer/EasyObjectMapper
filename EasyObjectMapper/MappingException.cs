using System;

namespace EasyObjectMapper
{
    public class MappingException : Exception
    {
        public MappingException(string message, Exception innerException):base(message, innerException)
        {
            
        }
    }
}