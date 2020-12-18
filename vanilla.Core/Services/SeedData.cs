using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using vanilla.Core.Models;

namespace vanilla.Core.Services
{
    public class SeedData
    {
        public SeedData()
        {
        }

        public IList<Station> AllStations()
        {
            var stationList = new List<Station>();

            stationList.Add(CreateStation("44087", "Thimble Shoal, VA", 37.026f, -76.151f));
            stationList.Add(CreateStation("44088", "Virginia Beach Offshore, VA", 36.614f, -74.842f));
            stationList.Add(CreateStation("44089", "Wallops Island, VA", 37.754f, -75.325f));
            stationList.Add(CreateStation("SWPV2", "Sewells Point, VA", 36.943f, -76.329f));
            stationList.Add(CreateStation("CHBV2", "Chesapeake Bay Bridge Tunnel, VA", 37.032f, -76.083f));

            return stationList;
        }

        private Station CreateStation(string code, string name, float lat, float lon)
        {
            return new Station()
            {
                StationCode = code,
                StationName = name,
                Lat = lat,
                Lon = lon,
                DateCreated = DateTime.Now
            };
        }
    }
}
