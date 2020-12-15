using System;
using System.Threading.Tasks;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using vanilla.Core.Models;
using vanilla.Core.Services;

namespace vanilla.Core.ViewModels
{
    public class StationDetailsViewModel : MvxViewModel<int>
    {
        readonly IMvxNavigationService _navigationService;
        readonly IStationRepository _stationRepository;

        int _stationId;

        public StationDetailsViewModel(IMvxNavigationService navigationService, IStationRepository stationRepository)
        {
            _navigationService = navigationService;
            _stationRepository = stationRepository;
        }

        public Station Station { get; set; }


        public override void Prepare(int parameter)
        {
            _stationId = parameter;
        }

        public async override Task Initialize()
        {
           await base.Initialize();

            Station = _stationRepository.GetStation(_stationId);
        }
    }
}
