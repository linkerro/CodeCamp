using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeCamp.Models
{
    public class Sponsors
    {
        public int Id { get; set; }
        public string LogoImageUrl { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string WebSiteUrl { get; set; }
    }
}
