using System;
using Prism.Navigation;

namespace KIDS.MOBILE.APP.PARENTS.ViewModels.Home
{
    public class HomeViewModel : BaseViewModel
    {
        public HomeViewModel()
        {

        }
        public override void Initialize(INavigationParameters parameters)
        {
            base.Initialize(parameters);
            Console.WriteLine();
        }
    }
}