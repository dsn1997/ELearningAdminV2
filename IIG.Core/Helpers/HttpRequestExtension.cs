using System.Net.Mime;
using System.Text;
using Microsoft.AspNetCore.Http;

namespace IIG.Core.Helpers
{
    public static class HttpRequestExtension
    {
        public static string GetHeaderByKey(this HttpRequest request, string headerKey)
        {
            return request.Headers.FirstOrDefault(x => x.Key == headerKey).Value.FirstOrDefault();
        }

        public static string GetRawRequestUrl(this HttpContext httpContext)
        {
            if (httpContext == null)
            {
                return null;
            }

            // https://stackoverflow.com/questions/38437005/how-to-get-current-url-in-view-in-asp-net-core-1-0
            return string.Format("{0}://{1}{2}{3}", httpContext.Request.Scheme, httpContext.Request.Host, httpContext.Request.Path, httpContext.Request.QueryString);
        }

        /// <summary>
        /// lấy correlation-id (crid) từ trong header x-crid ra, nếu chưa có tự động generate và ensure
        /// </summary>
        /// <param name="headerData">request header data</param>
        /// <returns>correlation id (guid format type = N)</returns>
        public static string EnsureRequestCrid(this IHeaderDictionary headerData)
        {
            var name = "x-crid";
            if (!headerData.ContainsKey(name))
            {
                headerData.Append(name, Guid.NewGuid().ToString());
            }

            return headerData[name].ToString();
        }

        /// <summary>
        /// gắn correlation-id (crid) vào response header
        /// </summary>
        /// <param name="headerData">response header data</param>
        /// <param name="crid">correlation id (guid format type = N)</param>
        public static void EnsureResponseCrid(this IHeaderDictionary headerData, string crid)
        {
            var name = "x-crid";
            if (!headerData.ContainsKey(name))
            {
                headerData.Append(name, crid);
            }
            else
            {
                headerData[name] = crid;
            }
        }

        public static Dictionary<string, string> ToDictionary(this IHeaderDictionary headerData)
        {
            if (headerData == null)
            {
                return null;
            }

            Dictionary<string, string> dic = new Dictionary<string, string>();
            foreach (string key in headerData.Keys)
            {
                if (!dic.ContainsKey(key))
                {
                    dic.Add(key, headerData[key]);
                }
            }

            return dic;
        }

        public static async Task<string> GetPayloadString(this HttpRequest request)
        {
            if (request == null || IsInValidLoggingPayload(request?.ContentType))
            {
                return string.Empty;
            }

            string payloadString = string.Empty;
            request.EnableBuffering();
            using (StreamReader reader = new StreamReader(
                request.Body,
                encoding: Encoding.UTF8,
                detectEncodingFromByteOrderMarks: false,
                bufferSize: -1,
                leaveOpen: true))
            {
                payloadString = await reader.ReadToEndAsync();
                request.Body.Position = 0;
            }

            return payloadString;
        }

        private static List<String> _excludeMimeTypeContent = new()
        {
            "application/octet-stream",
            "application/ogg",
            "application/pdf",
            "application/zip",
            "application/vnd"
        };
        
        public static bool IsInValidLoggingPayload(string contentType)
        {
            if (contentType == null) return true;

            if (contentType.StartsWith("text/", StringComparison.OrdinalIgnoreCase)) return false;

            if (contentType.StartsWith("application/", StringComparison.OrdinalIgnoreCase))
            {
                return _excludeMimeTypeContent.Any(type => contentType.StartsWith(type));
            }

            return true;
        }
    }
}
