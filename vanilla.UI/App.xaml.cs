using System.Diagnostics;
using MvvmCross;
using vanilla.Core.Services;
using Xamarin.Forms;

namespace vanilla.UI
{
    public partial class App : Application
    {
        public App()
        {           
            InitializeComponent();
        }    

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
            Trace.WriteLine("Sleepting...");

            var repo =  Mvx.IoCProvider.GetSingleton<IStationRepository>();
            if (repo != null)
            {
                repo.Dispose();
            }
        }

        protected override void OnResume()
        {
        }
    }
}
