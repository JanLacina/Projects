using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebScrapper.Entities
{
    public class Specification
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Value { get; set; }
    }
}
