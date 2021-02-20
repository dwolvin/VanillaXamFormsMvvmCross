using System.Collections.Generic;
using System.Diagnostics;
using MvvmCross;
using vanilla.Core.Services;
using vanilla.UI.Themes;
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

            var repo = Mvx.IoCProvider.GetSingleton<IStationRepository>();
            if (repo != null)
            {
                repo.Dispose();
            }
        }

        private OSAppTheme _currentAppTheme = OSAppTheme.Unspecified;

        protected override void OnResume()
        {
            //Set AppTheme from OS Theme
            Application.Current.UserAppTheme = (Application.Current.RequestedTheme == OSAppTheme.Dark) ?
                OSAppTheme.Dark : OSAppTheme.Light;

            this.CurrentAppTheme = Application.Current.RequestedTheme;
        }

        public OSAppTheme CurrentAppTheme
        {
            get => _currentAppTheme;            
            set
            {
                if (_currentAppTheme != value)
                {
                    ICollection<ResourceDictionary> mergedDictionaries = Application.Current.Resources.MergedDictionaries;
                    if (mergedDictionaries != null)
                    {
                        mergedDictionaries.Clear();

                        if (value == OSAppTheme.Dark)
                        {
                            mergedDictionaries.Add(new DarkTheme());
                        }
                        else
                        {
                            mergedDictionaries.Add(new LightTheme());
                        }
                        _currentAppTheme = value;
                    }
                }
            }
        }
    }
}