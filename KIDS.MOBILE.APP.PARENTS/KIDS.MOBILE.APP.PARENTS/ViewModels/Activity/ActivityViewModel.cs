using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Prism.Navigation;
using Xamarin.Forms;

namespace KIDS.MOBILE.APP.PARENTS.ViewModels.Activity
{
    public class ActivityViewModel : BaseViewModel
    {
        #region Properties
        //private IMessageService _messageService;
        private ObservableCollection<ExerciseClass> _activityList = new ObservableCollection<ExerciseClass>();
        public ObservableCollection<ExerciseClass> ActivityList
        {
            get => _activityList;
            set => SetProperty(ref _activityList, value);
        }
        //public DelegateCommand AddCommand { get; }
        #endregion

        #region Contructor
        public ActivityViewModel(INavigationService navigationService) : base(navigationService)
        {
            _navigationService = navigationService;
            //_messageService = messageService;
            //AddCommand = new DelegateCommand(OnAddClick);
        }
        public override void Initialize(INavigationParameters parameters)
        {
            try
            {
                base.Initialize(parameters);
                ActivityList = new ObservableCollection<ExerciseClass>(GetActivityList());
                IsLoading = true;
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
        #endregion

        #region Private methods
        private List<ExerciseClass> GetActivityList()
        {
            return new List<ExerciseClass>
            {
                new ExerciseClass
                {
                    ClassName = "Yoga",
                    Instructor = "Maharshi Patanjali",
                    ClassTime = TodayAt(8,00),
                },
                 new ExerciseClass
                {
                    ClassName = "ABS + Stretch",
                    Instructor = "David Hasslehoff",
                    ClassTime = TodayAt(9,30),
                },
                 new ExerciseClass
                {
                    ClassName = "Body Sculpt",
                    Instructor = "Sadie Terry",
                    ClassTime = DateTime.Now.AddHours(3),
                },
                 new ExerciseClass
                {
                    ClassName = "Cycle",
                    Instructor = "Lance Armstrong",
                    ClassTime = TodayAt(12,00),
                },
                 new ExerciseClass
                {
                    ClassName = "Aerobics",
                    Instructor = "Jacky Chan",
                    ClassTime = TodayAt(15,30),
                },
                 new ExerciseClass
                {
                    ClassName = "Weights",
                    Instructor = "Arnold Schwarzenegger",
                    ClassTime = TodayAt(18,00),
                    IsLast = true
                },
            };
        }

        private DateTime TodayAt(int hour, int minute)
        {
            return new DateTime(DateTime.Now.Year,
                DateTime.Now.Month,
                DateTime.Now.Day,
                hour, minute, 0);
        }
        #endregion
    }

    public class ExerciseClass
    {
        public DateTime ClassTime { get; set; }
        public string ClassName { get; set; }
        public string Instructor { get; set; }

        public bool IsLast { get; set; } = false;
    }
}

