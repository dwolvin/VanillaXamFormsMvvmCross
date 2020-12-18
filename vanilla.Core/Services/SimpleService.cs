using System;
using System.Threading.Tasks;
using MvvmCross.Logging;
using vanilla.Core.Models;

namespace vanilla.Core.Services
{

    public interface ISimpleService
    {
        Task SleepTask();
        Task BoomTask();
        Task<Station> CreateStation(float lat, float lon, string name, string code);
        void SeedDatabase();
    }
    public class SimpleService : ISimpleService
    {
        IMvxLog _log;
        readonly IStationRepository _stationRepository;
        public SimpleService(IMvxLogProvider logProvider, IStationRepository stationRepository)
        {
            _log = logProvider.GetLogFor<SimpleService>();
            _stationRepository = stationRepository;
        }
        public async Task SleepTask()
        {
            _log.Debug("Starting sleep task");
            await Task.Delay(2000);
            _log.Debug("Finished sleep task");
        }

        public async Task BoomTask()
        {
            _log.Debug("Starting boom task");
            await Task.Delay(0);
            _log.Debug("Booming");
            throw new Exception("Unexpected exception!");
        }

        public async Task<Station> CreateStation(float lat, float lon, string name, string code)
        {
            //We are simulating an api call, replete with network latency
            await Task.Delay(20);

            var station = new Station()
            {
                StationName = name,
                StationCode = code,
                Lat = lat,
                Lon = lon,
                DateCreated = DateTime.Now
            };
            return station;
        }

        public void SeedDatabase()
        {
            var stations = new SeedData().AllStations();

            foreach (var s in stations)
            {
               _stationRepository.InsertStation(s);
            }
            
        }
    }
}