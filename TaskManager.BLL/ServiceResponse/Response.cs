using System.Net;

namespace TaskManager.BLL.ServiceResponse
{
    public class Response<T>
    {
        public string Message { get; set; }
        public T? Value { get; set; }
        public bool Success { get; set; }
        public HttpStatusCode StatusCode { get; set; }

        public Response
        (
            string errorMessage,
            bool success,
            HttpStatusCode statusCode
        )
        {
            Message = errorMessage;
            Success = success;
            StatusCode = statusCode;
        }

        public Response
        (
            string errorMessage,
            T value,
            bool success,
            HttpStatusCode statusCode
        )
        {
            Message = errorMessage;
            Value = value;
            Success = success;
            StatusCode = statusCode;
        }
        public Response
        (
            string errorMessage,
            HttpStatusCode statusCode,
            bool success
        )
        {
            Message = errorMessage;
            Value = default;
            Success = success;
            StatusCode = statusCode;
        }

     
        public static Response<T> 
        Ok
        (
            T value,
            string message
        )
        {
            return new Response<T>(message, value, true, HttpStatusCode.OK);
        }

     
        public static Response<T> 
        NotFound
        (
            string errorMessage
        )
        {
            return new Response<T>(errorMessage, HttpStatusCode.NotFound, false);
        }

      
        public static Response<T> 
        SuccessMessage
        (
            string message
        )
        {
            return new Response<T>(message, HttpStatusCode.OK, true);
        }

        public static Response<T> 
        ErrorMsg
        (
            string ThrowMessage
        )
        {
            return new Response<T>(ThrowMessage, HttpStatusCode.InternalServerError, false);
        }

        public static Response<T> 
        UnSuccessMessage
        (
            string message
        )
        {
            return new Response<T>(message, false, HttpStatusCode.BadRequest);
        }

    }
}