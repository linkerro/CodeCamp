using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeCamp.Models
{
    public class Session
    {
        public string Description { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public int Id { get; set; }
        public bool OverrideTracks { get; set; }
        public List<int> SpeakerIds { get; set; }
        public string Title { get; set; }
        public string TrackId { get; set; }
        public List<Speaker> Speakers { get; set; }
        public Track Track { get; set; }
    }
}
