using System.Collections.Generic;

namespace Model
{
    public class Track
    {
        public string Name { get; set; }
        public string Notes { get; set; }
        public int Id { get; set; }
        public List<Session> Sessions { get; set; }
    }
}
