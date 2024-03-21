using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace MFBauphysikMobilMAUI.Models
{
    public class EinstellungModel : INotifyPropertyChanged
    {
        private double _defaultSize;
        public double DefaultSize
        {
            get { return _defaultSize; }
            set
            {
                if(_defaultSize ==  value) return;
                _defaultSize = value;
                OnPropertyChanged(nameof(DefaultSize));
            }
        }
        private double _microSize;
        public double MicroSize
        {
            get { return _microSize; }
            set
            {
                if (_microSize == value) return;
                _microSize = value;
                OnPropertyChanged(nameof(MicroSize));
            }
        }

        private double _titleSize;
        public double TitleSize
        {
            get { return _titleSize; }
            set { if (_titleSize == value) return;
            _titleSize = value;
                OnPropertyChanged(nameof(TitleSize));
            }
        }

        private double _largeSize;
        public double LargeSize
        {
            get { return _largeSize; }
            set
            {
                if (_largeSize == value) return;
                _largeSize = value;
                OnPropertyChanged(nameof(LargeSize));
            }
        }

        private double _mediumSize;
        public double MediumSize
        {
            get { return _mediumSize; }
            set
            {
                if( _mediumSize == value) return;
                _mediumSize = value;
                OnPropertyChanged(nameof(MediumSize));
            }
        }

        private bool _alt_jung;
        public bool Alt_Jung
        {
            get { return _alt_jung; }
            set
            {
                if (_alt_jung == value)
                    return;
                _alt_jung = value;
                OnPropertyChanged(nameof(Alt_Jung));
            }
        }
        private bool _jung_alt;
        public bool Jung_Alt
        {
            get { return _jung_alt; }
            set
            {
                if (_jung_alt == value)
                    return;
                _jung_alt = value;
                OnPropertyChanged(nameof(Jung_Alt));
            }
        }
        private bool _a_z;
        public bool A_Z
        {
            get { return _a_z; }
            set
            {
                if (_a_z == value)
                    return;
                _a_z = value;
                OnPropertyChanged(nameof(A_Z));
            }
        }
        private bool _z_a;
        public bool Z_A
        {
            get { return _z_a; }
            set
            {
                if (_z_a == value)
                    return;
                _z_a = value;
                OnPropertyChanged(nameof(Z_A));
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
/*rivate bool _vomPC_immer;
        public bool VomPC_immer
        {
            get { return _vomPC_immer; }
            set
            {
                if (_vomPC_immer == value)
                    return;
                _vomPC_immer = value;
                OnPropertyChanged(nameof(VomPC_immer));
            }
        }
        private bool _vomPC_wifi;
        public bool VomPC_wifi
        {
            get { return _vomPC_wifi; }
            set
            {
                if (_vomPC_wifi == value)
                    return;
                _vomPC_wifi = value;
                OnPropertyChanged(nameof(VomPC_wifi));
            }
        }

        private bool _vomPC_not;
        public bool VomPC_not
        {
            get { return _vomPC_not; }
            set
            {
                if (_vomPC_not == value)
                    return;
                _vomPC_not = value;
                OnPropertyChanged(nameof(VomPC_not));
            }
        }

        private bool _zumPC_immer;
        public bool ZumPC_immer
        {
            get { return _zumPC_immer; }
            set
            {
                if (_zumPC_immer == value)
                    return;
                _zumPC_immer = value;
                OnPropertyChanged(nameof(ZumPC_immer));
            }
        }

        private bool _zumPC_wifi;
        public bool ZumPC_wifi
        {
            get { return _zumPC_wifi; }
            set
            {
                if (_zumPC_wifi == value)
                    return;
                _zumPC_wifi = value;
                OnPropertyChanged(nameof(ZumPC_wifi));
            }
        }

        private bool _zumPC_not;
        public bool ZumPC_not
        {
            get { return _zumPC_not; }
            set
            {
                if (_zumPC_not == value)
                    return;
                _zumPC_not = value;
                OnPropertyChanged(nameof(ZumPC_not));
            }
        }

        private bool _element_immer;
        public bool Element_immer
        {
            get { return _element_immer; }
            set
            {
                if (_element_immer == value)
                    return;
                _element_immer = value;
                OnPropertyChanged(nameof(Element_immer));
            }
        }

        private bool _element_wifi;
        public bool Element_wifi
        {
            get { return _element_wifi; }
            set
            {
                if (_element_wifi == value)
                    return;
                _element_wifi = value;
                OnPropertyChanged(nameof(Element_wifi));
            }
        }

        private bool _element_not;
        public bool Element_not
        {
            get { return _element_not; }
            set
            {
                if (_element_not == value)
                    return;
                _element_not = value;
                OnPropertyChanged(nameof(Element_not));
            }
        }

        private string _zeit;
        public string Zeit
        {
            get { return _zeit; }
            set
            {
                if (_zeit == value)
                    return;
                _zeit = value;
                OnPropertyChanged(nameof(Zeit));
            }
        }*/
