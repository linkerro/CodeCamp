using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CodeCamp.Models
{
    public class Event
    {
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public String Title { get; set; }

        public Location Location { get; set; }
        public List<Speaker> Speakers { get; set; }
        public List<Session> Sessions { get; set; }
        public List<Track> Tracks { get; set; }

        public static Event Parse(string jsonContent)
        {
            Event newEvent = JsonConvert.DeserializeObject<Event>(jsonContent);
            return newEvent;
        }
    }
}
