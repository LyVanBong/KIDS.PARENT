﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Prism.Navigation;
using Xamarin.Forms;

namespace KIDS.MOBILE.APP.PARENTS.ViewModels.HeatlCare
{
    public class HealthCareViewModel : BaseViewModel
    {
        #region Properties
        
        private ObservableCollection<HealthInformationModel> _healthList;
        public ObservableCollection<HealthInformationModel> HealthList
        {
            get => _healthList;
            set => SetProperty(ref _healthList, value);
        }
        #endregion

        #region Constructor
        public HealthCareViewModel(INavigationService navigationService) : base(navigationService)
        {
            _navigationService = navigationService;
        }

        public override void Initialize(INavigationParameters parameters)
        {
            base.Initialize(parameters);
            HealthList = new ObservableCollection<HealthInformationModel>(GetHealthList());
        }
        #endregion

        #region Public methods
        #endregion

        #region Private methods
        private List<HealthInformationModel> GetHealthList()
        {
            return new List<HealthInformationModel>
            {
                new HealthInformationModel
                {
                    BackgroundGradientStart="#cf86ff",
                    Name = "Chiều cao",
                    Information = "130 cm"
                },
                new HealthInformationModel
                {
                    BackgroundGradientStart="#8691ff",
                    Name = "Cân nặng",
                    Information = "30 kg"
                },
                new HealthInformationModel
                {
                    BackgroundGradientStart="#ff9686",
                    Name = "Bệnh về mắt",
                    Information = "Không"
                },
                new HealthInformationModel
                {
                    BackgroundGradientStart="#cf86ff",
                    Name = "Dị ứng",
                    Information = "Không"
                },
                new HealthInformationModel
                {
                    BackgroundGradientStart="#8691ff",
                    Name = "Bệnh về tim",
                    Information = "Không"
                },
            };
        }
        #endregion
    }

    public class HealthInformationModel
    {
        public string BackgroundGradientStart { get; set; }
        public string Name { get; set; }
        public string Information { get; set; }
    }
}

