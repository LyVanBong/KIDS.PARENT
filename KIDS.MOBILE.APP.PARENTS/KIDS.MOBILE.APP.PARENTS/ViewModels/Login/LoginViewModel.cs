using System;
using System.Windows.Input;
using KIDS.MOBILE.APP.PARENTS.Views.Home;
using Prism.Navigation;
using Xamarin.Forms;

namespace KIDS.MOBILE.APP.PARENTS.ViewModels.Login
{
    public class LoginViewModel : BaseViewModel
    {
        private INavigationService _navigationService;
        public ICommand DoLoginCommand { get; private set; }
        public LoginViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            DoLoginCommand = new Command(DoLogin);
        }

        private void DoLogin()
        {
            _navigationService.NavigateAsync("/MainPage?selected=HomePage");
        }

        public override void Initialize(INavigationParameters parameters)
        {
            base.Initialize(parameters);
            Console.WriteLine();
        }
    }
}