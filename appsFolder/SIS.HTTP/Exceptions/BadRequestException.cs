namespace SIS.HTTP.Exceptions
{
    using System;

    public class BadRequestException:Exception
    {

        protected const string ExceptionMessage = "The Request was malformed or contains unsupported elements.";

        public BadRequestException()
        :base(ExceptionMessage){}

    }
}
