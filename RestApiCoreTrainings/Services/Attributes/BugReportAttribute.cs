using System;

namespace BusinessLayer.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Property, AllowMultiple = true)]
    public class BugReportAttribute : Attribute
    {
        public BugReportAttribute(string message, string taskId = null)
        {
            Message = message;
            TaskId = taskId;
        }

        public string Message { get; }

        public string TaskId { get; }

        public bool IsReported => !string.IsNullOrEmpty(TaskId);
    }
}
