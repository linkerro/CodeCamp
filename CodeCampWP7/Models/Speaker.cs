﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodeCamp.Models
{
    public class Speaker
    {
        public string Bio { get; set; }
        public string BlogUrl { get; set; }
        public string Company { get; set; }
        public string CompanyUrl { get; set; }
        public string FacebookProfileUrl { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get { return FirstName + " " + LastName; } }
        public int Id { get; set; }
        public string LinkedInProfileUrl { get; set; }
        public string PortraitImageUrl { get; set; }
        public string Title { get; set; }
        public string TwitterProfileUrl { get; set; }
    }
}
