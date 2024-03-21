using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace MFBauphysikMobilMAUI.Models
{
    public class Berechnung_R : INotifyPropertyChanged
    {
        private double? _rj;
        public double? Rj
        {
            get { return _rj; }
            set
            {
                if (_rj == value)
                    return;
                _rj = value;
                OnPropertyChanged(nameof(Rj));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));

        }
    }
}
