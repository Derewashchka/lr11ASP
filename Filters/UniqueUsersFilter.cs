using Microsoft.AspNetCore.Mvc.Filters;

namespace lr11.Filters
{
    public class UniqueUsersFilter : IActionFilter
    {
        private static readonly HashSet<string> _uniqueUsers = new();
        private readonly string _logPath;
        private static readonly object _lock = new();

        public UniqueUsersFilter()
        {
            _logPath = Path.Combine(Directory.GetCurrentDirectory(), "logs", "unique-users.txt");
            var logsDirectory = Path.GetDirectoryName(_logPath);
            if (!Directory.Exists(logsDirectory))
            {
                Directory.CreateDirectory(logsDirectory!);
            }
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var ipAddress = context.HttpContext.Connection.RemoteIpAddress?.ToString() ?? "unknown";
            var userAgent = context.HttpContext.Request.Headers["User-Agent"].ToString();
            var userIdentifier = $"{ipAddress}_{userAgent}";

            lock (_lock)
            {
                if (_uniqueUsers.Add(userIdentifier))
                {
                    var timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    var logMessage = $"{timestamp} - New unique user detected. Total unique users: {_uniqueUsers.Count}{Environment.NewLine}";
                    File.AppendAllText(_logPath, logMessage);
                }
            }
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
        }
    }
}