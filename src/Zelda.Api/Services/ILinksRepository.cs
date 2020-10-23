using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Zelda.Models;

namespace Zelda.Api.Services
{
    public interface ILinksRepository
    {
        void AddLink(Link link);
        void DeleteLink(Link link);
        Task<Link> GetLinkAsync(Guid id);
        Task<IEnumerable<Link>> GetLinksAsync();
        bool LinkExists(Guid id);
        Task<bool> SaveChangesAsync();
        void UpdateLink(Link link);
    }
}