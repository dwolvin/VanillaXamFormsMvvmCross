using System;
using System.Threading;
using System.Threading.Tasks;
using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using vanilla.Core.Models;
using vanilla.Core.PopupHelpers;
using vanilla.Core.Services;

namespace vanilla.Core.ViewModels
{
    public class HomeViewModel : MvxViewModel
    {
        private readonly IMvxNavigationService _navigationService;
        private readonly IMvxLog _log;
        private readonly ISimpleService _simpleService;
        private readonly IStationRepository _stationService;

        private MvxInteraction<ConfirmActionModel> _confirmInteraction = new MvxInteraction<ConfirmActionModel>();
        public IMvxInteraction<ConfirmActionModel> ConfirmInteraction => _confirmInteraction;

        public IMvxCommand ConfirmSomethingCommand => new MvxCommand(()=>ConfirmSomething());
        public IMvxCommand ResetTextCommand => new MvxCommand(ResetText);
        public IMvxCommand CrashCommand => new MvxCommand(() => MvxNotifyTask.Create(() => DoHeavyLifting(), onException: ex => HandleBackgroundException(ex)));

        public IMvxCommand<int> StationsCommand => new MvxCommand<int>(async (viewCount)=> await ShowStationList(viewCount));

        private async Task ShowStationList(int viewCount)
        {
            if (viewCount == 2)
            {                
                await _navigationService.Navigate<StationsRootViewModel>();
            }
            else
            {
                await _navigationService.Navigate<StationListViewModel>();
            }
        }

        public HomeViewModel(IMvxNavigationService navigationService,
            IMvxLogProvider logProvider,
            ISimpleService simpleService,
            IStationRepository stationService)
        {
            _navigationService = navigationService;
            _log = logProvider.GetLogFor<HomeViewModel>();
            _simpleService = simpleService;
            _stationService = stationService;
        }

        public override void Prepare()
        {
            base.Prepare();
        }

        private async Task DoHeavyLifting()
        {
            IsBusy = true;

            await _simpleService.SleepTask();
            await _simpleService.BoomTask();

            IsBusy = false;
        }

        private void HandleBackgroundException(Exception ex)
        {
            _log.Error(ex, "Background Exception!");
            IsBusy = false;
        }

        private bool _isBusy;
        public bool IsBusy
        {
            get { return _isBusy; }
            set { SetProperty(ref _isBusy, value); }
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
