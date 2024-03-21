using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace MFBauphysikMobilMAUI.Models
{
    public class KonfigurationModel : INotifyPropertyChanged
    {
        private bool _darkmode;
        public bool DarkMode
        {
            get { return _darkmode; }
            set
            {
                if (_darkmode == value)
                    return;
                _darkmode = value;
                OnPropertyChanged(nameof(DarkMode));
            }
        }

        private bool _lightmode;
        public bool LightMode
        {
            get { return _lightmode; }
            set
            {
                if (_lightmode == value)
                    return;
                _lightmode = value;
                OnPropertyChanged(nameof(LightMode));
            }
        }

        public bool _device;
        public bool Device
        {
            get { return _device; }
            set
            {
                if (_device == value)
                    return;
                _device = value;
                OnPropertyChanged(nameof(Device));
            }
        }


        private int _size;
        public int SizeClass
        {
            get { return _size; }
            set
            {
                if (_size == value)
                    return;
                _size = value;
                OnPropertyChanged(nameof(SizeClass));
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
