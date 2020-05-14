namespace Taskly.Comments.Model.Exceptions
{
    public class InvalidArgumentException : AppException
    {
        public InvalidArgumentException(string message)
            : base(message)
        {
        }
    }
}
