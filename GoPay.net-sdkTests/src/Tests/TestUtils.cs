using HttpTracer;
using HttpTracer.Logger;
using System;
using System.Collections.Generic;

namespace GoPay.Tests

{
    public class TestUtils
    {

        /*
        public const string CLIENT_ID = "1744960415";
        public const string CLIENT_SECRET = "h9wyRz2s";
        public const long GOID = 8339303643;
        */

        public const string CLIENT_ID = "1689337452";
        public const string CLIENT_SECRET = "CKr7FyEE";
        public const long GOID = 8712700986;

        public const string API_URL = @"https://gw.sandbox.gopay.com/api";

        public const string CLIENT_ID_EET = "1365575992";
        public const string CLIENT_SECRET_EET = "NUVsrv4W";
        public const long GOID_EET = 8289213768L;

        public static GPConnector CreateClient()
        {
            return new GPConnector(API_URL, CLIENT_ID, CLIENT_SECRET, true, handler => new HttpTracerHandler(handler, new ConsoleLogger(), HttpMessageParts.All));
        }
    }


    public class ConsoleLogger : ILogger
    {
        public void Log(string message) => Console.WriteLine(message);
    }
}
