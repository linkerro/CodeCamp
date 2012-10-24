﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CodeCampWP7.Models;
//using System.Threading.Tasks;
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
        public List<TimeSlot> TimeSlots { get; set; }

        public static Event Parse(string jsonContent)
        {
            Event newEvent = JsonConvert.DeserializeObject<Event>(jsonContent);
            foreach (Session session in newEvent.Sessions)
            {
                session.SpeakerList = (from x in newEvent.Speakers where session.SpeakerRefIds.Contains(x.Id) select x).ToList();
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
                                  select new TimeSlot() { Start = g.Key, Sessions = g.ToList() }).ToList();
            var test=(from x in newEvent.Sessions
                                  group x by x.Start into g
                                  orderby g.Key
                                  select g);
            return newEvent;
        }
    }
}