using Common.Loggers.Interfaces;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;
using ILogger = Serilog.ILogger;

namespace Common.Loggers.SeriLog
{
    public class LogManager : ILogManager
    {
        private readonly ILogger _logger;

        public LogManager(string envName)
        {
            var loggerConfiguration = CreateLoggerConfiguration(envName);
            _logger = loggerConfiguration.CreateLogger();
        }
        protected virtual LoggerConfiguration CreateLoggerConfiguration(string envName)
        {
            var loggerConfiguration = new LoggerConfiguration()
               .MinimumLevel.Information()
               .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
               .Enrich.FromLogContext()
               .WriteTo.Console();

            if (envName == Environments.Development)
            {
                loggerConfiguration = loggerConfiguration.WriteTo.Debug();
            }
            return loggerConfiguration;
        }

        private static string FormatMessage(string message, string prefix)
        {
            var pref = string.IsNullOrEmpty(prefix) ? string.Empty : $"[ { prefix }]-";
            return $"{ pref}{ message }";
        }
        public void Debug(string message, string prefix = "")
        {
            _logger.Debug(FormatMessage(message, prefix));
        }

        public void Error(Exception ex, string message, string prefix = "")
        {
            _logger.Error(ex, FormatMessage(message, prefix));
        }

        public void Error(string message, string prefix = "")
        {
            _logger.Error(FormatMessage(message, prefix));
        }

        public void Info(string message, string prefix = "")
        {
            _logger.Information(FormatMessage(message, prefix));
        }

        public void Warn(string message, string prefix = "")
        {
            _logger.Warning(FormatMessage(message, prefix));
        }
    }
}
