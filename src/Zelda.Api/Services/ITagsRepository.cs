using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Zelda.Models;

namespace Zelda.Api.Services
{
    public interface ITagsRepository
    {
        void AddTag(Tag tag);
        void DeleteTag(Tag tag);
        Task<Tag> GetTagAsync(Guid id);
        Task<IEnumerable<Tag>> GetTagsAsync();
        Task<bool> SaveChangesAsync();
        bool TagExists(Guid id);
        void UpdateTag(Tag tag);
    }
}