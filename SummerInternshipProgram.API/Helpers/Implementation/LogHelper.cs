using SummerInternshipProgram.API.Helpers.Interface;

namespace SummerInternshipProgram.API.Helpers.Implementation
{
    public class LogHelper : ILogHelper
    {

        private readonly ILogger<LogHelper> _logger;
        public LogHelper(ILogger<LogHelper> logger)
        {
            _logger = logger;
        }


        public void LogError(string requestId, string message, string ip, string methodName, Exception ex)
        {
            _logger.LogError(ex, $"requestId:{requestId}, Method Name:{methodName}, IP:{ip}, Message:{message}, exception:{ex}");
        }

        public void LogInformation(string requestId, string message, string ip, string methodName)
        {
            _logger.LogInformation($"requestId:{requestId}, Method Name:{methodName}, IP:{ip}, Message:{message}");
        }

        public void LogInformation(string requestId, string message, string ip, string methodName, object arguemnet)
        {
            _logger.LogInformation($"requestId:{requestId}, Method Name:{methodName}, IP:{ip}, Message:{message}", arguemnet);
        }

        public void LogTrace(string requestId, string message, string ip, string methodName, Exception ex)
        {
            _logger.LogError(ex, $"requestId:{requestId}, Method Name:{methodName}, IP:{ip}, Message:{message}, exception:{ex} ");
        }

        public void logWarning(string requestId, string message, string ip, string methodName)
        {
            _logger.LogWarning($"requestId:{requestId}, Method Name:{methodName}, IP:{ip}, Message:{message}");
        }
    }
}
