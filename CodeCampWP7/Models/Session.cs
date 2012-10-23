using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using System.Threading.Tasks;

namespace CodeCamp.Models
{
    public class Session
    {
        private string _speakers = null;
        public string Description { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public int Id { get; set; }
        public bool OverrideTracks { get; set; }
        public List<int?> SpeakerRefIds { get; set; }
        public string Title { get; set; }
        public int? TrackRefId { get; set; }
        public List<Speaker> SpeakerList { get; set; }
        public Track Track { get; set; }
        public string Speakers
        {
            get
            {
                if (string.IsNullOrEmpty(_speakers) && SpeakerList.Count > 0)
                {
                    for (int i = 0; i < SpeakerList.Count - 1; i++)
                        _speakers += SpeakerList[i].FullName + ", ";
                    _speakers += SpeakerList[SpeakerList.Count - 1].FullName;
                }
                return _speakers;
            }
        }
    }
}
