namespace SummerInternshipProgram.API.Helpers.Interface
{
    public interface ILogHelper
    {
        void LogError(string requestId, string message, string ip, string methodName, Exception ex);
        void LogInformation(string requestId, string message, string ip, string methodName);
        void LogInformation(string requestId, string message, string ip, string methodName, object arguemnet);
        void LogTrace(string requestId, string message, string ip, string methodName, Exception ex);
        void logWarning(string requestId, string message, string ip, string methodName);
    }
}
