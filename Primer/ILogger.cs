//-----------------------------------------------------------------------
// <copyright file="ILogger.cs" company="James Dingle">
//     Copyright (c) James Dingle
// </copyright>
//-----------------------------------------------------------------------

namespace Primer
{
    using System;

    /// <summary>
    /// Provides a mechanism for logging data about the operation of an application and its components.
    /// </summary>
    public interface ILogger
    {
        /// <summary>
        /// Log a Trace event. Use this to record very detailed and fine-grained information such as performance and feature-usage statistics.
        /// </summary>
        /// <param name="eventId">A code that uniquely identifies the information being recorded. </param>
        /// <param name="state">The object that the information is related to.</param>
        /// <param name="ex">Any <see cref="Exception"/> that may have been thrown.</param>
        void Trace(int eventId, object state, Exception ex);
        
        /// <summary>
        /// Log an event related to application debugging. Use this to record detailed information that could be of use to developers when investigating or fixing a problem.
        /// </summary>
        /// <param name="eventId">A code that uniquely identifies the debug event being recorded. </param>
        /// <param name="state">The object that the information is related to.</param>
        /// <param name="ex">Any <see cref="Exception"/> that may have been thrown.</param>
        void Debug(int eventId, object state, Exception ex);
        
        /// <summary>
        /// Log an informational event.
        /// </summary>
        /// <param name="eventId">A code that uniquely identifies the information being recorded.</param>
        /// <param name="state">The object that the warning is related to.</param>
        /// <param name="ex">The <see cref="Exception"/> that has been thrown, if any.</param>
        void Info(int eventId, object state, Exception ex);
        
        /// <summary>
        /// Log a warning. Use this when something bad, but not serious and that is reversible or repairable happens. For example, an user has performed an action against recommendation, or an object is in an inconsistent sate.
        /// </summary>
        /// <param name="eventId">A code that uniquely identifies the warning being recorded. </param>
        /// <param name="state">The object that the warning is related to.</param>
        /// <param name="ex">The <see cref="Exception"/> that has been thrown, if any.</param>
        void Warning(int eventId, object state, Exception ex);
        
        /// <summary>
        /// Log an error. Use this when something bad, serious and probably un-reversible or un-repairable happens.
        /// </summary>
        /// <param name="eventId">A code that uniquely identifies the specific error that has occurred. </param>
        /// <param name="state">The object where the error was caught.</param>
        /// <param name="ex">The <see cref="Exception"/> that has been thrown, if any.</param>
        void Error(int eventId, object state, Exception ex);

        /// <summary>
        /// Log a Fatal Error. Use this when something very bad has happened and the application can no longer continue.
        /// </summary>
        /// <param name="eventId">A code that uniquely identifies the specific error that has occurred. </param>
        /// <param name="state">The object where the error was caught.</param>
        /// <param name="ex">The <see cref="Exception"/> that has been thrown, if any.</param>
        void Fatal(int eventId, object state, Exception ex);

        #region Potential Future Methods 

        ////void Trace(int eventId, object state, Exception ex, Func<object, Exception, string> formatter);

        ////void Debug(int eventId, object state, Exception ex, Func<object, Exception, string> formatter);

        ////void Info(int eventId, object state, Exception ex, Func<object, Exception, string> formatter);

        ////void Warning(int eventId, object state, Exception ex, Func<object, Exception, string> formatter);

        ////void Error(int eventId, object state, Exception ex, Func<object, Exception, string> formatter);

        ////void Fatal(int eventId, object state, Exception ex, Func<object, Exception, string> formatter);

        #endregion
    }
}