using GameTrader.Core.Factories;
using GameTrader.Core.Helpers;
using GameTrader.Core.StaticData;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Security.Claims;

namespace GameTrader.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {

        public BaseController()
        {
        }

        #region Protected Properties

        protected string UserId
        {
            get
            {

                var authorizationHeader = Request.Headers["Authorization"].FirstOrDefault();
                var token = string.IsNullOrEmpty(authorizationHeader) ? null : authorizationHeader.Split(" ").Last();

                return string.IsNullOrEmpty(token) ? null : JWTHelper.ExtractClaimsFromJwt(token, "id");
            }
        }

        protected string UserRole
        {
            get
            {

                var authorizationHeader = Request.Headers["Authorization"].FirstOrDefault();
                var token = string.IsNullOrEmpty(authorizationHeader) ? null : authorizationHeader.Split(" ").Last();

                return string.IsNullOrEmpty(token) ? null : JWTHelper.ExtractClaimsFromJwt(token, ClaimTypes.Role);
            }
        }
        #endregion

        #region Protected Functions

        /// <summary>
        /// Create Ok Response
        /// </summary>
        /// <returns></returns>
        protected ResponseFactory OK()
        => ResponseFactory.Create(HttpStatusCode.OK, new(), ValidationMessages.OperationSucceded);

        /// <summary>
        /// Create Unauthorized Response
        /// </summary>
        /// <returns></returns>
        protected ResponseFactory Unauthorized(string validationMessage)
        => ResponseFactory.Create(HttpStatusCode.Unauthorized, new(), validationMessage);

        /// <summary>
        /// Create Ok Response with Validation Message Only
        /// </summary>
        /// <param name="validationMessage"></param>
        /// <returns></returns>
        protected ResponseFactory OK(string validationMessage)
        => ResponseFactory.Create(HttpStatusCode.OK, new(), validationMessage);

        /// <summary>
        /// Create Ok Response with return Data Only
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        protected ResponseFactory OK(object data)
            => ResponseFactory.Create(HttpStatusCode.OK, data, new());

        /// <summary>
        /// Create Ok Response with Validation Message And return Data
        /// </summary>
        /// <param name="data"></param>
        /// <param name="validationMessage"></param>
        /// <returns></returns>
        protected ResponseFactory OK(object data, string validationMessage)
            => ResponseFactory.Create(HttpStatusCode.OK, data, validationMessage);

        /// <summary>
        /// Create Bad Response without any arguments
        /// </summary>
        /// <returns></returns>
        protected ResponseFactory BadRequest()
            => ResponseFactory.Create(HttpStatusCode.BadRequest, new(), ValidationMessages.BadRequest);

        /// <summary>
        /// Create Bad Response with Validation Message Only
        /// </summary>
        /// <param name="validationMessage"></param>
        /// <returns></returns>
        protected ResponseFactory BadRequest(string validationMessage)
            => ResponseFactory.Create(HttpStatusCode.BadRequest, new(), validationMessage);

        /// <summary>
        /// Create Bad Response with Array of Validation Messages
        /// </summary>
        /// <param name="validationMessage"></param>
        /// <returns></returns>
        protected ResponseFactory BadRequest(string[] validationMessage)
        {
            string validationMessages = string.Empty;
            foreach (var item in validationMessage)
                validationMessages = validationMessages + item + " ";

            return ResponseFactory.Create(HttpStatusCode.BadRequest, new(), validationMessage);
        }

        /// <summary>
        /// Create NotFound Response with Validation Message Only
        /// </summary>
        /// <param name="validationMessage"></param>
        /// <returns></returns>
        protected ResponseFactory NotFound(string validationMessage)
            => ResponseFactory.Create(HttpStatusCode.NotFound, new(), validationMessage);

        /// <summary>
        /// Create InternalServerError Response with Validation Message Only
        /// </summary>
        /// <param name="validationMessage"></param>
        /// <returns></returns>
        protected ResponseFactory InternalServerError(string validationMessage)
            => ResponseFactory.Create(HttpStatusCode.InternalServerError, new(), validationMessage);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="validationMessage"></param>
        /// <returns></returns>
        protected ResponseFactory InternalServerError()
        => ResponseFactory.Create(HttpStatusCode.InternalServerError, new(), ValidationMessages.OperationFaild);

        #endregion

    }
}
