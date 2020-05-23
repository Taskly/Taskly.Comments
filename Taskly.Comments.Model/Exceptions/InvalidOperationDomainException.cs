namespace Taskly.Comments.Model.Exceptions
{
    public class InvalidOperationDomainException : DomainException
    {
        public InvalidOperationDomainException(string message)
            : base(message)
        {
        }
    }
}
