using System;
using System.Collections.Generic;
using System.Linq;
using vanilla.UI;

using Foundation;
using MvvmCross.Forms.Platforms.Ios.Core;
using UIKit;

namespace vanilla.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : MvxFormsApplicationDelegate<MvxFormsIosSetup<Core.App, UI.App>, Core.App, UI.App>
    {
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {

            return base.FinishedLaunching(app, options);
        }
    }
}
