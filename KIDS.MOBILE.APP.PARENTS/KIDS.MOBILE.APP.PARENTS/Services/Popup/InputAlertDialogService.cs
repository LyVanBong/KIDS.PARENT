using System;
using System.Threading.Tasks;
using KIDS.MOBILE.APP.PARENTS.Views.HealthCare;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;

namespace KIDS.MOBILE.APP.PARENTS.Services.Popup
{
    public class InputAlertDialogService : IInputAlertDialogService
    {
        public static InputAlertDialogBase<PopupDataModel> Popup;
        public async Task<PopupDataModel> OpenMultipleDataInputAlertDialog(
            string entry1Value,
            string entry2Value,
            string entry3Value)
        {
            // create the TextInputView
            var inputView = new CreateHealthInformationPopup(entry1Value,entry2Value,entry3Value);

            // create the Transparent Popup Page
            // of type string since we need a string return
            Popup = new InputAlertDialogBase<PopupDataModel>(inputView);

            // subscribe to the TextInputView's Button click event
            inputView.SaveButtonEventHandler +=
                (sender, obj) =>
                {
                    Popup.PageClosedTaskCompletionSource.SetResult(((CreateHealthInformationPopup)sender).MultipleDataResult);
                };

            // subscribe to the TextInputView's Button click event
            inputView.CancelButtonEventHandler +=
                (sender, obj) =>
                {
                    Popup.PageClosedTaskCompletionSource.SetResult(null);
                };

            // return user inserted text value
            return await Navigate(Popup);
        }

        public void ClosePopup()
        {
            try
            {
                Popup?.PageClosedTaskCompletionSource?.SetResult(null);
            }
            catch (Exception ex)
            {

            }
        }

        private async Task<T> Navigate<T>(InputAlertDialogBase<T> popup)
        {
            // Push the page to Navigation Stack
            await PopupNavigation.Instance.PushAsync(popup);

            // await for the user to enter the text input
            var result = await popup.PageClosedTask;

            // Pop the page from Navigation Stack
            await PopupNavigation.Instance.PopAsync();

            return result;
        }
    }

    public class InputAlertDialogBase<T> : PopupPage
    {
        // the awaitable task
        public Task<T> PageClosedTask { get { return PageClosedTaskCompletionSource.Task; } }

        // the task completion source
        public TaskCompletionSource<T> PageClosedTaskCompletionSource { get; set; }

        public InputAlertDialogBase(View contentBody)
        {
            Content = contentBody;

            // init the task completion source
            PageClosedTaskCompletionSource = new System.Threading.Tasks.TaskCompletionSource<T>();

            this.BackgroundColor = new Color(0, 0, 0, 0.4);
        }

        protected override bool OnBackButtonPressed()
        {
            // Prevent back button pressed action on android
            //return base.OnBackButtonPressed();
            return true;
        }

        // Invoced when background is clicked
        protected override bool OnBackgroundClicked()
        {
            // Prevent background clicked action
            //return base.OnBackgroundClicked();
            return false;
        }
    }
}
