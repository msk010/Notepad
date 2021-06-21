using Notepad.Domain.Entities;
using System;

namespace Notepad.Domain.Common
{
    public interface IAuditableEntity
    {
        DateTimeOffset CreatedOn { get; }
        DateTimeOffset? UpdatedOn { get; }

        int CreatedById { get; }
        User CreatedBy { get; }
    }
}
