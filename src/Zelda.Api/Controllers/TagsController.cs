using System;
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
    public class TagsController : ControllerBase
    {
        private readonly ITagsRepository _tagsRepo;
        private readonly IMapper _mapper;

        public TagsController(ITagsRepository linksRepo, IMapper mapper)
        {
            _tagsRepo = linksRepo ??
                throw new ArgumentNullException(nameof(linksRepo));
            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));
        }

        /// <summary>
        /// Gets a list of all tags
        /// </summary>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Tag>>> GetTags()
        {
            var tags = await _tagsRepo.GetTagsAsync();
            return Ok(_mapper.Map<IEnumerable<TagDto>>(tags));
        }

        /// <summary>
        /// Gets a specific tag by its id
        /// </summary>
        /// <param name="id">The id of the tag</param>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Tag>> GetTag(Guid id)
        {
            var tag = await _tagsRepo.GetTagAsync(id);
            return tag == null ? NotFound() : Ok(_mapper.Map<TagDto>(tag));
        }

        /// <summary>
        /// Updates the details of a given tag
        /// </summary>
        /// <param name="id">The id of the tag to update</param>
        /// <param name="tag">The updated properties of the tag</param>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateTag(Guid id, TagToUpdateDto tag)
        {
            var tagEntity = await _tagsRepo.GetTagAsync(id);
            if (tagEntity == null)
            {
                return NotFound();
            }

            _mapper.Map(tag, tagEntity);
            _tagsRepo.UpdateTag(tagEntity);

            // It might be more proper to handle this error in the repository
            try
            {
                await _tagsRepo.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) when (!_tagsRepo.TagExists(id))
            {
                return NotFound();
            }

            return NoContent();
        }

        /// <summary>
        /// Creates a new tag
        /// </summary>
        /// <param name="tag">The details of the tag to be created</param>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        public async Task<ActionResult<Tag>> CreateTag(TagToCreateDto tag)
        {
            if (tag == null)
            {
                throw new ArgumentNullException(nameof(tag));
            }

            var tagEntity = _mapper.Map<Tag>(tag);
            _tagsRepo.AddTag(tagEntity);
            await _tagsRepo.SaveChangesAsync();

            var tagToReturn = _mapper.Map<TagDto>(tagEntity);
            return CreatedAtAction(
                "GetTag",
                new { id = tagToReturn.Id },
                tagToReturn);
        }

        /// <summary>
        /// Deletes the specified tag
        /// </summary>
        /// <param name="id">The id of the tag to delete</param>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteTag(Guid id)
        {
            var tag = await _tagsRepo.GetTagAsync(id);
            if (tag == null)
            {
                return NotFound();
            }
            _tagsRepo.DeleteTag(tag);
            await _tagsRepo.SaveChangesAsync();
            return NoContent();
        }
    }
}
