using System;
using System.Collections.Generic;
using System.IO;
using LiteDB;
using vanilla.Core.Models;

namespace vanilla.Core.Services
{
    public interface IStationRepository
    {
        IList<Station> GetAllStations();
        void InsertStation(Station station);
        void UpdateStation(Station station);
        Station GetStation(string stationCode);
        Station GetStation(int id);
    }

    public class StationRepository : IStationRepository
    {
        private static string dbFileName = @"stations.db";
        private static string collectionName = "stations";
        private LiteDatabase _db;
        private ILiteCollection<Station> _collection;
        private string AppDataFolder => Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        private string dbFileFullPath => Path.Combine(AppDataFolder, dbFileName);

        public StationRepository()
        {
            _db = new LiteDatabase(dbFileFullPath);
            _collection = _db.GetCollection<Station>(collectionName);
            _collection.EnsureIndex(x => x.StationCode, true);
            _db.Checkpoint();
        }

        public IList<Station> GetAllStations(){
            return _collection.Query().Where(x => true).ToList();
        }

        public void InsertStation(Station station)
        {
            if (!_collection.Exists(x => x.StationCode.Equals(station.StationCode)))
            {
                 _collection.Insert(station);
                _db.Checkpoint();
            }
        }

        public void UpdateStation(Station station)
        {
            _collection.Update(station);
            _db.Checkpoint();
        }

        public Station GetStation(string stationCode)
        {
            return _collection.FindOne(x => x.StationCode == stationCode);
        }

        public Station GetStation(int id)
        {
            return _collection.FindById(id);
        }

        public void DeleteStation(Station station)
        {
            _collection.Delete(station.Id);
            _db.Checkpoint();
        }
    }
}