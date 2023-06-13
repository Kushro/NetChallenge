using System;

namespace NetChallenge.Domain.Exceptions
{
    public class BusinessException : Exception
    {
        public int Status { get; set; } = 409;

        public BusinessException() { }

        public BusinessException(string message) : base(message) { }
    }
}