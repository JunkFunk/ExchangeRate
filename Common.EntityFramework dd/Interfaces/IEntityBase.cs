using System;

namespace Common.EntityFramework.Interfaces
{
    public interface IEntityBase
    {
        Guid Id { get; set; }
    }
}