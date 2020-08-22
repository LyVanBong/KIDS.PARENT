using Prism.Mvvm;
using Prism.Navigation;

namespace KIDS.MOBILE.APP.PARENTS.ViewModels
{
    public class BaseViewModel : BindableBase, INavigatedAware, IInitialize
    {
        public virtual void OnNavigatedFrom(INavigationParameters parameters)
        {
        }

        public virtual void OnNavigatedTo(INavigationParameters parameters)
        {
        }

        public virtual void Initialize(INavigationParameters parameters)
        {

        }
    }
}