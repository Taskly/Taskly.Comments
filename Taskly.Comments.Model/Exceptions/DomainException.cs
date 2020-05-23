using System;

namespace Taskly.Comments.Model.Exceptions
{
    public abstract class DomainException : Exception
    {
        public DomainException(string message)
            : base(message)
        {
        }
    }
}
