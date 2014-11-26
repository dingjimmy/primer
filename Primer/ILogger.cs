// Copyright (c) James Dingle

using System;

namespace Primer
{
    public interface ILogger
    {

        void Trace(int eventId, object state, Exception ex);
        //void Trace(int eventId, object state, Exception ex, Func<object, Exception, string> formatter);

        void Debug(int eventId, object state, Exception ex);
        //void Debug(int eventId, object state, Exception ex, Func<object, Exception, string> formatter);

        void Info(int eventId, object state, Exception ex);
        //void Info(int eventId, object state, Exception ex, Func<object, Exception, string> formatter);

        void Warning(int eventId, object state, Exception ex);
        //void Warning(int eventId, object state, Exception ex, Func<object, Exception, string> formatter);

        void Error(int eventId, object state, Exception ex);
        //void Error(int eventId, object state, Exception ex, Func<object, Exception, string> formatter);

        void Fatal(int eventId, object state, Exception ex);
        //void Fatal(int eventId, object state, Exception ex, Func<object, Exception, string> formatter);

    }
}