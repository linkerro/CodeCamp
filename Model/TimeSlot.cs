﻿using System;
using System.Collections.Generic;

namespace Model
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
