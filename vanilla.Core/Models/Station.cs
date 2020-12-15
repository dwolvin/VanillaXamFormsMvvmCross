using System;
namespace vanilla.Core.Models
{
    public class Station
    {
        public int Id { get; set; }
        public string StationCode { get; set; }
        public string StationName { get; set; }
        public float Lat { get; set; }
        public float Lon { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
