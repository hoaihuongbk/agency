using System;
using ServiceStack;
using ServiceStack.Logging;

namespace Sima.Common.Logging
{
    public class SlackLogger : ILog
    {
        public string IncomingWebHookUrl { get; set; }

        public SlackLogger(string incomingWebHookUrl)
        {
            this.IncomingWebHookUrl = incomingWebHookUrl;
        }


        public void Info(object message)
        {
            IncomingWebHookUrl.PostJsonToUrl(new { text = "INFO:" + message });
        }

        public void Info(object message, Exception exception)
        {
            IncomingWebHookUrl.PostJsonToUrl(new { text = "INFO:" + message + Environment.NewLine + " StackTrace:" + Environment.NewLine + exception.StackTrace });
        }
        public void Debug(object message)
        {
            IncomingWebHookUrl.PostJsonToUrl(new { text = "Debug:" + message });
        }

        public void Debug(object message, Exception exception)
        {
            IncomingWebHookUrl.PostJsonToUrl(new { text = "Debug:" + message + Environment.NewLine + " StackTrace:" + Environment.NewLine + exception.StackTrace });
        }

        public void DebugFormat(string format, params object[] args)
        {
            throw new NotImplementedException();
        }

        public void Error(object message)
        {
            IncomingWebHookUrl.PostJsonToUrl(new { text = "Error:" + message });
        }

        public void Error(object message, Exception exception)
        {
            IncomingWebHookUrl.PostJsonToUrl(new { text = "Error:" + message + Environment.NewLine + " StackTrace:" + Environment.NewLine + exception.StackTrace });
        }

        public void ErrorFormat(string format, params object[] args)
        {
            throw new NotImplementedException();
        }

        public void Fatal(object message)
        {
            IncomingWebHookUrl.PostJsonToUrl(new
            {
                text = "Fatal:" + message
            });
        }

        public void Fatal(object message, Exception exception)
        {
            IncomingWebHookUrl.PostJsonToUrl(new
            {
                text = "Fatal:" + message + Environment.NewLine + " StackTrace:" + Environment.NewLine + exception.StackTrace
            });
        }

        public void FatalFormat(string format, params object[] args)
        {
            throw new NotImplementedException();
        }


        public void InfoFormat(string format, params object[] args)
        {
            throw new NotImplementedException();
        }

        public void Warn(object message)
        {
            IncomingWebHookUrl.PostJsonToUrl(new
            {
                text = "Warn:" + message
            });
        }

        public void Warn(object message, Exception exception)
        {
            IncomingWebHookUrl.PostJsonToUrl(new
            {
                text = "Warn:" + message + Environment.NewLine + " StackTrace:" + Environment.NewLine + exception.StackTrace
            });
        }

        public void WarnFormat(string format, params object[] args)
        {
            throw new NotImplementedException();
        }

        public bool IsDebugEnabled { get; }
    }
}
