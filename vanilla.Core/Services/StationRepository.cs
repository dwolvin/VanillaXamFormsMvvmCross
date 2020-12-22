using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using LiteDB;
using vanilla.Core.Models;

namespace vanilla.Core.Services
{
    public interface IStationRepository : IDisposable
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

        private void InitDatabase()
        {
            Trace.WriteLine("Init Database.");
            _db = new LiteDatabase(dbFileFullPath);
            _collection = _db.GetCollection<Station>(collectionName);
            _collection.EnsureIndex(x => x.StationCode, true);
            _db.Checkpoint();
        }

        private ILiteCollection<Station> Collection
        {
            get
            {
                if (_db == null)
                {
                    InitDatabase();
                }
                return _collection;
            }
        }

        public IList<Station> GetAllStations(){
            return Collection.Query().Where(x => true).ToList();
        }

        public void InsertStation(Station station)
        {
            if (!Collection.Exists(x => x.StationCode.Equals(station.StationCode)))
            {
                 Collection.Insert(station);
                _db.Checkpoint();
            }
        }

        public void UpdateStation(Station station)
        {
            Collection.Update(station);
            _db.Checkpoint();
        }

        public Station GetStation(string stationCode)
        {
            return Collection.FindOne(x => x.StationCode == stationCode);
        }

        public Station GetStation(int id)
        {
            return Collection.FindById(id);
        }

        public void DeleteStation(Station station)
        {
            Collection.Delete(station.Id);
            _db.Checkpoint();
        }

        public void Dispose()
        {
            if (_db != null)
            {
                Trace.WriteLine("Disposing database.");
                _db.Dispose();
                _db = null;
            }           
        }
    }
}