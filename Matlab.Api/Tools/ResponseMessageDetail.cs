using System.Diagnostics.CodeAnalysis;
using System.Web.Http.ModelBinding;

namespace Matlab.Api.Tools
{
    /// <summary>
    /// Data about <see cref="ResponseMessage"/>
    /// </summary>
    [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Local")]
    public class ResponseMessageDetail
    {
        /// <summary>
        /// response code
        /// </summary>
        public ResponseMessage.ResponseCode Code { get; }
        /// <summary>
        /// response message
        /// </summary>
        public string Message { get; set; }

        // ReSharper disable once MemberCanBePrivate.Global
        /// <summary>
        /// Result Object
        /// </summary>
        public object Result { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="code"></param>
        /// <param name="message"></param>
        /// <param name="result"></param>
        public ResponseMessageDetail(ResponseMessage.ResponseCode code, string message, object result = null)
        {
            Code = code;
            
            if (result?.GetType() == typeof(ModelStateDictionary))
            {
                foreach (var item in (ModelStateDictionary)result)
                {
                    message += $"{item.Key} : {item.Value}\n";
                }
            }
            else
            {
                Message = message;
                Result = result;
            }
        }

        //public static readonly ResponseMessageDetail InternalError =
        //    new ResponseMessageDetail(ResponseMessage.ResponseCode.InternalErrord, ErrorMessages.InternalError);

        //public static ResponseMessageDetail Ok => new ResponseMessageDetail(ResponseMessage.ResponseCode.Ok, null);
    }
}