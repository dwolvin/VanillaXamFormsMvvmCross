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
    public partial class StationListView : MvxContentPage, IMvxOverridePresentationAttribute
    {
        public StationListView()
        {
            InitializeComponent();
        }

        public MvxBasePresentationAttribute PresentationAttribute(MvxViewModelRequest request)
        {
            if (Device.Idiom == TargetIdiom.TV)
            {
                return new MvxMasterDetailPagePresentationAttribute(MasterDetailPosition.Master);
            }
            else
            {
                return new MvxContentPagePresentationAttribute();
            }
        }
    }
}
