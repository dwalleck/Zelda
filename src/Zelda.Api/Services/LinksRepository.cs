﻿using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Zelda.Models;
using Microsoft.EntityFrameworkCore;
using Zelda.Data;

namespace Zelda.Api.Services
{
    public class LinksRepository
    {
        public class LinksRepository : ILinksRepository
        {
            private readonly ZeldaContext _context;
            private readonly ILogger<LinksRepository> _logger;

            public LinksRepository(ZeldaContext context,
                ILogger<LinksRepository> logger)
            {
                _context = context ?? throw new ArgumentNullException(nameof(context));
                _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            }

            public void AddLink(Link link)
            {
                if (link == null)
                {
                    throw new ArgumentNullException(nameof(link));
                }
                _context.Links.Add(link);
            }

            public async Task<IEnumerable<Link>> GetLinksAsync()
            {
                return await _context.Links.ToListAsync();
            }

            public async Task<Link> GetLinkAsync(Guid id)
            {
                return await _context.Links.FindAsync(id);
            }

            public void DeleteLink(Link link)
            {
                _context.Links.Remove(link);
            }

            public void UpdateLink(Link link)
            {
                _context.Entry(link).State = EntityState.Modified;
            }

            public async Task<bool> SaveChangesAsync() => await _context.SaveChangesAsync() > 0;

            public bool LinkExists(Guid id) => _context.Links.Any(l => l.Id == id);
        }
    }
}