using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.ServiceModel.Channels;
using System.Web;

namespace Matlab.Logger
{
    public static class LogThis
    {
        private static int EventId = 0;
        static LogThis()
        {

            if (!EventLog.SourceExists("Matlab.Logger"))
            {
                EventLog.CreateEventSource("Matlab.Logger", "Application");
            }
        }
        private static string GetIp(HttpRequestMessage request)
        {
            if (request.Properties.ContainsKey("MS_HttpContext"))
            {
                return ((HttpContextWrapper)request.Properties["MS_HttpContext"]).Request.UserHostAddress;
            }

            if (request.Properties.ContainsKey(RemoteEndpointMessageProperty.Name))
            {
                RemoteEndpointMessageProperty prop = (RemoteEndpointMessageProperty)request.Properties[RemoteEndpointMessageProperty.Name];
                return prop.Address;
            }

            if (HttpContext.Current != null)
            {
                return HttpContext.Current.Request.UserHostAddress;
            }

            return "test";
        }

        private static string GetUserName(HttpRequestMessage request)
        {
            return request.GetRequestContext()?.Principal.Identity.Name;
        }

        public static void BaseLog(HttpRequestMessage request, LogLevel logLevel, string action = null, Dictionary<LogProperties, object> dataDictionary = null)
        {
            if (dataDictionary == null)
            {
                dataDictionary = new Dictionary<LogProperties, object>();
            }
            if (!string.IsNullOrWhiteSpace(action)) dataDictionary.Add(LogProperties.Action, action);
            if (request != null)
            {
                dataDictionary.Add(LogProperties.Ip, GetIp(request));
                dataDictionary.Add(LogProperties.User, GetUserName(request));
            }
            BaseLog(logLevel, dataDictionary);
        }

        private static void BaseLog(LogLevel logLevel, Dictionary<LogProperties, object> dataDictionary = null)
        {

            if (dataDictionary == null)
            {
                dataDictionary = new Dictionary<LogProperties, object>();


            }

            using (var eventLog = new EventLog("Matlab.Logger"))
            {
                eventLog.Source = "Matlab.Logger";
                eventLog.Log = "Application";

                string message = "";
                foreach (var valuePair in dataDictionary)
                {
                    message += $"{valuePair.Key}: {valuePair.Value}\n";
                }

                //eventLog.WriteEntry("Log message example", EventLogEntryType.Information, 101, 1);
                eventLog.WriteEntry(message, logLevel.ConvertToEventLogEntryType(),EventId);
            }

            EventId++;
        }

        private static EventLogEntryType ConvertToEventLogEntryType(this LogLevel value)
        {
            switch (value)
            {
                case LogLevel.Debug:
                    return EventLogEntryType.SuccessAudit;
                case LogLevel.Info:
                    return EventLogEntryType.Information;
                case LogLevel.Warn:
                    return EventLogEntryType.Warning;
                case LogLevel.Error:
                    return EventLogEntryType.Error;
                case LogLevel.Fatal:
                    return EventLogEntryType.FailureAudit;
                default:
                    throw new ArgumentOutOfRangeException(nameof(value), value, null);
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
        Id,
        IdType,
        From,
        To,
        AdditionalData,
        State
    }


}
