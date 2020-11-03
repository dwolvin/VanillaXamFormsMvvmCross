using MvvmCross.Base;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Forms.Views;
using MvvmCross.ViewModels;
using vanilla.Core.PopupHelpers;
using vanilla.Core.ViewModels;

namespace vanilla.UI.Views
{
    public partial class HomeView : MvxContentPage
    {

        public HomeView()
        {
            InitializeComponent();
        }

        // ConfirmDelete Interaction
        private IMvxInteraction<ConfirmActionModel> _confirmActionInteraction;
        public IMvxInteraction<ConfirmActionModel> ConfirmActionInteraction
        {
            get => _confirmActionInteraction;
            set
            {
                if (_confirmActionInteraction != null)
                    _confirmActionInteraction.Requested -= OnInteractionRequested;

                _confirmActionInteraction = value;
                _confirmActionInteraction.Requested += OnInteractionRequested;
            }
        }

        private async void OnInteractionRequested(object sender, MvxValueEventArgs<ConfirmActionModel> eventArgs)
        {
            var confirmActionModel = eventArgs.Value;
            var status = await DisplayAlert(confirmActionModel.Title, confirmActionModel.Question, confirmActionModel.OKButtonText, confirmActionModel.CancelButtonText);
            confirmActionModel.ConfirmActionCallback(status);
        }

        protected override void OnViewModelSet()
        {

            var set = this.CreateBindingSet<HomeView, HomeViewModel>();
            set.Bind(this).For(view => view.ConfirmActionInteraction).To(ViewModel => ViewModel.ConfirmInteraction).TwoWay();
            set.Apply();

            base.OnViewModelSet();
        }
    }
}
