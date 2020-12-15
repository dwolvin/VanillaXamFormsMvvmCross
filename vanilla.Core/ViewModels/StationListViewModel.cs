using System;
using System.Collections.Generic;
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
        readonly IStationRepository _stationRepository;

        public StationListViewModel(IMvxNavigationService navigationService, IStationRepository stationRepository)
        {
            _navigationService = navigationService;
            _stationRepository = stationRepository;
        }

        public ICollection<Station> Stations { get; set; }
        public Station SelectedStation { get; set; }

    }
}