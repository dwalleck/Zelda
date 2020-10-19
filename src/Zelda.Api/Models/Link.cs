using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Zelda.Models
{
    public class Link
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public Uri Url { get; set; }

        public IList<Tag> Tags { get; set; } = new List<Tag>();
    }
}
