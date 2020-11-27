using System.Linq;
using KIDS.MOBILE.APP.PARENTS.Configurations;
using KIDS.MOBILE.APP.PARENTS.Models.User;
using KIDS.MOBILE.APP.PARENTS.Resources;
using KIDS.MOBILE.APP.PARENTS.Services.User;
using Prism.Navigation;

namespace KIDS.MOBILE.APP.PARENTS.ViewModels.Account
{
    public class StudentProfileViewModel : BaseViewModel
    {
        private IUserService _userService;
        private StudentModel _studentModel;
        private string _avatar;
        private string _sex;

        public string Sex
        {
            get => _sex;
            set => SetProperty(ref _sex, value);
        }

        public string Avatar
        {
            get => _avatar;
            set => SetProperty(ref _avatar, value);
        }

        public StudentModel StudentModel
        {
            get => _studentModel;
            set => SetProperty(ref _studentModel, value);
        }

        public StudentProfileViewModel(IUserService userService)
        {
            _userService = userService;
        }

        public override async void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
            var student = await _userService.GetStudent(AppConstants.User.StudentID);
            if (student.Code > 0)
            {
                StudentModel = student.Data.SingleOrDefault();
                Avatar = StudentModel.TmpPicture;
                if (StudentModel.Sex == true)
                    Sex = Resource._00031;
                Sex = Resource._00032;
            }
        }
    }
}