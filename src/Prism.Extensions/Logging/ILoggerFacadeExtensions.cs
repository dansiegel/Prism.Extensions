using System;

namespace Prism.Logging
{
    /// <summary>
    /// A collection of extensions for the <see cref="ILoggerFacade" />
    /// </summary>
    public static class ILoggerFacadeExtensions
    {
        /// <summary>
        /// Write a new log entry with the specified category and priority.
        /// </summary>
        /// <param name="logger">The Logger</param>
        /// <param name="message">Message body to log.</param>
        /// <param name="category">Category of the entry.</param>
        public static void Log(this ILoggerFacade logger, object message, Category category = Category.Debug)
        {
            logger.Log($"{message}", category, Priority.None);
        }

        /// <summary>
        /// Write a new log entry with the specified category and priority.
        /// </summary>
        /// <param name="logger">The Logger</param>
        /// <param name="exception">Exception to log.</param>
        /// <param name="category">Category of the entry.</param>
        public static void Log(this ILoggerFacade logger, Exception exception, Category category = Category.Exception)
        {
            logger.Log($"{exception}", category, Priority.High);
        }

        /// <summary>
        /// Write a new log entry with the specified category and priority.
        /// </summary>
        /// <param name="logger">The Logger</param>
        /// <param name="condition">The condition in which we should write to the log</param>
        /// <param name="message">Message body to log.</param>
        /// <param name="category">Category of the entry.</param>
        public static void LogIf(this ILoggerFacade logger, bool condition, object message, Category category = Category.Debug)
        {
            if(condition)
                logger.Log(message, category);
        }
    }
}