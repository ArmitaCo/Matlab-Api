using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using Newtonsoft.Json;

namespace Matlab.Api.Tools
{
    /// <summary>
    /// Response the request with code and message
    /// </summary>
    public class ResponseMessage:IHttpActionResult
    {
        /// <summary>
        /// Have data about <see cref="ResponseMessageDetail"/>
        /// </summary>
        // ReSharper disable once MemberCanBePrivate.Global
        public ResponseMessageDetail Data { get; }
        /// <summary>
        /// resolve <see cref="ResponseMessageDetail.Code" /> directly
        /// </summary>
        [JsonIgnore]
        public ResponseCode Code => Data.Code;

        /// <summary>
        /// resolve <see cref="ResponseMessageDetail.Message" />directly
        /// </summary>
        [JsonIgnore]
        // ReSharper disable once MemberCanBePrivate.Global
        public string Message => Data.Message;


        /*
                /// <summary>
                /// resolve<see cref="ResponseMessageDetail.Result" /> directly
                /// </summary>
                [JsonIgnore]
                public object Result
                {
                    get => Data.Result;
                    set => Data.Result = value;
                }
        */

        [JsonIgnore]
        private string ActionName { get; }


        /// <summary>
        /// 
        /// </summary>
        [SuppressMessage("ReSharper", "MissingXmlDoc")]
        public enum ResponseCode
        {
            FormatError = 100,
            NotFound = 101,
            AuthenticationFailed = 102,
            Expire = 103,
            UserStatusFaild = 104,
            Ok = 200,
            InternalErrord = 500
        }

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="code"></param>
        /// <param name="message"></param>
        /// <param name="result"></param>
        public ResponseMessage(ResponseCode code, string message, object result = null)
        {
            StackTrace st = new StackTrace();
            var stackFrames = st.GetFrames();
            ActionName = stackFrames?.Select(x => x.GetMethod().Name).FirstOrDefault(x => x.StartsWith("Post") && !x.EndsWith("Validator"))?.Replace("Post", "");
            Data = new ResponseMessageDetail(code, message, result);
        }

        /// <summary>
        /// Define Internal Error
        /// </summary>
        public static readonly ResponseMessage InternalError =
            new ResponseMessage(ResponseCode.InternalErrord, ErrorMessages.InternalError);

        /// <summary>
        /// Base ok <see cref="ResponseMessage"/>
        /// </summary>
        public static ResponseMessage Ok => new ResponseMessage(ResponseCode.Ok, ErrorMessages.OK);

        //public static ResponseMessage UserStatusError(UserStatusEnum userStatusEnum)
        //{
        //    switch (userStatusEnum)
        //    {
        //        case UserStatusEnum.WaitingToMobileVerification:
        //            return new ResponseMessage(ResponseCode.UserStatusFaild, ErrorMessages.UserStatusMobileCode);
        //        case UserStatusEnum.WaitingToFillInformation:
        //            return new ResponseMessage(ResponseCode.UserStatusFaild, ErrorMessages.UserStatusInformation);
        //        case UserStatusEnum.Verified:
        //            return new ResponseMessage(ResponseCode.UserStatusFaild, ErrorMessages.UserVerified);
        //        case UserStatusEnum.Deactivated:
        //            return new ResponseMessage(ResponseCode.UserStatusFaild, ErrorMessages.UserStatusDeactivated);
        //        default:
        //            throw new ArgumentOutOfRangeException(nameof(userStatusEnum), userStatusEnum, null);
        //    }
        //}

        //public static ResponseMessage UserStatusError(UserStatus userStatus)
        //{
        //    return UserStatusError(userStatus.StatusEnum);
        //}

        //public static ResponseMessage UserStatusError(User user)
        //{
        //    return UserStatusError((UserStatusEnum)user.UUsID);
        //}


        /// <summary>
        /// ok <see cref="ResponseMessage" /> with <paramref name="result" />
        /// </summary>
        /// <param name="result"></param>
        /// <returns>
        /// 
        /// </returns>
        public static ResponseMessage OkWithResult(object result)
        {
            return new ResponseMessage(ResponseCode.Ok, ErrorMessages.OK, result);
        }

        /// <summary>
        /// ok <see cref="ResponseMessage" /> with <paramref name="message"/>
        /// </summary>
        /// <param name="message"></param>
        /// <returns>
        /// 
        /// </returns>
        public static ResponseMessage OkWithMessage(string message)
        {
            return new ResponseMessage(ResponseCode.Ok, message);
        }

        /// <summary>
        /// Log on serialize <see cref="ResponseMessage"/>
        /// </summary>
        /// <param name="context"></param>
        [OnSerializing]
        public void SerializingLogger(StreamingContext context)
        {
            if (Code != ResponseCode.Ok)
            {
                HttpRequestMessage httpRequestMessage =
                    HttpContext.Current.Items["MS_HttpRequestMessage"] as HttpRequestMessage;
                var ip = StaticTools.GetIp(httpRequestMessage);
                //Logger.ActionFailed(ip, ActionName, Message, new Dictionary<string, string> { { DataKind.Code, ((int)Code).ToString() }, { DataKind.Error, Code.ToString() } });
            }
        }

        #region Implementation of IHttpActionResult

        /// <inheritdoc />
        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            return Task.FromResult(new HttpResponseMessage()
            {
                Content = new ObjectContent<ResponseMessage>(this,GlobalConfiguration.Configuration.Formatters.JsonFormatter),
                StatusCode = HttpStatusCode.OK
            });
        }

        #endregion
    }
}