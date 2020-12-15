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
    }
    public class SimpleService : ISimpleService
    {
        IMvxLog _log;
        public SimpleService(IMvxLogProvider logProvider)
        {
            _log = logProvider.GetLogFor<SimpleService>();
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
    }
}
