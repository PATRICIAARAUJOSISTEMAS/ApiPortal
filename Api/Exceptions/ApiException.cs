using System;
using System.Net;

namespace Api.Exceptions
{
    public class ApiException : Exception
    {
        public ApiException(string mesagem, HttpStatusCode code) : base(mesagem)
        {
            Code = code;
        }

        public HttpStatusCode Code { get; private set; }
    }
}