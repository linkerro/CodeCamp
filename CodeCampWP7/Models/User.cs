using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodeCampWP7.Models
{
    public class User
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Company { get; set; }
        public string Technology { get; set; }
        public bool ShareMyContact { get; set; }
        public int Experience { get; set; }
    }
}
