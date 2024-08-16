using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using CommunityToolkit.Mvvm;
using Microsoft.Maui.Controls;
using Microsoft.Maui;
using CommunityToolkit.Mvvm.ComponentModel;

namespace MFBauphysikMobilMAUI.Models
{
   /* public class Musteraufbau : ObservableObject, INotifyPropertyChanged
    {
        private string _musterName;
        public string MusterName 
        {
            get { return _musterName; }
            set
            {
                if (_musterName == value) return;
                _musterName = value;
                OnPropertyChanged(nameof(MusterName));
            }
        }       
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }      
    }*/

    public class Musteraufbau
    {
        public string MusterName { get; set; }
    }
}
