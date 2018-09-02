using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Matlab.Logger
{
    public static class LogThis
    {
        private static string GetIp(HttpRequestMessage request)
        {
            if (request.Properties.ContainsKey("MS_HttpContext"))
            {
                return ((HttpContextWrapper)request.Properties["MS_HttpContext"]).Request.UserHostAddress;
            }
            else if (request.Properties.ContainsKey(RemoteEndpointMessageProperty.Name))
            {
                RemoteEndpointMessageProperty prop = (RemoteEndpointMessageProperty)request.Properties[RemoteEndpointMessageProperty.Name];
                return prop.Address;
            }
            else if (HttpContext.Current != null)
            {
                return HttpContext.Current.Request.UserHostAddress;
            }
            else
            {
                return "test";
            }
        }

        private static string GetUserName(HttpRequestMessage request)
        {
            return request?.GetUserPrincipal()?.Identity.Name;
        }

        public static async void BaseLog(HttpRequestMessage request, LogLevel logLevel, string action = null, Dictionary<LogProperties, object> dataDictionary = null)
        {
            if (dataDictionary==null)
            {
                dataDictionary=new Dictionary<LogProperties, object>();
            }
            if(string.IsNullOrWhiteSpace(action))dataDictionary.Add(LogProperties.Action,action);
            if (request != null)
            {
                dataDictionary.Add(LogProperties.Ip,GetIp(request));
                dataDictionary.Add(LogProperties.User,GetUserName(request));
            }
            BaseLog(logLevel,dataDictionary);
        }

        private static async void BaseLog(LogLevel logLevel, Dictionary<LogProperties, object> dataDictionary = null)
        {
            if (dataDictionary == null)
            {
                dataDictionary = new Dictionary<LogProperties, object>();
            }

        }
    }

    public enum LogLevel
    {
        Debug,
        Info,
        Warn,
        Error,
        Fatal
    }

    public enum LogProperties 
    {
        Ip,
        User,
        Count,
        Action,
        Message,
        Error,
        Id
    }

    
}
