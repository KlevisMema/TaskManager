/* 
 This class represents a generic response that encapsulates the result of an operation.
 It provides properties to store a message, value, success flag, and status code.
*/

using System.Net;

namespace TaskManager.HELPERS.ServiceResponse
{
    /// <summary>
    /// Represents a generic response class that encapsulates the result of an operation.
    /// </summary>
    /// <typeparam name="T">The type of the value in the response.</typeparam>
    public class Response<T>
    {
        /// <summary>
        /// Gets or sets the message associated with the response.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets the value contained in the response.
        /// </summary>
        public T? Value { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the operation was successful.
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// Gets or sets the status code associated with the response.
        /// </summary>
        public HttpStatusCode StatusCode { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Response{T}"/> class with the specified error message, success flag, and status code.
        /// </summary>
        /// <param name="errorMessage">The error message associated with the response.</param>
        /// <param name="success">A value indicating whether the operation was successful.</param>
        /// <param name="statusCode">The status code associated with the response.</param>
        public Response(string errorMessage, bool success, HttpStatusCode statusCode)
        {
            Message = errorMessage;
            Success = success;
            StatusCode = statusCode;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Response{T}"/> class with the specified error message, value, success flag, and status code.
        /// </summary>
        /// <param name="errorMessage">The error message associated with the response.</param>
        /// <param name="value">The value contained in the response.</param>
        /// <param name="success">A value indicating whether the operation was successful.</param>
        /// <param name="statusCode">The status code associated with the response.</param>
        public Response(string errorMessage, T value, bool success, HttpStatusCode statusCode)
        {
            Message = errorMessage;
            Value = value;
            Success = success;
            StatusCode = statusCode;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Response{T}"/> class with the specified error message, status code, and success flag.
        /// </summary>
        /// <param name="errorMessage">The error message associated with the response.</param>
        /// <param name="statusCode">The status code associated with the response.</param>
        /// <param name="success">A value indicating whether the operation was successful.</param>
        public Response(string errorMessage, HttpStatusCode statusCode, bool success)
        {
            Message = errorMessage;
            Value = default;
            Success = success;
            StatusCode = statusCode;
        }

        /// <summary>
        /// Creates a successful response with the specified value and message.
        /// </summary>
        /// <param name="value">The value contained in the response.</param>
        /// <param name="message">The message associated with the response.</param>
        /// <returns>A successful response.</returns>
        public static Response<T> Ok(T value, string message)
        {
            return new Response<T>(message, value, true, HttpStatusCode.OK);
        }

        /// <summary>
        /// Creates a not found response with the specified error message.
        /// </summary>
        /// <param name="errorMessage">The error message associated with the response.</param>
        /// <returns>A not found response.</returns>
        public static Response<T> NotFound(string errorMessage)
        {
            return new Response<T>(errorMessage, HttpStatusCode.NotFound, false);
        }

        /// <summary>
        /// Creates a successful response with the specified message.
        /// </summary>
        /// <param name="message">The message associated with the response.</param>
        /// <returns>A successful response.</returns>
        public static Response<T> SuccessMessage(string message)
        {
            return new Response<T>(message, HttpStatusCode.OK, true);
        }

        /// <summary>
        /// Creates an error response with the specified error message.
        /// </summary>
        /// <param name="throwMessage">The error message associated with the response.</param>
        /// <returns>An error response.</returns>
        public static Response<T> ErrorMsg(string throwMessage)
        {
            return new Response<T>(throwMessage, HttpStatusCode.InternalServerError, false);
        }

        /// <summary>
        /// Creates an unsuccessful response with the specified message.
        /// </summary>
        /// <param name="message">The message associated with the response.</param>
        /// <returns>An unsuccessful response.</returns>
        public static Response<T> UnSuccessMessage(string message)
        {
            return new Response<T>(message, false, HttpStatusCode.BadRequest);
        }
    }
}