using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.IO;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace KIDS.MOBILE.APP.PARENTS.ViewModels
{
    public class BaseViewModel : BindableBase, INavigatedAware, IInitialize
    {
        public BaseViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            BackCommand = new DelegateCommand(OnBackClicked);
        }

        protected INavigationService _navigationService;
        public DelegateCommand BackCommand { get; set; }
        private bool _isLoading;
        public bool IsLoading
        {
            get => _isLoading;
            set => SetProperty(ref _isLoading, value);
        }
        public virtual void OnNavigatedFrom(INavigationParameters parameters)
        {
        }

        public virtual void OnNavigatedTo(INavigationParameters parameters)
        {
        }

        public virtual void Initialize(INavigationParameters parameters)
        {

        }

        private async void OnBackClicked()
        {
            await _navigationService.GoBackAsync();
        }

        protected string ImageSourceToBase64(ImageSource source)
        {
            StreamImageSource streamImageSource = (StreamImageSource)source;
            System.Threading.CancellationToken cancellationToken = System.Threading.CancellationToken.None;
            Task<Stream> task = streamImageSource.Stream(cancellationToken);
            Stream stream = task.Result;

            byte[] b;
            using (MemoryStream ms = new MemoryStream())
            {
                stream.CopyTo(ms);
                b = ms.ToArray();
            }

            return Convert.ToBase64String(b);
        }
    }
}