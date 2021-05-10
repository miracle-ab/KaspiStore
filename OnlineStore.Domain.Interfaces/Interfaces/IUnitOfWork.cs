using OnlineStore.Domain.Core.Entities;
using System;

namespace OnlineStore.Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Product> Products { get; }
        void Save();
    }
}
