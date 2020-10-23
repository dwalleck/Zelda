﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Zelda.Api.Services;
using Zelda.Data;
using Zelda.Models;
using Zelda.Shared.Dtos;

namespace Zelda.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LinksController : ControllerBase
    {
        private readonly ILinksRepository _linksRepo;
        private readonly IMapper _mapper;

        public LinksController(ILinksRepository linksRepo, IMapper mapper)
        {
            _linksRepo = linksRepo ??
                throw new ArgumentNullException(nameof(linksRepo));
            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));
        }

        /// <summary>
        /// Gets a list of saved links
        /// </summary>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Link>>> GetLinks()
        {
            var links = await _linksRepo.GetLinksAsync();
            return Ok(links);
        }

        /// <summary>
        /// Gets the details for a specific link
        /// </summary>
        /// <param name="id">The id of the link</param>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Link>> GetLink(Guid id)
        {
            var link = await _linksRepo.GetLinkAsync(id);
            return link == null ? NotFound() : Ok(link);
        }

        /// <summary>
        /// Updates the specified link with the provided values
        /// </summary>
        /// <param name="id">The id of the link to update</param>
        /// <param name="link">The updated values for the link</param>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateLink(Guid id, LinkToUpdateDto link)
        {
            var linkEntity = await _linksRepo.GetLinkAsync(id);
            if (linkEntity == null)
            {
                return NotFound();
            }

            _mapper.Map(link, linkEntity);
            _linksRepo.UpdateLink(linkEntity);

            // It might be more proper to handle this error in the repository
            try
            {
                await _linksRepo.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) when (!_linksRepo.LinkExists(id))
            {
                return NotFound();
            }

            return NoContent();
        }

        /// <summary>
        /// Creates a link with the provided values
        /// </summary>
        /// <param name="link">The details of the new link to create</param>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        public async Task<ActionResult<Link>> CreateLink(Link link)
        {
            if (link == null)
            {
                throw new ArgumentNullException(nameof(link));
            }

            var linkEntity = _mapper.Map<Link>(link);
            _linksRepo.AddLink(linkEntity);
            await _linksRepo.SaveChangesAsync();

            var linkToReturn = _mapper.Map<LinkDto>(linkEntity);
            return CreatedAtAction(
                "GetLink",
                new { id = linkToReturn.Id },
                linkToReturn);
        }

        /// <summary>
        /// Deletes a link
        /// </summary>
        /// <param name="id">The id of the link to be deleted</param>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteLink(Guid id)
        {
            var link = await _linksRepo.GetLinkAsync(id);
            if (link == null)
            {
                return NotFound();
            }
            _linksRepo.DeleteLink(link);
            await _linksRepo.SaveChangesAsync();
            return NoContent();
        }
    }
}
