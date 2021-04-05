using KIDS.MOBILE.APP.PARENTS.Views.HealthCare;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace KIDS.MOBILE.APP.PARENTS.Views.Popups
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ThanksMessagePopup : ContentView
    {
        public ThanksMessagePopup(string entry1Value)
        {
            InitializeComponent();
            if (!string.IsNullOrEmpty(entry1Value))
            {
                TextEntry1.Text = entry1Value;
            }
            // handling events to expose to public
            SaveButton.Clicked += SaveButton_Clicked;
            CancelButton.Clicked += CancelButton_Clicked;
            TextEntry1.TextChanged += TextEntry1_TextChanged;
            MultipleDataResult = new PopupDataModel();
        }

        // public event handler to expose 
        // the Save button's click event
        public EventHandler SaveButtonEventHandler { get; set; }

        // public event handler to expose 
        // the Cancel button's click event
        public EventHandler CancelButtonEventHandler { get; set; }

        // public string to expose the 
        // text Entry input's value
        public PopupDataModel MultipleDataResult { get; set; }

        private void SaveButton_Clicked(object sender, EventArgs e)
        {
            // invoke the event handler if its being subscribed
            SaveButtonEventHandler?.Invoke(this, e);
        }

        private void CancelButton_Clicked(object sender, EventArgs e)
        {
            // invoke the event handler if its being subscribed
            CancelButtonEventHandler?.Invoke(this, e);
        }

        private void TextEntry1_TextChanged(object sender, TextChangedEventArgs e)
        {
            MultipleDataResult.Value1 = TextEntry1.Text;
        }
    }
}