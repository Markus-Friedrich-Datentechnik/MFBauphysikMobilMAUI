using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace MFBauphysikMobilMAUI.Models
{
    public class KlimadatenClass : INotifyPropertyChanged
    {
        private int _tauDauer;
        public int TauDauer
        {
            get { return _tauDauer; }
            set { if (_tauDauer == value) return;
                _tauDauer = value;
                OnPropertyChanged(nameof(TauDauer));
            }
        }


        private int _innenTemp;
        public int InnenTemp
        {
            get { return _innenTemp; }
            set
            {
                if (_innenTemp == value) return;
                _innenTemp = value;
                OnPropertyChanged(nameof(InnenTemp));
            }
        }

        private int _innenFeuchte;
        public int InnenFeuchte
        {
            get { return _innenFeuchte; }
            set
            {
                if (_innenFeuchte == value) return;
                _innenFeuchte = value;
                OnPropertyChanged(nameof(InnenFeuchte));
            }
        }

        private int _außenTemp;
        public int AußenTemp
        {
            get { return _außenTemp; }
            set
            {
                if (_außenTemp == value) return;
                _außenTemp = value;
                OnPropertyChanged(nameof(AußenTemp));
            }
        }

        private int _außenFeuchte;
        public int AußenFeuchte
        {
            get { return _außenFeuchte; }
            set
            {
                if (_außenFeuchte == value)
                    return;
                _außenFeuchte= value;
                OnPropertyChanged(nameof(AußenFeuchte)) ;
            }
        }

        private int _verdunstungsDauer;
        public int VerdunstungsDauer
        {
            get { return _verdunstungsDauer; }
            set
            {
                if (_verdunstungsDauer == value)
                    return;
                _verdunstungsDauer = value;
                OnPropertyChanged(nameof(_verdunstungsDauer));
            }
        }

        private int _innenDruckVerdunstung;
        public int InnenDruckVerdunstung
        {
            get { return _innenDruckVerdunstung; }
            set
            {
                if (_innenDruckVerdunstung == value)
                    return;
                _innenDruckVerdunstung = value;
                OnPropertyChanged(nameof(InnenDruckVerdunstung));
            }
        }

        private int _außenDruckVerdunstung;
        public int AußenDruckVerdunstung
        {
            get { return _außenDruckVerdunstung; }
            set
            {
                if (_außenDruckVerdunstung == value)
                    return;
                _außenDruckVerdunstung = value;
                OnPropertyChanged(nameof(AußenDruckVerdunstung)) ;
            }
        }

        private int _wände;
        public int Wände
        {
            get { return _wände; }
            set
            {
                if (_wände == value) return;
                _wände= value;
                OnPropertyChanged(nameof(Wände));
            }
        }
        private int _dächer;
        public int Dächer
        {
            get { return _dächer; }
            set
            {
                if (_dächer == value) return;
                _dächer= value;
                OnPropertyChanged(nameof(Dächer));
            }
        }


        private int _innen_Wasserdampfdruck;
        public int InnenWasserdampfdruck
        {
            get { return _innen_Wasserdampfdruck; }
            set
            {
                if (_innen_Wasserdampfdruck == value) return;
                _innen_Wasserdampfdruck= value;
                OnPropertyChanged(nameof(InnenWasserdampfdruck));
            }
        }

        private int _außen_Wasserdampfdruck;
        public int AußenWasserdampfdruck
        {
            get { return _außen_Wasserdampfdruck; }
            set
            {
                if (_außen_Wasserdampfdruck== value) return;
                _außen_Wasserdampfdruck = value;
                OnPropertyChanged(nameof(AußenWasserdampfdruck));
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;

        private double _size_default;
        public double SizeDefault
        {
            get { return _size_default; }
            set
            {
                if (_size_default == value)
                    return;
                _size_default = value;
                OnPropertyChanged(nameof(SizeDefault));
            }
        }
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
