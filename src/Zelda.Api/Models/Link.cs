using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace Zelda.Models
{
    public class Link
    {
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public Uri Url { get; set; }

        public DateTimeOffset CreatedAt { get; set; }

        public IList<Tag> Tags { get; set; } = new List<Tag>();
    }
}
