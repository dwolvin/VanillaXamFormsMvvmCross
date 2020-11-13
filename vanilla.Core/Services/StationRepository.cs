using System;
using System.Collections.Generic;
using LiteDB;

namespace vanilla.Core.Services
{

    public class Station {
        public int Id { get; set; }
        public string StationCode { get; set; }
        public string StationName { get; set; }
        public float Lat { get; set; }
        public float Lon { get; set; }
        public DateTime DateCreated { get; set; }
    }

    public interface IStationRepository
    {
        IEnumerable<Station> GetAllStations();
        void UpsertStation(Station station);
        Station GetStation(string stationCode);
    }

    public class StationRepository : IStationRepository, IDisposable
    {
        private static string dbFileName = @"stations.db";
        private LiteDatabase _db;
        private ILiteCollection<Station> _stationCollection;

        public StationRepository()
        {
            //This singleton service keeps the database file open
            _db = new LiteDatabase(dbFileName);

            _stationCollection = _db.GetCollection<Station>("stations");

            //Create an index on StationCode
            _stationCollection.EnsureIndex(x => x.StationCode, true);
        }

        public IEnumerable<Station> GetAllStations(){
             return _stationCollection.FindAll();
        }

        public void UpsertStation(Station station)
        {
            _stationCollection.Upsert(station);
        }

        public Station GetStation(string stationCode)
        {
            return _stationCollection.FindOne(x => x.StationCode == stationCode);
        }

        public void DeleteStation(Station station)
        {
            _stationCollection.Delete(station.Id);
        }

        public void Dispose()
        {
            if (_db != null)
            {
                _db.Dispose();
            }
        }
    }
}