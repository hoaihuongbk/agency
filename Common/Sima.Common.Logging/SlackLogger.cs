using System;
using ServiceStack;
using ServiceStack.Logging;

namespace Sima.Common.Logging
{
    public class SlackLogger : ILog
    {
        public string incomingWebHookUrl { get; set; }

        public SlackLogger(string incomingWebHookUrl)
        {
            this.incomingWebHookUrl = incomingWebHookUrl;
        }


        public void Info(object message)
        {
            incomingWebHookUrl.PostJsonToUrl(new { text = "INFO:" + message.ToString() });
        }

        public void Info(object message, Exception exception)
        {
            incomingWebHookUrl.PostJsonToUrl(new { text = "INFO:" + message.ToString() + Environment.NewLine + " StackTrace:" + Environment.NewLine + exception.StackTrace });
        }
        public void Debug(object message)
        {
            incomingWebHookUrl.PostJsonToUrl(new { text = "Debug:" + message.ToString() });
        }

        public void Debug(object message, Exception exception)
        {
            incomingWebHookUrl.PostJsonToUrl(new { text = "Debug:" + message.ToString() + Environment.NewLine + " StackTrace:" + Environment.NewLine + exception.StackTrace });
        }

        public void DebugFormat(string format, params object[] args)
        {
            throw new NotImplementedException();
        }

        public void Error(object message)
        {
            incomingWebHookUrl.PostJsonToUrl(new { text = "Error:" + message.ToString() });
        }

        public void Error(object message, Exception exception)
        {
            incomingWebHookUrl.PostJsonToUrl(new { text = "Error:" + message.ToString() + Environment.NewLine + " StackTrace:" + Environment.NewLine + exception.StackTrace });
        }

        public void ErrorFormat(string format, params object[] args)
        {
            throw new NotImplementedException();
        }

        public void Fatal(object message)
        {
            incomingWebHookUrl.PostJsonToUrl(new
            {
                text = "Fatal:" + message.ToString()
            });
        }

        public void Fatal(object message, Exception exception)
        {
            incomingWebHookUrl.PostJsonToUrl(new
            {
                text = "Fatal:" + message.ToString() + Environment.NewLine + " StackTrace:" + Environment.NewLine + exception.StackTrace
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
            incomingWebHookUrl.PostJsonToUrl(new
            {
                text = "Warn:" + message.ToString()
            });
        }

        public void Warn(object message, Exception exception)
        {
            incomingWebHookUrl.PostJsonToUrl(new
            {
                text = "Warn:" + message.ToString() + Environment.NewLine + " StackTrace:" + Environment.NewLine + exception.StackTrace
            });
        }

        public void WarnFormat(string format, params object[] args)
        {
            throw new NotImplementedException();
        }

        public bool IsDebugEnabled { get; }
    }
}
