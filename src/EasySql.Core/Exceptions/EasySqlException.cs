using System;

namespace EasySql.Exceptions
{
    public class EasySqlException : Exception
    {
        public EasySqlException() : base()
        {
        }

        public EasySqlException(string message) : base(message)
        {
        }

        public EasySqlException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
