using System;
using Zelda.Shared.Dtos;
using AutoMapper;
using Zelda.Models;

namespace Zelda.Api.Profiles
{
    public class TagsProfile : Profile
    {
        public TagsProfile()
        {
            CreateMap<TagToCreateDto, Tag>();
            CreateMap<TagToUpdateDto, Tag>();
            CreateMap<Tag, TagDto>();
        }
    }
}
