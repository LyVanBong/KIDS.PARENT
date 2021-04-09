using System;
using System.Threading.Tasks;
using KIDS.MOBILE.APP.PARENTS.Views.HealthCare;

namespace KIDS.MOBILE.APP.PARENTS.Services.Popup
{
    public interface IInputAlertDialogService
    {
        void ClosePopup();
        Task<PopupDataModel> OpenMultipleDataInputAlertDialog(
            string entry1Value,
            string entry2Value,
            string entry3Value);
        Task<PopupDataModel> OpenThanksMessagePopup(string entry1Value);
    }
}
