using System;

namespace Taskly.Comments.Model.Exceptions
{
    public class NotFoundDomainException : DomainException
    {
        public NotFoundDomainException(string entity, object id)
            : base($"{entity} with id '{id} not found.")
        {
        }
    }
}
