using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodeCamp.Models
{
    public class Track
    {
        public string Name { get; set; }
        public string Notes { get; set; }
        public int Id { get; set; }
        public List<Session> Sessions { get; set; }
    }
}
