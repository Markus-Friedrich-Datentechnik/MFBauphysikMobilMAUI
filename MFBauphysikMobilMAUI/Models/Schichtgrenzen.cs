using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Transactions;

namespace MFBauphysikMobilMAUI.Models
{
    public class Schichtgrenzen : INotifyPropertyChanged
    {    
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));

        }


        private double _dampfteildruck;
        public double Dampfteildruck
        {
            get { return _dampfteildruck;}
            set
            {
                if (_dampfteildruck == value)
                    return;
                _dampfteildruck = value;
                OnPropertyChanged(nameof(Dampfteildruck));
            }
        }

        private double _sd;
        public double Sd
        {
            get { return _sd; }
            set
            {
                if (_sd == value)
                    return;
                _sd = value;
                OnPropertyChanged(nameof(Sd));
            }
        }

        private double _sumsd;
        public double SumSd
        {
            get { return _sumsd; }
            set
            {
                if (_sumsd == value)
                    return;
                _sumsd = value;
                OnPropertyChanged(nameof(SumSd));
            }
        }
    }
}

