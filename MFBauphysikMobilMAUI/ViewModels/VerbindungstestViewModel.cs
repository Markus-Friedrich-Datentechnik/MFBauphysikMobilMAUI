using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Microsoft.Maui.Controls;
using Microsoft.Maui;

namespace MFBauphysikMobilMAUI.ViewModels
{
    public class VerbindungstestViewModel
    {
        private string detailsText;
        public event PropertyChangedEventHandler PropertyChanged;

        public string DetailsText
        {
            get => this.detailsText; 
            set
            {
                this.detailsText = value;
                this.PropertyChanged(this, new PropertyChangedEventArgs(nameof(this.DetailsText)));
            }
        }
        public ICommand CommandDetail => new Command(() => { this.DetailsText = "Details verbergen"; });
    }

}
