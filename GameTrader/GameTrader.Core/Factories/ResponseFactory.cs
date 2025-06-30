using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace GameTrader.Core.Factories
{
    public class ResponseFactory
    {
        /// <summary>
        /// Create Response Body
        /// </summary>
        /// <param name="StatusCode"></param>
        /// <param name="Result"></param>
        /// <param name="validationMessage"></param>
        /// <returns></returns>
        public static ResponseFactory Create(HttpStatusCode StatusCode, object Result = null, object validationMessage = null)
            => new(StatusCode, Result, validationMessage);

        /// <summary>
        /// Return ValidationMessages List of Response
        /// </summary>
        public object ValidationMessages { get; set; }

        /// <summary>
        /// Return Response Data Body
        /// </summary>
        public object Data { get; set; }

        /// <summary>
        /// Success Flag of Response
        /// </summary>
        public bool Success { get; set; }


        /// <summary>
        /// Private Constractor Initialize Response
        /// </summary>
        /// <param name="statusCode"></param>
        /// <param name="result"></param>
        /// <param name="errorMessage"></param>
        private ResponseFactory(HttpStatusCode statusCode, object result = null, object validationMessage = null)
        {
            Success = ((int)statusCode) == 200;
            Data = result is null ? new() : result;
            ValidationMessages = validationMessage != null ? validationMessage : string.Empty;
        }
    }
}
