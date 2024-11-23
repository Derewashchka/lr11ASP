using Microsoft.AspNetCore.Mvc.Filters;

namespace lr11.Filters
{
    public class ActionLoggerFilter : IActionFilter
    {
        private readonly string _logPath;

        public ActionLoggerFilter()
        {
            _logPath = Path.Combine(Directory.GetCurrentDirectory(), "logs", "action-logs.txt");
            var logsDirectory = Path.GetDirectoryName(_logPath);
            if (!Directory.Exists(logsDirectory))
            {
                Directory.CreateDirectory(logsDirectory!);
            }
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var actionName = context.ActionDescriptor.DisplayName;
            var timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            var logMessage = $"{timestamp} - Executed action: {actionName}{Environment.NewLine}";

            File.AppendAllText(_logPath, logMessage);
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
        }
    }
}