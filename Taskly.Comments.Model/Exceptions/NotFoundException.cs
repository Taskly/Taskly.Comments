using System;

namespace Taskly.Comments.Model.Exceptions
{
    public class NotFoundException : AppException
    {
        public NotFoundException(string entity, string id)
            : base($"{entity} with id '{id} not found.")
        {
        }
    }
}
