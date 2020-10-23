using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Zelda.Models;
using Zelda.Shared.Dtos;

namespace Zelda.Api.Profiles
{
    public class LinksProfile : Profile
    {
        public LinksProfile()
        {
            CreateMap<LinkToCreateDto, Link>();
            CreateMap<LinkToUpdateDto, Link>();
            CreateMap<Link, LinkDto>();
        }
    }
}
