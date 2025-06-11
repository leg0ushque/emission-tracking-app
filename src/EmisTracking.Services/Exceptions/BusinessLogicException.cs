using System;
using System.Collections.Generic;

namespace EmisTracking.Services.Exceptions
{

    public class BusinessLogicException : Exception
    {
        public BusinessLogicException()
        {
        }

        public BusinessLogicException(string message) : base(message)
        {
        }

        public BusinessLogicException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public List<FieldError> FieldErrors { get; set; }

        public string SerializedData { get; set; }
    }
}
