using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using vanilla.Core.Models;
using vanilla.Core.Services;

namespace vanilla.Core.ViewModels
{
    public class StationListViewModel : MvxViewModel
    {
        IMvxNavigationService _navigationService;
        private Station _selectedStation;
        readonly IStationRepository _stationRepository;

        public StationListViewModel(IMvxNavigationService navigationService, IStationRepository stationRepository)
        {
            _navigationService = navigationService;
            _stationRepository = stationRepository;
        }

        public IEnumerable<Station> Stations { get; set; }
        public Station SelectedStation {
            get => _selectedStation;
            set {
                if (_selectedStation != value) {
                    SetProperty(ref _selectedStation, value);

                    DisplaySelectedStation();
                }
            }
        }

        private void DisplaySelectedStation()
        {
            _navigationService.Navigate<StationDetailsViewModel, int>(SelectedStation.Id);
        }

        public async override Task Initialize()
        {
            await base.Initialize();

            Stations = _stationRepository.GetAllStations();
        }

    }
}