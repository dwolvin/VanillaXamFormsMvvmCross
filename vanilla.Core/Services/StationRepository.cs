using System;
using System.Collections.Generic;
using LiteDB;
using vanilla.Core.Models;

namespace vanilla.Core.Services
{
    public interface IStationRepository
    {
        IEnumerable<Station> GetAllStations();
        void UpsertStation(Station station);
        Station GetStation(string stationCode);
        Station GetStation(int id);
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

        public Station GetStation(int id)
        {
            return _stationCollection.FindById(id);
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