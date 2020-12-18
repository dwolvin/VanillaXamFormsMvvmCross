using System;
using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Forms.Views;

namespace vanilla.UI.Views
{
    [MvxMasterDetailPagePresentation(MasterDetailPosition.Root , NoHistory = false, Title ="Stations Root Page")]
    public partial class StationsRootView : MvxMasterDetailPage
    {
        public StationsRootView()
        {           
            InitializeComponent();
        }
    }
}
