namespace Taskly.Comments.Model.Exceptions
{
    public class InvalidArgumentDomainException : DomainException
    {
        public InvalidArgumentDomainException(string message)
            : base(message)
        {
        }
    }
}
