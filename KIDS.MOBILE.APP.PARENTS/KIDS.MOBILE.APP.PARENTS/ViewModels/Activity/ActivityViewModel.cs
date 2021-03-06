using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using KIDS.MOBILE.APP.PARENTS.Configurations;
using KIDS.MOBILE.APP.PARENTS.Models.Activity;
using KIDS.MOBILE.APP.PARENTS.Services.Activity;
using Prism.Navigation;
using Xamarin.Forms;

namespace KIDS.MOBILE.APP.PARENTS.ViewModels.Activity
{
    public class ActivityViewModel : BaseViewModel
    {
        #region Properties
        private IActivityService _activityService;

        private ObservableCollection<ExerciseClass> _activityList = new ObservableCollection<ExerciseClass>();
        public ObservableCollection<ExerciseClass> ActivityList
        {
            get => _activityList;
            set
            {
                _activityList = value;
                RaisePropertyChanged(nameof(ActivityList));
            }
        }

        private ObservableCollection<ExerciseClass> _afternoonActivityList = new ObservableCollection<ExerciseClass>();
        public ObservableCollection<ExerciseClass> AfternoonActivityList
        {
            get => _afternoonActivityList;
            set
            {
                _afternoonActivityList = value;
                RaisePropertyChanged(nameof(AfternoonActivityList));
            }
        }

        private DateTime selectedDate;
        public DateTime SelectedDate
        {
            get => selectedDate;
            set => SetProperty(ref selectedDate, value);
        }

        public bool IsActivityVisible
        {
            get => ActivityList.Any() && AfternoonActivityList.Any();
        }

        private bool _IsDailyCommentVisible;
        public bool IsDailyCommentVisible
        {
            get => _IsDailyCommentVisible;
            set => SetProperty(ref _IsDailyCommentVisible, value);
        }

        private string _StudyingComment;
        public string StudyingComment
        {
            get => _StudyingComment;
            set
            {
                _StudyingComment = value;
                RaisePropertyChanged(nameof(StudyingComment));
                RaisePropertyChanged(nameof(IsStudyingCommentVisible));
            }
        }

        public bool IsStudyingCommentVisible { get => !string.IsNullOrEmpty(StudyingComment); }
        private string studentId;
        private string classId;
        private string gradeId;

        private bool hasAnyActivity;
        public bool HasAnyActivity
        {
            get => hasAnyActivity;
            set => SetProperty(ref hasAnyActivity, value);
        }
        #endregion

        #region Contructor
        public ActivityViewModel(INavigationService navigationService,IActivityService activityService) : base(navigationService)
        {
            _navigationService = navigationService;
            _activityService = activityService;
        }
        public override void Initialize(INavigationParameters parameters)
        {
            try
            {
                base.Initialize(parameters);
                IsLoading = true;
                SelectedDate = DateTime.Now;
                studentId = AppConstants.User.StudentID;
                classId = AppConstants.User.ClassID;
                gradeId = AppConstants.User.GradeID;
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
        #endregion

        #region Public methods
        public async Task GetDailyActivity(DateTime date)
        {
            SelectedDate = date;
            await GetActivityList();
        }
        #endregion

        #region Private methods
        private async Task GetActivityList()
        {
            try
            {
                ActivityList?.Clear();
                AfternoonActivityList?.Clear();
                var selectedDate = SelectedDate.ToString("yyyy/MM/dd");
                var listMorningActivity = await _activityService.GetMorningActivity(studentId, classId, selectedDate);
                var listAfternoonActivity = await _activityService.GetAfternoonActivity(studentId, classId, selectedDate);

                HasAnyActivity = listMorningActivity?.Data?.Any() == true || listAfternoonActivity?.Data?.Any() == true;

                var morningActivities = new List<ExerciseClass>();
                var afternoonActivities = new List<ExerciseClass>();

                if(listMorningActivity?.Data?.Any() == true)
                {
                    foreach (var item in listMorningActivity.Data)
                    {
                        morningActivities.Add(new ExerciseClass
                        {
                            Id = item.ID,
                            Instructor = item.Contents,
                            ClassTime = item.ThoiGian,
                            IsLast = false,
                            Title = string.Empty
                        });
                    }
                    ActivityList = new ObservableCollection<ExerciseClass>(morningActivities);
                }

                if (listAfternoonActivity?.Data?.Any() == true)
                {
                    foreach (var item in listAfternoonActivity.Data)
                    {
                        afternoonActivities.Add(new ExerciseClass
                        {
                            Id = item.ID,
                            Instructor = item.Contents,
                            ClassTime = item.ThoiGian,
                            IsLast = false,
                            Title = string.Empty
                        });
                    }
                    AfternoonActivityList = new ObservableCollection<ExerciseClass>(afternoonActivities);
                }
            }
            catch (Exception ex)
            {

            }
        }

        
        #endregion
    }

    public class ExerciseClass
    {
        public Guid? Id { get; set; }
        public string ClassTime { get; set; }
        public string Title { get; set; }
        public string Instructor { get; set; }

        public bool IsLast { get; set; } = false;
    }

    public class MenuToDay
    {
        public Guid? Id { get; set; }
        public string Time { get; set; }
        public string Content { get; set; }
        public string Comment { get; set; }
        public ImageSource Image { get; set; }
        public Color TextColor { get; set; }
    }
}

