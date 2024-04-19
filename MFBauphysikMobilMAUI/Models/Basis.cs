using Newtonsoft.Json;
using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Transactions;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using SQLitePCL;
using SQLiteNetExtensions.Attributes;
using CommunityToolkit.Mvvm.ComponentModel;

namespace MFBauphysikMobilMAUI.Models
{
    public class Basis : ObservableObject, INotifyPropertyChanged 
    {       
        [PrimaryKey, AutoIncrement]
        public int ID_Bauteil { get; set; }
        public double SizeClass { get; set; }

        public int ID_Sort { get;set; }

        private string? _name;
        public string Bezeichnung
        {
            get { return _name; }
            set
            {
                if (_name == value)
                    return;
                _name = value;
                OnPropertyChanged(nameof(Bezeichnung));
            }
        }

        private double? _lambda;
        public double? Wärmeleitfähigkeit 
        {
            get { return _lambda; }
            set
            {
                if (_lambda == value)
                    return;
                _lambda = value;
                OnPropertyChanged(nameof(Wärmeleitfähigkeit));              
            }
        }

        public double? LR1 { get; set; }
        public double? LR2 { get; set; }
        public double? LR3 { get; set; }
        public double? LR4 { get; set; }
        public double? LR5 { get; set; }
        public double? DLR1 { get; set; }
        public double? DLR2 { get; set; }
        public double? DLR3 { get; set; }
        public double? DLR4 { get; set; }
        public double? DLR5 { get; set; }


        private double? _dicke;
        public double? Dicke
        {
            get
            {
                return _dicke;
            }
            set
            {
                if (_dicke == value)
                    return;
                _dicke = value;
                OnPropertyChanged(nameof(Dicke));                
              
            }
        }
        //Berechnen R = Dicke/Lambda
        private double? _r;
        public double? R
        {
            get
            {
                return _r;
            }
            set
            {
                if (_r == value)
                    return;
                _r = value;
                OnPropertyChanged(nameof(R));               
            }
        }

        private bool _fester_R;
        public bool Fester_R
        {
            get { return _fester_R; }
            set 
            {
                if (_fester_R == value)
                    return;
                _fester_R = value;
                OnPropertyChanged(nameof(Fester_R));
            }
        }

        private bool _fester_sd;
        public bool Fester_sd
        {
            get { return _fester_sd; }
            set
            {
                if( _fester_sd == value)
                    return;
                _fester_sd = value;
                OnPropertyChanged(nameof(Fester_sd));
            }
        }

        //Auswahl
        private bool _kapillar;
        public bool Kapillar
        {
            get { return _kapillar; }
            set
            {
                if (_kapillar == value)
                    return;
                _kapillar = value;
                OnPropertyChanged(nameof(Kapillar));
            }
        }
        private bool _holz;
        public bool Holz
        {
            get { return _holz; }
            set
            {
                if (_holz == value) return;
                _holz = value;
                OnPropertyChanged(nameof(Holz));
            }
        }
        private bool _holzwerkstoff;
        public bool Holzwerkstoff
        {
            get { return _holzwerkstoff; }
            set
            {
                if (_holzwerkstoff == value)
                    return;
                _holzwerkstoff = value;
                OnPropertyChanged(nameof(Holzwerkstoff));
            }
        }

        private bool _sonstiges;
        public bool sonstiges
        {
            get { return _sonstiges; }
            set
            {
                if (_sonstiges == value)
                    return;
                _sonstiges = value;
                OnPropertyChanged(nameof(sonstiges));
            }
        }

        private bool _keineLuft;
        public bool KeineLuft
        {
            get { return _keineLuft; }
            set
            {
                if (_keineLuft == value) 
                    return;
                _keineLuft = value;               
                OnPropertyChanged(nameof(KeineLuft));
            }
        }

        private bool _evntlLuft;
        public bool EvntlLuft
        {
            get { return _evntlLuft; }
            set
            {
                if (_evntlLuft == value)
                    return;
                _evntlLuft = value;
                OnPropertyChanged(nameof(EvntlLuft));
            }
        }

        private bool _mitLuft;
        public bool MitLuft
        {
            get { return _mitLuft; }
            set
            {
                if (_mitLuft == value)
                    return;
                _mitLuft = value;
                OnPropertyChanged(nameof(MitLuft));
            }
        }

        private double? _dampfdiffusionswiderstand_min;
        public double? Dampfdiffusionswiderstand_Min
        {
            get { return _dampfdiffusionswiderstand_min; }
            set
            {
                if (_dampfdiffusionswiderstand_min == value)
                    return;
                _dampfdiffusionswiderstand_min = value;
                OnPropertyChanged(nameof(Dampfdiffusionswiderstand_Min));
            }
        }
        private double? _dampfdiffusionswiderstand_max;
        public double? Dampfdiffusionswiderstand_Max
        {
            get { return _dampfdiffusionswiderstand_max; }
            set
            {
                if (_dampfdiffusionswiderstand_max == value)
                    return;
                _dampfdiffusionswiderstand_max = value;
                OnPropertyChanged(nameof(Dampfdiffusionswiderstand_Max));
            }
        }

        private double? _sd_min;
        public double? Sd_Min
        {
            get { return _sd_min; }
            set
            {
                if (_sd_min == value) return;
                _sd_min = value;
                OnPropertyChanged(nameof(Sd_Min));   
            }
        }

        private double? _sd_max;
        public double? Sd_Max
        {
            get { return _sd_max; }
            set
            {
                if (_sd_max == value) return;
                _sd_max = value;
                OnPropertyChanged(nameof(Sd_Max));               
            }
        }

        private double? _sd;
        public double? Sd
        {
            get { return _sd; }
            set
            {
                if (_sd == value) return;
                _sd = value;
                OnPropertyChanged(nameof(Sd));
            }
        }

        private double? _gewicht;
        public double? Gewicht
        {
            get { return _gewicht; }
            set
            {
                if (_gewicht == value) return;
                _gewicht = value;
                OnPropertyChanged(nameof(Gewicht));
            }
        }

        private double? _tempverlauf;
        public double? Tempverlauf
        {
            get { return _tempverlauf; }
            set
            {
                if (value == _tempverlauf) return;
                _tempverlauf = value;
                OnPropertyChanged(nameof(Tempverlauf));
            }
        }


        private double? _dampfteildruck;
        public double? Dampfteildruck
        {
            get { return _dampfteildruck; }
            set
            {
                if (_dampfteildruck == value) return;
                _dampfteildruck = value;
                OnPropertyChanged(nameof(Dampfteildruck));
            }
        }

        private double? _dampfsättigungsdruck;
        public double? Dampfsättigungsdruck
        {
            get { return _dampfsättigungsdruck; }
            set
            {
                if (_dampfsättigungsdruck == value) return;
                _dampfsättigungsdruck = value;
                OnPropertyChanged(nameof(Dampfsättigungsdruck));
            }
        }

        private double? _dichte;
        public double? Rohdichte
        {
            get { return _dichte; }
            set
            {
                if (_dichte == value) return;
                _dichte = value;
                OnPropertyChanged(nameof(Rohdichte));
            }
        }

        //Kennzeichen von Ebene mit Tauwasserausfall
        private bool _tw;
        public bool TW
        {
            get { return _tw; }
            set
            {
                if (_tw == value) return;
                _tw = value;
                OnPropertyChanged(nameof(TW));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        [ForeignKey(typeof(MainModel))]
        public int ModelID {  get; set; }   

    }
}
