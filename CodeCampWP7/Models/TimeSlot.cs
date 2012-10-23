using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CodeCamp.Models;

namespace CodeCamp.Models
{
    public class TimeSlot
    {
        public List<Session> Sessions { get; set; }
        public DateTime Start { get; set; }
        public string StartTime
        {
            get
            {
                return Start.ToString("HH:mm");
            }
        }
    }
}
