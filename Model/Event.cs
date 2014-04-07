//using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using CodeCamp.Models;
using Newtonsoft.Json;

namespace Model
{
    public class Event
    {
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public String Title { get; set; }

        public Location Location { get; set; }
        public Speaker[] Speakers { get; set; }
        public Session[] Sessions { get; set; }
        public List<Track> Tracks { get; set; }
        public TimeSlot[] TimeSlots { get; set; }

        public static Event Parse(string jsonContent)
        {
            var newEvent = JsonConvert.DeserializeObject<Event>(jsonContent);


            foreach (Session session in newEvent.Sessions)
            {
                session.SpeakerList = (from x in newEvent.Speakers where session.SpeakerRefIds.Contains(x.Id) select x).ToList();
                session.Description = session.Description.Replace(@"\'", "'").Replace("<br/>", "\n").Replace(@"\n", "\n");
            }

            foreach (var speaker in newEvent.Speakers)
            {
                Speaker speakerTemp = speaker;
                speaker.Sessions = newEvent.Sessions.Where(s => s.SpeakerRefIds.Contains(speakerTemp.Id));
                speaker.Bio = speaker.Bio.Replace(@"\'", "'").Replace("<br/>", "\n").Replace(@"\n", "\n");
            }

            foreach (Track track in newEvent.Tracks)
            {
                track.Sessions = (from x in newEvent.Sessions where x.TrackRefId == track.Id orderby x.Start select x).ToList();
                foreach (Session session in track.Sessions)
                {
                    session.Track = track;
                }
            }

            newEvent.TimeSlots = (from x in newEvent.Sessions
                                  group x by x.Start into g
                                  orderby g.Key
                                  select new TimeSlot { Start = g.Key, Sessions = g.ToList() }).ToArray();
            return newEvent;
        }
    }
}
