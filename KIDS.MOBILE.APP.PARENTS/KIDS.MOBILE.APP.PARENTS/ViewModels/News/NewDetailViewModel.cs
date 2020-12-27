using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Text;

namespace KIDS.MOBILE.APP.PARENTS.ViewModels.News
{
    public class NewDetailViewModel : BaseViewModel
    {
        private Guid? newId = Guid.Empty;
        private string _content;
        public string Content
        {
            get => _content;
            set => SetProperty(ref _content, value);
        }
        public NewDetailViewModel(INavigationService navigationService) : base(navigationService)
        {
        }

        public override void Initialize(INavigationParameters parameters)
        {
            base.Initialize(parameters);
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            try
            {
                IsLoading = true;
                base.OnNavigatedTo(parameters);
                newId = parameters["NewsId"] as Nullable<Guid>;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                IsLoading = false;
            }
        }
    }
}
