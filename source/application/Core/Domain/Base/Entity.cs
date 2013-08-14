using System;
using Intrigma.DonorSpace.Core.Interfaces.Data.Read;
using Intrigma.DonorSpace.Core.Interfaces.Data.Write;

namespace Intrigma.DonorSpace.Core.Domain.Base
{
    /// <summary>
    /// Define an entity with Guid typed Id
    /// It is one of the 3 places to change the type id for all domain models to another type like int
    /// change the 2 others  <see cref = "IRepository{T}" /> and <see cref="IQuery{T}"/>.
    /// </summary>
    [Serializable]
    public abstract class Entity : EntityWithTypedId<int> { }

    /// <summary>
    /// Base class to inherit from for ValueObjects
    /// </summary>
    [Serializable]
    public abstract class ValueObject : ComparableObject { }
}
