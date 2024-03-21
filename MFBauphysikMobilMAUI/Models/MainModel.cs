using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using CommunityToolkit.Mvvm;

namespace MFBauphysikMobilMAUI.Models
{
    public class MainModel : INotifyPropertyChanged
    {//Outer_Class

        public int Selected { get; set; }

        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        private string _projectName;
        public string ProjectName
        {
            get { return _projectName; }
            set
            {
                if (_projectName == value)
                    return;
                _projectName = value;
                Utils.Settings.LastProjekt = value;
                OnPropertyChanged("ProjectName");
            }
        }
        public DateTime Date { get; set; }
        private string _bv_ersatz;
        public string BV_Ersatz
        {
            get { return _bv_ersatz; }
            set
            {
                if (_bv_ersatz == value)
                    return;
                _bv_ersatz = value;
                Utils.Settings.LastBV = value;
                OnPropertyChanged("BV_Ersatz");
            }
        }

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

        private double _size_class;
        public double SizeClass
        {
            get { return _size_class; }
            set
            {
                if (_size_class == value) return;
                _size_class = value;
                OnPropertyChanged(nameof(SizeClass));
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        //Basis
        ObservableCollection<BefestigerBasis> _befestiger_basis = new ObservableCollection<BefestigerBasis>();
        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public ObservableCollection<BefestigerBasis> Befestiger_Basis
        {
            get { return _befestiger_basis; }
            set
            {
                _befestiger_basis = value;
                OnPropertyChanged(nameof(Befestiger_Basis));
            }
        }

        ObservableCollection<Basis> _bauteil_basis = new ObservableCollection<Basis>();
        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public ObservableCollection<Basis> Bauteil_Basis
        {
            get { return _bauteil_basis; }
            set
            {
                _bauteil_basis = value;
                OnPropertyChanged(nameof(Bauteil_Basis));
            }
        }


        //Sparren
        ObservableCollection<BefestigerSparren> _befestiger_sparren = new ObservableCollection<BefestigerSparren>();
        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public ObservableCollection<BefestigerSparren> Befestiger_Sparren
        {
            get { return _befestiger_sparren; }
            set
            {
                _befestiger_sparren = value;
                OnPropertyChanged(nameof(Befestiger_Sparren));
            }
        }

        ObservableCollection<Sparren> _bauteil_sparren = new ObservableCollection<Sparren>();
        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public ObservableCollection<Sparren> Bauteil_Sparren
        {
            get { return _bauteil_sparren; }
            set
            {
                _bauteil_sparren = value;
                OnPropertyChanged(nameof(Bauteil_Sparren));
            }
        }


        //Gefach
        ObservableCollection<BefestigerGefach> _befestiger_gefach = new ObservableCollection<BefestigerGefach>();
        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public ObservableCollection<BefestigerGefach> Befestiger_Gefach
        {
            get { return _befestiger_gefach; }
            set
            {
                _befestiger_gefach = value;
                OnPropertyChanged(nameof(Befestiger_Gefach));
            }
        }

        ObservableCollection<Gefach> _bauteil_gefach = new ObservableCollection<Gefach>();
        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public ObservableCollection<Gefach> Bauteil_Gefach
        {
            get { return _bauteil_gefach; }
            set
            {
                _bauteil_gefach = value;
                OnPropertyChanged(nameof(Bauteil_Gefach));
            }
        }


        //Ständer 
        ObservableCollection<BefestigerStänder> _befestiger_ständer = new ObservableCollection<BefestigerStänder>();
        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public ObservableCollection<BefestigerStänder> Befestiger_Ständer
        {
            get { return _befestiger_ständer; }
            set
            {
                _befestiger_ständer = value;
                OnPropertyChanged(nameof(Befestiger_Ständer));
            }
        }

        ObservableCollection<Ständer> _bauteil_ständer = new ObservableCollection<Ständer>();
        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public ObservableCollection<Ständer> Bauteil_Ständer
        {
            get { return _bauteil_ständer; }
            set
            {
                _bauteil_ständer = value;
                OnPropertyChanged(nameof(Bauteil_Ständer));
            }
        }

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }      
    }
}

