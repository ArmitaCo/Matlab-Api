using System.Data.Entity.Spatial;
using System.Globalization;
using System.Net.Http;
using System.ServiceModel.Channels;
using System.Web;

namespace Matlab.Api.Tools
{
    public static class StaticTools
    {
        /// <summary>
        /// Get Requester Ip
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static string GetIp(HttpRequestMessage request)
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

        /// <summary>
        /// Create <see cref="DbGeography"/> from two double value
        /// </summary>
        /// <param name="latitude"></param>
        /// <param name="longitude"></param>
        /// <returns></returns>
        public static DbGeography CreatePoint(double latitude, double longitude)
        {
            var point = string.Format(CultureInfo.InvariantCulture.NumberFormat,
                "POINT({0} {1})", longitude, latitude);
            // 4326 is most common coordinate system used by GPS/Maps
            return DbGeography.PointFromText(point, 4326);
        }
    }
}