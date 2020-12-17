using System;
using System.Collections.Generic;
using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Forms.Views;
using Xamarin.Forms;

namespace vanilla.UI.Views
{
    [MvxMasterDetailPagePresentation(MasterDetailPosition.Root , NoHistory = false)]
    public partial class StationsRootView : MvxMasterDetailPage
    {
        public StationsRootView()
        {           
            InitializeComponent();
        }
    }
}
