using System;
using System.Collections.Generic;
using KIDS.MOBILE.APP.PARENTS.ViewModels.MedicineAdvise;
using Prism.Mvvm;
using Xamarin.Forms;

namespace KIDS.MOBILE.APP.PARENTS.Views.MedicineAdvise
{
    public partial class CreteMedicineAdvisePage : PageBase
    {
        CreateMedicineAdviseViewModel vm;
        public CreteMedicineAdvisePage()
        {
            InitializeComponent();
            ViewModelLocator.SetAutowireViewModel(this, true);
            vm = (CreateMedicineAdviseViewModel)this.BindingContext;
        }

        private void Delete_Tapped(object sender, EventArgs e)
        {
            try
            {
                var item = (sender as Image).BindingContext as MedicineModel;
                vm.OnDeleteClick(item);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
