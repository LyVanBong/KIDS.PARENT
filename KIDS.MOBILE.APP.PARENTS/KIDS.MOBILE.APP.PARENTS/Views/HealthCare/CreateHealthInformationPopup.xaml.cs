using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace KIDS.MOBILE.APP.PARENTS.Views.HealthCare
{
    public partial class CreateHealthInformationPopup : ContentView
    {
        public CreateHealthInformationPopup()
        {
            InitializeComponent();
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

        public CreateHealthInformationPopup
            (string entry1Value,
            string entry2Value,
            string entry3Value)
        {
            InitializeComponent();

            if (!string.IsNullOrEmpty(entry1Value))
            {
                TextEntry1.Text = entry1Value;
            }

            if (!string.IsNullOrEmpty(entry2Value))
            {
                TextEntry2.Text = entry2Value;
            }

            if (!string.IsNullOrEmpty(entry2Value))
            {
                TextEntry3.Text = entry3Value;
            }

            // handling events to expose to public
            SaveButton.Clicked += SaveButton_Clicked;
            CancelButton.Clicked += CancelButton_Clicked;
            TextEntry1.TextChanged += TextEntry1_TextChanged;
            TextEntry2.TextChanged += TextEntry2_TextChanged;
            TextEntry3.TextChanged += TextEntry3_TextChanged;

            MultipleDataResult = new PopupDataModel();
        }

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

        private void TextEntry2_TextChanged(object sender, TextChangedEventArgs e)
        {
            MultipleDataResult.Value2 = TextEntry2.Text;
        }

        private void TextEntry3_TextChanged(object sender, TextChangedEventArgs e)
        {
            MultipleDataResult.Value3 = TextEntry3.Text;
        }
    }

    public class PopupDataModel
    {
        public string Value1 { get; set; }
        public string Value2 { get; set; }
        public string Value3 { get; set; }
    }
}
