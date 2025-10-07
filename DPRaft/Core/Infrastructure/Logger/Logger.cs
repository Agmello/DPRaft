using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Core.BuildingBlocks.Messaging.Observer;
using Serilog;
using Core.SharedKernel;
using Core.Extensions;
using Serilog.Events;

namespace Core.Infrastructure.Logger
{
    internal class Logger : Contract.ILogger
    {
        ILogger m_logger;
        IEventObserver m_eventObserver;

        public Logger(IEventObserver observer)
        {
            m_eventObserver = observer;

            var appName = "DPRaft";
            var basePath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            var logDir = Path.Combine(basePath, appName, "logs");
            Directory.CreateDirectory(logDir);
            var todayStamp = DateTime.Now.ToString("yyMMdd");
            var baseName = $"log_{todayStamp}";
            var logPath = Path.Combine(logDir, $"{baseName}.log");
            var suffix = 1;
            while (File.Exists(logPath))
            {
                logPath = Path.Combine(logDir, $"{baseName}_{suffix}.log");
                suffix++;
            };

            m_logger = new LoggerConfiguration()
                    .MinimumLevel.Verbose()
                    .Enrich.FromLogContext()
                    .WriteTo.File(logPath)
                    .CreateLogger();

            m_eventObserver.SubscribeSafe<Event>(LogEvent);
        }

        public void Debug(string message)
        {
            m_logger.Debug(message);
        }

        public void Error(string message)
        {
            m_logger.Error(message);
        }

        public void Fatal(string message)
        {
            m_logger.Fatal(message);
        }

        public void Info(string message)
        {
            m_logger.Information(message);
        }

        public void Warn(string message)
        {
            m_logger.Warning(message);
        }

        void LogEvent(Event @event)
        {
            var message = @event.Log();
            var severity = message.Severity.TryCast<LogEventLevel>(out bool success);

            m_logger.Write(severity, message.Message);
        }

    }
}
