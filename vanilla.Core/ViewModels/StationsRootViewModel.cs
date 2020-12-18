using System;
using System.Threading.Tasks;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace vanilla.Core.ViewModels
{
    public class StationsRootViewModel : MvxViewModel
    {
        private readonly IMvxNavigationService _navigationService;
        public StationsRootViewModel(IMvxNavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public override void ViewAppearing()
        {
            base.ViewAppearing();
            MvxNotifyTask.Create(async () => await this.InitializeViewModels());
        }

        private async Task InitializeViewModels()
        {
            await _navigationService.Navigate<StationListViewModel>();
            await _navigationService.Navigate<StationDetailsViewModel>();
        }
    }
}
