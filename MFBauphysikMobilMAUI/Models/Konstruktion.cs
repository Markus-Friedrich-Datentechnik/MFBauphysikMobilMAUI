using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace MFBauphysikMobilMAUI.Models
{
    public class Konstruktion : INotifyPropertyChanged
    {
        private bool _kaltdach;
        private bool _warmdach;
        private bool _hinterluftWand;
        private bool _kein_hinterlufWand;
        private bool _verschattet;

        public bool Kaltdach
        {
            get { return _kaltdach; }
            set
            {
                if (_kaltdach == value)
                    return;
                _kaltdach = value;
                OnPropertyChanged(nameof(Kaltdach));
            }
        }
        public bool Warmdach
        {
            get { return _warmdach; }
            set
            {
                if (_warmdach == value)
                    return;
                _warmdach = value;
                OnPropertyChanged(nameof(Warmdach));
            }
        }

        public bool HinterluftWand
        {
            get { return _hinterluftWand; }
            set
            {
                if (_hinterluftWand == value)
                    return;
                _hinterluftWand = value;
                OnPropertyChanged(nameof(HinterluftWand));
            }
        }

        public bool Kein_HinterluftWand
        {
            get { return _kein_hinterlufWand; }
            set
            {
                if (_kein_hinterlufWand == value)
                    return;
                _kein_hinterlufWand = value;
                OnPropertyChanged(nameof(Kein_HinterluftWand));
            }
        }

        public bool Verschattet
        {
            get { return _verschattet; }
            set
            {
                if (_verschattet == value)
                    return;
                _verschattet = value;
                OnPropertyChanged(nameof(Verschattet));
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
