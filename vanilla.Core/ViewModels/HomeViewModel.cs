﻿using System;
using System.Threading.Tasks;
using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.ViewModels;
using vanilla.Core.PopupHelpers;
using vanilla.Core.Services;

namespace vanilla.Core.ViewModels
{
    public class HomeViewModel : MvxViewModel
    {
        private IMvxLog _log;
        private ISimpleService _simpleService;
        private readonly IStationRepository _stationService;

        private MvxInteraction<ConfirmActionModel> _confirmInteraction = new MvxInteraction<ConfirmActionModel>();
        public IMvxInteraction<ConfirmActionModel> ConfirmInteraction => _confirmInteraction;

        public IMvxCommand ConfirmSomethingCommand => new MvxCommand(()=>ConfirmSomething());
        public IMvxCommand ResetTextCommand => new MvxCommand(ResetText);


        public HomeViewModel(IMvxLogProvider logProvider, ISimpleService simpleService, IStationRepository stationService)
        {
            _log = logProvider.GetLogFor<HomeViewModel>();
            _simpleService = simpleService;
            _stationService = stationService;
        }

        private void ResetText()
        {
            Station s = _stationService.GetStation("AAA");

            if (s == null)
            {
                s = new Station()
                {
                    StationName = "Test Station",
                    StationCode = "AAA",
                    Lat= 37.035705f,
                    Lon = -76.202770f,
                    DateCreated = DateTime.Now
                };

            }

            _stationService.UpsertStation(s);

            Text = $"{s.Id} - {s.StationName}";
        }

        private void ConfirmSomething()
        {
            var confirmActionModel = new ConfirmActionModel()
            {
                ConfirmActionCallback = async (confirmed) => { await HandleConfirmationResult(confirmed); }
            };

            _log.Debug("Ask the user to confirm.");
            _confirmInteraction.Raise(confirmActionModel);
        }

        private async Task HandleConfirmationResult(bool confirmed)
        {
            if (confirmed)
            {
                //do destructive things
                _log.Debug("User confirmed this action.");
            }
            else
            {
                _log.Debug("User declined.");
            }
        }

        private string _text = "Hello MvvmCross";
        public string Text
        {
            get { return _text; }
            set { SetProperty(ref _text, value); }
        }
    }
}
