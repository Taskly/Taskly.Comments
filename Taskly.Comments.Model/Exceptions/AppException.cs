using System;

namespace Taskly.Comments.Model.Exceptions
{
    public abstract class AppException : Exception
    {
        public AppException(string message)
            : base(message)
        {
        }
    }
}
