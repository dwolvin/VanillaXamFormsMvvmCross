using System;
using MvvmCross.IoC;
using MvvmCross.ViewModels;
using vanilla.Core.ViewModels;

namespace vanilla.Core
{
    public class App : MvxApplication
    {
        public override void Initialize()
        {
            CreatableTypes()
                .EndingWith("Service")
                .AsInterfaces()
                .RegisterAsLazySingleton();
            RegisterAppStart<HomeViewModel>();
        }
    }
}
