using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Zelda.Models
{
    public class Tag
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public IList<Link> Links { get; set; } = new List<Link>();
    }
}
