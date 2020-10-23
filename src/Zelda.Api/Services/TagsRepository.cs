using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Zelda.Data;
using Zelda.Models;

namespace Zelda.Api.Services
{
    public class TagsRepository : ITagsRepository
    {
        private readonly ZeldaContext _context;

        public TagsRepository(ZeldaContext context,
            ILogger<TagsRepository> logger)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public void AddTag(Tag tag)
        {
            if (tag == null)
            {
                throw new ArgumentNullException(nameof(tag));
            }
            _context.Tags.Add(tag);
        }

        public async Task<IEnumerable<Tag>> GetTagsAsync()
        {
            return await _context.Tags.ToListAsync();
        }

        public async Task<Tag> GetTagAsync(Guid id)
        {
            return await _context.Tags.FindAsync(id);
        }

        public void DeleteTag(Tag tag)
        {
            _context.Tags.Remove(tag);
        }

        public void UpdateTag(Tag tag)
        {
            _context.Entry(tag).State = EntityState.Modified;
        }

        public async Task<bool> SaveChangesAsync() => await _context.SaveChangesAsync() > 0;

        public bool TagExists(Guid id) => _context.Tags.Any(t => t.Id == id);
    }
}
