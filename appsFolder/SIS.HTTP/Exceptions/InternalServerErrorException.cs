namespace SIS.HTTP.Exceptions
{
    using System;

    public class InternalServerErrorException:Exception
    {

        protected const string ErrorMessage = "The Server has encountered an error";

        public InternalServerErrorException()
            :base(ErrorMessage)
        { }
    }
}
