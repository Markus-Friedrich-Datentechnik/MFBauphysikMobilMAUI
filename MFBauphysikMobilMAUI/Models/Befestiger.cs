using MFBauphysikMobilMAUI.Models;
using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace MFBauphysikMobilMAUI.Models
{
    public class Befestiger : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        [PrimaryKey, AutoIncrement]
        public int ID_Befestiger { get; set; }

        private string _bezeichnung;
        public string Bezeichnung
        {
            get { return _bezeichnung; }
            set
            {
                if (_bezeichnung == value) return;
                _bezeichnung = value;
                OnPropertyChanged(nameof(Bezeichnung));
            }
        }

        private double? _anzahl;
        public double? Anzahl
        {
            get { return _anzahl; }
            set
            {
                if (_anzahl == value)
                    return;
                _anzahl = value;
                OnPropertyChanged(nameof(Anzahl));
            }
        }

        private double? _lambda_f;
        public double? Wärmeleitfähigkeit_f
        {
            get { return _lambda_f; }
            set
            {
                if (_lambda_f == value) return;
                _lambda_f = value;
                OnPropertyChanged(nameof(Wärmeleitfähigkeit_f));
            }
        }

        private double? _d;
        public double? Durchmesser
        {
            get { return _d; }
            set
            {
                if (_d == value) return;
                _d = value;
                OnPropertyChanged(nameof(Durchmesser));
            }
        }

        private double? _d1;
        public double? Eindringtiefe
        {
            get { return _d1; }
            set
            {
                if (_d1 == value) return;
                _d1 = value;
                OnPropertyChanged(nameof(Eindringtiefe));
            }
        }

        private double? _länge;
        public double? Länge
        {
            get { return _länge; }
            set
            {
                if (_länge == value) return;
                _länge = value;
                OnPropertyChanged(nameof(Länge));
            }
        }

        private double? _ufi;
        public double? Uf_i
        {
            get { return _ufi; }
            set
            {
                if (_ufi == value) return;
                _ufi = value;
                OnPropertyChanged(nameof(Uf_i));
            }
        }


        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));

        }
        [ForeignKey(typeof(MainModel))]
        public int ModelID { get; set; }   //foreign key
    }
}
