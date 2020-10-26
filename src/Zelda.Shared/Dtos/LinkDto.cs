using System;
using System.Collections.Generic;
using System.Text;

namespace Zelda.Shared.Dtos
{
    public class LinkDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Url { get; set; }

        public List<TagDto> Tags { get; set; }
    }
}
