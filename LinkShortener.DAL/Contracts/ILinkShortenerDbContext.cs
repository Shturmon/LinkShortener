using System;
using System.Data.Entity;

namespace LinkShortener.DAL.Contracts
{
    public interface ILinkShortenerDbContext : IDisposable
    {
        DbContext DbContext { get; }
    }
}
