using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Hqub.MusicBrainz.API
{
    public class HttpClientException : ReadableException
    {
        public HttpClientException(HttpStatusCode statusCode)
        {
            // if more than 1 request per second per ip (on average)
            if (statusCode == HttpStatusCode.ServiceUnavailable)
            {
                _errorMessage = "Please try again";
                _errorDescription = "The server is temporarily unavailable, usually due to high load or maintenance.";
            }
            else if (statusCode == HttpStatusCode.NotFound)
            {
                _errorMessage = "Error 404";
                _errorDescription = "The requested resource does not exist on the server.";
            }
            else
            {
                _errorMessage = "Network Error";
                _errorDescription = statusCode.ToString();
            }
        }

        public HttpClientException(int hresult)
        {
            uint errorCode = (uint)hresult;
            // 0x80072F30 -> when not connected to wifi/3g
            // 0x80072EE7 -> When invalid domain name couldn't be found
            // 0x80190194 -> When ensureSuccessCode fails -> error: 404

            _errorMessage = "Network Problem";
            _errorDescription = "You are not connected to the Internet.";
        }
    }
}
