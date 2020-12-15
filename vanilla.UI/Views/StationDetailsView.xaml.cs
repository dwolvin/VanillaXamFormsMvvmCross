using System;
using System.Collections.Generic;
using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Forms.Views;
using MvvmCross.Presenters;
using MvvmCross.Presenters.Attributes;
using MvvmCross.ViewModels;
using Xamarin.Forms;

namespace vanilla.UI.Views
{
    public partial class StationDetailsView : MvxContentPage, IMvxOverridePresentationAttribute
    {
        public StationDetailsView()
        {
            InitializeComponent();
        }

        public MvxBasePresentationAttribute PresentationAttribute(MvxViewModelRequest request)
        {
            if (Device.Idiom == TargetIdiom.Tablet)
            {
                return new MvxMasterDetailPagePresentationAttribute(MasterDetailPosition.Detail) { NoHistory = true};
            }
            else
            {
                return new MvxContentPagePresentationAttribute() { WrapInNavigationPage = true };
            }
        }
    }
}