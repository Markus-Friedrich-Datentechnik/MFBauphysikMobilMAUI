using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Maui.Controls.Xaml;
using System.Collections.ObjectModel;
using MFBauphysikMobilMAUI.Models;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using MFBauphysikMobilMAUI.ViewModels;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography;
using System.Transactions;
using System.Runtime.InteropServices.ComTypes;
using CommunityToolkit.Mvvm;
using System.Windows.Input;
using SQLitePCL;
using System.ComponentModel.Design;
using MFBauphysikMobilMAUI.Data;
using System.Collections;
using System.Runtime.ExceptionServices;
using MFBauphysikMobilMAUI.NewProject;
using static System.Net.Mime.MediaTypeNames;
using System.Diagnostics;
using MFBauphysikMobilMAUI.Helpers;
using Microsoft.Maui.Controls;
using Microsoft.Maui;
using TestBauphysikMaui;


namespace MFBauphysikMobilMAUI.NewProject
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CalculationPage : ContentPage, INotifyPropertyChanged
    {
        //MainModel Base
        public MainModel main_model { get; set; }
        private string _aufbau;
        public string Aufbau
        {
            get { return _aufbau; }
            set
            {
                if (_aufbau == value) return;
                _aufbau = value;
                OnPropertyChanged(nameof(Aufbau));
            }
        }
        public Basis? newItem_Bauteil { get; set; }
        public BefestigerBasis? newItem_Befestiger { get;set; }
        //Definiert ObservableCollection OnPropertyChanged BauteilListe
        ObservableCollection<Basis> _basis = new ObservableCollection<Basis>();
        public ObservableCollection<Basis> BasisList
        {
            get { return _basis; }
            set
            {
                _basis = value;
                OnPropertyChanged(nameof(BasisList));
            }
        }       

        //Definiert ObservableCollection OnPropertyChanged BefestigerListe
        ObservableCollection<BefestigerBasis> _befestiger = new ObservableCollection<BefestigerBasis>();
        public ObservableCollection<BefestigerBasis> BefestigerList
        {
            get { return _befestiger; }
            set
            {
                _befestiger = value;
                OnPropertyChanged(nameof(BefestigerBasis));
            }
        }

        ObservableCollection<KlimadatenClass> _klimadaten = new ObservableCollection<KlimadatenClass>();
        public ObservableCollection<KlimadatenClass> Klimadaten
        {
            get { return _klimadaten; }
            set
            {
                _klimadaten = value;
                OnPropertyChanged(nameof(KlimadatenClass));
            }
        }

        ObservableCollection<Schichtgrenzen> _schichtgrenze = new ObservableCollection<Schichtgrenzen>();
        public ObservableCollection<Schichtgrenzen> Schichtgrenze
        {
            get { return _schichtgrenze; }
            set
            {
                _schichtgrenze = value;
                OnPropertyChanged(nameof(Schichtgrenze));
            }
        }

        ObservableCollection<Konstruktion> _konstruktion = new ObservableCollection<Konstruktion>();
        public ObservableCollection<Konstruktion> Konstruktion
        {
            get { return _konstruktion; }
            set
            {
                _konstruktion = value;
                OnPropertyChanged(nameof(Konstruktion));
            }
        }

        ObservableCollection<Berechnung_R> _berechnung_Rj = new ObservableCollection<Berechnung_R>();
        public ObservableCollection<Berechnung_R> Berechnung_Rj
        {
            get { return _berechnung_Rj; }
            set
            {
                _berechnung_Rj = value;
                OnPropertyChanged(nameof(Berechnung_R));
            }
        }

        //Anzeige wenn Befestiger in Basis existiert
        private bool _befestigerBoolean;
        public bool BefestigerBoolean
        {
            get { return _befestigerBoolean; }
            set
            {
                if (_befestigerBoolean == value) return;
                _befestigerBoolean = value;
                OnPropertyChanged(nameof(BefestigerBoolean));
            }
        }
        private bool _befestigerBooleanEmpty;
        public bool BefestigerBooleanEmpty
        {
            get { return _befestigerBooleanEmpty; }
            set
            {
                if (_befestigerBooleanEmpty == value) return;
                _befestigerBooleanEmpty = value;
                OnPropertyChanged(nameof(BefestigerBooleanEmpty));
            }
        }

        //Definiert Rgesamt OnPropertyChanged
        private double? _rbasis;
        public double? Rbasis
        {
            get { return _rbasis; }
            set
            {
                if (_rbasis == value)
                    return;
                _rbasis = value;
                OnPropertyChanged(nameof(Rbasis));
                //Rupper = 1 / (((_anteilBasis / _gesamtflächen) / _rbasis) + ((_anteilSparren / _gesamtflächen) / _rsparren));
            }
        }        

        //Definiert Uwert OnPropertyChanged
        private double? _ubasis;
        public double? Ubasis
        {
            get { return _ubasis; }
            set
            {
                if (_ubasis == value)
                    return;
                _ubasis = value;
                OnPropertyChanged(nameof(Ubasis));
            }
        }                  

        //Korrektur für mechanische Befestigungselemente
        private double? _uf_basis;
        public double? Uf_Basis
        {
            get { return _uf_basis; }
            set
            {
                if (_uf_basis == value)
                    return;
                _uf_basis = value;
                OnPropertyChanged(nameof(Uf_Basis));
            }
        }
        private double? _uf;
        public double? Uf
        {
            get { return _uf; }
            set
            {
                if (_uf == value)
                    return;
                _uf = value;
                OnPropertyChanged(nameof(Uf));
                Gesamt_du = _uf + _ug;
            }
        }

        //Korrektur für Luftspalte
        private double? _ug_basis;
        public double? Ug_Basis
        {
            get { return _ug_basis; }
            set
            {
                if (_ug_basis == value)
                    return;
                _ug_basis = value;
                OnPropertyChanged(nameof(Ug_Basis));
            }
        }
        private double? _ug;
        public double? Ug
        {
            get { return _ug; }
            set
            {
                if (_ug == value)
                    return;
                _ug = value;
                OnPropertyChanged(nameof(Ug));
                Gesamt_du = _uf + _ug;
            }
        }
        private double? _du_g;
        public double? DU_g
        {
            get { return _du_g; }
            set
            {
                if (_du_g == value)
                    return;
                _du_g = value;
                OnPropertyChanged(nameof(DU_g));
            }
        }

        //Ugesamt mit Korrektur
        private double? _delta_u;
        public double? Delta_U
        {
            get { return _delta_u; }
            set
            {
                if (_delta_u == value)
                    return;
                _delta_u = value;
                OnPropertyChanged(nameof(Delta_U));
            }
        }
        private double? _anteilKorrektur;
        public double? AnteilKorrektur
        {
            get { return _anteilKorrektur; }
            set
            {
                if (_anteilKorrektur == value)
                    return;
                _anteilKorrektur = value;
                OnPropertyChanged(nameof(AnteilKorrektur));
            }
        }
        private double? _gesamt_du;
        public double? Gesamt_du
        {
            get { return _gesamt_du; }
            set
            {
                if (_gesamt_du == value)
                    return;
                _gesamt_du = value;
                OnPropertyChanged(nameof(Gesamt_du));
            }
        }

        //Wärmestromsdichte
        private double? _stromdichte;
        public double? Wärmestromdichte
        {
            get { return _stromdichte; }
            set
            {
                if (_stromdichte == value)
                    return;
                _stromdichte = value;
                OnPropertyChanged(nameof(Wärmestromdichte));
            }
        }

        private double? _faktor_dampfdruckverteilung;
        public double? Faktor_Dampfdruckverteilung
        {
            get { return _faktor_dampfdruckverteilung; }
            set
            {
                if (_faktor_dampfdruckverteilung == value)
                    return;
                _faktor_dampfdruckverteilung = value;
                OnPropertyChanged(nameof(Faktor_Dampfdruckverteilung));
            }
        }

        private double? _tauwassermasse;
        public double? Tauwassermasse
        {
            get { return _tauwassermasse; }
            set
            {
                if (_tauwassermasse == value)
                    return;
                _tauwassermasse = value;
                OnPropertyChanged(nameof(Tauwassermasse));
            }
        }
        private double? _zulTauwasser;
        public double? ZulTauwasser
        {
            get { return _zulTauwasser; }
            set
            {
                if (_zulTauwasser == value)
                    return;
                _zulTauwasser = value;
                OnPropertyChanged(nameof(ZulTauwasser));
            }
        }
        private double? _verdunstungsmasse;
        public double? Verdunstungsmasse
        {
            get { return _verdunstungsmasse; }
            set
            {
                if (_verdunstungsmasse == value)
                    return;
                _verdunstungsmasse = value;
                OnPropertyChanged(nameof(Verdunstungsmasse));
            }
        }

        private double? _sdm;
        public double? Sdm
        {
            get { return _sdm; }
            set
            {
                if (_sdm == value) return;
                _sdm = value; OnPropertyChanged(nameof(Sdm));
            }
        }

        private double? _gesamtSdBasis;
        public double? Gesamt_SdBasis
        {
            get { return _gesamtSdBasis; }
            set
            {
                if (_gesamtSdBasis == value)
                    return;
                _gesamtSdBasis = value;
                OnPropertyChanged(nameof(Gesamt_SdBasis));
            }
        }       

        private double? _delta0;
        public double? Delta0
        {
            get { return _delta0; }
            set {
                if (_delta0 == value) return;
                _delta0 = value;
                OnPropertyChanged(nameof(Delta0));
            }
        }
        private double? _aufwärts;
        public double? Aufwärts
        {
            get { return _aufwärts; }
            set
            {
                if (_aufwärts == value) return;
                _aufwärts = value;
                OnPropertyChanged(nameof(Aufwärts));
            }
        }
        private double? _horizontal;
        public double? Horizontal
        {
            get { return _horizontal; }
            set
            {
                if (_horizontal == value)
                    return;
                _horizontal = value;
                OnPropertyChanged(nameof(Horizontal));
            }
        }
        private double? _außen;
        public double? Außen
        {
            get { return _außen; }
            set
            {
                if (_außen == value) return;
                _außen = value;
                OnPropertyChanged(nameof(Außen));
            }
        }
        private double? _innen_TWN;
        public double? Innen_TWN
        {
            get { return _innen_TWN; }
            set
            {
                if (_innen_TWN == value) return;
                _innen_TWN = value;
                OnPropertyChanged(nameof(Innen_TWN));
            }
        }
        private string? _konstruktionstyp;
        public string? Konstruktionstyp
        {
            get { return _konstruktionstyp; }
            set
            {
                if (_konstruktionstyp == value) return;
                _konstruktionstyp = value;
                OnPropertyChanged(nameof(Konstruktionstyp));
            }
        }

        private double? _pc;
        public double? Pc
        {
            get { return _pc; }
            set
            {
                if (_pc == value) return;
                _pc = value;
                OnPropertyChanged(nameof(Pc));
            }
        }
        private bool _nachweis_basis;
        public bool NachweisBasis
        {
            get { return _nachweis_basis; }
            set
            {
                if (_nachweis_basis == value) return;
                _nachweis_basis = value;
                OnPropertyChanged(nameof(NachweisBasis));
            }
        }        

        private double? _summe_dicke_basis;
        public double? Summe_Dicke_Basis
        {
            get { return _summe_dicke_basis; }
            set
            {
                if (_summe_dicke_basis == value) return;
                _summe_dicke_basis = value;
                OnPropertyChanged(nameof(Summe_Dicke_Basis));
            }
        }

        private double? _test_entry;
        public double? TestEntry
        {
            get { return _test_entry; }
            set
            {
                if (_test_entry == value) return;
                _test_entry = value;
                OnPropertyChanged(nameof(TestEntry));
            }
        }

        private double _size_default;
        public double SizeDefault
        {
            get { return _size_default; }
            set
            {
                if (_size_default == value) return;
                _size_default = value;
                OnPropertyChanged(nameof(SizeDefault));
            }
        }

        private double _size_medium;
        public double SizeMedium
        {
            get { return _size_medium; }
            set
            {
                if (_size_medium == value)
                    return;
                _size_medium = value;
                OnPropertyChanged(nameof(SizeMedium));
            }
        }

        private bool _reorder;
        public bool Reorder
        {
            get { return _reorder; }
            set
            {
                if (_reorder == value) return;
                _reorder = value;
                OnPropertyChanged(nameof(Reorder));
            }
        }


        //Elemente zur Collection hinzufügen
        //public CalculationPage(Models.Musteraufbau muster)
        public CalculationPage(MainModel muster)
        {
            if (muster == null)
                throw new ArgumentNullException(nameof(muster));
            BindingContext = this;
            InitializeComponent();
            Klimadaten = new ObservableCollection<KlimadatenClass>()
            {
                new KlimadatenClass()
                {
                    //Tauperiode
                    TauDauer = 2160,
                    InnenTemp = 20,
                    InnenFeuchte = 50,

                    AußenTemp = -5,
                    AußenFeuchte = 80,

                    //Verdunstungsperiode
                    VerdunstungsDauer = 2160,
                    InnenDruckVerdunstung = 1200,
                    AußenDruckVerdunstung = 1200,

                    Wände = 1700,
                    Dächer = 2000,

                    //Wasserdampfteildruck Tauperiode
                    InnenWasserdampfdruck = 1168,
                    AußenWasserdampfdruck = 321,
                }
            };

            //Konstante definieren
            Delta0 = 2 * Math.Pow(10, (-10));
            Aufwärts = 0.10;
            Horizontal = 0.13;
            Außen = 0.04;
            Innen_TWN = 0.25;

            main_model = new MainModel
            {
                Selected = muster.Selected,
                ID = muster.ID,
                MusterName = muster.MusterName,
                ProjectName = muster.ProjectName,
                Date = muster.Date,
                BV_Ersatz = muster.BV_Ersatz,
                Befestiger_Basis = muster.Befestiger_Basis,
                Bauteil_Basis = muster.Bauteil_Basis,
            };
            Aufbau = main_model.MusterName;
            //Musteraufbauten mit Liste von Bauteilen
            if (main_model.MusterName == "Beton_G200_EPS_PYE")
            {
                // BefestigerList = muster.Befestiger_Basis;
                if (main_model.Selected == 0)
                {
                    BasisList.Add(new Basis()
                    {
                        ID_Sort = 1,
                        Bezeichnung = "Beton armiert (1% Stahl)",
                        Wärmeleitfähigkeit = 2.3,
                        R = 0.078261,
                        Dicke = 0.18,
                        Kapillar = true,
                        sonstiges = true,
                        KeineLuft = true,
                        Sd_Min = 14.40,
                        Sd_Max = 23.40,
                        Dampfdiffusionswiderstand_Min = 80,
                        Dampfdiffusionswiderstand_Max = 130,
                        Rohdichte = 2300,
                    });
                    BasisList.Add(new Basis()
                    {
                        ID_Sort = 2,
                        Bezeichnung = "G200 S5 (DIN 52 131)",
                        Dicke = 0.0050,
                        R = 0.029412,
                        Wärmeleitfähigkeit = 0.17,
                        Rohdichte = 1200,
                        Kapillar = true,
                        sonstiges = true,
                        KeineLuft = true,
                        Sd_Min = 300.00,
                        Sd_Max = 300.00,
                        Dampfdiffusionswiderstand_Min = 60000,
                        Dampfdiffusionswiderstand_Max = 60000
                    });
                    BasisList.Add(new Basis()
                    {
                        ID_Sort = 3,
                        Bezeichnung = "EPS 035, mit Stufenfalz",
                        R = 5.142857,
                        Dicke = 0.18,
                        Wärmeleitfähigkeit = 0.0350,
                        Rohdichte = 30,
                        Kapillar = true,
                        sonstiges = true,
                        KeineLuft = true,
                        Sd_Min = 3.60,
                        Sd_Max = 18.00,
                        Dampfdiffusionswiderstand_Min = 20,
                        Dampfdiffusionswiderstand_Max = 100
                    });
                    BasisList.Add(new Basis()
                    {
                        ID_Sort = 4,
                        Bezeichnung = "PYE G200 S4",
                        R = 0.0,
                        Dicke = 0.004,
                        Wärmeleitfähigkeit = 0.000,
                        Fester_R = true,
                        Rohdichte = 1000,
                        Kapillar = true,
                        sonstiges = true,
                        KeineLuft = true,
                        Sd_Min = 80.00,
                        Sd_Max = 120.00,
                        Dampfdiffusionswiderstand_Min = 20000,
                        Dampfdiffusionswiderstand_Max = 30000
                    }) ;
                    BasisList.Add(new Basis()
                    {
                        ID_Sort = 5,
                        Bezeichnung = "PYE PV200 S5 beschiefert",
                        R = 0.0,
                        Dicke = 0.0050,
                        Wärmeleitfähigkeit = 0.000,
                        Fester_R = true,
                        Rohdichte = 1000,
                        Kapillar = true,
                        sonstiges = true,
                        KeineLuft = true,
                        Sd_Min = 100.00,
                        Sd_Max = 150.00,
                        Dampfdiffusionswiderstand_Min = 20000,
                        Dampfdiffusionswiderstand_Max = 30000
                    });
                    foreach (Basis i in BasisList)
                    {
                        i.ModelID = main_model.ID;

                    }
                    for (int i = 0; i < BasisList.Count; i++)
                     {
                         App.Database.SaveBauteilAsync(BasisList[i]);
                     }

                }
                Konstruktionstyp = "unbelüftetes Dach (Warmdach) \r\n(unverschattet mit dunkler Deckung/Abdichtung)";               
            }         

            //Beton EPS
            else if (main_model.MusterName == "Beton_KSD_EPS_EPDM")
            {
                if (main_model.Selected == 0)
                {

                    BasisList.Add(new Basis()
                    {
                        ID_Sort = 1,
                        Bezeichnung = "Beton armiert (1% Stahl)",
                        R = 0.078261,
                        Dicke = 0.18,
                        Wärmeleitfähigkeit = 2.3,
                        Rohdichte = 2300,
                        Kapillar = true,
                        sonstiges = true,
                        KeineLuft = true,
                        Dampfdiffusionswiderstand_Min = 80,
                        Dampfdiffusionswiderstand_Max = 130,
                        Sd_Min = 14.40,
                        Sd_Max = 23.40,
                    });
                    BasisList.Add(new Basis()
                    {
                        ID_Sort = 2,
                        Bezeichnung = "KSD",
                        R = 0.008824,
                        Dicke = 0.0015,
                        Wärmeleitfähigkeit = 0.17,
                        Rohdichte = 1000.0,
                        Kapillar = true,
                        sonstiges = true,
                        KeineLuft = true,
                        Sd_Min = 1500.00,
                        Sd_Max = 1500.00,
                        Fester_sd = true,
                        Dampfdiffusionswiderstand_Min = 1000000.00,
                        Dampfdiffusionswiderstand_Max = 1000000.00
                    });
                    BasisList.Add(new Basis()
                    {
                        ID_Sort = 3,
                        Bezeichnung = "EPS 035, mit Stufenfalz",
                        R = 5.142857 ,
                        Dicke = 0.18,
                        Wärmeleitfähigkeit = 0.03500,
                        Rohdichte = 30.0,
                        Kapillar = true,
                        sonstiges = true,
                        KeineLuft = true,
                        Sd_Min = 3.6,
                        Sd_Max = 18.00,
                        Dampfdiffusionswiderstand_Min = 20.00,
                        Dampfdiffusionswiderstand_Max = 100.00
                    });
                    BasisList.Add(new Basis()
                    {
                        ID_Sort = 4,
                        Bezeichnung = "EPDM Dachbahn",
                        R = 0.00,
                        Dicke = 0.0013,
                        Wärmeleitfähigkeit = 0.00,
                        Rohdichte = 1150.0,
                        Kapillar = true,
                        sonstiges = true,
                        KeineLuft = true,
                        Fester_R = true,
                        Sd_Min = 78.00,
                        Sd_Max = 78.00,
                        Dampfdiffusionswiderstand_Min = 60000,
                        Dampfdiffusionswiderstand_Max = 60000
                    });
                    foreach (Basis i in BasisList)
                    {
                        i.ModelID = main_model.ID;
                    }
                    for (int i = 0; i < BasisList.Count; i++)
                    {
                        App.Database.SaveBauteilAsync(BasisList[i]);
                    }
                }
                Konstruktionstyp = "unbelüftetes Dach (Warmdach) \r\n(unverschattet mit dunkler Deckung/Abdichtung)";

            }
            else if (main_model.MusterName == "Beton_KSD_EPS_Evalon")
            {
                if (main_model.Selected == 0)
                {

                    BasisList.Add(new Basis()
                    {
                        ID_Sort = 1,
                        Bezeichnung = "Beton armiert (1% Stahl)",
                        R = 0.078261,
                        Dicke = 0.18,
                        Wärmeleitfähigkeit = 2.3,
                        Rohdichte = 2300,
                        Kapillar = true,
                        sonstiges = true,
                        KeineLuft = true,
                        Dampfdiffusionswiderstand_Min = 80,
                        Dampfdiffusionswiderstand_Max = 130,
                        Sd_Min = 14.40,
                        Sd_Max = 23.40,
                    });
                    BasisList.Add(new Basis()
                    {
                        ID_Sort = 2,
                        Bezeichnung = "KSD",
                        R = 0.008824,
                        Dicke = 0.0015,
                        Wärmeleitfähigkeit = 0.17,
                        Rohdichte = 1000.0,
                        Kapillar = true,
                        sonstiges = true,
                        KeineLuft = true,
                        Sd_Min = 1500.00,
                        Sd_Max = 1500.00,
                        Fester_sd = true,
                        Dampfdiffusionswiderstand_Min = 1000000.00,
                        Dampfdiffusionswiderstand_Max = 1000000.00
                    });
                    BasisList.Add(new Basis()
                    {
                        ID_Sort = 3,
                        Bezeichnung = "EPS 035, mit Stufenfalz",
                        R = 5.142857,
                        Dicke = 0.18,
                        Wärmeleitfähigkeit = 0.03500,
                        Rohdichte = 30.0,
                        Kapillar = true,
                        sonstiges = true,
                        KeineLuft = true,
                        Sd_Min = 3.6,
                        Sd_Max = 18.00,
                        Dampfdiffusionswiderstand_Min = 20.00,
                        Dampfdiffusionswiderstand_Max = 100.00
                    });
                    BasisList.Add(new Basis()
                    {
                        ID_Sort = 4,
                        Bezeichnung = "EVALON V",
                        R = 0.00,
                        Dicke = 0.0012,
                        Wärmeleitfähigkeit = 0.00,
                        Rohdichte = 1250.0,
                        Kapillar = true,
                        sonstiges = true,
                        KeineLuft = true,
                        Fester_R = true,
                        Sd_Min = 24.00,
                        Sd_Max = 24.00,
                        Dampfdiffusionswiderstand_Min = 20000,
                        Dampfdiffusionswiderstand_Max = 20000
                    });
                    foreach (Basis i in BasisList)
                    {
                        i.ModelID = main_model.ID;
                    }
                    for (int i = 0; i < BasisList.Count; i++)
                    {
                        App.Database.SaveBauteilAsync(BasisList[i]);
                    }
                }
                Konstruktionstyp = "unbelüftetes Dach (Warmdach) \r\n(unverschattet mit dunkler Deckung/Abdichtung)";

            }
            else if (main_model.MusterName == "Beton_KSD_EPS_FPO")
            {
                if (main_model.Selected == 0)
                {

                    BasisList.Add(new Basis()
                    {
                        ID_Sort = 1,
                        Bezeichnung = "Beton armiert (1% Stahl)",
                        R = 0.078261,
                        Dicke = 0.18,
                        Wärmeleitfähigkeit = 2.3,
                        Rohdichte = 2300,
                        Kapillar = true,
                        sonstiges = true,
                        KeineLuft = true,
                        Dampfdiffusionswiderstand_Min = 80,
                        Dampfdiffusionswiderstand_Max = 130,
                        Sd_Min = 14.40,
                        Sd_Max = 23.40,
                    });
                    BasisList.Add(new Basis()
                    {
                        ID_Sort = 2,
                        Bezeichnung = "KSD",
                        R = 0.008824,
                        Dicke = 0.0015,
                        Wärmeleitfähigkeit = 0.17,
                        Rohdichte = 1000.0,
                        Kapillar = true,
                        sonstiges = true,
                        KeineLuft = true,
                        Sd_Min = 1500.00,
                        Sd_Max = 1500.00,
                        Fester_sd = true,
                        Dampfdiffusionswiderstand_Min = 1000000.00,
                        Dampfdiffusionswiderstand_Max = 1000000.00
                    });
                    BasisList.Add(new Basis()
                    {
                        ID_Sort = 3,
                        Bezeichnung = "EPS 035, mit Stufenfalz",
                        R = 5.142857,
                        Dicke = 0.18,
                        Wärmeleitfähigkeit = 0.03500,
                        Rohdichte = 30.0,
                        Kapillar = true,
                        sonstiges = true,
                        KeineLuft = true,
                        Sd_Min = 3.6,
                        Sd_Max = 18.00,
                        Dampfdiffusionswiderstand_Min = 20.00,
                        Dampfdiffusionswiderstand_Max = 100.00
                    });
                    BasisList.Add(new Basis()
                    {
                        ID_Sort = 4,
                        Bezeichnung = "Glasvlies",
                        R = 0.000100,
                        Dicke = 0.001,
                        Wärmeleitfähigkeit = 10.000000,
                        Rohdichte = 400.0,
                        sonstiges = true,
                        KeineLuft = true,
                        Sd_Min = 0.000,
                        Sd_Max = 0.00,
                        Dampfdiffusionswiderstand_Min = 1.0,
                        Dampfdiffusionswiderstand_Max = 1.0
                    });
                    BasisList.Add(new Basis()
                    {
                        ID_Sort = 5,
                        Bezeichnung = "FPO Dachbahn",
                        R = 0.00000,
                        Dicke = 0.0018,
                        Wärmeleitfähigkeit = 0.000000,
                        Rohdichte = 1100.0,
                        sonstiges = true,
                        Kapillar = true,
                        KeineLuft = true,
                        Fester_R = true,
                        Fester_sd = true,
                        Sd_Min = 180.000,
                        Sd_Max = 180.00,
                        Dampfdiffusionswiderstand_Min = 100000.00,
                        Dampfdiffusionswiderstand_Max = 100000.00
                    });
                    foreach (Basis i in BasisList)
                    {
                        i.ModelID = main_model.ID;
                    }
                    for (int i = 0; i < BasisList.Count; i++)
                    {
                        App.Database.SaveBauteilAsync(BasisList[i]);
                    }
                }
                Konstruktionstyp = "unbelüftetes Dach (Warmdach) \r\n(unverschattet mit dunkler Deckung/Abdichtung)";

            }
            else if (main_model.MusterName == "Beton_KSD_EPS_PVC")
            {
                if (main_model.Selected == 0)
                {

                    BasisList.Add(new Basis()
                    {
                        ID_Sort = 1,
                        Bezeichnung = "Beton armiert (1% Stahl)",
                        R = 0.078261,
                        Dicke = 0.18,
                        Wärmeleitfähigkeit = 2.3,
                        Rohdichte = 2300,
                        Kapillar = true,
                        sonstiges = true,
                        KeineLuft = true,
                        Dampfdiffusionswiderstand_Min = 80,
                        Dampfdiffusionswiderstand_Max = 130,
                        Sd_Min = 14.40,
                        Sd_Max = 23.40,
                    });
                    BasisList.Add(new Basis()
                    {
                        ID_Sort = 2,
                        Bezeichnung = "KSD",
                        R = 0.008824,
                        Dicke = 0.0015,
                        Wärmeleitfähigkeit = 0.17,
                        Rohdichte = 1000.0,
                        Kapillar = true,
                        sonstiges = true,
                        KeineLuft = true,
                        Sd_Min = 1500.00,
                        Sd_Max = 1500.00,
                        Fester_sd = true,
                        Dampfdiffusionswiderstand_Min = 1000000.00,
                        Dampfdiffusionswiderstand_Max = 1000000.00
                    });
                    BasisList.Add(new Basis()
                    {
                        ID_Sort = 3,
                        Bezeichnung = "EPS 035, mit Stufenfalz",
                        R = 5.142857,
                        Dicke = 0.18,
                        Wärmeleitfähigkeit = 0.03500,
                        Rohdichte = 30.0,
                        Kapillar = true,
                        sonstiges = true,
                        KeineLuft = true,
                        Sd_Min = 3.6,
                        Sd_Max = 18.00,
                        Dampfdiffusionswiderstand_Min = 20.00,
                        Dampfdiffusionswiderstand_Max = 100.00
                    });
                    BasisList.Add(new Basis()
                    {
                        ID_Sort = 4,
                        Bezeichnung = "Glasvlies",
                        R = 0.000100,
                        Dicke = 0.001,
                        Wärmeleitfähigkeit = 10.000000,
                        Rohdichte = 400.0,
                        sonstiges = true,
                        KeineLuft = true,
                        Sd_Min = 0.000,
                        Sd_Max = 0.00,
                        Dampfdiffusionswiderstand_Min = 1.0,
                        Dampfdiffusionswiderstand_Max = 1.0
                    });
                    BasisList.Add(new Basis()
                    {
                        ID_Sort = 5,
                        Bezeichnung = "PVC-P (DIN 16730)",
                        R = 0.00000,
                        Dicke = 0.0015,
                        Wärmeleitfähigkeit = 0.000000,
                        Rohdichte = 1200.0,
                        sonstiges = true,
                        Kapillar = true,
                        KeineLuft = true,
                        Fester_R = true,
                        Sd_Min = 15.000,
                        Sd_Max = 45.00,
                        Dampfdiffusionswiderstand_Min = 10000.00,
                        Dampfdiffusionswiderstand_Max = 30000.00
                    });
                    foreach (Basis i in BasisList)
                    {
                        i.ModelID = main_model.ID;
                    }
                    for (int i = 0; i < BasisList.Count; i++)
                    {
                        App.Database.SaveBauteilAsync(BasisList[i]);
                    }
                }
                Konstruktionstyp = "unbelüftetes Dach (Warmdach) \r\n(unverschattet mit dunkler Deckung/Abdichtung)";

            }
            else if (main_model.MusterName == "Beton_KSD_EPS_PYE_einlagig")
            {
                if (main_model.Selected == 0)
                {

                    BasisList.Add(new Basis()
                    {
                        ID_Sort = 1,
                        Bezeichnung = "Beton armiert (1% Stahl)",
                        R = 0.078261,
                        Dicke = 0.18,
                        Wärmeleitfähigkeit = 2.3,
                        Rohdichte = 2300,
                        Kapillar = true,
                        sonstiges = true,
                        KeineLuft = true,
                        Dampfdiffusionswiderstand_Min = 80,
                        Dampfdiffusionswiderstand_Max = 130,
                        Sd_Min = 14.40,
                        Sd_Max = 23.40,
                    });
                    BasisList.Add(new Basis()
                    {
                        ID_Sort = 2,
                        Bezeichnung = "KSD",
                        R = 0.008824,
                        Dicke = 0.0015,
                        Wärmeleitfähigkeit = 0.17,
                        Rohdichte = 1000.0,
                        Kapillar = true,
                        sonstiges = true,
                        KeineLuft = true,
                        Sd_Min = 1500.00,
                        Sd_Max = 1500.00,
                        Fester_sd = true,
                        Dampfdiffusionswiderstand_Min = 1000000.00,
                        Dampfdiffusionswiderstand_Max = 1000000.00
                    });
                    BasisList.Add(new Basis()
                    {
                        ID_Sort = 3,
                        Bezeichnung = "EPS 035, mit Stufenfalz",
                        R =  5.142857,
                        Dicke = 0.18,
                        Wärmeleitfähigkeit = 0.03500,
                        Rohdichte = 30.0,
                        Kapillar = true,
                        sonstiges = true,
                        KeineLuft = true,
                        Sd_Min = 3.6,
                        Sd_Max = 18.00,
                        Dampfdiffusionswiderstand_Min = 20.00,
                        Dampfdiffusionswiderstand_Max = 100.00
                    });
                    BasisList.Add(new Basis()
                    {
                        ID_Sort = 4,
                        Bezeichnung = "Glasvlies",
                        R = 0.000100,
                        Dicke = 0.001,
                        Wärmeleitfähigkeit = 10.000000,                        
                        Rohdichte = 400.0,
                        sonstiges = true,
                        KeineLuft = true,
                        Sd_Min = 0.000,
                        Sd_Max = 0.00,
                        Dampfdiffusionswiderstand_Min = 1.0,
                        Dampfdiffusionswiderstand_Max = 1.0
                    });
                    BasisList.Add(new Basis()
                    {
                        ID_Sort = 5,
                        Bezeichnung = "PYE PV200 S5 beschiefert",
                        R = 0.00,
                        Dicke = 0.005,
                        Wärmeleitfähigkeit = 0.00,
                        Rohdichte = 1000.0,
                        Kapillar = true,
                        sonstiges = true,
                        KeineLuft = true,
                        Fester_R = true,
                        Sd_Min = 100.000,
                        Sd_Max = 150.00,
                        Dampfdiffusionswiderstand_Min = 20000,
                        Dampfdiffusionswiderstand_Max = 30000
                    });
                    foreach (Basis i in BasisList)
                    {
                        i.ModelID = main_model.ID;
                    }
                    for (int i = 0; i < BasisList.Count; i++)
                    {
                        App.Database.SaveBauteilAsync(BasisList[i]);
                    }
                }
                Konstruktionstyp = "unbelüftetes Dach (Warmdach) \r\n(unverschattet mit dunkler Deckung/Abdichtung)";

            }
            else if (main_model.MusterName == "Beton_KSD_EPS_PYE_zweilagig")
            {
                if (main_model.Selected == 0)
                {

                    BasisList.Add(new Basis()
                    {
                        ID_Sort = 1,
                        Bezeichnung = "Beton armiert (1% Stahl)",
                        R = 0.078261,
                        Dicke = 0.18,
                        Wärmeleitfähigkeit = 2.3,
                        Rohdichte = 2300,
                        Kapillar = true,
                        sonstiges = true,
                        KeineLuft = true,
                        Dampfdiffusionswiderstand_Min = 80,
                        Dampfdiffusionswiderstand_Max = 130,
                        Sd_Min = 14.40,
                        Sd_Max = 23.40,
                    });
                    BasisList.Add(new Basis()
                    {
                        ID_Sort = 2,
                        Bezeichnung = "KSD",
                        R = 0.008824,
                        Dicke = 0.0015,
                        Wärmeleitfähigkeit = 0.17,
                        Rohdichte = 1000.0,
                        Kapillar = true,
                        sonstiges = true,
                        KeineLuft = true,
                        Sd_Min = 1500.00,
                        Sd_Max = 1500.00,
                        Fester_sd = true,
                        Dampfdiffusionswiderstand_Min = 1000000.00,
                        Dampfdiffusionswiderstand_Max = 1000000.00
                    });
                    BasisList.Add(new Basis()
                    {
                        ID_Sort = 3,
                        Bezeichnung = "EPS 035, mit Stufenfalz",
                        R = 5.142857,
                        Dicke = 0.18,
                        Wärmeleitfähigkeit = 0.03500,
                        Rohdichte = 30.0,
                        Kapillar = true,
                        sonstiges = true,
                        KeineLuft = true,
                        Sd_Min = 3.6,
                        Sd_Max = 18.00,
                        Dampfdiffusionswiderstand_Min = 20.00,
                        Dampfdiffusionswiderstand_Max = 100.00
                    });
                    BasisList.Add(new Basis()
                    {
                        ID_Sort = 4,
                        Bezeichnung = "PYE G200 S4",
                        R = 0.00,
                        Dicke = 0.004,
                        Wärmeleitfähigkeit = 0.00,                        
                        Rohdichte = 1000.0,
                        Kapillar = true,
                        sonstiges = true,
                        KeineLuft = true,
                        Fester_R = true,
                        Sd_Min = 80.000,
                        Sd_Max = 120.00,
                        Dampfdiffusionswiderstand_Min = 20000,
                        Dampfdiffusionswiderstand_Max = 30000
                    });
                    BasisList.Add(new Basis()
                    {
                        ID_Sort = 5,
                        Bezeichnung = "PYE PV200 S5 beschiefert",
                        R = 0.00,
                        Dicke = 0.005,
                        Wärmeleitfähigkeit = 0.00,
                        Rohdichte = 1000.0,
                        Kapillar = true,
                        sonstiges = true,
                        KeineLuft = true,
                        Fester_R = true,
                        Sd_Min = 100.000,
                        Sd_Max = 150.00,
                        Dampfdiffusionswiderstand_Min = 20000,
                        Dampfdiffusionswiderstand_Max = 30000
                    });
                    foreach (Basis i in BasisList)
                    {
                        i.ModelID = main_model.ID;
                    }
                    for (int i = 0; i < BasisList.Count; i++)
                    {
                        App.Database.SaveBauteilAsync(BasisList[i]);
                    }
                }
                Konstruktionstyp = "unbelüftetes Dach (Warmdach) \r\n(unverschattet mit dunkler Deckung/Abdichtung)";

            }

            //Beton Mifa
            else if (main_model.MusterName == "Beton_KSD_MIFA_EPDM")
            {
                if (main_model.Selected == 0)
                {

                    BasisList.Add(new Basis()
                    {
                        ID_Sort = 1,
                        Bezeichnung = "Beton armiert (1% Stahl)",
                        R = 0.078261,
                        Dicke = 0.18,
                        Wärmeleitfähigkeit = 2.3,
                        Rohdichte = 2300,
                        Kapillar = true,
                        sonstiges = true,
                        KeineLuft = true,
                        Dampfdiffusionswiderstand_Min = 80,
                        Dampfdiffusionswiderstand_Max = 130,
                        Sd_Min = 14.40,
                        Sd_Max = 23.40,
                    });
                    BasisList.Add(new Basis()
                    {
                        ID_Sort = 2,
                        Bezeichnung = "KSD",
                        R = 0.008824,
                        Dicke = 0.0015,
                        Wärmeleitfähigkeit = 0.17,
                        Rohdichte = 1000.0,
                        Kapillar = true,
                        sonstiges = true,
                        KeineLuft = true,
                        Sd_Min = 1500.00,
                        Sd_Max = 1500.00,
                        Fester_sd = true,
                        Dampfdiffusionswiderstand_Min = 1000000.00,
                        Dampfdiffusionswiderstand_Max = 1000000.00
                    });
                    BasisList.Add(new Basis()
                    {
                        ID_Sort = 3,
                        Bezeichnung = "Mineralfaser 038",
                        R = 5.263158,
                        Dicke = 0.2,
                        Wärmeleitfähigkeit = 0.038,
                        Rohdichte = 120.0,
                        Kapillar = true,
                        sonstiges = true,
                        EvntlLuft = true,
                        Sd_Min =0.20,
                        Sd_Max = 0.20,
                        Dampfdiffusionswiderstand_Min = 1.00,
                        Dampfdiffusionswiderstand_Max = 1.00
                    });
                    BasisList.Add(new Basis()
                    {
                        ID_Sort = 4,
                        Bezeichnung = "EPDM",
                        R = 0.008,
                        Dicke = 0.002,
                        Wärmeleitfähigkeit = 0.25,
                        Rohdichte = 1150.0,
                        Kapillar = true,
                        sonstiges = true,
                        KeineLuft = true,
                        Sd_Min = 120.00,
                        Sd_Max = 120.00,
                        Dampfdiffusionswiderstand_Min = 60000,
                        Dampfdiffusionswiderstand_Max = 60000
                    });
                    foreach (Basis i in BasisList)
                    {
                        i.ModelID = main_model.ID;
                    }
                    for (int i = 0; i < BasisList.Count; i++)
                    {
                        App.Database.SaveBauteilAsync(BasisList[i]);
                    }
                }
                Konstruktionstyp = "unbelüftetes Dach (Warmdach) \r\n(unverschattet mit dunkler Deckung/Abdichtung)";

            }
            else if (main_model.MusterName == "Beton_KSD_MIFA_Evalon")
            {
                if (main_model.Selected == 0)
                {

                    BasisList.Add(new Basis()
                    {
                        ID_Sort = 1,
                        Bezeichnung = "Beton armiert (1% Stahl)",
                        R = 0.078261,
                        Dicke = 0.18,
                        Wärmeleitfähigkeit = 2.3,
                        Rohdichte = 2300,
                        Kapillar = true,
                        sonstiges = true,
                        KeineLuft = true,
                        Dampfdiffusionswiderstand_Min = 80,
                        Dampfdiffusionswiderstand_Max = 130,
                        Sd_Min = 14.40,
                        Sd_Max = 23.40,
                    });
                    BasisList.Add(new Basis()
                    {
                        ID_Sort = 2,
                        Bezeichnung = "KSD",
                        R = 0.008824,
                        Dicke = 0.0015,
                        Wärmeleitfähigkeit = 0.17,
                        Rohdichte = 1000.0,
                        Kapillar = true,
                        sonstiges = true,
                        KeineLuft = true,
                        Sd_Min = 1500.00,
                        Sd_Max = 1500.00,
                        Fester_sd = true,
                        Dampfdiffusionswiderstand_Min = 1000000.00,
                        Dampfdiffusionswiderstand_Max = 1000000.00
                    });
                    BasisList.Add(new Basis()
                    {
                        ID_Sort = 3,
                        Bezeichnung = "Mineralfaser 038",
                        R = 5.263158,
                        Dicke = 0.2,
                        Wärmeleitfähigkeit = 0.038,
                        Rohdichte = 120.0,
                        Kapillar = true,
                        sonstiges = true,
                        EvntlLuft = true,
                        Sd_Min = 0.20,
                        Sd_Max = 0.20,
                        Dampfdiffusionswiderstand_Min = 1.00,
                        Dampfdiffusionswiderstand_Max = 1.00
                    });
                    BasisList.Add(new Basis()
                    {
                        ID_Sort = 4,
                        Bezeichnung = "EVALON V",
                        R = 0.007059,
                        Dicke = 0.0012,
                        Wärmeleitfähigkeit = 0.17,
                        Rohdichte = 1250.0,
                        Kapillar = true,
                        sonstiges = true,
                        KeineLuft = true,
                        Fester_R = true,
                        Sd_Min = 24.00,
                        Sd_Max = 24.00,
                        Dampfdiffusionswiderstand_Min = 20000,
                        Dampfdiffusionswiderstand_Max = 20000
                    });
                    foreach (Basis i in BasisList)
                    {
                        i.ModelID = main_model.ID;
                    }
                    for (int i = 0; i < BasisList.Count; i++)
                    {
                        App.Database.SaveBauteilAsync(BasisList[i]);
                    }
                }
                Konstruktionstyp = "unbelüftetes Dach (Warmdach) \r\n(unverschattet mit dunkler Deckung/Abdichtung)";

            }
            else if (main_model.MusterName == "Beton_KSD_MIFA_FPO")
            {
                if (main_model.Selected == 0)
                {

                    BasisList.Add(new Basis()
                    {
                        ID_Sort = 1,
                        Bezeichnung = "Beton armiert (1% Stahl)",
                        R = 0.078261,
                        Dicke = 0.18,
                        Wärmeleitfähigkeit = 2.3,
                        Rohdichte = 2300,
                        Kapillar = true,
                        sonstiges = true,
                        KeineLuft = true,
                        Dampfdiffusionswiderstand_Min = 80,
                        Dampfdiffusionswiderstand_Max = 130,
                        Sd_Min = 14.40,
                        Sd_Max = 23.40,
                    });
                    BasisList.Add(new Basis()
                    {
                        ID_Sort = 2,
                        Bezeichnung = "KSD",
                        R = 0.008824,
                        Dicke = 0.0015,
                        Wärmeleitfähigkeit = 0.17,
                        Rohdichte = 1000.0,
                        Kapillar = true,
                        sonstiges = true,
                        KeineLuft = true,
                        Sd_Min = 1500.00,
                        Sd_Max = 1500.00,
                        Fester_sd = true,
                        Dampfdiffusionswiderstand_Min = 1000000.00,
                        Dampfdiffusionswiderstand_Max = 1000000.00
                    });
                    BasisList.Add(new Basis()
                    {
                        ID_Sort = 3,
                        Bezeichnung = "Mineralfaser 038",
                        R = 5.263158,
                        Dicke = 0.2,
                        Wärmeleitfähigkeit = 0.038,
                        Rohdichte = 120.0,
                        Kapillar = true,
                        sonstiges = true,
                        EvntlLuft = true,
                        Sd_Min = 0.20,
                        Sd_Max = 0.20,
                        Dampfdiffusionswiderstand_Min = 1.00,
                        Dampfdiffusionswiderstand_Max = 1.00
                    });
                    BasisList.Add(new Basis()
                    {
                        ID_Sort = 4,
                        Bezeichnung = "FPO",
                        R = 0.010588,
                        Dicke = 0.0018,
                        Wärmeleitfähigkeit = 0.17,
                        Rohdichte = 1100.0,
                        sonstiges = true,
                        Kapillar = true,
                        KeineLuft = true,
                        Fester_sd = true,
                        Sd_Min = 180.000,
                        Sd_Max = 180.00,
                        Dampfdiffusionswiderstand_Min = 100000.00,
                        Dampfdiffusionswiderstand_Max = 100000.00
                    });
                    foreach (Basis i in BasisList)
                    {
                        i.ModelID = main_model.ID;
                    }
                    for (int i = 0; i < BasisList.Count; i++)
                    {
                        App.Database.SaveBauteilAsync(BasisList[i]);
                    }
                }
                Konstruktionstyp = "unbelüftetes Dach (Warmdach) \r\n(unverschattet mit dunkler Deckung/Abdichtung)";

            }
            else if (main_model.MusterName == "Beton_KSD_MIFA_PVC")
            {
                if (main_model.Selected == 0)
                {

                    BasisList.Add(new Basis()
                    {
                        ID_Sort = 1,
                        Bezeichnung = "Beton armiert (1% Stahl)",
                        R = 0.078261,
                        Dicke = 0.18,
                        Wärmeleitfähigkeit = 2.3,
                        Rohdichte = 2300,
                        Kapillar = true,
                        sonstiges = true,
                        KeineLuft = true,
                        Dampfdiffusionswiderstand_Min = 80,
                        Dampfdiffusionswiderstand_Max = 130,
                        Sd_Min = 14.40,
                        Sd_Max = 23.40,
                    });
                    BasisList.Add(new Basis()
                    {
                        ID_Sort = 2,
                        Bezeichnung = "KSD",
                        R = 0.008824,
                        Dicke = 0.0015,
                        Wärmeleitfähigkeit = 0.17,
                        Rohdichte = 1000.0,
                        Kapillar = true,
                        sonstiges = true,
                        KeineLuft = true,
                        Sd_Min = 1500.00,
                        Sd_Max = 1500.00,
                        Fester_sd = true,
                        Dampfdiffusionswiderstand_Min = 1000000.00,
                        Dampfdiffusionswiderstand_Max = 1000000.00
                    });
                    BasisList.Add(new Basis()
                    {
                        ID_Sort = 3,
                        Bezeichnung = "Mineralfaser 038",
                        R = 5.263158,
                        Dicke = 0.2,
                        Wärmeleitfähigkeit = 0.038,
                        Rohdichte = 120.0,
                        Kapillar = true,
                        sonstiges = true,
                        EvntlLuft = true,
                        Sd_Min = 0.20,
                        Sd_Max = 0.20,
                        Dampfdiffusionswiderstand_Min = 1.00,
                        Dampfdiffusionswiderstand_Max = 1.00
                    });                   
                    BasisList.Add(new Basis()
                    {
                        ID_Sort = 4,
                        Bezeichnung = "PVC-P (DIN 16730)",
                        R = 0.008824,
                        Dicke = 0.0015,
                        Wärmeleitfähigkeit = 0.17,
                        Rohdichte = 1200.0,
                        sonstiges = true,
                        Kapillar = true,
                        KeineLuft = true,
                        Sd_Min = 15.000,
                        Sd_Max = 45.00,
                        Dampfdiffusionswiderstand_Min = 10000.00,
                        Dampfdiffusionswiderstand_Max = 30000.00
                    });
                    foreach (Basis i in BasisList)
                    {
                        i.ModelID = main_model.ID;
                    }
                    for (int i = 0; i < BasisList.Count; i++)
                    {
                        App.Database.SaveBauteilAsync(BasisList[i]);
                    }
                }
                Konstruktionstyp = "unbelüftetes Dach (Warmdach) \r\n(unverschattet mit dunkler Deckung/Abdichtung)";

            }
            else if (main_model.MusterName == "Beton_KSD_MIFA_PYE_einlagig")
            {
                if (main_model.Selected == 0)
                {

                    BasisList.Add(new Basis()
                    {
                        ID_Sort = 1,
                        Bezeichnung = "Beton armiert (1% Stahl)",
                        R = 0.078261,
                        Dicke = 0.18,
                        Wärmeleitfähigkeit = 2.3,
                        Rohdichte = 2300,
                        Kapillar = true,
                        sonstiges = true,
                        KeineLuft = true,
                        Dampfdiffusionswiderstand_Min = 80,
                        Dampfdiffusionswiderstand_Max = 130,
                        Sd_Min = 14.40,
                        Sd_Max = 23.40,
                    });
                    BasisList.Add(new Basis()
                    {
                        ID_Sort = 2,
                        Bezeichnung = "KSD",
                        R = 0.008824,
                        Dicke = 0.0015,
                        Wärmeleitfähigkeit = 0.17,
                        Rohdichte = 1000.0,
                        Kapillar = true,
                        sonstiges = true,
                        KeineLuft = true,
                        Sd_Min = 1500.00,
                        Sd_Max = 1500.00,
                        Fester_sd = true,
                        Dampfdiffusionswiderstand_Min = 1000000.00,
                        Dampfdiffusionswiderstand_Max = 1000000.00
                    });
                    BasisList.Add(new Basis()
                    {
                        ID_Sort = 3,
                        Bezeichnung = "Mineralfaser 038",
                        R = 5.263158,
                        Dicke = 0.2,
                        Wärmeleitfähigkeit = 0.038,
                        Rohdichte = 120.0,
                        Kapillar = true,
                        sonstiges = true,
                        EvntlLuft = true,
                        Sd_Min = 0.20,
                        Sd_Max = 0.20,
                        Dampfdiffusionswiderstand_Min = 1.00,
                        Dampfdiffusionswiderstand_Max = 1.00
                    });
                    BasisList.Add(new Basis()
                    {
                        ID_Sort = 4,
                        Bezeichnung = "Glasvlies",
                        R = 0.000100,
                        Dicke = 0.001,
                        Wärmeleitfähigkeit = 10.000000,
                        Rohdichte = 400.0,
                        sonstiges = true,
                        KeineLuft = true,
                        Sd_Min = 0.000,
                        Sd_Max = 0.00,
                        Dampfdiffusionswiderstand_Min = 1.0,
                        Dampfdiffusionswiderstand_Max = 1.0
                    });
                    BasisList.Add(new Basis()
                    {
                        ID_Sort = 5,
                        Bezeichnung = "PYE PV200 S5 beschiefert",
                        R = 0.029412,
                        Dicke = 0.005,
                        Wärmeleitfähigkeit = 0.17,
                        Rohdichte = 1000.0,
                        Kapillar = true,
                        sonstiges = true,
                        KeineLuft = true,
                        Fester_R = true,
                        Sd_Min = 100.000,
                        Sd_Max = 150.00,
                        Dampfdiffusionswiderstand_Min = 20000,
                        Dampfdiffusionswiderstand_Max = 30000
                    });
                    foreach (Basis i in BasisList)
                    {
                        i.ModelID = main_model.ID;
                    }
                    for (int i = 0; i < BasisList.Count; i++)
                    {
                        App.Database.SaveBauteilAsync(BasisList[i]);
                    }
                }
                Konstruktionstyp = "unbelüftetes Dach (Warmdach) \r\n(unverschattet mit dunkler Deckung/Abdichtung)";

            }
            else if (main_model.MusterName == "Beton_KSD_MIFA_PYE_zweilagig")
            {
                if (main_model.Selected == 0)
                {

                    BasisList.Add(new Basis()
                    {
                        ID_Sort = 1,
                        Bezeichnung = "Beton armiert (1% Stahl)",
                        R = 0.078261,
                        Dicke = 0.18,
                        Wärmeleitfähigkeit = 2.3,
                        Rohdichte = 2300,
                        Kapillar = true,
                        sonstiges = true,
                        KeineLuft = true,
                        Dampfdiffusionswiderstand_Min = 80,
                        Dampfdiffusionswiderstand_Max = 130,
                        Sd_Min = 14.40,
                        Sd_Max = 23.40,
                    });
                    BasisList.Add(new Basis()
                    {
                        ID_Sort = 2,
                        Bezeichnung = "KSD",
                        R = 0.008824,
                        Dicke = 0.0015,
                        Wärmeleitfähigkeit = 0.17,
                        Rohdichte = 1000.0,
                        Kapillar = true,
                        sonstiges = true,
                        KeineLuft = true,
                        Sd_Min = 1500.00,
                        Sd_Max = 1500.00,
                        Fester_sd = true,
                        Dampfdiffusionswiderstand_Min = 1000000.00,
                        Dampfdiffusionswiderstand_Max = 1000000.00
                    });
                    BasisList.Add(new Basis()
                    {
                        ID_Sort = 3,
                        Bezeichnung = "Mineralfaser 038",
                        R = 5.263158,
                        Dicke = 0.2,
                        Wärmeleitfähigkeit = 0.038,
                        Rohdichte = 120.0,
                        Kapillar = true,
                        sonstiges = true,
                        EvntlLuft = true,
                        Sd_Min = 0.20,
                        Sd_Max = 0.20,
                        Dampfdiffusionswiderstand_Min = 1.00,
                        Dampfdiffusionswiderstand_Max = 1.00
                    });
                    BasisList.Add(new Basis()
                    {
                        ID_Sort = 4,
                        Bezeichnung = "PYE G200 S4",
                        R = 0.00,
                        Dicke = 0.004,
                        Wärmeleitfähigkeit = 0.00,
                        Rohdichte = 1000.0,
                        Kapillar = true,
                        sonstiges = true,
                        KeineLuft = true,
                        Fester_R = true,
                        Sd_Min = 80.000,
                        Sd_Max = 120.00,
                        Dampfdiffusionswiderstand_Min = 20000,
                        Dampfdiffusionswiderstand_Max = 30000
                    });
                    BasisList.Add(new Basis()
                    {
                        ID_Sort = 5,
                        Bezeichnung = "PYE PV200 S5 beschiefert",
                        R = 0.00,
                        Dicke = 0.005,
                        Wärmeleitfähigkeit = 0.00,
                        Rohdichte = 1000.0,
                        Kapillar = true,
                        sonstiges = true,
                        KeineLuft = true,
                        Fester_R = true,
                        Sd_Min = 100.000,
                        Sd_Max = 150.00,
                        Dampfdiffusionswiderstand_Min = 20000,
                        Dampfdiffusionswiderstand_Max = 30000
                    });
                    foreach (Basis i in BasisList)
                    {
                        i.ModelID = main_model.ID;
                    }
                    for (int i = 0; i < BasisList.Count; i++)
                    {
                        App.Database.SaveBauteilAsync(BasisList[i]);
                    }
                }
                Konstruktionstyp = "unbelüftetes Dach (Warmdach) \r\n(unverschattet mit dunkler Deckung/Abdichtung)";

            }

            //Beton Pur
            else if (main_model.MusterName == "Beton_KSD_PUR_EPDM")
            {
                if (main_model.Selected == 0)
                {

                    BasisList.Add(new Basis()
                    {
                        ID_Sort = 1,
                        Bezeichnung = "Beton armiert (1% Stahl)",
                        R = 0.078261,
                        Dicke = 0.18,
                        Wärmeleitfähigkeit = 2.3,
                        Rohdichte = 2300,
                        Kapillar = true,
                        sonstiges = true,
                        KeineLuft = true,
                        Dampfdiffusionswiderstand_Min = 80,
                        Dampfdiffusionswiderstand_Max = 130,
                        Sd_Min = 14.40,
                        Sd_Max = 23.40,
                    });
                    BasisList.Add(new Basis()
                    {
                        ID_Sort = 2,
                        Bezeichnung = "KSD",
                        R = 0.008824,
                        Dicke = 0.0015,
                        Wärmeleitfähigkeit = 0.17,
                        Rohdichte = 1000.0,
                        Kapillar = true,
                        sonstiges = true,
                        KeineLuft = true,
                        Sd_Min = 1500.00,
                        Sd_Max = 1500.00,
                        Fester_sd = true,
                        Dampfdiffusionswiderstand_Min = 1000000.00,
                        Dampfdiffusionswiderstand_Max = 1000000.00
                    });
                    BasisList.Add(new Basis()
                    {
                        ID_Sort = 3,
                        Bezeichnung = "PUR 023",
                        R = 5.217391,
                        Fester_R = true,
                        Dicke = 0.12,
                        Wärmeleitfähigkeit = 0.023,
                        Rohdichte = 30.0,
                        Kapillar = true,
                        sonstiges = true,
                        EvntlLuft = true,
                        Sd_Min = 4.80,
                        Sd_Max = 24.00,
                        Dampfdiffusionswiderstand_Min = 40.00,
                        Dampfdiffusionswiderstand_Max = 200.00
                    });
                    BasisList.Add(new Basis()
                    {
                        ID_Sort = 4,
                        Bezeichnung = "EPDM Dachbahn",
                        R = 0.000,
                        Fester_R = true,
                        Dicke = 0.0013,
                        Wärmeleitfähigkeit = 0.000,
                        Rohdichte = 1150.0,
                        Kapillar = true,
                        sonstiges = true,
                        KeineLuft = true,
                        Sd_Min = 78.00,
                        Sd_Max = 78.00,
                        Dampfdiffusionswiderstand_Min = 60000,
                        Dampfdiffusionswiderstand_Max = 60000
                    });
                    foreach (Basis i in BasisList)
                    {
                        i.ModelID = main_model.ID;
                    }
                    for (int i = 0; i < BasisList.Count; i++)
                    {
                        App.Database.SaveBauteilAsync(BasisList[i]);
                    }
                }
                Konstruktionstyp = "unbelüftetes Dach (Warmdach) \r\n(unverschattet mit dunkler Deckung/Abdichtung)";

            }
            else if (main_model.MusterName == "Beton_KSD_PUR_Evalon")
            {
                if (main_model.Selected == 0)
                {

                    BasisList.Add(new Basis()
                    {
                        ID_Sort = 1,
                        Bezeichnung = "Beton armiert (1% Stahl)",
                        R = 0.078261,
                        Dicke = 0.18,
                        Wärmeleitfähigkeit = 2.3,
                        Rohdichte = 2300,
                        Kapillar = true,
                        sonstiges = true,
                        KeineLuft = true,
                        Dampfdiffusionswiderstand_Min = 80,
                        Dampfdiffusionswiderstand_Max = 130,
                        Sd_Min = 14.40,
                        Sd_Max = 23.40,
                    });
                    BasisList.Add(new Basis()
                    {
                        ID_Sort = 2,
                        Bezeichnung = "KSD",
                        R = 0.008824,
                        Dicke = 0.0015,
                        Wärmeleitfähigkeit = 0.17,
                        Rohdichte = 1000.0,
                        Kapillar = true,
                        sonstiges = true,
                        KeineLuft = true,
                        Sd_Min = 1500.00,
                        Sd_Max = 1500.00,
                        Fester_sd = true,
                        Dampfdiffusionswiderstand_Min = 1000000.00,
                        Dampfdiffusionswiderstand_Max = 1000000.00
                    });
                    BasisList.Add(new Basis()
                    {
                        ID_Sort = 3,
                        Bezeichnung = "PUR 023",
                        R = 5.217391,
                        Fester_R = true,
                        Dicke = 0.12,
                        Wärmeleitfähigkeit = 0.023,
                        Rohdichte = 30.0,
                        Kapillar = true,
                        sonstiges = true,
                        EvntlLuft = true,
                        Sd_Min = 4.80,
                        Sd_Max = 24.00,
                        Dampfdiffusionswiderstand_Min = 40.00,
                        Dampfdiffusionswiderstand_Max = 200.00
                    });
                    BasisList.Add(new Basis()
                    {
                        ID_Sort = 4,
                        Bezeichnung = "EVALON V",
                        R = 0.00,
                        Dicke = 0.0012,
                        Wärmeleitfähigkeit = 0.00,
                        Rohdichte = 1250.0,
                        Kapillar = true,
                        sonstiges = true,
                        KeineLuft = true,
                        Fester_R = true,
                        Sd_Min = 24.00,
                        Sd_Max = 24.00,
                        Dampfdiffusionswiderstand_Min = 20000,
                        Dampfdiffusionswiderstand_Max = 20000
                    });
                    foreach (Basis i in BasisList)
                    {
                        i.ModelID = main_model.ID;
                    }
                    for (int i = 0; i < BasisList.Count; i++)
                    {
                        App.Database.SaveBauteilAsync(BasisList[i]);
                    }
                }
                Konstruktionstyp = "unbelüftetes Dach (Warmdach) \r\n(unverschattet mit dunkler Deckung/Abdichtung)";

            }
            else if (main_model.MusterName == "Beton_KSD_PUR_FPO")
            {
                if (main_model.Selected == 0)
                {

                    BasisList.Add(new Basis()
                    {
                        ID_Sort = 1,
                        Bezeichnung = "Beton armiert (1% Stahl)",
                        R = 0.078261,
                        Dicke = 0.18,
                        Wärmeleitfähigkeit = 2.3,
                        Rohdichte = 2300,
                        Kapillar = true,
                        sonstiges = true,
                        KeineLuft = true,
                        Dampfdiffusionswiderstand_Min = 80,
                        Dampfdiffusionswiderstand_Max = 130,
                        Sd_Min = 14.40,
                        Sd_Max = 23.40,
                    });
                    BasisList.Add(new Basis()
                    {
                        ID_Sort = 2,
                        Bezeichnung = "KSD",
                        R = 0.008824,
                        Dicke = 0.0015,
                        Wärmeleitfähigkeit = 0.17,
                        Rohdichte = 1000.0,
                        Kapillar = true,
                        sonstiges = true,
                        KeineLuft = true,
                        Sd_Min = 1500.00,
                        Sd_Max = 1500.00,
                        Fester_sd = true,
                        Dampfdiffusionswiderstand_Min = 1000000.00,
                        Dampfdiffusionswiderstand_Max = 1000000.00
                    });
                    BasisList.Add(new Basis()
                    {
                        ID_Sort = 3,
                        Bezeichnung = "PUR 023",
                        R = 5.217391,
                        Fester_R = true,
                        Dicke = 0.12,
                        Wärmeleitfähigkeit = 0.023,
                        Rohdichte = 30.0,
                        Kapillar = true,
                        sonstiges = true,
                        EvntlLuft = true,
                        Sd_Min = 4.80,
                        Sd_Max = 24.00,
                        Dampfdiffusionswiderstand_Min = 40.00,
                        Dampfdiffusionswiderstand_Max = 200.00
                    });
                    BasisList.Add(new Basis()
                    {
                        ID_Sort = 4,
                        Bezeichnung = "FPO",
                        R = 0.010588,
                        Dicke = 0.0018,
                        Wärmeleitfähigkeit = 0.17,
                        Rohdichte = 1100.0,
                        sonstiges = true,
                        Kapillar = true,
                        KeineLuft = true,
                        Fester_sd = true,
                        Sd_Min = 180.000,
                        Sd_Max = 180.00,
                        Dampfdiffusionswiderstand_Min = 100000.00,
                        Dampfdiffusionswiderstand_Max = 100000.00
                    });
                    foreach (Basis i in BasisList)
                    {
                        i.ModelID = main_model.ID;
                    }
                    for (int i = 0; i < BasisList.Count; i++)
                    {
                        App.Database.SaveBauteilAsync(BasisList[i]);
                    }
                }
                Konstruktionstyp = "unbelüftetes Dach (Warmdach) \r\n(unverschattet mit dunkler Deckung/Abdichtung)";

            }
            else if (main_model.MusterName == "Beton_KSD_PUR_PVC")
            {
                if (main_model.Selected == 0)
                {

                    BasisList.Add(new Basis()
                    {
                        ID_Sort = 1,
                        Bezeichnung = "Beton armiert (1% Stahl)",
                        R = 0.078261,
                        Dicke = 0.18,
                        Wärmeleitfähigkeit = 2.3,
                        Rohdichte = 2300,
                        Kapillar = true,
                        sonstiges = true,
                        KeineLuft = true,
                        Dampfdiffusionswiderstand_Min = 80,
                        Dampfdiffusionswiderstand_Max = 130,
                        Sd_Min = 14.40,
                        Sd_Max = 23.40,
                    });
                    BasisList.Add(new Basis()
                    {
                        ID_Sort = 2,
                        Bezeichnung = "KSD",
                        R = 0.008824,
                        Dicke = 0.0015,
                        Wärmeleitfähigkeit = 0.17,
                        Rohdichte = 1000.0,
                        Kapillar = true,
                        sonstiges = true,
                        KeineLuft = true,
                        Sd_Min = 1500.00,
                        Sd_Max = 1500.00,
                        Fester_sd = true,
                        Dampfdiffusionswiderstand_Min = 1000000.00,
                        Dampfdiffusionswiderstand_Max = 1000000.00
                    });
                    BasisList.Add(new Basis()
                    {
                        ID_Sort = 3,
                        Bezeichnung = "PUR 023",
                        R = 5.217391,
                        Fester_R = true,
                        Dicke = 0.12,
                        Wärmeleitfähigkeit = 0.023,
                        Rohdichte = 30.0,
                        Kapillar = true,
                        sonstiges = true,
                        EvntlLuft = true,
                        Sd_Min = 4.80,
                        Sd_Max = 24.00,
                        Dampfdiffusionswiderstand_Min = 40.00,
                        Dampfdiffusionswiderstand_Max = 200.00
                    });
                    BasisList.Add(new Basis()
                    {
                        ID_Sort = 4,
                        Bezeichnung = "Glasvlies",
                        R = 0.0001,
                        Dicke = 0.001,
                        Wärmeleitfähigkeit = 10.00,
                        Rohdichte = 400.0,
                        sonstiges = true,
                        KeineLuft = true,
                        Sd_Min = 0.00,
                        Sd_Max = 0.00,
                        Dampfdiffusionswiderstand_Min = 1.00,
                        Dampfdiffusionswiderstand_Max = 2.00
                    });
                    BasisList.Add(new Basis()
                    {
                        ID_Sort = 5,
                        Bezeichnung = "PVC-P (DIN 16730)",
                        R = 0.00,
                        Fester_R = true,
                        Dicke = 0.0015,
                        Wärmeleitfähigkeit = 0.00,
                        Rohdichte = 1200.0,
                        sonstiges = true,
                        Kapillar = true,
                        KeineLuft = true,
                        Sd_Min = 15.000,
                        Sd_Max = 45.00,
                        Dampfdiffusionswiderstand_Min = 10000.00,
                        Dampfdiffusionswiderstand_Max = 30000.00
                    });
                    foreach (Basis i in BasisList)
                    {
                        i.ModelID = main_model.ID;
                    }
                    for (int i = 0; i < BasisList.Count; i++)
                    {
                        App.Database.SaveBauteilAsync(BasisList[i]);
                    }
                }
                Konstruktionstyp = "unbelüftetes Dach (Warmdach) \r\n(unverschattet mit dunkler Deckung/Abdichtung)";

            }
            else if (main_model.MusterName == "Beton_KSD_PUR_PYE_einlagig")
            {
                if (main_model.Selected == 0)
                {

                    BasisList.Add(new Basis()
                    {
                        ID_Sort = 1,
                        Bezeichnung = "Beton armiert (1% Stahl)",
                        R = 0.078261,
                        Dicke = 0.18,
                        Wärmeleitfähigkeit = 2.3,
                        Rohdichte = 2300,
                        Kapillar = true,
                        sonstiges = true,
                        KeineLuft = true,
                        Dampfdiffusionswiderstand_Min = 80,
                        Dampfdiffusionswiderstand_Max = 130,
                        Sd_Min = 14.40,
                        Sd_Max = 23.40,
                    });
                    BasisList.Add(new Basis()
                    {
                        ID_Sort = 2,
                        Bezeichnung = "KSD",
                        R = 0.008824,
                        Dicke = 0.0015,
                        Wärmeleitfähigkeit = 0.17,
                        Rohdichte = 1000.0,
                        Kapillar = true,
                        sonstiges = true,
                        KeineLuft = true,
                        Sd_Min = 1500.00,
                        Sd_Max = 1500.00,
                        Fester_sd = true,
                        Dampfdiffusionswiderstand_Min = 1000000.00,
                        Dampfdiffusionswiderstand_Max = 1000000.00
                    });
                    BasisList.Add(new Basis()
                    {
                        ID_Sort = 3,
                        Bezeichnung = "PUR 023, mit Stufenfalz",
                        R = 6.086957,
                        Dicke = 0.14,
                        Wärmeleitfähigkeit = 0.023,
                        Rohdichte = 30.0,
                        Kapillar = true,
                        sonstiges = true,
                        KeineLuft = true,
                        Sd_Min = 5.60,
                        Sd_Max = 28.00,
                        Dampfdiffusionswiderstand_Min = 40.00,
                        Dampfdiffusionswiderstand_Max = 200.00
                    });
                    BasisList.Add(new Basis()
                    {
                        ID_Sort = 4,
                        Bezeichnung = "Glasvlies",
                        R = 0.000100,
                        Dicke = 0.001,
                        Wärmeleitfähigkeit = 10.000000,
                        Rohdichte = 400.0,
                        sonstiges = true,
                        KeineLuft = true,
                        Sd_Min = 0.000,
                        Sd_Max = 0.00,
                        Dampfdiffusionswiderstand_Min = 1.0,
                        Dampfdiffusionswiderstand_Max = 1.0
                    });
                    BasisList.Add(new Basis()
                    {
                        ID_Sort = 5,
                        Bezeichnung = "PYE PV200 S5 beschiefert",
                        R = 0.00,
                        Dicke = 0.005,
                        Wärmeleitfähigkeit = 0.00,
                        Rohdichte = 1000.0,
                        Kapillar = true,
                        sonstiges = true,
                        KeineLuft = true,
                        Fester_R = true,
                        Sd_Min = 100.000,
                        Sd_Max = 150.00,
                        Dampfdiffusionswiderstand_Min = 20000,
                        Dampfdiffusionswiderstand_Max = 30000
                    });
                    foreach (Basis i in BasisList)
                    {
                        i.ModelID = main_model.ID;
                    }
                    for (int i = 0; i < BasisList.Count; i++)
                    {
                        App.Database.SaveBauteilAsync(BasisList[i]);
                    }
                }
                Konstruktionstyp = "unbelüftetes Dach (Warmdach) \r\n(unverschattet mit dunkler Deckung/Abdichtung)";

            }
            else if (main_model.MusterName == "Beton_KSD_PUR_PYE_zweilagig")
            {
                if (main_model.Selected == 0)
                {

                    BasisList.Add(new Basis()
                    {
                        ID_Sort = 1,
                        Bezeichnung = "Beton armiert (1% Stahl)",
                        R = 0.078261,
                        Dicke = 0.18,
                        Wärmeleitfähigkeit = 2.3,
                        Rohdichte = 2300,
                        Kapillar = true,
                        sonstiges = true,
                        KeineLuft = true,
                        Dampfdiffusionswiderstand_Min = 80,
                        Dampfdiffusionswiderstand_Max = 130,
                        Sd_Min = 14.40,
                        Sd_Max = 23.40,
                    });
                    BasisList.Add(new Basis()
                    {
                        ID_Sort = 2,
                        Bezeichnung = "KSD",
                        R = 0.008824,
                        Dicke = 0.0015,
                        Wärmeleitfähigkeit = 0.17,
                        Rohdichte = 1000.0,
                        Kapillar = true,
                        sonstiges = true,
                        KeineLuft = true,
                        Sd_Min = 1500.00,
                        Sd_Max = 1500.00,
                        Fester_sd = true,
                        Dampfdiffusionswiderstand_Min = 1000000.00,
                        Dampfdiffusionswiderstand_Max = 1000000.00
                    });
                    BasisList.Add(new Basis()
                    {
                        ID_Sort = 3,
                        Bezeichnung = "PUR 023",
                        R = 5.217391,
                        Fester_R = true,
                        Dicke = 0.12,
                        Wärmeleitfähigkeit = 0.023,
                        Rohdichte = 30.0,
                        Kapillar = true,
                        sonstiges = true,
                        EvntlLuft = true,
                        Sd_Min = 4.80,
                        Sd_Max = 24.00,
                        Dampfdiffusionswiderstand_Min = 40.00,
                        Dampfdiffusionswiderstand_Max = 200.00
                    });
                    BasisList.Add(new Basis()
                    {
                        ID_Sort = 4,
                        Bezeichnung = "PYE G200 S4",
                        R = 0.00,
                        Dicke = 0.004,
                        Wärmeleitfähigkeit = 0.00,
                        Rohdichte = 1000.0,
                        Kapillar = true,
                        sonstiges = true,
                        KeineLuft = true,
                        Fester_R = true,
                        Sd_Min = 80.000,
                        Sd_Max = 120.00,
                        Dampfdiffusionswiderstand_Min = 20000,
                        Dampfdiffusionswiderstand_Max = 30000
                    });
                    BasisList.Add(new Basis()
                    {
                        ID_Sort = 5,
                        Bezeichnung = "PYE PV200 S5 beschiefert",
                        R = 0.00,
                        Dicke = 0.005,
                        Wärmeleitfähigkeit = 0.00,
                        Rohdichte = 1000.0,
                        Kapillar = true,
                        sonstiges = true,
                        KeineLuft = true,
                        Fester_R = true,
                        Sd_Min = 100.000,
                        Sd_Max = 150.00,
                        Dampfdiffusionswiderstand_Min = 20000,
                        Dampfdiffusionswiderstand_Max = 30000
                    });
                    foreach (Basis i in BasisList)
                    {
                        i.ModelID = main_model.ID;
                    }
                    for (int i = 0; i < BasisList.Count; i++)
                    {
                        App.Database.SaveBauteilAsync(BasisList[i]);
                    }
                }
                Konstruktionstyp = "unbelüftetes Dach (Warmdach) \r\n(unverschattet mit dunkler Deckung/Abdichtung)";

            }


            //OSB EPS
            else if (main_model.MusterName == "OSB_KSD_EPS_EPDM")
            {
                if (main_model.Selected == 0)
                {

                    BasisList.Add(new Basis()
                    {
                        ID_Sort = 1,
                        Bezeichnung = "OSB-Platten",
                        R = 0.169231,
                        Dicke = 0.022,
                        Wärmeleitfähigkeit = 0.130,
                        Rohdichte = 650,
                        Kapillar = true,
                        Holzwerkstoff = true,
                        KeineLuft = true,
                        Dampfdiffusionswiderstand_Min = 30.00,
                        Dampfdiffusionswiderstand_Max = 50.00,
                        Sd_Min = 0.66,
                        Sd_Max = 1.10,
                    });
                    BasisList.Add(new Basis()
                    {
                        ID_Sort = 2,
                        Bezeichnung = "KSD",
                        R = 0.008824,
                        Dicke = 0.0015,
                        Wärmeleitfähigkeit = 0.17,
                        Rohdichte = 1000.0,
                        Kapillar = true,
                        sonstiges = true,
                        KeineLuft = true,
                        Sd_Min = 1500.00,
                        Sd_Max = 1500.00,
                        Fester_sd = true,
                        Dampfdiffusionswiderstand_Min = 1000000.00,
                        Dampfdiffusionswiderstand_Max = 1000000.00
                    });
                    BasisList.Add(new Basis()
                    {
                        ID_Sort = 3,
                        Bezeichnung = "EPS 035, mit Stufenfalz",
                        R =   5.142857,
                        Dicke = 0.18,
                        Wärmeleitfähigkeit = 0.03500,
                        Rohdichte = 30.0,
                        Kapillar = true,
                        sonstiges = true,
                        KeineLuft = true,
                        Sd_Min = 3.6,
                        Sd_Max = 18.00,
                        Dampfdiffusionswiderstand_Min = 20.00,
                        Dampfdiffusionswiderstand_Max = 100.00
                    });
                    BasisList.Add(new Basis()
                    {
                        ID_Sort = 4,
                        Bezeichnung = "EPDM Dachbahn",
                        R = 0.00,
                        Dicke = 0.0013,
                        Wärmeleitfähigkeit = 0.00,
                        Rohdichte = 1150.0,
                        Kapillar = true,
                        sonstiges = true,
                        KeineLuft = true,
                        Fester_R = true,
                        Sd_Min = 78.00,
                        Sd_Max = 78.00,
                        Dampfdiffusionswiderstand_Min = 60000,
                        Dampfdiffusionswiderstand_Max = 60000
                    });
                    foreach (Basis i in BasisList)
                    {
                        i.ModelID = main_model.ID;
                    }
                    for (int i = 0; i < BasisList.Count; i++)
                    {
                        App.Database.SaveBauteilAsync(BasisList[i]);
                    }
                }
                Konstruktionstyp = "unbelüftetes Dach (Warmdach) \r\n(unverschattet mit dunkler Deckung/Abdichtung)";

            }
            else if (main_model.MusterName == "OSB_KSD_EPS_Evalon")
            {
                if (main_model.Selected == 0)
                {

                    BasisList.Add(new Basis()
                    {
                        ID_Sort = 1,
                        Bezeichnung = "OSB-Platten",
                        R = 0.169231,
                        Dicke = 0.022,
                        Wärmeleitfähigkeit = 0.130,
                        Rohdichte = 650,
                        Kapillar = true,
                        Holzwerkstoff = true,
                        KeineLuft = true,
                        Dampfdiffusionswiderstand_Min = 30.00,
                        Dampfdiffusionswiderstand_Max = 50.00,
                        Sd_Min = 0.66,
                        Sd_Max = 1.10,
                    });
                    BasisList.Add(new Basis()
                    {
                        ID_Sort = 2,
                        Bezeichnung = "KSD",
                        R = 0.008824,
                        Dicke = 0.0015,
                        Wärmeleitfähigkeit = 0.17,
                        Rohdichte = 1000.0,
                        Kapillar = true,
                        sonstiges = true,
                        KeineLuft = true,
                        Sd_Min = 1500.00,
                        Sd_Max = 1500.00,
                        Fester_sd = true,
                        Dampfdiffusionswiderstand_Min = 1000000.00,
                        Dampfdiffusionswiderstand_Max = 1000000.00
                    });
                    BasisList.Add(new Basis()
                    {
                        ID_Sort = 3,
                        Bezeichnung = "EPS 035, mit Stufenfalz",
                        R = 5.142857,
                        Dicke = 0.18,
                        Wärmeleitfähigkeit = 0.03500,
                        Rohdichte = 30.0,
                        Kapillar = true,
                        sonstiges = true,
                        KeineLuft = true,
                        Sd_Min = 3.6,
                        Sd_Max = 18.00,
                        Dampfdiffusionswiderstand_Min = 20.00,
                        Dampfdiffusionswiderstand_Max = 100.00
                    });
                    BasisList.Add(new Basis()
                    {
                        ID_Sort = 4,
                        Bezeichnung = "EVALON V",
                        R = 0.00,
                        Dicke = 0.0012,
                        Wärmeleitfähigkeit = 0.00,
                        Rohdichte = 1250.0,
                        Kapillar = true,
                        sonstiges = true,
                        KeineLuft = true,
                        Fester_R = true,
                        Sd_Min = 24.00,
                        Sd_Max = 24.00,
                        Dampfdiffusionswiderstand_Min = 20000,
                        Dampfdiffusionswiderstand_Max = 20000
                    });
                    foreach (Basis i in BasisList)
                    {
                        i.ModelID = main_model.ID;
                    }
                    for (int i = 0; i < BasisList.Count; i++)
                    {
                        App.Database.SaveBauteilAsync(BasisList[i]);
                    }
                }
                Konstruktionstyp = "unbelüftetes Dach (Warmdach) \r\n(unverschattet mit dunkler Deckung/Abdichtung)";

            }
            else if (main_model.MusterName == "OSB_KSD_EPS_FPO")
            {
                if (main_model.Selected == 0)
                {

                    BasisList.Add(new Basis()
                    {
                        ID_Sort = 1,
                        Bezeichnung = "OSB-Platten",
                        R = 0.169231,
                        Dicke = 0.022,
                        Wärmeleitfähigkeit = 0.130,
                        Rohdichte = 650,
                        Kapillar = true,
                        Holzwerkstoff = true,
                        KeineLuft = true,
                        Dampfdiffusionswiderstand_Min = 30.00,
                        Dampfdiffusionswiderstand_Max = 50.00,
                        Sd_Min = 0.66,
                        Sd_Max = 1.10,
                    });
                    BasisList.Add(new Basis()
                    {
                        ID_Sort = 2,
                        Bezeichnung = "KSD",
                        R = 0.008824,
                        Dicke = 0.0015,
                        Wärmeleitfähigkeit = 0.17,
                        Rohdichte = 1000.0,
                        Kapillar = true,
                        sonstiges = true,
                        KeineLuft = true,
                        Sd_Min = 1500.00,
                        Sd_Max = 1500.00,
                        Fester_sd = true,
                        Dampfdiffusionswiderstand_Min = 1000000.00,
                        Dampfdiffusionswiderstand_Max = 1000000.00
                    });
                    BasisList.Add(new Basis()
                    {
                        ID_Sort = 3,
                        Bezeichnung = "EPS 035, mit Stufenfalz",
                        R =  5.142857,
                        Dicke = 0.18,
                        Wärmeleitfähigkeit = 0.03500,
                        Rohdichte = 30.0,
                        Kapillar = true,
                        sonstiges = true,
                        KeineLuft = true,
                        Sd_Min = 3.6,
                        Sd_Max = 18.00,
                        Dampfdiffusionswiderstand_Min = 20.00,
                        Dampfdiffusionswiderstand_Max = 100.00
                    });
                    BasisList.Add(new Basis()
                    {
                        ID_Sort = 4,
                        Bezeichnung = "FPO Dachbahn",
                        R = 0.00000,
                        Dicke = 0.0018,
                        Wärmeleitfähigkeit = 0.000000,
                        Rohdichte = 1100.0,
                        sonstiges = true,
                        Kapillar = true,
                        KeineLuft = true,
                        Fester_R = true,
                        Fester_sd = true,
                        Sd_Min = 180.000,
                        Sd_Max = 180.00,
                        Dampfdiffusionswiderstand_Min = 100000.00,
                        Dampfdiffusionswiderstand_Max = 100000.00
                    });
                    foreach (Basis i in BasisList)
                    {
                        i.ModelID = main_model.ID;
                    }
                    for (int i = 0; i < BasisList.Count; i++)
                    {
                        App.Database.SaveBauteilAsync(BasisList[i]);
                    }
                }
                Konstruktionstyp = "unbelüftetes Dach (Warmdach) \r\n(unverschattet mit dunkler Deckung/Abdichtung)";

            }
            else if (main_model.MusterName == "OSB_KSD_EPS_PVC")
            {
                if (main_model.Selected == 0)
                {

                    BasisList.Add(new Basis()
                    {
                        ID_Sort = 1,
                        Bezeichnung = "OSB-Platten",
                        R = 0.169231,
                        Dicke = 0.022,
                        Wärmeleitfähigkeit = 0.130,
                        Rohdichte = 650,
                        Kapillar = true,
                        Holzwerkstoff = true,
                        KeineLuft = true,
                        Dampfdiffusionswiderstand_Min = 30.00,
                        Dampfdiffusionswiderstand_Max = 50.00,
                        Sd_Min = 0.66,
                        Sd_Max = 1.10,
                    });
                    BasisList.Add(new Basis()
                    {
                        ID_Sort = 2,
                        Bezeichnung = "KSD",
                        R = 0.008824,
                        Dicke = 0.0015,
                        Wärmeleitfähigkeit = 0.17,
                        Rohdichte = 1000.0,
                        Kapillar = true,
                        sonstiges = true,
                        KeineLuft = true,
                        Sd_Min = 1500.00,
                        Sd_Max = 1500.00,
                        Fester_sd = true,
                        Dampfdiffusionswiderstand_Min = 1000000.00,
                        Dampfdiffusionswiderstand_Max = 1000000.00
                    });
                    BasisList.Add(new Basis()
                    {
                        ID_Sort = 3,
                        Bezeichnung = "EPS 035, mit Stufenfalz",
                        R =  5.142857,
                        Dicke = 0.18,
                        Wärmeleitfähigkeit = 0.03500,
                        Rohdichte = 30.0,
                        Kapillar = true,
                        sonstiges = true,
                        KeineLuft = true,
                        Sd_Min = 3.6,
                        Sd_Max = 18.00,
                        Dampfdiffusionswiderstand_Min = 20.00,
                        Dampfdiffusionswiderstand_Max = 100.00
                    });
                    BasisList.Add(new Basis()
                    {
                        ID_Sort = 4,
                        Bezeichnung = "Glasvlies",
                        R = 0.000100,
                        Dicke = 0.001,
                        Wärmeleitfähigkeit = 10.000000,
                        Rohdichte = 400.0,
                        sonstiges = true,
                        KeineLuft = true,
                        Sd_Min = 0.000,
                        Sd_Max = 0.00,
                        Dampfdiffusionswiderstand_Min = 1.0,
                        Dampfdiffusionswiderstand_Max = 1.0
                    });
                    BasisList.Add(new Basis()
                    {
                        ID_Sort = 5,
                        Bezeichnung = "PVC-P (DIN 16730)",
                        R = 0.00000,
                        Dicke = 0.0015,
                        Wärmeleitfähigkeit = 0.000000,
                        Rohdichte = 1200.0,
                        sonstiges = true,
                        Kapillar = true,
                        KeineLuft = true,
                        Fester_R = true,
                        Sd_Min = 15.000,
                        Sd_Max = 45.00,
                        Dampfdiffusionswiderstand_Min = 10000.00,
                        Dampfdiffusionswiderstand_Max = 30000.00
                    });
                    foreach (Basis i in BasisList)
                    {
                        i.ModelID = main_model.ID;
                    }
                    for (int i = 0; i < BasisList.Count; i++)
                    {
                        App.Database.SaveBauteilAsync(BasisList[i]);
                    }
                }
                Konstruktionstyp = "unbelüftetes Dach (Warmdach) \r\n(unverschattet mit dunkler Deckung/Abdichtung)";

            }
            else if (main_model.MusterName == "OSB_KSD_EPS_PYE_einlagig")
            {
                if (main_model.Selected == 0)
                {

                    BasisList.Add(new Basis()
                    {
                        ID_Sort = 1,
                        Bezeichnung = "OSB-Platten",
                        R = 0.169231,
                        Dicke = 0.022,
                        Wärmeleitfähigkeit = 0.130,
                        Rohdichte = 650,
                        Kapillar = true,
                        Holzwerkstoff = true,
                        KeineLuft = true,
                        Dampfdiffusionswiderstand_Min = 30.00,
                        Dampfdiffusionswiderstand_Max = 50.00,
                        Sd_Min = 0.66,
                        Sd_Max = 1.10,
                    });
                    BasisList.Add(new Basis()
                    {
                        ID_Sort = 2,
                        Bezeichnung = "KSD",
                        R = 0.008824,
                        Dicke = 0.0015,
                        Wärmeleitfähigkeit = 0.17,
                        Rohdichte = 1000.0,
                        Kapillar = true,
                        sonstiges = true,
                        KeineLuft = true,
                        Sd_Min = 1500.00,
                        Sd_Max = 1500.00,
                        Fester_sd = true,
                        Dampfdiffusionswiderstand_Min = 1000000.00,
                        Dampfdiffusionswiderstand_Max = 1000000.00
                    });
                    BasisList.Add(new Basis()
                    {
                        ID_Sort = 3,
                        Bezeichnung = "EPS 035, mit Stufenfalz",
                        R = 5.142857,
                        Dicke = 0.18,
                        Wärmeleitfähigkeit = 0.03500,
                        Rohdichte = 30.0,
                        Kapillar = true,
                        sonstiges = true,
                        KeineLuft = true,
                        Sd_Min = 3.6,
                        Sd_Max = 18.00,
                        Dampfdiffusionswiderstand_Min = 20.00,
                        Dampfdiffusionswiderstand_Max = 100.00
                    });
                    BasisList.Add(new Basis()
                    {
                        ID_Sort = 4,
                        Bezeichnung = "Glasvlies",
                        R = 0.000100,
                        Dicke = 0.001,
                        Wärmeleitfähigkeit = 10.000000,
                        Rohdichte = 400.0,
                        sonstiges = true,
                        KeineLuft = true,
                        Sd_Min = 0.000,
                        Sd_Max = 0.00,
                        Dampfdiffusionswiderstand_Min = 1.0,
                        Dampfdiffusionswiderstand_Max = 1.0
                    });
                    BasisList.Add(new Basis()
                    {
                        ID_Sort = 5,
                        Bezeichnung = "PYE PV200 S5 beschiefert",
                        R = 0.00,
                        Dicke = 0.005,
                        Wärmeleitfähigkeit = 0.00,
                        Rohdichte = 1000.0,
                        Kapillar = true,
                        sonstiges = true,
                        KeineLuft = true,
                        Fester_R = true,
                        Sd_Min = 100.000,
                        Sd_Max = 150.00,
                        Dampfdiffusionswiderstand_Min = 20000,
                        Dampfdiffusionswiderstand_Max = 30000
                    });
                    foreach (Basis i in BasisList)
                    {
                        i.ModelID = main_model.ID;
                    }
                    for (int i = 0; i < BasisList.Count; i++)
                    {
                        App.Database.SaveBauteilAsync(BasisList[i]);
                    }
                }
                Konstruktionstyp = "unbelüftetes Dach (Warmdach) \r\n(unverschattet mit dunkler Deckung/Abdichtung)";

            }
            else if (main_model.MusterName == "OSB_KSD_EPS_PYE_zweilagig")
            {
                if (main_model.Selected == 0)
                {

                    BasisList.Add(new Basis()
                    {
                        ID_Sort = 1,
                        Bezeichnung = "OSB-Platten",
                        R = 0.169231,
                        Dicke = 0.022,
                        Wärmeleitfähigkeit = 0.130,
                        Rohdichte = 650,
                        Kapillar = true,
                        Holzwerkstoff = true,
                        KeineLuft = true,
                        Dampfdiffusionswiderstand_Min = 30.00,
                        Dampfdiffusionswiderstand_Max = 50.00,
                        Sd_Min = 0.66,
                        Sd_Max = 1.10,
                    });
                    BasisList.Add(new Basis()
                    {
                        ID_Sort = 2,
                        Bezeichnung = "KSD",
                        R = 0.008824,
                        Dicke = 0.0015,
                        Wärmeleitfähigkeit = 0.17,
                        Rohdichte = 1000.0,
                        Kapillar = true,
                        sonstiges = true,
                        KeineLuft = true,
                        Sd_Min = 1500.00,
                        Sd_Max = 1500.00,
                        Fester_sd = true,
                        Dampfdiffusionswiderstand_Min = 1000000.00,
                        Dampfdiffusionswiderstand_Max = 1000000.00
                    });
                    BasisList.Add(new Basis()
                    {
                        ID_Sort = 3,
                        Bezeichnung = "EPS 035, mit Stufenfalz",
                        R =  5.142857,
                        Dicke = 0.18,
                        Wärmeleitfähigkeit = 0.03500,
                        Rohdichte = 30.0,
                        Kapillar = true,
                        sonstiges = true,
                        KeineLuft = true,
                        Sd_Min = 3.6,
                        Sd_Max = 18.00,
                        Dampfdiffusionswiderstand_Min = 20.00,
                        Dampfdiffusionswiderstand_Max = 100.00
                    });
                    BasisList.Add(new Basis()
                    {
                        ID_Sort = 4,
                        Bezeichnung = "PYE G200 S4",
                        R = 0.00,
                        Dicke = 0.004,
                        Wärmeleitfähigkeit = 0.00,
                        Rohdichte = 1000.0,
                        Kapillar = true,
                        sonstiges = true,
                        KeineLuft = true,
                        Fester_R = true,
                        Sd_Min = 80.000,
                        Sd_Max = 120.00,
                        Dampfdiffusionswiderstand_Min = 20000,
                        Dampfdiffusionswiderstand_Max = 30000
                    });
                    BasisList.Add(new Basis()
                    {
                        ID_Sort = 4,
                        Bezeichnung = "PYE PV200 S5 beschiefert",
                        R = 0.00,
                        Dicke = 0.005,
                        Wärmeleitfähigkeit = 0.00,
                        Rohdichte = 1000.0,
                        Kapillar = true,
                        sonstiges = true,
                        KeineLuft = true,
                        Fester_R = true,
                        Sd_Min = 100.000,
                        Sd_Max = 150.00,
                        Dampfdiffusionswiderstand_Min = 20000,
                        Dampfdiffusionswiderstand_Max = 30000
                    });
                    foreach (Basis i in BasisList)
                    {
                        i.ModelID = main_model.ID;
                    }
                    for (int i = 0; i < BasisList.Count; i++)
                    {
                        App.Database.SaveBauteilAsync(BasisList[i]);
                    }
                }
                Konstruktionstyp = "unbelüftetes Dach (Warmdach) \r\n(unverschattet mit dunkler Deckung/Abdichtung)";

            }

            //OSB MIFA
            else if (main_model.MusterName == "OSB_KSD_MIFA_EPDM")
            {
                if (main_model.Selected == 0)
                {

                    BasisList.Add(new Basis()
                    {
                        ID_Sort = 1,
                        Bezeichnung = "OSB-Platten",
                        R = 0.169231,
                        Dicke = 0.022,
                        Wärmeleitfähigkeit = 0.130,
                        Rohdichte = 650,
                        Kapillar = true,
                        Holzwerkstoff = true,
                        KeineLuft = true,
                        Dampfdiffusionswiderstand_Min = 30.00,
                        Dampfdiffusionswiderstand_Max = 50.00,
                        Sd_Min = 0.66,
                        Sd_Max = 1.10,
                    });
                    BasisList.Add(new Basis()
                    {
                        ID_Sort = 2,
                        Bezeichnung = "KSD",
                        R = 0.008824,
                        Dicke = 0.0015,
                        Wärmeleitfähigkeit = 0.17,
                        Rohdichte = 1000.0,
                        Kapillar = true,
                        sonstiges = true,
                        KeineLuft = true,
                        Sd_Min = 1500.00,
                        Sd_Max = 1500.00,
                        Fester_sd = true,
                        Dampfdiffusionswiderstand_Min = 1000000.00,
                        Dampfdiffusionswiderstand_Max = 1000000.00
                    });
                    BasisList.Add(new Basis()
                    {
                        ID_Sort = 3,
                        Bezeichnung = "Mineralfaser 038",
                        R = 5.263158,
                        Dicke = 0.2,
                        Wärmeleitfähigkeit = 0.038,
                        Rohdichte = 120.0,
                        Kapillar = true,
                        sonstiges = true,
                        EvntlLuft = true,
                        Sd_Min = 0.20,
                        Sd_Max = 0.20,
                        Dampfdiffusionswiderstand_Min = 1.00,
                        Dampfdiffusionswiderstand_Max = 1.00
                    });
                    BasisList.Add(new Basis()
                    {
                        ID_Sort = 4,
                        Bezeichnung = "EPDM",
                        R = 0.008,
                        Dicke = 0.002,
                        Wärmeleitfähigkeit = 0.25,
                        Rohdichte = 1150.0,
                        Kapillar = true,
                        sonstiges = true,
                        KeineLuft = true,
                        Sd_Min = 120.00,
                        Sd_Max = 120.00,
                        Dampfdiffusionswiderstand_Min = 60000,
                        Dampfdiffusionswiderstand_Max = 60000
                    });
                    foreach (Basis i in BasisList)
                    {
                        i.ModelID = main_model.ID;
                    }
                    for (int i = 0; i < BasisList.Count; i++)
                    {
                        App.Database.SaveBauteilAsync(BasisList[i]);
                    }
                }
                Konstruktionstyp = "unbelüftetes Dach (Warmdach) \r\n(unverschattet mit dunkler Deckung/Abdichtung)";

            }
            else if (main_model.MusterName == "OSB_KSD_MIFA_Evalon")
            {
                if (main_model.Selected == 0)
                {

                    BasisList.Add(new Basis()
                    {
                        ID_Sort = 1,
                        Bezeichnung = "OSB-Platten",
                        R = 0.169231,
                        Dicke = 0.022,
                        Wärmeleitfähigkeit = 0.130,
                        Rohdichte = 650,
                        Kapillar = true,
                        Holzwerkstoff = true,
                        KeineLuft = true,
                        Dampfdiffusionswiderstand_Min = 30.00,
                        Dampfdiffusionswiderstand_Max = 50.00,
                        Sd_Min = 0.66,
                        Sd_Max = 1.10,
                    });
                    BasisList.Add(new Basis()
                    {
                        ID_Sort = 2,
                        Bezeichnung = "KSD",
                        R = 0.008824,
                        Dicke = 0.0015,
                        Wärmeleitfähigkeit = 0.17,
                        Rohdichte = 1000.0,
                        Kapillar = true,
                        sonstiges = true,
                        KeineLuft = true,
                        Sd_Min = 1500.00,
                        Sd_Max = 1500.00,
                        Fester_sd = true,
                        Dampfdiffusionswiderstand_Min = 1000000.00,
                        Dampfdiffusionswiderstand_Max = 1000000.00
                    });
                    BasisList.Add(new Basis()
                    {
                        ID_Sort = 3,
                        Bezeichnung = "Mineralfaser 038",
                        R = 5.263158,
                        Dicke = 0.2,
                        Wärmeleitfähigkeit = 0.038,
                        Rohdichte = 120.0,
                        Kapillar = true,
                        sonstiges = true,
                        EvntlLuft = true,
                        Sd_Min = 0.20,
                        Sd_Max = 0.20,
                        Dampfdiffusionswiderstand_Min = 1.00,
                        Dampfdiffusionswiderstand_Max = 1.00
                    });
                    BasisList.Add(new Basis()
                    {
                        ID_Sort = 4,
                        Bezeichnung = "EVALON V",
                        R = 0.00,
                        Dicke = 0.0012,
                        Wärmeleitfähigkeit = 0.00,
                        Rohdichte = 1250.0,
                        Kapillar = true,
                        sonstiges = true,
                        KeineLuft = true,
                        Fester_R = true,
                        Sd_Min = 24.00,
                        Sd_Max = 24.00,
                        Dampfdiffusionswiderstand_Min = 20000,
                        Dampfdiffusionswiderstand_Max = 20000
                    });
                    foreach (Basis i in BasisList)
                    {
                        i.ModelID = main_model.ID;
                    }
                    for (int i = 0; i < BasisList.Count; i++)
                    {
                        App.Database.SaveBauteilAsync(BasisList[i]);
                    }
                }
                Konstruktionstyp = "unbelüftetes Dach (Warmdach) \r\n(unverschattet mit dunkler Deckung/Abdichtung)";

            }
            else if (main_model.MusterName == "OSB_KSD_MIFA_FPO")
            {
                if (main_model.Selected == 0)
                {

                    BasisList.Add(new Basis()
                    {
                        ID_Sort = 1,
                        Bezeichnung = "OSB-Platten",
                        R = 0.169231,
                        Dicke = 0.022,
                        Wärmeleitfähigkeit = 0.130,
                        Rohdichte = 650,
                        Kapillar = true,
                        Holzwerkstoff = true,
                        KeineLuft = true,
                        Dampfdiffusionswiderstand_Min = 30.00,
                        Dampfdiffusionswiderstand_Max = 50.00,
                        Sd_Min = 0.66,
                        Sd_Max = 1.10,
                    });
                    BasisList.Add(new Basis()
                    {
                        ID_Sort = 2,
                        Bezeichnung = "KSD",
                        R = 0.008824,
                        Dicke = 0.0015,
                        Wärmeleitfähigkeit = 0.17,
                        Rohdichte = 1000.0,
                        Kapillar = true,
                        sonstiges = true,
                        KeineLuft = true,
                        Sd_Min = 1500.00,
                        Sd_Max = 1500.00,
                        Fester_sd = true,
                        Dampfdiffusionswiderstand_Min = 1000000.00,
                        Dampfdiffusionswiderstand_Max = 1000000.00
                    });
                    BasisList.Add(new Basis()
                    {
                        ID_Sort = 3,
                        Bezeichnung = "Mineralfaser 038",
                        R = 5.263158,
                        Dicke = 0.2,
                        Wärmeleitfähigkeit = 0.038,
                        Rohdichte = 120.0,
                        Kapillar = true,
                        sonstiges = true,
                        EvntlLuft = true,
                        Sd_Min = 0.20,
                        Sd_Max = 0.20,
                        Dampfdiffusionswiderstand_Min = 1.00,
                        Dampfdiffusionswiderstand_Max = 1.00
                    });
                    BasisList.Add(new Basis()
                    {
                        ID_Sort = 4,
                        Bezeichnung = "FPO",
                        R = 0.00,
                        Fester_R = true,
                        Dicke = 0.0018,
                        Wärmeleitfähigkeit = 0.00,
                        Rohdichte = 1100.0,
                        sonstiges = true,
                        Kapillar = true,
                        KeineLuft = true,
                        Fester_sd = true,
                        Sd_Min = 180.000,
                        Sd_Max = 180.00,
                        Dampfdiffusionswiderstand_Min = 100000.00,
                        Dampfdiffusionswiderstand_Max = 100000.00
                    });
                    foreach (Basis i in BasisList)
                    {
                        i.ModelID = main_model.ID;
                    }
                    for (int i = 0; i < BasisList.Count; i++)
                    {
                        App.Database.SaveBauteilAsync(BasisList[i]);
                    }
                }
                Konstruktionstyp = "unbelüftetes Dach (Warmdach) \r\n(unverschattet mit dunkler Deckung/Abdichtung)";

            }
            else if (main_model.MusterName == "OSB_KSD_MIFA_PVC")
            {
                if (main_model.Selected == 0)
                {

                    BasisList.Add(new Basis()
                    {
                        ID_Sort = 1,
                        Bezeichnung = "OSB-Platten",
                        R = 0.169231,
                        Dicke = 0.022,
                        Wärmeleitfähigkeit = 0.130,
                        Rohdichte = 650,
                        Kapillar = true,
                        Holzwerkstoff = true,
                        KeineLuft = true,
                        Dampfdiffusionswiderstand_Min = 30.00,
                        Dampfdiffusionswiderstand_Max = 50.00,
                        Sd_Min = 0.66,
                        Sd_Max = 1.10,
                    });
                    BasisList.Add(new Basis()
                    {
                        ID_Sort = 2,
                        Bezeichnung = "KSD",
                        R = 0.008824,
                        Dicke = 0.0015,
                        Wärmeleitfähigkeit = 0.17,
                        Rohdichte = 1000.0,
                        Kapillar = true,
                        sonstiges = true,
                        KeineLuft = true,
                        Sd_Min = 1500.00,
                        Sd_Max = 1500.00,
                        Fester_sd = true,
                        Dampfdiffusionswiderstand_Min = 1000000.00,
                        Dampfdiffusionswiderstand_Max = 1000000.00
                    });
                    BasisList.Add(new Basis()
                    {
                        ID_Sort = 3,
                        Bezeichnung = "Mineralfaser 038",
                        R = 5.263158,
                        Dicke = 0.2,
                        Wärmeleitfähigkeit = 0.038,
                        Rohdichte = 120.0,
                        Kapillar = true,
                        sonstiges = true,
                        EvntlLuft = true,
                        Sd_Min = 0.20,
                        Sd_Max = 0.20,
                        Dampfdiffusionswiderstand_Min = 1.00,
                        Dampfdiffusionswiderstand_Max = 1.00
                    });
                    BasisList.Add(new Basis()
                    {
                        ID_Sort = 4,
                        Bezeichnung = "PVC-P (DIN 16730)",
                        R = 0.00,
                        Fester_R = true,
                        Dicke = 0.0015,
                        Wärmeleitfähigkeit = 0.000,
                        Rohdichte = 1200.0,
                        sonstiges = true,
                        Kapillar = true,
                        KeineLuft = true,
                        Sd_Min = 15.000,
                        Sd_Max = 45.00,
                        Dampfdiffusionswiderstand_Min = 10000.00,
                        Dampfdiffusionswiderstand_Max = 30000.00
                    });
                    foreach (Basis i in BasisList)
                    {
                        i.ModelID = main_model.ID;
                    }
                    for (int i = 0; i < BasisList.Count; i++)
                    {
                        App.Database.SaveBauteilAsync(BasisList[i]);
                    }
                }
                Konstruktionstyp = "unbelüftetes Dach (Warmdach) \r\n(unverschattet mit dunkler Deckung/Abdichtung)";

            }
            else if (main_model.MusterName == "OSB_KSD_MIFA_PYE_einlagig")
            {
                if (main_model.Selected == 0)
                {

                    BasisList.Add(new Basis()
                    {
                        ID_Sort = 1,
                        Bezeichnung = "OSB-Platten",
                        R = 0.169231,
                        Dicke = 0.022,
                        Wärmeleitfähigkeit = 0.130,
                        Rohdichte = 650,
                        Kapillar = true,
                        Holzwerkstoff = true,
                        KeineLuft = true,
                        Dampfdiffusionswiderstand_Min = 30.00,
                        Dampfdiffusionswiderstand_Max = 50.00,
                        Sd_Min = 0.66,
                        Sd_Max = 1.10,
                    });
                    BasisList.Add(new Basis()
                    {
                        ID_Sort = 2,
                        Bezeichnung = "KSD",
                        R = 0.008824,
                        Dicke = 0.0015,
                        Wärmeleitfähigkeit = 0.17,
                        Rohdichte = 1000.0,
                        Kapillar = true,
                        sonstiges = true,
                        KeineLuft = true,
                        Sd_Min = 1500.00,
                        Sd_Max = 1500.00,
                        Fester_sd = true,
                        Dampfdiffusionswiderstand_Min = 1000000.00,
                        Dampfdiffusionswiderstand_Max = 1000000.00
                    });
                    BasisList.Add(new Basis()
                    {
                        ID_Sort = 3,
                        Bezeichnung = "Mineralfaser 038",
                        R = 5.263158,
                        Dicke = 0.2,
                        Wärmeleitfähigkeit = 0.038,
                        Rohdichte = 120.0,
                        Kapillar = true,
                        sonstiges = true,
                        EvntlLuft = true,
                        Sd_Min = 0.20,
                        Sd_Max = 0.20,
                        Dampfdiffusionswiderstand_Min = 1.00,
                        Dampfdiffusionswiderstand_Max = 1.00
                    });
                    BasisList.Add(new Basis()
                    {
                        ID_Sort = 4,
                        Bezeichnung = "Glasvlies",
                        R = 0.000100,
                        Dicke = 0.001,
                        Wärmeleitfähigkeit = 10.000000,
                        Rohdichte = 400.0,
                        sonstiges = true,
                        KeineLuft = true,
                        Sd_Min = 0.000,
                        Sd_Max = 0.00,
                        Dampfdiffusionswiderstand_Min = 1.0,
                        Dampfdiffusionswiderstand_Max = 1.0
                    });
                    BasisList.Add(new Basis()
                    {
                        ID_Sort = 5,
                        Bezeichnung = "PYE PV200 S5 beschiefert",
                        R = 0.00,
                        Dicke = 0.005,
                        Wärmeleitfähigkeit = 0.00,
                        Rohdichte = 1000.0,
                        Kapillar = true,
                        sonstiges = true,
                        KeineLuft = true,
                        Fester_R = true,
                        Sd_Min = 100.000,
                        Sd_Max = 150.00,
                        Dampfdiffusionswiderstand_Min = 20000,
                        Dampfdiffusionswiderstand_Max = 30000
                    });
                    foreach (Basis i in BasisList)
                    {
                        i.ModelID = main_model.ID;
                    }
                    for (int i = 0; i < BasisList.Count; i++)
                    {
                        App.Database.SaveBauteilAsync(BasisList[i]);
                    }
                }
                Konstruktionstyp = "unbelüftetes Dach (Warmdach) \r\n(unverschattet mit dunkler Deckung/Abdichtung)";

            }
            else if (main_model.MusterName == "OSB_KSD_MIFA_PYE_zweilagig")
            {
                if (main_model.Selected == 0)
                {

                    BasisList.Add(new Basis()
                    {
                        ID_Sort = 1,
                        Bezeichnung = "OSB-Platten",
                        R = 0.169231,
                        Dicke = 0.022,
                        Wärmeleitfähigkeit = 0.130,
                        Rohdichte = 650,
                        Kapillar = true,
                        Holzwerkstoff = true,
                        KeineLuft = true,
                        Dampfdiffusionswiderstand_Min = 30.00,
                        Dampfdiffusionswiderstand_Max = 50.00,
                        Sd_Min = 0.66,
                        Sd_Max = 1.10,
                    });
                    BasisList.Add(new Basis()
                    {
                        ID_Sort = 2,
                        Bezeichnung = "KSD",
                        R = 0.008824,
                        Dicke = 0.0015,
                        Wärmeleitfähigkeit = 0.17,
                        Rohdichte = 1000.0,
                        Kapillar = true,
                        sonstiges = true,
                        KeineLuft = true,
                        Sd_Min = 1500.00,
                        Sd_Max = 1500.00,
                        Fester_sd = true,
                        Dampfdiffusionswiderstand_Min = 1000000.00,
                        Dampfdiffusionswiderstand_Max = 1000000.00
                    });
                    BasisList.Add(new Basis()
                    {
                        ID_Sort = 3,
                        Bezeichnung = "Mineralfaser 038",
                        R = 5.263158,
                        Dicke = 0.2,
                        Wärmeleitfähigkeit = 0.038,
                        Rohdichte = 120.0,
                        Kapillar = true,
                        sonstiges = true,
                        EvntlLuft = true,
                        Sd_Min = 0.20,
                        Sd_Max = 0.20,
                        Dampfdiffusionswiderstand_Min = 1.00,
                        Dampfdiffusionswiderstand_Max = 1.00
                    });
                    BasisList.Add(new Basis()
                    {
                        ID_Sort = 4,
                        Bezeichnung = "PYE G200 S4",
                        R = 0.00,
                        Dicke = 0.004,
                        Wärmeleitfähigkeit = 0.00,
                        Rohdichte = 1000.0,
                        Kapillar = true,
                        sonstiges = true,
                        KeineLuft = true,
                        Fester_R = true,
                        Sd_Min = 80.000,
                        Sd_Max = 120.00,
                        Dampfdiffusionswiderstand_Min = 20000,
                        Dampfdiffusionswiderstand_Max = 30000
                    });
                    BasisList.Add(new Basis()
                    {
                        ID_Sort = 5,
                        Bezeichnung = "PYE PV200 S5 beschiefert",
                        R = 0.00,
                        Dicke = 0.005,
                        Wärmeleitfähigkeit = 0.00,
                        Rohdichte = 1000.0,
                        Kapillar = true,
                        sonstiges = true,
                        KeineLuft = true,
                        Fester_R = true,
                        Sd_Min = 100.000,
                        Sd_Max = 150.00,
                        Dampfdiffusionswiderstand_Min = 20000,
                        Dampfdiffusionswiderstand_Max = 30000
                    });
                    foreach (Basis i in BasisList)
                    {
                        i.ModelID = main_model.ID;
                    }
                    for (int i = 0; i < BasisList.Count; i++)
                    {
                        App.Database.SaveBauteilAsync(BasisList[i]);
                    }
                }
                Konstruktionstyp = "unbelüftetes Dach (Warmdach) \r\n(unverschattet mit dunkler Deckung/Abdichtung)";

            }

            //OSB PUR
            else if (main_model.MusterName == "OSB_KSD_PUR_EPDM")
            {
                if (main_model.Selected == 0)
                {

                    BasisList.Add(new Basis()
                    {
                        ID_Sort = 1,
                        Bezeichnung = "OSB-Platten",
                        R = 0.169231,
                        Dicke = 0.022,
                        Wärmeleitfähigkeit = 0.130,
                        Rohdichte = 650,
                        Kapillar = true,
                        Holzwerkstoff = true,
                        KeineLuft = true,
                        Dampfdiffusionswiderstand_Min = 30.00,
                        Dampfdiffusionswiderstand_Max = 50.00,
                        Sd_Min = 0.66,
                        Sd_Max = 1.10,
                    });
                    BasisList.Add(new Basis()
                    {
                        ID_Sort = 2,
                        Bezeichnung = "KSD",
                        R = 0.008824,
                        Dicke = 0.0015,
                        Wärmeleitfähigkeit = 0.17,
                        Rohdichte = 1000.0,
                        Kapillar = true,
                        sonstiges = true,
                        KeineLuft = true,
                        Sd_Min = 1500.00,
                        Sd_Max = 1500.00,
                        Fester_sd = true,
                        Dampfdiffusionswiderstand_Min = 1000000.00,
                        Dampfdiffusionswiderstand_Max = 1000000.00
                    });
                    BasisList.Add(new Basis()
                    {
                        ID_Sort = 3,
                        Bezeichnung = "PUR 023",
                        R = 5.217391,
                        Fester_R = true,
                        Dicke = 0.12,
                        Wärmeleitfähigkeit = 0.023,
                        Rohdichte = 30.0,
                        Kapillar = true,
                        sonstiges = true,
                        EvntlLuft = true,
                        Sd_Min = 4.80,
                        Sd_Max = 24.00,
                        Dampfdiffusionswiderstand_Min = 40.00,
                        Dampfdiffusionswiderstand_Max = 200.00
                    });
                    BasisList.Add(new Basis()
                    {
                        ID_Sort = 4,
                        Bezeichnung = "EPDM Dachbahn",
                        R = 0.000,
                        Fester_R = true,
                        Dicke = 0.0013,
                        Wärmeleitfähigkeit = 0.000,
                        Rohdichte = 1150.0,
                        Kapillar = true,
                        sonstiges = true,
                        KeineLuft = true,
                        Sd_Min = 78.00,
                        Sd_Max = 78.00,
                        Dampfdiffusionswiderstand_Min = 60000,
                        Dampfdiffusionswiderstand_Max = 60000
                    });
                    foreach (Basis i in BasisList)
                    {
                        i.ModelID = main_model.ID;
                    }
                    for (int i = 0; i < BasisList.Count; i++)
                    {
                        App.Database.SaveBauteilAsync(BasisList[i]);
                    }
                }
                Konstruktionstyp = "unbelüftetes Dach (Warmdach) \r\n(unverschattet mit dunkler Deckung/Abdichtung)";

            }
            else if (main_model.MusterName == "OSB_KSD_PUR_Evalon")
            {
                if (main_model.Selected == 0)
                {

                    BasisList.Add(new Basis()
                    {
                        ID_Sort = 1,
                        Bezeichnung = "OSB-Platten",
                        R = 0.169231,
                        Dicke = 0.022,
                        Wärmeleitfähigkeit = 0.130,
                        Rohdichte = 650,
                        Kapillar = true,
                        Holzwerkstoff = true,
                        KeineLuft = true,
                        Dampfdiffusionswiderstand_Min = 30.00,
                        Dampfdiffusionswiderstand_Max = 50.00,
                        Sd_Min = 0.66,
                        Sd_Max = 1.10,
                    });
                    BasisList.Add(new Basis()
                    {
                        ID_Sort = 2,
                        Bezeichnung = "KSD",
                        R = 0.008824,
                        Dicke = 0.0015,
                        Wärmeleitfähigkeit = 0.17,
                        Rohdichte = 1000.0,
                        Kapillar = true,
                        sonstiges = true,
                        KeineLuft = true,
                        Sd_Min = 1500.00,
                        Sd_Max = 1500.00,
                        Fester_sd = true,
                        Dampfdiffusionswiderstand_Min = 1000000.00,
                        Dampfdiffusionswiderstand_Max = 1000000.00
                    });
                    BasisList.Add(new Basis()
                    {
                        ID_Sort = 3,
                        Bezeichnung = "PUR 023",
                        R = 5.217391,
                        Fester_R = true,
                        Dicke = 0.12,
                        Wärmeleitfähigkeit = 0.023,
                        Rohdichte = 30.0,
                        Kapillar = true,
                        sonstiges = true,
                        EvntlLuft = true,
                        Sd_Min = 4.80,
                        Sd_Max = 24.00,
                        Dampfdiffusionswiderstand_Min = 40.00,
                        Dampfdiffusionswiderstand_Max = 200.00
                    });
                    BasisList.Add(new Basis()
                    {
                        ID_Sort = 4,
                        Bezeichnung = "EVALON V",
                        R = 0.00,
                        Dicke = 0.0012,
                        Wärmeleitfähigkeit = 0.00,
                        Rohdichte = 1250.0,
                        Kapillar = true,
                        sonstiges = true,
                        KeineLuft = true,
                        Fester_R = true,
                        Sd_Min = 24.00,
                        Sd_Max = 24.00,
                        Dampfdiffusionswiderstand_Min = 20000,
                        Dampfdiffusionswiderstand_Max = 20000
                    });
                    foreach (Basis i in BasisList)
                    {
                        i.ModelID = main_model.ID;
                    }
                    for (int i = 0; i < BasisList.Count; i++)
                    {
                        App.Database.SaveBauteilAsync(BasisList[i]);
                    }
                }
                Konstruktionstyp = "unbelüftetes Dach (Warmdach) \r\n(unverschattet mit dunkler Deckung/Abdichtung)";

            }
            else if (main_model.MusterName == "OSB_KSD_PUR_FPO")
            {
                if (main_model.Selected == 0)
                {

                    BasisList.Add(new Basis()
                    {
                        ID_Sort = 1,
                        Bezeichnung = "OSB-Platten",
                        R = 0.169231,
                        Dicke = 0.022,
                        Wärmeleitfähigkeit = 0.130,
                        Rohdichte = 650,
                        Kapillar = true,
                        Holzwerkstoff = true,
                        KeineLuft = true,
                        Dampfdiffusionswiderstand_Min = 30.00,
                        Dampfdiffusionswiderstand_Max = 50.00,
                        Sd_Min = 0.66,
                        Sd_Max = 1.10,
                    });
                    BasisList.Add(new Basis()
                    {
                        ID_Sort = 2,
                        Bezeichnung = "KSD",
                        R = 0.008824,
                        Dicke = 0.0015,
                        Wärmeleitfähigkeit = 0.17,
                        Rohdichte = 1000.0,
                        Kapillar = true,
                        sonstiges = true,
                        KeineLuft = true,
                        Sd_Min = 1500.00,
                        Sd_Max = 1500.00,
                        Fester_sd = true,
                        Dampfdiffusionswiderstand_Min = 1000000.00,
                        Dampfdiffusionswiderstand_Max = 1000000.00
                    });
                    BasisList.Add(new Basis()
                    {
                        ID_Sort = 3,
                        Bezeichnung = "PUR 023",
                        R = 5.217391,
                        Fester_R = true,
                        Dicke = 0.12,
                        Wärmeleitfähigkeit = 0.023,
                        Rohdichte = 30.0,
                        Kapillar = true,
                        sonstiges = true,
                        EvntlLuft = true,
                        Sd_Min = 4.80,
                        Sd_Max = 24.00,
                        Dampfdiffusionswiderstand_Min = 40.00,
                        Dampfdiffusionswiderstand_Max = 200.00
                    });
                    BasisList.Add(new Basis()
                    {
                        ID_Sort = 4,
                        Bezeichnung = "FPO",
                        R = 0.000,
                        Fester_R = true,
                        Dicke = 0.0018,
                        Wärmeleitfähigkeit = 0.00,
                        Rohdichte = 1100.0,
                        sonstiges = true,
                        Kapillar = true,
                        KeineLuft = true,
                        Fester_sd = true,
                        Sd_Min = 180.000,
                        Sd_Max = 180.00,
                        Dampfdiffusionswiderstand_Min = 100000.00,
                        Dampfdiffusionswiderstand_Max = 100000.00
                    });
                    foreach (Basis i in BasisList)
                    {
                        i.ModelID = main_model.ID;
                    }
                    for (int i = 0; i < BasisList.Count; i++)
                    {
                        App.Database.SaveBauteilAsync(BasisList[i]);
                    }
                }
                Konstruktionstyp = "unbelüftetes Dach (Warmdach) \r\n(unverschattet mit dunkler Deckung/Abdichtung)";

            }
            else if (main_model.MusterName == "OSB_KSD_PUR_PVC")
            {
                if (main_model.Selected == 0)
                {

                    BasisList.Add(new Basis()
                    {
                        ID_Sort = 1,
                        Bezeichnung = "OSB-Platten",
                        R = 0.169231,
                        Dicke = 0.022,
                        Wärmeleitfähigkeit = 0.130,
                        Rohdichte = 650,
                        Kapillar = true,
                        Holzwerkstoff = true,
                        KeineLuft = true,
                        Dampfdiffusionswiderstand_Min = 30.00,
                        Dampfdiffusionswiderstand_Max = 50.00,
                        Sd_Min = 0.66,
                        Sd_Max = 1.10,
                    });
                    BasisList.Add(new Basis()
                    {
                        ID_Sort = 2,
                        Bezeichnung = "KSD",
                        R = 0.008824,
                        Dicke = 0.0015,
                        Wärmeleitfähigkeit = 0.17,
                        Rohdichte = 1000.0,
                        Kapillar = true,
                        sonstiges = true,
                        KeineLuft = true,
                        Sd_Min = 1500.00,
                        Sd_Max = 1500.00,
                        Fester_sd = true,
                        Dampfdiffusionswiderstand_Min = 1000000.00,
                        Dampfdiffusionswiderstand_Max = 1000000.00
                    });
                    BasisList.Add(new Basis()
                    {
                        ID_Sort = 3,
                        Bezeichnung = "PUR 023",
                        R = 5.217391,
                        Fester_R = true,
                        Dicke = 0.12,
                        Wärmeleitfähigkeit = 0.023,
                        Rohdichte = 30.0,
                        Kapillar = true,
                        sonstiges = true,
                        EvntlLuft = true,
                        Sd_Min = 4.80,
                        Sd_Max = 24.00,
                        Dampfdiffusionswiderstand_Min = 40.00,
                        Dampfdiffusionswiderstand_Max = 200.00
                    });
                    BasisList.Add(new Basis()
                    {
                        ID_Sort = 4,
                        Bezeichnung = "Glasvlies",
                        R = 0.0001,
                        Dicke = 0.001,
                        Wärmeleitfähigkeit = 10.00,
                        Rohdichte = 400.0,
                        sonstiges = true,
                        KeineLuft = true,
                        Sd_Min = 0.00,
                        Sd_Max = 0.00,
                        Dampfdiffusionswiderstand_Min = 1.00,
                        Dampfdiffusionswiderstand_Max = 2.00
                    });
                    BasisList.Add(new Basis()
                    {
                        ID_Sort = 5,
                        Bezeichnung = "PVC-P (DIN 16730)",
                        R = 0.00,
                        Fester_R = true,
                        Dicke = 0.0015,
                        Wärmeleitfähigkeit = 0.00,
                        Rohdichte = 1200.0,
                        sonstiges = true,
                        Kapillar = true,
                        KeineLuft = true,
                        Sd_Min = 15.000,
                        Sd_Max = 45.00,
                        Dampfdiffusionswiderstand_Min = 10000.00,
                        Dampfdiffusionswiderstand_Max = 30000.00
                    });
                    foreach (Basis i in BasisList)
                    {
                        i.ModelID = main_model.ID;
                    }
                    for (int i = 0; i < BasisList.Count; i++)
                    {
                        App.Database.SaveBauteilAsync(BasisList[i]);
                    }
                }
                Konstruktionstyp = "unbelüftetes Dach (Warmdach) \r\n(unverschattet mit dunkler Deckung/Abdichtung)";

            }
            else if (main_model.MusterName == "OSB_KSD_PUR_PYE_einlagig")
            {
                if (main_model.Selected == 0)
                {

                    BasisList.Add(new Basis()
                    {
                        ID_Sort = 1,
                        Bezeichnung = "OSB-Platten",
                        R = 0.169231,
                        Dicke = 0.022,
                        Wärmeleitfähigkeit = 0.130,
                        Rohdichte = 650,
                        Kapillar = true,
                        Holzwerkstoff = true,
                        KeineLuft = true,
                        Dampfdiffusionswiderstand_Min = 30.00,
                        Dampfdiffusionswiderstand_Max = 50.00,
                        Sd_Min = 0.66,
                        Sd_Max = 1.10,
                    });
                    BasisList.Add(new Basis()
                    {
                        ID_Sort = 2,
                        Bezeichnung = "KSD",
                        R = 0.008824,
                        Dicke = 0.0015,
                        Wärmeleitfähigkeit = 0.17,
                        Rohdichte = 1000.0,
                        Kapillar = true,
                        sonstiges = true,
                        KeineLuft = true,
                        Sd_Min = 1500.00,
                        Sd_Max = 1500.00,
                        Fester_sd = true,
                        Dampfdiffusionswiderstand_Min = 1000000.00,
                        Dampfdiffusionswiderstand_Max = 1000000.00
                    });
                    BasisList.Add(new Basis()
                    {
                        ID_Sort = 3,
                        Bezeichnung = "PUR 023, mit Stufenfalz",
                        R = 6.086957,
                        Dicke = 0.14,
                        Wärmeleitfähigkeit = 0.023,
                        Rohdichte = 30.0,
                        Kapillar = true,
                        sonstiges = true,
                        KeineLuft = true,
                        Sd_Min = 5.60,
                        Sd_Max = 28.00,
                        Dampfdiffusionswiderstand_Min = 40.00,
                        Dampfdiffusionswiderstand_Max = 200.00
                    });
                    BasisList.Add(new Basis()
                    {
                        ID_Sort = 4,
                        Bezeichnung = "Glasvlies",
                        R = 0.000100,
                        Dicke = 0.001,
                        Wärmeleitfähigkeit = 10.000000,
                        Rohdichte = 400.0,
                        sonstiges = true,
                        KeineLuft = true,
                        Sd_Min = 0.000,
                        Sd_Max = 0.00,
                        Dampfdiffusionswiderstand_Min = 1.0,
                        Dampfdiffusionswiderstand_Max = 1.0
                    });
                    BasisList.Add(new Basis()
                    {
                        ID_Sort = 5,
                        Bezeichnung = "PYE PV200 S5 beschiefert",
                        R = 0.00,
                        Dicke = 0.005,
                        Wärmeleitfähigkeit = 0.00,
                        Rohdichte = 1000.0,
                        Kapillar = true,
                        sonstiges = true,
                        KeineLuft = true,
                        Fester_R = true,
                        Sd_Min = 100.000,
                        Sd_Max = 150.00,
                        Dampfdiffusionswiderstand_Min = 20000,
                        Dampfdiffusionswiderstand_Max = 30000
                    });
                    foreach (Basis i in BasisList)
                    {
                        i.ModelID = main_model.ID;
                    }
                    for (int i = 0; i < BasisList.Count; i++)
                    {
                        App.Database.SaveBauteilAsync(BasisList[i]);
                    }
                }
                Konstruktionstyp = "unbelüftetes Dach (Warmdach) \r\n(unverschattet mit dunkler Deckung/Abdichtung)";

            }
            else if (main_model.MusterName == "OSB_KSD_PUR_PYE_zweilagig")
            {
                if (main_model.Selected == 0)
                {

                    BasisList.Add(new Basis()
                    {
                        ID_Sort = 1,
                        Bezeichnung = "OSB-Platten",
                        R = 0.169231,
                        Dicke = 0.022,
                        Wärmeleitfähigkeit = 0.130,
                        Rohdichte = 650,
                        Kapillar = true,
                        Holzwerkstoff = true,
                        KeineLuft = true,
                        Dampfdiffusionswiderstand_Min = 30.00,
                        Dampfdiffusionswiderstand_Max = 50.00,
                        Sd_Min = 0.66,
                        Sd_Max = 1.10,
                    });
                    BasisList.Add(new Basis()
                    {
                        ID_Sort = 2,
                        Bezeichnung = "KSD",
                        R = 0.008824,
                        Dicke = 0.0015,
                        Wärmeleitfähigkeit = 0.17,
                        Rohdichte = 1000.0,
                        Kapillar = true,
                        sonstiges = true,
                        KeineLuft = true,
                        Sd_Min = 1500.00,
                        Sd_Max = 1500.00,
                        Fester_sd = true,
                        Dampfdiffusionswiderstand_Min = 1000000.00,
                        Dampfdiffusionswiderstand_Max = 1000000.00
                    });
                    BasisList.Add(new Basis()
                    {
                        ID_Sort = 3,
                        Bezeichnung = "PUR 023, mit Stufenfalz",
                        R = 5.217391,
                        Dicke = 0.12,
                        Wärmeleitfähigkeit = 0.023,
                        Rohdichte = 30.0,
                        Kapillar = true,
                        sonstiges = true,
                        KeineLuft = true,
                        Sd_Min = 4.80,
                        Sd_Max = 24.00,
                        Dampfdiffusionswiderstand_Min = 40.00,
                        Dampfdiffusionswiderstand_Max = 200.00
                    });
                    BasisList.Add(new Basis()
                    {
                        ID_Sort = 4,
                        Bezeichnung = "PYE G200 S4",
                        R = 0.00,                        
                        Dicke = 0.004,
                        Wärmeleitfähigkeit = 0.00,
                        Rohdichte = 1000.0,
                        Kapillar = true,
                        sonstiges = true,
                        KeineLuft = true,
                        Fester_R = true,
                        Sd_Min = 80.000,
                        Sd_Max = 120.00,
                        Dampfdiffusionswiderstand_Min = 20000,
                        Dampfdiffusionswiderstand_Max = 30000
                    });
                    BasisList.Add(new Basis()
                    {
                        ID_Sort = 5,
                        Bezeichnung = "PYE PV200 S5 beschiefert",
                        R = 0.00,
                        Dicke = 0.005,
                        Wärmeleitfähigkeit = 0.00,
                        Rohdichte = 1000.0,
                        Kapillar = true,
                        sonstiges = true,
                        KeineLuft = true,
                        Fester_R = true,
                        Sd_Min = 100.000,
                        Sd_Max = 150.00,
                        Dampfdiffusionswiderstand_Min = 20000,
                        Dampfdiffusionswiderstand_Max = 30000
                    });
                    foreach (Basis i in BasisList)
                    {
                        i.ModelID = main_model.ID;
                    }
                    for (int i = 0; i < BasisList.Count; i++)
                    {
                        App.Database.SaveBauteilAsync(BasisList[i]);
                    }
                }
                Konstruktionstyp = "unbelüftetes Dach (Warmdach) \r\n(unverschattet mit dunkler Deckung/Abdichtung)";

            }

            //Trapezblech EPS
            else if (main_model.MusterName == "Trapezblech_KSD_EPS_EPDM")
            {
                if (main_model.Selected == 0)
                {

                    BasisList.Add(new Basis()
                    {
                        ID_Sort = 1,
                        Bezeichnung = "Trapezblech, Stahl, sd 10",
                        R = 0.000018,
                        Dicke = 0.0008,
                        Wärmeleitfähigkeit = 50.0,
                        Rohdichte = 7860.0,
                        Kapillar = true,
                        Holzwerkstoff = true,
                        KeineLuft = true,
                        Fester_sd = true,
                        Dampfdiffusionswiderstand_Min = 11363.64,
                        Dampfdiffusionswiderstand_Max = 11363.64,
                        Sd_Min = 10.0,
                        Sd_Max = 10.0,
                    });
                    BasisList.Add(new Basis()
                    {
                        ID_Sort = 2,
                        Bezeichnung = "KSD",
                        R = 0.008824,
                        Dicke = 0.0015,
                        Wärmeleitfähigkeit = 0.17,
                        Rohdichte = 1000.0,
                        Kapillar = true,
                        sonstiges = true,
                        KeineLuft = true,
                        Sd_Min = 1500.00,
                        Sd_Max = 1500.00,
                        Fester_sd = true,
                        Dampfdiffusionswiderstand_Min = 1000000.00,
                        Dampfdiffusionswiderstand_Max = 1000000.00
                    });
                    BasisList.Add(new Basis()
                    {
                        ID_Sort = 3,
                        Bezeichnung = "EPS 035, mit Stufenfalz",
                        R =  5.142857,
                        Dicke = 0.18,
                        Wärmeleitfähigkeit = 0.03500,
                        Rohdichte = 30.0,
                        Kapillar = true,
                        sonstiges = true,
                        KeineLuft = true,
                        Sd_Min = 3.6,
                        Sd_Max = 18.00,
                        Dampfdiffusionswiderstand_Min = 20.00,
                        Dampfdiffusionswiderstand_Max = 100.00
                    });
                    BasisList.Add(new Basis()
                    {
                        ID_Sort = 4,
                        Bezeichnung = "EPDM Dachbahn",
                        R = 0.00,
                        Dicke = 0.0013,
                        Wärmeleitfähigkeit = 0.00,
                        Rohdichte = 1150.0,
                        Kapillar = true,
                        sonstiges = true,
                        KeineLuft = true,
                        Fester_R = true,
                        Sd_Min = 78.00,
                        Sd_Max = 78.00,
                        Dampfdiffusionswiderstand_Min = 60000,
                        Dampfdiffusionswiderstand_Max = 60000
                    });
                    foreach (Basis i in BasisList)
                    {
                        i.ModelID = main_model.ID;
                    }
                    for (int i = 0; i < BasisList.Count; i++)
                    {
                        App.Database.SaveBauteilAsync(BasisList[i]);
                    }
                }
                Konstruktionstyp = "unbelüftetes Dach (Warmdach) \r\n(unverschattet mit dunkler Deckung/Abdichtung)";

            }
            else if (main_model.MusterName == "Trapezblech_KSD_EPS_Evalon")
            {
                if (main_model.Selected == 0)
                {

                    BasisList.Add(new Basis()
                    {
                        ID_Sort = 1,
                        Bezeichnung = "Trapezblech, Stahl, sd 10",
                        R = 0.000018,
                        Dicke = 0.0008,
                        Wärmeleitfähigkeit = 50.0,
                        Rohdichte = 7860.0,
                        Kapillar = true,
                        Holzwerkstoff = true,
                        KeineLuft = true,
                        Fester_sd = true,
                        Dampfdiffusionswiderstand_Min = 11363.64,
                        Dampfdiffusionswiderstand_Max = 11363.64,
                        Sd_Min = 10.0,
                        Sd_Max = 10.0,
                    });
                    BasisList.Add(new Basis()
                    {
                        ID_Sort = 2,
                        Bezeichnung = "KSD",
                        R = 0.008824,
                        Dicke = 0.0015,
                        Wärmeleitfähigkeit = 0.17,
                        Rohdichte = 1000.0,
                        Kapillar = true,
                        sonstiges = true,
                        KeineLuft = true,
                        Sd_Min = 1500.00,
                        Sd_Max = 1500.00,
                        Fester_sd = true,
                        Dampfdiffusionswiderstand_Min = 1000000.00,
                        Dampfdiffusionswiderstand_Max = 1000000.00
                    });
                    BasisList.Add(new Basis()
                    {
                        ID_Sort = 3,
                        Bezeichnung = "EPS 035, mit Stufenfalz",
                        R = 5.142857,
                        Dicke = 0.18,
                        Wärmeleitfähigkeit = 0.03500,
                        Rohdichte = 30.0,
                        Kapillar = true,
                        sonstiges = true,
                        KeineLuft = true,
                        Sd_Min = 3.6,
                        Sd_Max = 18.00,
                        Dampfdiffusionswiderstand_Min = 20.00,
                        Dampfdiffusionswiderstand_Max = 100.00
                    });
                    BasisList.Add(new Basis()
                    {
                        ID_Sort = 4,
                        Bezeichnung = "EVALON V",
                        R = 0.007059,
                        Dicke = 0.0012,
                        Wärmeleitfähigkeit = 0.17,
                        Rohdichte = 1250.0,
                        Kapillar = true,
                        sonstiges = true,
                        KeineLuft = true,
                        Sd_Min = 24.00,
                        Sd_Max = 24.00,
                        Dampfdiffusionswiderstand_Min = 20000,
                        Dampfdiffusionswiderstand_Max = 20000
                    });
                    foreach (Basis i in BasisList)
                    {
                        i.ModelID = main_model.ID;
                    }
                    for (int i = 0; i < BasisList.Count; i++)
                    {
                        App.Database.SaveBauteilAsync(BasisList[i]);
                    }
                }
                Konstruktionstyp = "unbelüftetes Dach (Warmdach) \r\n(unverschattet mit dunkler Deckung/Abdichtung)";

            }
            else if (main_model.MusterName == "Trapezblech_KSD_EPS_FPO")
            {
                if (main_model.Selected == 0)
                {

                    BasisList.Add(new Basis()
                    {
                        ID_Sort = 1,
                        Bezeichnung = "Trapezblech, Stahl, sd 10",
                        R = 0.000018,
                        Dicke = 0.0008,
                        Wärmeleitfähigkeit = 50.0,
                        Rohdichte = 7860.0,
                        Kapillar = true,
                        Holzwerkstoff = true,
                        KeineLuft = true,
                        Fester_sd = true,
                        Dampfdiffusionswiderstand_Min = 11363.64,
                        Dampfdiffusionswiderstand_Max = 11363.64,
                        Sd_Min = 10.0,
                        Sd_Max = 10.0,
                    });
                    BasisList.Add(new Basis()
                    {
                        ID_Sort = 2,
                        Bezeichnung = "KSD",
                        R = 0.008824,
                        Dicke = 0.0015,
                        Wärmeleitfähigkeit = 0.17,
                        Rohdichte = 1000.0,
                        Kapillar = true,
                        sonstiges = true,
                        KeineLuft = true,
                        Sd_Min = 1500.00,
                        Sd_Max = 1500.00,
                        Fester_sd = true,
                        Dampfdiffusionswiderstand_Min = 1000000.00,
                        Dampfdiffusionswiderstand_Max = 1000000.00
                    });
                    BasisList.Add(new Basis()
                    {
                        ID_Sort = 3,
                        Bezeichnung = "EPS 035, mit Stufenfalz",
                        R = 5.142857,
                        Dicke = 0.18,
                        Wärmeleitfähigkeit = 0.03500,
                        Rohdichte = 30.0,
                        Kapillar = true,
                        sonstiges = true,
                        KeineLuft = true,
                        Sd_Min = 3.6,
                        Sd_Max = 18.00,
                        Dampfdiffusionswiderstand_Min = 20.00,
                        Dampfdiffusionswiderstand_Max = 100.00
                    });
                    BasisList.Add(new Basis()
                    {
                        ID_Sort = 4,
                        Bezeichnung = "FPO",
                        R = 0.010588,
                        Dicke = 0.0018,
                        Wärmeleitfähigkeit = 0.017,
                        Rohdichte = 1100.0,
                        sonstiges = true,
                        Kapillar = true,
                        KeineLuft = true,
                        Fester_sd = true,
                        Sd_Min = 180.000,
                        Sd_Max = 180.00,
                        Dampfdiffusionswiderstand_Min = 100000.00,
                        Dampfdiffusionswiderstand_Max = 100000.00
                    });
                    foreach (Basis i in BasisList)
                    {
                        i.ModelID = main_model.ID;
                    }
                    for (int i = 0; i < BasisList.Count; i++)
                    {
                        App.Database.SaveBauteilAsync(BasisList[i]);
                    }
                }
                Konstruktionstyp = "unbelüftetes Dach (Warmdach) \r\n(unverschattet mit dunkler Deckung/Abdichtung)";

            }
            else if (main_model.MusterName == "Trapezblech_KSD_EPS_PVC")
            {
                if (main_model.Selected == 0)
                {

                    BasisList.Add(new Basis()
                    {
                        ID_Sort = 1,
                        Bezeichnung = "Trapezblech, Stahl, sd 10",
                        R = 0.000018,
                        Dicke = 0.0008,
                        Wärmeleitfähigkeit = 50.0,
                        Rohdichte = 7860.0,
                        Kapillar = true,
                        Holzwerkstoff = true,
                        KeineLuft = true,
                        Fester_sd = true,
                        Dampfdiffusionswiderstand_Min = 11363.64,
                        Dampfdiffusionswiderstand_Max = 11363.64,
                        Sd_Min = 10.0,
                        Sd_Max = 10.0,
                    });
                    BasisList.Add(new Basis()
                    {
                        ID_Sort = 2,
                        Bezeichnung = "KSD",
                        R = 0.008824,
                        Dicke = 0.0015,
                        Wärmeleitfähigkeit = 0.17,
                        Rohdichte = 1000.0,
                        Kapillar = true,
                        sonstiges = true,
                        KeineLuft = true,
                        Sd_Min = 1500.00,
                        Sd_Max = 1500.00,
                        Fester_sd = true,
                        Dampfdiffusionswiderstand_Min = 1000000.00,
                        Dampfdiffusionswiderstand_Max = 1000000.00
                    });
                    BasisList.Add(new Basis()
                    {
                        ID_Sort = 3,
                        Bezeichnung = "EPS 035, mit Stufenfalz",
                        R =  5.142857,
                        Dicke = 0.18,
                        Wärmeleitfähigkeit = 0.03500,
                        Rohdichte = 30.0,
                        Kapillar = true,
                        sonstiges = true,
                        KeineLuft = true,
                        Sd_Min = 3.6,
                        Sd_Max = 18.00,
                        Dampfdiffusionswiderstand_Min = 20.00,
                        Dampfdiffusionswiderstand_Max = 100.00
                    });
                    BasisList.Add(new Basis()
                    {
                        ID_Sort = 4,
                        Bezeichnung = "Rohglasvlies",
                        R = 0.000100,
                        Dicke = 0.001,
                        Wärmeleitfähigkeit = 10.000000,
                        Rohdichte = 400.0,
                        sonstiges = true,
                        KeineLuft = true,
                        Sd_Min = 0.000,
                        Sd_Max = 0.00,
                        Dampfdiffusionswiderstand_Min = 1.0,
                        Dampfdiffusionswiderstand_Max = 1.0
                    });
                    BasisList.Add(new Basis()
                    {
                        ID_Sort = 5,
                        Bezeichnung = "PVC-P (DIN 16730)",
                        R = 0.008824,
                        Dicke = 0.0015,
                        Wärmeleitfähigkeit = 0.17,
                        Rohdichte = 1200.0,
                        sonstiges = true,
                        Kapillar = true,
                        KeineLuft = true,
                        Fester_R = true,
                        Sd_Min = 15.000,
                        Sd_Max = 45.00,
                        Dampfdiffusionswiderstand_Min = 10000.00,
                        Dampfdiffusionswiderstand_Max = 30000.00
                    });
                    foreach (Basis i in BasisList)
                    {
                        i.ModelID = main_model.ID;
                    }
                    for (int i = 0; i < BasisList.Count; i++)
                    {
                        App.Database.SaveBauteilAsync(BasisList[i]);
                    }
                }
                Konstruktionstyp = "unbelüftetes Dach (Warmdach) \r\n(unverschattet mit dunkler Deckung/Abdichtung)";

            }
            else if (main_model.MusterName == "Trapezblech_KSD_EPS_PYE_einlagig")
            {
                if (main_model.Selected == 0)
                {

                    BasisList.Add(new Basis()
                    {
                        ID_Sort = 1,
                        Bezeichnung = "Trapezblech, Stahl, sd 10",
                        R = 0.000018,
                        Dicke = 0.0008,
                        Wärmeleitfähigkeit = 50.0,
                        Rohdichte = 7860.0,
                        Kapillar = true,
                        Holzwerkstoff = true,
                        KeineLuft = true,
                        Fester_sd = true,
                        Dampfdiffusionswiderstand_Min = 11363.64,
                        Dampfdiffusionswiderstand_Max = 11363.64,
                        Sd_Min = 10.0,
                        Sd_Max = 10.0,
                    });
                    BasisList.Add(new Basis()
                    {
                        ID_Sort = 2,
                        Bezeichnung = "KSD",
                        R = 0.008824,
                        Dicke = 0.0015,
                        Wärmeleitfähigkeit = 0.17,
                        Rohdichte = 1000.0,
                        Kapillar = true,
                        sonstiges = true,
                        KeineLuft = true,
                        Sd_Min = 1500.00,
                        Sd_Max = 1500.00,
                        Fester_sd = true,
                        Dampfdiffusionswiderstand_Min = 1000000.00,
                        Dampfdiffusionswiderstand_Max = 1000000.00
                    });
                    BasisList.Add(new Basis()
                    {
                        ID_Sort = 3,
                        Bezeichnung = "EPS 035, mit Stufenfalz",
                        R = 5.142857,
                        Dicke = 0.18,
                        Wärmeleitfähigkeit = 0.03500,
                        Rohdichte = 30.0,
                        Kapillar = true,
                        sonstiges = true,
                        KeineLuft = true,
                        Sd_Min = 3.6,
                        Sd_Max = 18.00,
                        Dampfdiffusionswiderstand_Min = 20.00,
                        Dampfdiffusionswiderstand_Max = 100.00
                    });                   
                    BasisList.Add(new Basis()
                    {
                        ID_Sort = 4,
                        Bezeichnung = "PYE PV200 S5 beschiefert",
                        R = 0.029412,
                        Dicke = 0.005,
                        Wärmeleitfähigkeit = 0.17,
                        Rohdichte = 1000.0,
                        Kapillar = true,
                        sonstiges = true,
                        KeineLuft = true,
                        Sd_Min = 100.000,
                        Sd_Max = 150.00,
                        Dampfdiffusionswiderstand_Min = 20000,
                        Dampfdiffusionswiderstand_Max = 30000
                    });
                    foreach (Basis i in BasisList)
                    {
                        i.ModelID = main_model.ID;
                    }
                    for (int i = 0; i < BasisList.Count; i++)
                    {
                        App.Database.SaveBauteilAsync(BasisList[i]);
                    }
                }
                Konstruktionstyp = "unbelüftetes Dach (Warmdach) \r\n(unverschattet mit dunkler Deckung/Abdichtung)";

            }
            else if (main_model.MusterName == "Trapezblech_KSD_EPS_PYE_zweilagig")
            {
                if (main_model.Selected == 0)
                {

                    BasisList.Add(new Basis()
                    {
                        ID_Sort = 1,
                        Bezeichnung = "Trapezblech, Stahl, sd 10",
                        R = 0.000018,
                        Dicke = 0.0008,
                        Wärmeleitfähigkeit = 50.0,
                        Rohdichte = 7860.0,
                        Kapillar = true,
                        Holzwerkstoff = true,
                        KeineLuft = true,
                        Fester_sd = true,
                        Dampfdiffusionswiderstand_Min = 11363.64,
                        Dampfdiffusionswiderstand_Max = 11363.64,
                        Sd_Min = 10.0,
                        Sd_Max = 10.0,
                    });
                    BasisList.Add(new Basis()
                    {
                        ID_Sort = 2,
                        Bezeichnung = "KSD",
                        R = 0.008824,
                        Dicke = 0.0015,
                        Wärmeleitfähigkeit = 0.17,
                        Rohdichte = 1000.0,
                        Kapillar = true,
                        sonstiges = true,
                        KeineLuft = true,
                        Sd_Min = 1500.00,
                        Sd_Max = 1500.00,
                        Fester_sd = true,
                        Dampfdiffusionswiderstand_Min = 1000000.00,
                        Dampfdiffusionswiderstand_Max = 1000000.00
                    });
                    BasisList.Add(new Basis()
                    {
                        ID_Sort = 3,
                        Bezeichnung = "EPS 035, mit Stufenfalz",
                        R =  5.142857,
                        Dicke = 0.18,
                        Wärmeleitfähigkeit = 0.035,
                        Rohdichte = 30.0,
                        Kapillar = true,
                        sonstiges = true,
                        KeineLuft = true,
                        Sd_Min = 3.6,
                        Sd_Max = 18.00,
                        Dampfdiffusionswiderstand_Min = 20.00,
                        Dampfdiffusionswiderstand_Max = 100.00
                    });
                    BasisList.Add(new Basis()
                    {
                        ID_Sort = 4,
                        Bezeichnung = "PYE G200 S4",
                        R = 0.023529,
                        Dicke = 0.004,
                        Wärmeleitfähigkeit = 0.17,
                        Rohdichte = 1200.0,
                        Kapillar = true,
                        sonstiges = true,
                        KeineLuft = true,
                        Sd_Min = 86.000,
                        Sd_Max = 86.00,
                        Dampfdiffusionswiderstand_Min = 21500,
                        Dampfdiffusionswiderstand_Max = 21500
                    });
                    BasisList.Add(new Basis()
                    {
                        ID_Sort = 4,
                        Bezeichnung = "PYE PV200 S5 beschiefert",
                        R = 0.029412,
                        Dicke = 0.005,
                        Wärmeleitfähigkeit = 0.17,
                        Rohdichte = 1000.0,
                        Kapillar = true,
                        sonstiges = true,
                        KeineLuft = true,
                        Sd_Min = 100.000,
                        Sd_Max = 150.00,
                        Dampfdiffusionswiderstand_Min = 20000,
                        Dampfdiffusionswiderstand_Max = 30000
                    });
                    foreach (Basis i in BasisList)
                    {
                        i.ModelID = main_model.ID;
                    }
                    for (int i = 0; i < BasisList.Count; i++)
                    {
                        App.Database.SaveBauteilAsync(BasisList[i]);
                    }
                }
                Konstruktionstyp = "unbelüftetes Dach (Warmdach) \r\n(unverschattet mit dunkler Deckung/Abdichtung)";

            }

            //Trapezblech MIFA KSD
            else if (main_model.MusterName == "Trapezblech_KSD_MIFA_EPDM")
            {
                if (main_model.Selected == 0)
                {

                    BasisList.Add(new Basis()
                    {
                        ID_Sort = 1,
                        Bezeichnung = "Trapezblech, Stahl, sd 10",
                        R = 0.000018,
                        Dicke = 0.0008,
                        Wärmeleitfähigkeit = 50.0,
                        Rohdichte = 7860.0,
                        Kapillar = true,
                        Holzwerkstoff = true,
                        KeineLuft = true,
                        Fester_sd = true,
                        Dampfdiffusionswiderstand_Min = 11363.64,
                        Dampfdiffusionswiderstand_Max = 11363.64,
                        Sd_Min = 10.0,
                        Sd_Max = 10.0,
                    });
                    BasisList.Add(new Basis()
                    {
                        ID_Sort = 2,
                        Bezeichnung = "KSD",
                        R = 0.008824,
                        Dicke = 0.0015,
                        Wärmeleitfähigkeit = 0.17,
                        Rohdichte = 1000.0,
                        Kapillar = true,
                        sonstiges = true,
                        KeineLuft = true,
                        Sd_Min = 1500.00,
                        Sd_Max = 1500.00,
                        Fester_sd = true,
                        Dampfdiffusionswiderstand_Min = 1000000.00,
                        Dampfdiffusionswiderstand_Max = 1000000.00
                    });
                    BasisList.Add(new Basis()
                    {
                        ID_Sort = 3,
                        Bezeichnung = "Mineralfaser 038",
                        R = 5.263158,
                        Dicke = 0.2,
                        Wärmeleitfähigkeit = 0.038,
                        Rohdichte = 120.0,
                        Kapillar = true,
                        sonstiges = true,
                        EvntlLuft = true,
                        Sd_Min = 0.20,
                        Sd_Max = 0.20,
                        Dampfdiffusionswiderstand_Min = 1.00,
                        Dampfdiffusionswiderstand_Max = 1.00
                    });
                    BasisList.Add(new Basis()
                    {
                        ID_Sort = 4,
                        Bezeichnung = "EPDM",
                        R = 0.008,
                        Dicke = 0.002,
                        Wärmeleitfähigkeit = 0.25,
                        Rohdichte = 1150.0,
                        Kapillar = true,
                        sonstiges = true,
                        KeineLuft = true,
                        Sd_Min = 120.00,
                        Sd_Max = 120.00,
                        Dampfdiffusionswiderstand_Min = 60000,
                        Dampfdiffusionswiderstand_Max = 60000
                    });
                    foreach (Basis i in BasisList)
                    {
                        i.ModelID = main_model.ID;
                    }
                    for (int i = 0; i < BasisList.Count; i++)
                    {
                        App.Database.SaveBauteilAsync(BasisList[i]);
                    }
                }
                Konstruktionstyp = "unbelüftetes Dach (Warmdach) \r\n(unverschattet mit dunkler Deckung/Abdichtung)";

            }
            else if (main_model.MusterName == "Trapezblech_KSD_MIFA_Evalon")
            {
                if (main_model.Selected == 0)
                {

                    BasisList.Add(new Basis()
                    {
                        ID_Sort = 1,
                        Bezeichnung = "Trapezblech, Stahl, sd 10",
                        R = 0.000018,
                        Dicke = 0.0008,
                        Wärmeleitfähigkeit = 50.0,
                        Rohdichte = 7860.0,
                        Kapillar = true,
                        Holzwerkstoff = true,
                        KeineLuft = true,
                        Fester_sd = true,
                        Dampfdiffusionswiderstand_Min = 11363.64,
                        Dampfdiffusionswiderstand_Max = 11363.64,
                        Sd_Min = 10.0,
                        Sd_Max = 10.0,
                    });
                    BasisList.Add(new Basis()
                    {
                        ID_Sort = 2,
                        Bezeichnung = "KSD",
                        R = 0.008824,
                        Dicke = 0.0015,
                        Wärmeleitfähigkeit = 0.17,
                        Rohdichte = 1000.0,
                        Kapillar = true,
                        sonstiges = true,
                        KeineLuft = true,
                        Sd_Min = 1500.00,
                        Sd_Max = 1500.00,
                        Fester_sd = true,
                        Dampfdiffusionswiderstand_Min = 1000000.00,
                        Dampfdiffusionswiderstand_Max = 1000000.00
                    });
                    BasisList.Add(new Basis()
                    {
                        ID_Sort = 3,
                        Bezeichnung = "Mineralfaser 038",
                        R = 5.263158,
                        Dicke = 0.2,
                        Wärmeleitfähigkeit = 0.038,
                        Rohdichte = 120.0,
                        Kapillar = true,
                        sonstiges = true,
                        EvntlLuft = true,
                        Sd_Min = 0.20,
                        Sd_Max = 0.20,
                        Dampfdiffusionswiderstand_Min = 1.00,
                        Dampfdiffusionswiderstand_Max = 1.00
                    });
                    BasisList.Add(new Basis()
                    {
                        ID_Sort = 4,
                        Bezeichnung = "EVALASTIC",
                        R = 0.007059,
                        Dicke = 0.0012,
                        Wärmeleitfähigkeit = 0.17,
                        Rohdichte = 1000.0,
                        Kapillar = true,
                        sonstiges = true,
                        KeineLuft = true,
                        Sd_Min = 67.20,
                        Sd_Max = 69.60,
                        Dampfdiffusionswiderstand_Min = 560000,
                        Dampfdiffusionswiderstand_Max = 580000
                    });
                    foreach (Basis i in BasisList)
                    {
                        i.ModelID = main_model.ID;
                    }
                    for (int i = 0; i < BasisList.Count; i++)
                    {
                        App.Database.SaveBauteilAsync(BasisList[i]);
                    }
                }
                Konstruktionstyp = "unbelüftetes Dach (Warmdach) \r\n(unverschattet mit dunkler Deckung/Abdichtung)";

            }
            else if (main_model.MusterName == "Trapezblech_KSD_MIFA_FPO")
            {
                if (main_model.Selected == 0)
                {

                    BasisList.Add(new Basis()
                    {
                        ID_Sort = 1,
                        Bezeichnung = "Trapezblech, Stahl, sd 10",
                        R = 0.000018,
                        Dicke = 0.0008,
                        Wärmeleitfähigkeit = 50.0,
                        Rohdichte = 7860.0,
                        Kapillar = true,
                        Holzwerkstoff = true,
                        KeineLuft = true,
                        Fester_sd = true,
                        Dampfdiffusionswiderstand_Min = 11363.64,
                        Dampfdiffusionswiderstand_Max = 11363.64,
                        Sd_Min = 10.0,
                        Sd_Max = 10.0,
                    });
                    BasisList.Add(new Basis()
                    {
                        ID_Sort = 2,
                        Bezeichnung = "KSD",
                        R = 0.008824,
                        Dicke = 0.0015,
                        Wärmeleitfähigkeit = 0.17,
                        Rohdichte = 1000.0,
                        Kapillar = true,
                        sonstiges = true,
                        KeineLuft = true,
                        Sd_Min = 1500.00,
                        Sd_Max = 1500.00,
                        Fester_sd = true,
                        Dampfdiffusionswiderstand_Min = 1000000.00,
                        Dampfdiffusionswiderstand_Max = 1000000.00
                    });
                    BasisList.Add(new Basis()
                    {
                        ID_Sort = 3,
                        Bezeichnung = "Mineralfaser 038",
                        R = 5.263158,
                        Dicke = 0.2,
                        Wärmeleitfähigkeit = 0.038,
                        Rohdichte = 120.0,
                        Kapillar = true,
                        sonstiges = true,
                        EvntlLuft = true,
                        Sd_Min = 0.20,
                        Sd_Max = 0.20,
                        Dampfdiffusionswiderstand_Min = 1.00,
                        Dampfdiffusionswiderstand_Max = 1.00
                    });
                    BasisList.Add(new Basis()
                    {
                        ID_Sort = 4,
                        Bezeichnung = "FPO",
                        R = 0.010588,
                        Dicke = 0.0018,
                        Wärmeleitfähigkeit = 0.17,
                        Rohdichte = 1100.0,
                        sonstiges = true,
                        Kapillar = true,
                        KeineLuft = true,
                        Fester_sd = true,
                        Sd_Min = 180.000,
                        Sd_Max = 180.00,
                        Dampfdiffusionswiderstand_Min = 100000.00,
                        Dampfdiffusionswiderstand_Max = 100000.00
                    });
                    foreach (Basis i in BasisList)
                    {
                        i.ModelID = main_model.ID;
                    }
                    for (int i = 0; i < BasisList.Count; i++)
                    {
                        App.Database.SaveBauteilAsync(BasisList[i]);
                    }
                }
                Konstruktionstyp = "unbelüftetes Dach (Warmdach) \r\n(unverschattet mit dunkler Deckung/Abdichtung)";

            }
            else if (main_model.MusterName == "Trapezblech_KSD_MIFA_PVC")
            {
                if (main_model.Selected == 0)
                {

                    BasisList.Add(new Basis()
                    {
                        ID_Sort = 1,
                        Bezeichnung = "Trapezblech, Stahl, sd 10",
                        R = 0.000018,
                        Dicke = 0.0008,
                        Wärmeleitfähigkeit = 50.0,
                        Rohdichte = 7860.0,
                        Kapillar = true,
                        Holzwerkstoff = true,
                        KeineLuft = true,
                        Fester_sd = true,
                        Dampfdiffusionswiderstand_Min = 11363.64,
                        Dampfdiffusionswiderstand_Max = 11363.64,
                        Sd_Min = 10.0,
                        Sd_Max = 10.0,
                    });
                    BasisList.Add(new Basis()
                    {
                        ID_Sort = 2,
                        Bezeichnung = "KSD",
                        R = 0.008824,
                        Dicke = 0.0015,
                        Wärmeleitfähigkeit = 0.17,
                        Rohdichte = 1000.0,
                        Kapillar = true,
                        sonstiges = true,
                        KeineLuft = true,
                        Sd_Min = 1500.00,
                        Sd_Max = 1500.00,
                        Fester_sd = true,
                        Dampfdiffusionswiderstand_Min = 1000000.00,
                        Dampfdiffusionswiderstand_Max = 1000000.00
                    });
                    BasisList.Add(new Basis()
                    {
                        ID_Sort = 3,
                        Bezeichnung = "Mineralfaser 038",
                        R = 5.263158,
                        Dicke = 0.2,
                        Wärmeleitfähigkeit = 0.038,
                        Rohdichte = 120.0,
                        Kapillar = true,
                        sonstiges = true,
                        EvntlLuft = true,
                        Sd_Min = 0.20,
                        Sd_Max = 0.20,
                        Dampfdiffusionswiderstand_Min = 1.00,
                        Dampfdiffusionswiderstand_Max = 1.00
                    });
                    BasisList.Add(new Basis()
                    {
                        ID_Sort = 4,
                        Bezeichnung = "Glasvlies",
                        R = 0.0001,
                        Wärmeleitfähigkeit = 10.0,
                        Dicke = 0.001,
                        sonstiges = true,
                        Rohdichte = 400.00,
                        Dampfdiffusionswiderstand_Min = 1.0,
                        Dampfdiffusionswiderstand_Max = 1.0,
                        Sd_Min = 0.00,
                        Sd_Max = 0.00,
                    });
                    BasisList.Add(new Basis()
                    {
                        ID_Sort = 5,
                        Bezeichnung = "PVC-P (DIN 16730)",
                        R = 0.008824,
                        Dicke = 0.0015,
                        Wärmeleitfähigkeit = 0.17,
                        Rohdichte = 1200.0,
                        sonstiges = true,
                        Kapillar = true,
                        KeineLuft = true,
                        Sd_Min = 15.000,
                        Sd_Max = 45.00,
                        Dampfdiffusionswiderstand_Min = 10000.00,
                        Dampfdiffusionswiderstand_Max = 30000.00
                    });
                    foreach (Basis i in BasisList)
                    {
                        i.ModelID = main_model.ID;
                    }
                    for (int i = 0; i < BasisList.Count; i++)
                    {
                        App.Database.SaveBauteilAsync(BasisList[i]);
                    }
                }
                Konstruktionstyp = "unbelüftetes Dach (Warmdach) \r\n(unverschattet mit dunkler Deckung/Abdichtung)";

            }
            else if (main_model.MusterName == "Trapezblech_KSD_MIFA_PYE_einlagig")
            {
                if (main_model.Selected == 0)
                {

                    BasisList.Add(new Basis()
                    {
                        ID_Sort = 1,
                        Bezeichnung = "Trapezblech, Stahl, sd 10",
                        R = 0.000018,
                        Dicke = 0.0008,
                        Wärmeleitfähigkeit = 50.0,
                        Rohdichte = 7860.0,
                        Kapillar = true,
                        Holzwerkstoff = true,
                        KeineLuft = true,
                        Fester_sd = true,
                        Dampfdiffusionswiderstand_Min = 11363.64,
                        Dampfdiffusionswiderstand_Max = 11363.64,
                        Sd_Min = 10.0,
                        Sd_Max = 10.0,
                    });
                    BasisList.Add(new Basis()
                    {
                        ID_Sort = 2,
                        Bezeichnung = "KSD",
                        R = 0.008824,
                        Fester_R = true,
                        Dicke = 0.0015,
                        Wärmeleitfähigkeit = 0.17,
                        Rohdichte = 1000.0,
                        Kapillar = true,
                        sonstiges = true,
                        KeineLuft = true,
                        Sd_Min = 1500.00,
                        Sd_Max = 1500.00,
                        Fester_sd = true,
                        Dampfdiffusionswiderstand_Min = 1000000.00,
                        Dampfdiffusionswiderstand_Max = 1000000.00
                    });
                    BasisList.Add(new Basis()
                    {
                        ID_Sort = 3,
                        Bezeichnung = "Mineralfaser 038",
                        R = 5.263158,
                        Dicke = 0.2,
                        Wärmeleitfähigkeit = 0.038,
                        Rohdichte = 120.0,
                        Kapillar = true,
                        sonstiges = true,
                        EvntlLuft = true,
                        Sd_Min = 0.20,
                        Sd_Max = 0.20,
                        Dampfdiffusionswiderstand_Min = 1.00,
                        Dampfdiffusionswiderstand_Max = 1.00
                    });
                    BasisList.Add(new Basis()
                    {
                        ID_Sort = 4,
                        Bezeichnung = "Glasvlies",
                        R = 0.000100,
                        Dicke = 0.001,
                        Wärmeleitfähigkeit = 10.000000,
                        Rohdichte = 400.0,
                        sonstiges = true,
                        KeineLuft = true,
                        Sd_Min = 0.000,
                        Sd_Max = 0.00,
                        Dampfdiffusionswiderstand_Min = 1.0,
                        Dampfdiffusionswiderstand_Max = 1.0
                    });
                    BasisList.Add(new Basis()
                    {
                        ID_Sort = 5,
                        Bezeichnung = "PYE PV200 S5 beschiefert",
                        R = 0.029412,
                        Dicke = 0.005,
                        Wärmeleitfähigkeit = 0.17,
                        Rohdichte = 1000.0,
                        Kapillar = true,
                        sonstiges = true,
                        KeineLuft = true,
                        Sd_Min = 100.000,
                        Sd_Max = 150.00,
                        Dampfdiffusionswiderstand_Min = 20000,
                        Dampfdiffusionswiderstand_Max = 30000
                    });
                    foreach (Basis i in BasisList)
                    {
                        i.ModelID = main_model.ID;
                    }
                    for (int i = 0; i < BasisList.Count; i++)
                    {
                        App.Database.SaveBauteilAsync(BasisList[i]);
                    }
                }
                Konstruktionstyp = "unbelüftetes Dach (Warmdach) \r\n(unverschattet mit dunkler Deckung/Abdichtung)";

            }
            else if (main_model.MusterName == "Trapezblech_KSD_MIFA_PYE_zweilagig")
            {
                if (main_model.Selected == 0)
                {

                    BasisList.Add(new Basis()
                    {
                        ID_Sort = 1,
                        Bezeichnung = "Trapezblech, Stahl, sd 10",
                        R = 0.000018,
                        Dicke = 0.0008,
                        Wärmeleitfähigkeit = 50.0,
                        Rohdichte = 7860.0,
                        Kapillar = true,
                        Holzwerkstoff = true,
                        KeineLuft = true,
                        Fester_sd = true,
                        Dampfdiffusionswiderstand_Min = 11363.64,
                        Dampfdiffusionswiderstand_Max = 11363.64,
                        Sd_Min = 10.0,
                        Sd_Max = 10.0,
                    });
                    BasisList.Add(new Basis()
                    {
                        ID_Sort = 2,
                        Bezeichnung = "KSD",
                        R = 0.008824,
                        Fester_R = true,
                        Dicke = 0.0015,
                        Wärmeleitfähigkeit = 0.17,
                        Rohdichte = 1000.0,
                        Kapillar = true,
                        sonstiges = true,
                        KeineLuft = true,
                        Sd_Min = 1500.00,
                        Sd_Max = 1500.00,
                        Fester_sd = true,
                        Dampfdiffusionswiderstand_Min = 1000000.00,
                        Dampfdiffusionswiderstand_Max = 1000000.00
                    });
                    BasisList.Add(new Basis()
                    {
                        ID_Sort = 3,
                        Bezeichnung = "Mineralfaser 038",
                        R = 5.263158,
                        Dicke = 0.2,
                        Wärmeleitfähigkeit = 0.038,
                        Rohdichte = 120.0,
                        Kapillar = true,
                        sonstiges = true,
                        EvntlLuft = true,
                        Sd_Min = 0.20,
                        Sd_Max = 0.20,
                        Dampfdiffusionswiderstand_Min = 1.00,
                        Dampfdiffusionswiderstand_Max = 1.00
                    });
                    BasisList.Add(new Basis()
                    {
                        ID_Sort = 4,
                        Bezeichnung = "Glasvlies",
                        R = 0.000100,
                        Dicke = 0.001,
                        Wärmeleitfähigkeit = 10.000000,
                        Rohdichte = 400.0,
                        sonstiges = true,
                        KeineLuft = true,
                        Sd_Min = 0.000,
                        Sd_Max = 0.00,
                        Dampfdiffusionswiderstand_Min = 1.0,
                        Dampfdiffusionswiderstand_Max = 1.0
                    });
                    BasisList.Add(new Basis()
                    {
                        ID_Sort = 5,
                        Bezeichnung = "PYE PV200 S4",
                        R = 0.023529,
                        Dicke = 0.004,
                        Wärmeleitfähigkeit = 0.17,
                        Rohdichte = 1200.0,
                        sonstiges = true,
                        Kapillar = true,
                        KeineLuft = true,
                        Sd_Min = 86.000,
                        Sd_Max = 86.00,
                        Dampfdiffusionswiderstand_Min = 21500.0,
                        Dampfdiffusionswiderstand_Max = 21500.0
                    });
                    BasisList.Add(new Basis()
                    {
                        ID_Sort = 6,
                        Bezeichnung = "PYE PV200 S5 beschiefert",
                        R = 0.029412,
                        Dicke = 0.005,
                        Wärmeleitfähigkeit = 0.17,
                        Rohdichte = 1000.0,
                        Kapillar = true,
                        sonstiges = true,
                        KeineLuft = true,
                        Sd_Min = 100.000,
                        Sd_Max = 150.00,
                        Dampfdiffusionswiderstand_Min = 20000,
                        Dampfdiffusionswiderstand_Max = 30000
                    });
                    foreach (Basis i in BasisList)
                    {
                        i.ModelID = main_model.ID;
                    }
                    for (int i = 0; i < BasisList.Count; i++)
                    {
                        App.Database.SaveBauteilAsync(BasisList[i]);
                    }
                }
                Konstruktionstyp = "unbelüftetes Dach (Warmdach) \r\n(unverschattet mit dunkler Deckung/Abdichtung)";

            }

            //Trapezblech PUR
            else if (main_model.MusterName == "Trapezblech_KSD_PUR_EPDM")
            {
                if (main_model.Selected == 0)
                {

                    BasisList.Add(new Basis()
                    {
                        ID_Sort = 1,
                        Bezeichnung = "Trapezblech, Stahl, sd 10",
                        R = 0.000018,
                        Dicke = 0.0008,
                        Wärmeleitfähigkeit = 50.0,
                        Rohdichte = 7860.0,
                        Kapillar = true,
                        Holzwerkstoff = true,
                        KeineLuft = true,
                        Fester_sd = true,
                        Dampfdiffusionswiderstand_Min = 11363.64,
                        Dampfdiffusionswiderstand_Max = 11363.64,
                        Sd_Min = 10.0,
                        Sd_Max = 10.0,
                    });
                    BasisList.Add(new Basis()
                    {
                        ID_Sort = 2,
                        Bezeichnung = "KSD",
                        R = 0.008824,
                        Dicke = 0.0015,
                        Wärmeleitfähigkeit = 0.17,
                        Rohdichte = 1000.0,
                        Kapillar = true,
                        sonstiges = true,
                        KeineLuft = true,
                        Sd_Min = 1500.00,
                        Sd_Max = 1500.00,
                        Fester_sd = true,
                        Dampfdiffusionswiderstand_Min = 1000000.00,
                        Dampfdiffusionswiderstand_Max = 1000000.00
                    });
                    BasisList.Add(new Basis()
                    {
                        ID_Sort = 3,
                        Bezeichnung = "PUR 023",
                        R = 5.217391,
                        Fester_R = true,
                        Dicke = 0.12,
                        Wärmeleitfähigkeit = 0.023,
                        Rohdichte = 30.0,
                        Kapillar = true,
                        sonstiges = true,
                        EvntlLuft = true,
                        Sd_Min = 4.80,
                        Sd_Max = 24.00,
                        Dampfdiffusionswiderstand_Min = 40.00,
                        Dampfdiffusionswiderstand_Max = 200.00
                    });
                    BasisList.Add(new Basis()
                    {
                        ID_Sort = 4,
                        Bezeichnung = "EPDM",
                        R = 0.008,
                        Dicke = 0.002,
                        Wärmeleitfähigkeit = 0.25,
                        Rohdichte = 1150.0,
                        Kapillar = true,
                        sonstiges = true,
                        KeineLuft = true,
                        Sd_Min = 120.00,
                        Sd_Max = 120.00,
                        Dampfdiffusionswiderstand_Min = 60000,
                        Dampfdiffusionswiderstand_Max = 60000
                    });
                    foreach (Basis i in BasisList)
                    {
                        i.ModelID = main_model.ID;
                    }
                    for (int i = 0; i < BasisList.Count; i++)
                    {
                        App.Database.SaveBauteilAsync(BasisList[i]);
                    }
                }
                Konstruktionstyp = "unbelüftetes Dach (Warmdach) \r\n(unverschattet mit dunkler Deckung/Abdichtung)";

            }
            else if (main_model.MusterName == "Trapezblech_KSD_PUR_Evalon")
            {
                if (main_model.Selected == 0)
                {

                    BasisList.Add(new Basis()
                    {
                        ID_Sort = 1,
                        Bezeichnung = "Trapezblech, Stahl, sd 10",
                        R = 0.000018,
                        Dicke = 0.0008,
                        Wärmeleitfähigkeit = 50.0,
                        Rohdichte = 7860.0,
                        Kapillar = true,
                        Holzwerkstoff = true,
                        KeineLuft = true,
                        Fester_sd = true,
                        Dampfdiffusionswiderstand_Min = 11363.64,
                        Dampfdiffusionswiderstand_Max = 11363.64,
                        Sd_Min = 10.0,
                        Sd_Max = 10.0,
                    });
                    BasisList.Add(new Basis()
                    {
                        ID_Sort = 2,
                        Bezeichnung = "KSD",
                        R = 0.008824,
                        Dicke = 0.0015,
                        Wärmeleitfähigkeit = 0.17,
                        Rohdichte = 1000.0,
                        Kapillar = true,
                        sonstiges = true,
                        KeineLuft = true,
                        Sd_Min = 1500.00,
                        Sd_Max = 1500.00,
                        Fester_sd = true,
                        Dampfdiffusionswiderstand_Min = 1000000.00,
                        Dampfdiffusionswiderstand_Max = 1000000.00
                    });
                    BasisList.Add(new Basis()
                    {
                        ID_Sort = 3,
                        Bezeichnung = "PUR 023",
                        R = 5.217391,
                        Fester_R = true,
                        Dicke = 0.12,
                        Wärmeleitfähigkeit = 0.023,
                        Rohdichte = 30.0,
                        Kapillar = true,
                        sonstiges = true,
                        EvntlLuft = true,
                        Sd_Min = 4.80,
                        Sd_Max = 24.00,
                        Dampfdiffusionswiderstand_Min = 40.00,
                        Dampfdiffusionswiderstand_Max = 200.00
                    });
                    BasisList.Add(new Basis()
                    {
                        ID_Sort = 4,
                        Bezeichnung = "EVALON",
                        R = 0.007059,
                        Dicke = 0.0012,
                        Wärmeleitfähigkeit = 0.17,
                        Rohdichte = 1250.0,
                        Kapillar = true,
                        sonstiges = true,
                        KeineLuft = true,
                        Sd_Min = 14.40,
                        Sd_Max = 21.60,
                        Dampfdiffusionswiderstand_Min = 12000,
                        Dampfdiffusionswiderstand_Max = 18000
                    });
                    foreach (Basis i in BasisList)
                    {
                        i.ModelID = main_model.ID;
                    }
                    for (int i = 0; i < BasisList.Count; i++)
                    {
                        App.Database.SaveBauteilAsync(BasisList[i]);
                    }
                }
                Konstruktionstyp = "unbelüftetes Dach (Warmdach) \r\n(unverschattet mit dunkler Deckung/Abdichtung)";

            }
            else if (main_model.MusterName == "Trapezblech_KSD_PUR_FPO")
            {
                if (main_model.Selected == 0)
                {

                    BasisList.Add(new Basis()
                    {
                        ID_Sort = 1,
                        Bezeichnung = "Trapezblech, Stahl, sd 10",
                        R = 0.000018,
                        Dicke = 0.0008,
                        Wärmeleitfähigkeit = 50.0,
                        Rohdichte = 7860.0,
                        Kapillar = true,
                        Holzwerkstoff = true,
                        KeineLuft = true,
                        Fester_sd = true,
                        Dampfdiffusionswiderstand_Min = 11363.64,
                        Dampfdiffusionswiderstand_Max = 11363.64,
                        Sd_Min = 10.0,
                        Sd_Max = 10.0,
                    });
                    BasisList.Add(new Basis()
                    {
                        ID_Sort = 2,
                        Bezeichnung = "KSD",
                        R = 0.008824,
                        Dicke = 0.0015,
                        Wärmeleitfähigkeit = 0.17,
                        Rohdichte = 1000.0,
                        Kapillar = true,
                        sonstiges = true,
                        KeineLuft = true,
                        Sd_Min = 1500.00,
                        Sd_Max = 1500.00,
                        Fester_sd = true,
                        Dampfdiffusionswiderstand_Min = 1000000.00,
                        Dampfdiffusionswiderstand_Max = 1000000.00
                    });
                    BasisList.Add(new Basis()
                    {
                        ID_Sort = 3,
                        Bezeichnung = "PUR 023",
                        R = 5.217391,
                        Fester_R = true,
                        Dicke = 0.12,
                        Wärmeleitfähigkeit = 0.023,
                        Rohdichte = 30.0,
                        Kapillar = true,
                        sonstiges = true,
                        EvntlLuft = true,
                        Sd_Min = 4.80,
                        Sd_Max = 24.00,
                        Dampfdiffusionswiderstand_Min = 40.00,
                        Dampfdiffusionswiderstand_Max = 200.00
                    });
                    BasisList.Add(new Basis()
                    {
                        ID_Sort = 4,
                        Bezeichnung = "FPO",
                        R = 0.010588,
                        Dicke = 0.0018,
                        Wärmeleitfähigkeit = 0.17,
                        Rohdichte = 1100.0,
                        sonstiges = true,
                        Kapillar = true,
                        KeineLuft = true,
                        Fester_sd = true,
                        Sd_Min = 180.000,
                        Sd_Max = 180.00,
                        Dampfdiffusionswiderstand_Min = 100000.00,
                        Dampfdiffusionswiderstand_Max = 100000.00
                    });
                    foreach (Basis i in BasisList)
                    {
                        i.ModelID = main_model.ID;
                    }
                    for (int i = 0; i < BasisList.Count; i++)
                    {
                        App.Database.SaveBauteilAsync(BasisList[i]);
                    }
                }
                Konstruktionstyp = "unbelüftetes Dach (Warmdach) \r\n(unverschattet mit dunkler Deckung/Abdichtung)";

            }
            else if (main_model.MusterName == "Trapezblech_KSD_PUR_PVC")
            {
                if (main_model.Selected == 0)
                {

                    BasisList.Add(new Basis()
                    {
                        ID_Sort = 1,
                        Bezeichnung = "Trapezblech, Stahl, sd 10",
                        R = 0.000018,
                        Dicke = 0.0008,
                        Wärmeleitfähigkeit = 50.0,
                        Rohdichte = 7860.0,
                        Kapillar = true,
                        Holzwerkstoff = true,
                        KeineLuft = true,
                        Fester_sd = true,
                        Dampfdiffusionswiderstand_Min = 11363.64,
                        Dampfdiffusionswiderstand_Max = 11363.64,
                        Sd_Min = 10.0,
                        Sd_Max = 10.0,
                    });
                    BasisList.Add(new Basis()
                    {
                        ID_Sort = 2,
                        Bezeichnung = "KSD",
                        R = 0.008824,
                        Dicke = 0.0015,
                        Wärmeleitfähigkeit = 0.17,
                        Rohdichte = 1000.0,
                        Kapillar = true,
                        sonstiges = true,
                        KeineLuft = true,
                        Sd_Min = 1500.00,
                        Sd_Max = 1500.00,
                        Fester_sd = true,
                        Dampfdiffusionswiderstand_Min = 1000000.00,
                        Dampfdiffusionswiderstand_Max = 1000000.00
                    });
                    BasisList.Add(new Basis()
                    {
                        ID_Sort = 3,
                        Bezeichnung = "PUR 023",
                        R = 5.217391,
                        Fester_R = true,
                        Dicke = 0.12,
                        Wärmeleitfähigkeit = 0.023,
                        Rohdichte = 30.0,
                        Kapillar = true,
                        sonstiges = true,
                        EvntlLuft = true,
                        Sd_Min = 4.80,
                        Sd_Max = 24.00,
                        Dampfdiffusionswiderstand_Min = 40.00,
                        Dampfdiffusionswiderstand_Max = 200.00
                    });                   
                    BasisList.Add(new Basis()
                    {
                        ID_Sort = 4,
                        Bezeichnung = "PVC-P (DIN 16730)",
                        R = 0.008824,
                        Dicke = 0.0015,
                        Wärmeleitfähigkeit = 0.17,
                        Rohdichte = 1200.0,
                        sonstiges = true,
                        Kapillar = true,
                        KeineLuft = true,
                        Sd_Min = 15.000,
                        Sd_Max = 45.00,
                        Dampfdiffusionswiderstand_Min = 10000.00,
                        Dampfdiffusionswiderstand_Max = 30000.00
                    });
                    foreach (Basis i in BasisList)
                    {
                        i.ModelID = main_model.ID;
                    }
                    for (int i = 0; i < BasisList.Count; i++)
                    {
                        App.Database.SaveBauteilAsync(BasisList[i]);
                    }
                }
                Konstruktionstyp = "unbelüftetes Dach (Warmdach) \r\n(unverschattet mit dunkler Deckung/Abdichtung)";

            }
            else if (main_model.MusterName == "Trapezblech_KSD_PUR_PYE_einlagig")
            {
                if (main_model.Selected == 0)
                {

                    BasisList.Add(new Basis()
                    {
                        ID_Sort = 1,
                        Bezeichnung = "Trapezblech, Stahl, sd 10",
                        R = 0.000018,
                        Dicke = 0.0008,
                        Wärmeleitfähigkeit = 50.0,
                        Rohdichte = 7860.0,
                        Kapillar = true,
                        Holzwerkstoff = true,
                        KeineLuft = true,
                        Fester_sd = true,
                        Dampfdiffusionswiderstand_Min = 11363.64,
                        Dampfdiffusionswiderstand_Max = 11363.64,
                        Sd_Min = 10.0,
                        Sd_Max = 10.0,
                    });
                    BasisList.Add(new Basis()
                    {
                        ID_Sort = 2,
                        Bezeichnung = "KSD",
                        R = 0.008824,
                        Dicke = 0.0015,
                        Wärmeleitfähigkeit = 0.17,
                        Rohdichte = 1000.0,
                        Kapillar = true,
                        sonstiges = true,
                        KeineLuft = true,
                        Sd_Min = 1500.00,
                        Sd_Max = 1500.00,
                        Fester_sd = true,
                        Dampfdiffusionswiderstand_Min = 1000000.00,
                        Dampfdiffusionswiderstand_Max = 1000000.00
                    });
                    BasisList.Add(new Basis()
                    {
                        ID_Sort = 3,
                        Bezeichnung = "PUR 023",
                        R = 5.217391,
                        Dicke = 0.12,
                        Wärmeleitfähigkeit = 0.023,
                        Rohdichte = 30.0,
                        Kapillar = true,
                        sonstiges = true,
                        EvntlLuft = true,
                        Sd_Min = 1500.00,
                        Sd_Max = 1500.00,
                        Fester_sd = true,
                        Dampfdiffusionswiderstand_Min = 12500.00,
                        Dampfdiffusionswiderstand_Max = 12500.00
                    });
                    BasisList.Add(new Basis()
                    {
                        ID_Sort = 4,
                        Bezeichnung = "PYE PV200 S5 beschiefert",
                        R = 0.029412,
                        Dicke = 0.005,
                        Wärmeleitfähigkeit = 0.17,
                        Rohdichte = 1000.0,
                        Kapillar = true,
                        sonstiges = true,
                        KeineLuft = true,
                        Sd_Min = 100.000,
                        Sd_Max = 150.00,
                        Dampfdiffusionswiderstand_Min = 20000,
                        Dampfdiffusionswiderstand_Max = 30000
                    });
                    foreach (Basis i in BasisList)
                    {
                        i.ModelID = main_model.ID;
                    }
                    for (int i = 0; i < BasisList.Count; i++)
                    {
                        App.Database.SaveBauteilAsync(BasisList[i]);
                    }
                }
                Konstruktionstyp = "unbelüftetes Dach (Warmdach) \r\n(unverschattet mit dunkler Deckung/Abdichtung)";

            }
            else if (main_model.MusterName == "Trapezblech_KSD_PUR_PYE_zweilagig")
            {
                if (main_model.Selected == 0)
                {

                    BasisList.Add(new Basis()
                    {
                        ID_Sort = 1,
                        Bezeichnung = "Trapezblech, Stahl, sd 10",
                        R = 0.000018,
                        Dicke = 0.0008,
                        Wärmeleitfähigkeit = 50.0,
                        Rohdichte = 7860.0,
                        Kapillar = true,
                        Holzwerkstoff = true,
                        KeineLuft = true,
                        Fester_sd = true,
                        Dampfdiffusionswiderstand_Min = 11363.64,
                        Dampfdiffusionswiderstand_Max = 11363.64,
                        Sd_Min = 10.0,
                        Sd_Max = 10.0,
                    });
                    BasisList.Add(new Basis()
                    {
                        ID_Sort = 2,
                        Bezeichnung = "KSD",
                        R = 0.008824,
                        Dicke = 0.0015,
                        Wärmeleitfähigkeit = 0.17,
                        Rohdichte = 1000.0,
                        Kapillar = true,
                        sonstiges = true,
                        KeineLuft = true,
                        Sd_Min = 1500.00,
                        Sd_Max = 1500.00,
                        Fester_sd = true,
                        Dampfdiffusionswiderstand_Min = 1000000.00,
                        Dampfdiffusionswiderstand_Max = 1000000.00
                    });
                    BasisList.Add(new Basis()
                    {
                        ID_Sort = 3,
                        Bezeichnung = "PUR 023",
                        R = 5.217391,
                        Dicke = 0.12,
                        Wärmeleitfähigkeit = 0.023,
                        Rohdichte = 30.0,
                        Kapillar = true,
                        sonstiges = true,
                        EvntlLuft = true,
                        Sd_Min = 1500.00,
                        Sd_Max = 1500.00,
                        Fester_sd = true,
                        Dampfdiffusionswiderstand_Min = 12500.00,
                        Dampfdiffusionswiderstand_Max = 12500.00
                    });
                    BasisList.Add(new Basis()
                    {
                        ID_Sort = 4,
                        Bezeichnung = "PYE G200 S4",
                        R = 0.00,
                        Dicke = 0.004,
                        Wärmeleitfähigkeit = 0.00,
                        Rohdichte = 1000.0,
                        Kapillar = true,
                        sonstiges = true,
                        KeineLuft = true,
                        Fester_R = true,
                        Sd_Min = 86.000,
                        Sd_Max = 86.00,
                        Dampfdiffusionswiderstand_Min = 21500.0,
                        Dampfdiffusionswiderstand_Max = 21500.0
                    });
                    BasisList.Add(new Basis()
                    {
                        ID_Sort = 5,
                        Bezeichnung = "PYE PV200 S5 beschiefert",
                        R = 0.00,
                        Dicke = 0.005,
                        Wärmeleitfähigkeit = 0.00,
                        Rohdichte = 1000.0,
                        Kapillar = true,
                        sonstiges = true,
                        KeineLuft = true,
                        Fester_R = true,
                        Sd_Min = 100.000,
                        Sd_Max = 150.00,
                        Dampfdiffusionswiderstand_Min = 20000,
                        Dampfdiffusionswiderstand_Max = 30000
                    });
                    foreach (Basis i in BasisList)
                    {
                        i.ModelID = main_model.ID;
                    }
                    for (int i = 0; i < BasisList.Count; i++)
                    {
                        App.Database.SaveBauteilAsync(BasisList[i]);
                    }
                }
                Konstruktionstyp = "unbelüftetes Dach (Warmdach) \r\n(unverschattet mit dunkler Deckung/Abdichtung)";

            }

            //Trapezblech MIFA PE
            else if (main_model.MusterName == "Trapezblech_PE_MIFA_EPDM")
            {
                if (main_model.Selected == 0)
                {

                    BasisList.Add(new Basis()
                    {
                        ID_Sort = 1,
                        Bezeichnung = "Trapezblech, Stahl, sd 10",
                        R = 0.000018,
                        Dicke = 0.0008,
                        Wärmeleitfähigkeit = 50.0,
                        Rohdichte = 7860.0,
                        Kapillar = true,
                        Holzwerkstoff = true,
                        KeineLuft = true,
                        Fester_sd = true,
                        Dampfdiffusionswiderstand_Min = 11363.64,
                        Dampfdiffusionswiderstand_Max = 11363.64,
                        Sd_Min = 10.0,
                        Sd_Max = 10.0,
                    });
                    BasisList.Add(new Basis()
                    {
                        ID_Sort = 2,
                        Bezeichnung = "PE-Folie d >= 0,2mm",
                        R = 0.000,
                        Dicke = 0.0002,
                        Wärmeleitfähigkeit = 0.00,
                        Rohdichte = 1000.0,
                        Kapillar = true,
                        sonstiges = true,
                        KeineLuft = true,
                        Sd_Min = 20.00,
                        Sd_Max = 20.00,
                        Dampfdiffusionswiderstand_Min = 1000000.00,
                        Dampfdiffusionswiderstand_Max = 1000000.00
                    });
                    BasisList.Add(new Basis()
                    {
                        ID_Sort = 3,
                        Bezeichnung = "Mineralfaser 038",
                        R = 5.263158,
                        Dicke = 0.2,
                        Wärmeleitfähigkeit = 0.038,
                        Rohdichte = 120.0,
                        Kapillar = true,
                        sonstiges = true,
                        EvntlLuft = true,
                        Sd_Min = 0.20,
                        Sd_Max = 0.20,
                        Dampfdiffusionswiderstand_Min = 1.00,
                        Dampfdiffusionswiderstand_Max = 1.00
                    });
                    BasisList.Add(new Basis()
                    {
                        ID_Sort = 4,
                        Bezeichnung = "EPDM Dachbahn",
                        R = 0.000,
                        Dicke = 0.0013,
                        Wärmeleitfähigkeit = 0.0,
                        Rohdichte = 1150.0,
                        Kapillar = true,
                        sonstiges = true,
                        KeineLuft = true,
                        Sd_Min = 78.00,
                        Sd_Max = 78.00,
                        Dampfdiffusionswiderstand_Min = 60000,
                        Dampfdiffusionswiderstand_Max = 60000
                    });
                    foreach (Basis i in BasisList)
                    {
                        i.ModelID = main_model.ID;
                    }
                    for (int i = 0; i < BasisList.Count; i++)
                    {
                        App.Database.SaveBauteilAsync(BasisList[i]);
                    }
                }
                Konstruktionstyp = "unbelüftetes Dach (Warmdach) \r\n(unverschattet mit dunkler Deckung/Abdichtung)";

            }
            else if (main_model.MusterName == "Trapezblech_PE_MIFA_Evalon")
            {
                if (main_model.Selected == 0)
                {

                    BasisList.Add(new Basis()
                    {
                        ID_Sort = 1,
                        Bezeichnung = "Trapezblech, Stahl, sd 10",
                        R = 0.000018,
                        Dicke = 0.0008,
                        Wärmeleitfähigkeit = 50.0,
                        Rohdichte = 7860.0,
                        Kapillar = true,
                        Holzwerkstoff = true,
                        KeineLuft = true,
                        Fester_sd = true,
                        Dampfdiffusionswiderstand_Min = 11363.64,
                        Dampfdiffusionswiderstand_Max = 11363.64,
                        Sd_Min = 10.0,
                        Sd_Max = 10.0,
                    });
                    BasisList.Add(new Basis()
                    {
                        ID_Sort = 2,
                        Bezeichnung = "PE-Folie d >= 0,2mm",
                        R = 0.000,
                        Dicke = 0.0002,
                        Wärmeleitfähigkeit = 0.00,
                        Rohdichte = 1000.0,
                        Kapillar = true,
                        sonstiges = true,
                        KeineLuft = true,
                        Sd_Min = 20.00,
                        Sd_Max = 20.00,
                        Dampfdiffusionswiderstand_Min = 1000000.00,
                        Dampfdiffusionswiderstand_Max = 1000000.00
                    });
                    BasisList.Add(new Basis()
                    {
                        ID_Sort = 3,
                        Bezeichnung = "Mineralfaser 038",
                        R = 5.263158,
                        Dicke = 0.2,
                        Wärmeleitfähigkeit = 0.038,
                        Rohdichte = 120.0,
                        Kapillar = true,
                        sonstiges = true,
                        EvntlLuft = true,
                        Sd_Min = 0.20,
                        Sd_Max = 0.20,
                        Dampfdiffusionswiderstand_Min = 1.00,
                        Dampfdiffusionswiderstand_Max = 1.00
                    });
                    BasisList.Add(new Basis()
                    {
                        ID_Sort = 4,
                        Bezeichnung = "Glasvlies",
                        Wärmeleitfähigkeit = 10.0,
                        R = 0.0001,
                        Rohdichte = 400,
                        Dicke = 0.001,
                        Dampfdiffusionswiderstand_Min= 1.0,
                        Dampfdiffusionswiderstand_Max = 1.0,
                        Sd_Min = 0.00,
                        Sd_Max = 0.00,
                        sonstiges = true,
                        KeineLuft = true,
                    });
                    BasisList.Add(new Basis()
                    {
                        ID_Sort = 5,
                        Bezeichnung = "EVALON",
                        R = 0.007059,
                        Dicke = 0.0012,
                        Wärmeleitfähigkeit = 0.17,
                        Rohdichte = 1000.0,
                        Kapillar = true,
                        sonstiges = true,
                        KeineLuft = true,
                        Sd_Min = 14.40,
                        Sd_Max = 21.60,
                        Dampfdiffusionswiderstand_Min = 12000,
                        Dampfdiffusionswiderstand_Max = 18000
                    });
                    foreach (Basis i in BasisList)
                    {
                        i.ModelID = main_model.ID;
                    }
                    for (int i = 0; i < BasisList.Count; i++)
                    {
                        App.Database.SaveBauteilAsync(BasisList[i]);
                    }
                }
                Konstruktionstyp = "unbelüftetes Dach (Warmdach) \r\n(unverschattet mit dunkler Deckung/Abdichtung)";

            }
            else if (main_model.MusterName == "Trapezblech_PE_MIFA_FPO")
            {
                if (main_model.Selected == 0)
                {

                    BasisList.Add(new Basis()
                    {
                        ID_Sort = 1,
                        Bezeichnung = "Trapezblech, Stahl, sd 10",
                        R = 0.000018,
                        Dicke = 0.0008,
                        Wärmeleitfähigkeit = 50.0,
                        Rohdichte = 7860.0,
                        Kapillar = true,
                        Holzwerkstoff = true,
                        KeineLuft = true,
                        Fester_sd = true,
                        Dampfdiffusionswiderstand_Min = 11363.64,
                        Dampfdiffusionswiderstand_Max = 11363.64,
                        Sd_Min = 10.0,
                        Sd_Max = 10.0,
                    });
                    BasisList.Add(new Basis()
                    {
                        ID_Sort = 2,
                        Bezeichnung = "PE-Folie d >= 0,2mm",
                        R = 0.000,
                        Dicke = 0.0002,
                        Wärmeleitfähigkeit = 0.00,
                        Rohdichte = 1000.0,
                        Kapillar = true,
                        sonstiges = true,
                        KeineLuft = true,
                        Sd_Min = 20.00,
                        Sd_Max = 20.00,
                        Dampfdiffusionswiderstand_Min = 1000000.00,
                        Dampfdiffusionswiderstand_Max = 1000000.00
                    });
                    BasisList.Add(new Basis()
                    {
                        ID_Sort = 3,
                        Bezeichnung = "Mineralfaser 038",
                        R = 5.263158,
                        Dicke = 0.2,
                        Wärmeleitfähigkeit = 0.038,
                        Rohdichte = 120.0,
                        Kapillar = true,
                        sonstiges = true,
                        EvntlLuft = true,
                        Sd_Min = 0.20,
                        Sd_Max = 0.20,
                        Dampfdiffusionswiderstand_Min = 1.00,
                        Dampfdiffusionswiderstand_Max = 1.00
                    });
                    BasisList.Add(new Basis()
                    {
                        ID_Sort = 4,
                        Bezeichnung = "FPO",
                        R = 0.010588,
                        Dicke = 0.0018,
                        Wärmeleitfähigkeit = 0.17,
                        Rohdichte = 1100.0,
                        sonstiges = true,
                        Kapillar = true,
                        KeineLuft = true,
                        Fester_sd = true,
                        Sd_Min = 180.000,
                        Sd_Max = 180.00,
                        Dampfdiffusionswiderstand_Min = 100000.00,
                        Dampfdiffusionswiderstand_Max = 100000.00
                    });
                    foreach (Basis i in BasisList)
                    {
                        i.ModelID = main_model.ID;
                    }
                    for (int i = 0; i < BasisList.Count; i++)
                    {
                        App.Database.SaveBauteilAsync(BasisList[i]);
                    }
                }
                Konstruktionstyp = "unbelüftetes Dach (Warmdach) \r\n(unverschattet mit dunkler Deckung/Abdichtung)";

            }
            else if (main_model.MusterName == "Trapezblech_PE_MIFA_PVC")
            {
                if (main_model.Selected == 0)
                {

                    BasisList.Add(new Basis()
                    {
                        ID_Sort = 1,
                        Bezeichnung = "Trapezblech, Stahl, sd 10",
                        R = 0.000018,
                        Dicke = 0.0008,
                        Wärmeleitfähigkeit = 50.0,
                        Rohdichte = 7860.0,
                        Kapillar = true,
                        Holzwerkstoff = true,
                        KeineLuft = true,
                        Fester_sd = true,
                        Dampfdiffusionswiderstand_Min = 11363.64,
                        Dampfdiffusionswiderstand_Max = 11363.64,
                        Sd_Min = 10.0,
                        Sd_Max = 10.0,
                    });
                    BasisList.Add(new Basis()
                    {
                        ID_Sort = 2,
                        Bezeichnung = "PE-Folie d >= 0,2mm",
                        R = 0.000,
                        Dicke = 0.0002,
                        Wärmeleitfähigkeit = 0.00,
                        Rohdichte = 1000.0,
                        Kapillar = true,
                        sonstiges = true,
                        KeineLuft = true,
                        Sd_Min = 20.00,
                        Sd_Max = 20.00,
                        Dampfdiffusionswiderstand_Min = 1000000.00,
                        Dampfdiffusionswiderstand_Max = 1000000.00
                    });
                    BasisList.Add(new Basis()
                    {
                        ID_Sort = 3,
                        Bezeichnung = "Mineralfaser 038",
                        R = 5.263158,
                        Dicke = 0.2,
                        Wärmeleitfähigkeit = 0.038,
                        Rohdichte = 120.0,
                        Kapillar = true,
                        sonstiges = true,
                        EvntlLuft = true,
                        Sd_Min = 0.20,
                        Sd_Max = 0.20,
                        Dampfdiffusionswiderstand_Min = 1.00,
                        Dampfdiffusionswiderstand_Max = 1.00
                    });
                    BasisList.Add(new Basis()
                    {
                        ID_Sort = 4,
                        Bezeichnung = "Glasvlies",
                        R = 0.0001,
                        Wärmeleitfähigkeit = 10.0,
                        Dicke = 0.001,
                        sonstiges = true,
                        Rohdichte = 400.00,
                        Dampfdiffusionswiderstand_Min = 1.0,
                        Dampfdiffusionswiderstand_Max = 1.0,
                        Sd_Min = 0.00,
                        Sd_Max = 0.00,
                    });
                    BasisList.Add(new Basis()
                    {
                        ID_Sort = 5,
                        Bezeichnung = "PVC-P (DIN 16730)",
                        R = 0.008824,
                        Dicke = 0.0015,
                        Wärmeleitfähigkeit = 0.17,
                        Rohdichte = 1200.0,
                        sonstiges = true,
                        Kapillar = true,
                        KeineLuft = true,
                        Sd_Min = 15.000,
                        Sd_Max = 45.00,
                        Dampfdiffusionswiderstand_Min = 10000.00,
                        Dampfdiffusionswiderstand_Max = 30000.00
                    });
                    foreach (Basis i in BasisList)
                    {
                        i.ModelID = main_model.ID;
                    }
                    for (int i = 0; i < BasisList.Count; i++)
                    {
                        App.Database.SaveBauteilAsync(BasisList[i]);
                    }
                }
                Konstruktionstyp = "unbelüftetes Dach (Warmdach) \r\n(unverschattet mit dunkler Deckung/Abdichtung)";

            }
            else if (main_model.MusterName == "Trapezblech_PE_MIFA_PYE_einlagig")
            {
                if (main_model.Selected == 0)
                {

                    BasisList.Add(new Basis()
                    {
                        ID_Sort = 1,
                        Bezeichnung = "Trapezblech, Stahl, sd 10",
                        R = 0.000018,
                        Dicke = 0.0008,
                        Wärmeleitfähigkeit = 50.0,
                        Rohdichte = 7860.0,
                        Kapillar = true,
                        Holzwerkstoff = true,
                        KeineLuft = true,
                        Fester_sd = true,
                        Dampfdiffusionswiderstand_Min = 11363.64,
                        Dampfdiffusionswiderstand_Max = 11363.64,
                        Sd_Min = 10.0,
                        Sd_Max = 10.0,
                    });
                    BasisList.Add(new Basis()
                    {
                        ID_Sort = 2,
                        Bezeichnung = "PE-Folie d >= 0,2mm",
                        R = 0.00,
                        Fester_R = true,
                        Dicke = 0.0002,
                        Wärmeleitfähigkeit = 0.00,
                        Rohdichte = 1000.0,
                        Kapillar = true,
                        sonstiges = true,
                        KeineLuft = true,
                        Sd_Min = 20.00,
                        Sd_Max = 20.00,
                        Fester_sd = true,
                        Dampfdiffusionswiderstand_Min = 1000000.00,
                        Dampfdiffusionswiderstand_Max = 1000000.00
                    });
                    BasisList.Add(new Basis()
                    {
                        ID_Sort = 3,
                        Bezeichnung = "Mineralfaser 038",
                        R = 5.263158,
                        Dicke = 0.2,
                        Wärmeleitfähigkeit = 0.038,
                        Rohdichte = 120.0,
                        Kapillar = true,
                        sonstiges = true,
                        EvntlLuft = true,
                        Sd_Min = 0.20,
                        Sd_Max = 0.20,
                        Dampfdiffusionswiderstand_Min = 1.00,
                        Dampfdiffusionswiderstand_Max = 1.00
                    });
                    BasisList.Add(new Basis()
                    {
                        ID_Sort = 4,
                        Bezeichnung = "Glasvlies",
                        R = 0.000100,
                        Dicke = 0.001,
                        Wärmeleitfähigkeit = 10.000000,
                        Rohdichte = 400.0,
                        sonstiges = true,
                        KeineLuft = true,
                        Sd_Min = 0.000,
                        Sd_Max = 0.00,
                        Dampfdiffusionswiderstand_Min = 1.0,
                        Dampfdiffusionswiderstand_Max = 1.0
                    });
                    BasisList.Add(new Basis()
                    {
                        ID_Sort = 5,
                        Bezeichnung = "PYE PV200 S5 beschiefert",
                        R = 0.029412,
                        Dicke = 0.005,
                        Wärmeleitfähigkeit = 0.17,
                        Rohdichte = 1000.0,
                        Kapillar = true,
                        sonstiges = true,
                        KeineLuft = true,
                        Sd_Min = 100.000,
                        Sd_Max = 150.00,
                        Dampfdiffusionswiderstand_Min = 20000,
                        Dampfdiffusionswiderstand_Max = 30000
                    });
                    foreach (Basis i in BasisList)
                    {
                        i.ModelID = main_model.ID;
                    }
                    for (int i = 0; i < BasisList.Count; i++)
                    {
                        App.Database.SaveBauteilAsync(BasisList[i]);
                    }
                }
                Konstruktionstyp = "unbelüftetes Dach (Warmdach) \r\n(unverschattet mit dunkler Deckung/Abdichtung)";

            }
            else if (main_model.MusterName == "Trapezblech_PE_MIFA_PYE_zweilagig")
            {
                if (main_model.Selected == 0)
                {

                    BasisList.Add(new Basis()
                    {
                        ID_Sort = 1,
                        Bezeichnung = "Trapezblech, Stahl, sd 10",
                        R = 0.000018,
                        Dicke = 0.0008,
                        Wärmeleitfähigkeit = 50.0,
                        Rohdichte = 7860.0,
                        Kapillar = true,
                        Holzwerkstoff = true,
                        KeineLuft = true,
                        Fester_sd = true,
                        Dampfdiffusionswiderstand_Min = 11363.64,
                        Dampfdiffusionswiderstand_Max = 11363.64,
                        Sd_Min = 10.0,
                        Sd_Max = 10.0,
                    });
                    BasisList.Add(new Basis()
                    {
                        ID_Sort = 2,
                        Bezeichnung = "PE-Folie d >= 0,2mm",
                        R = 0.00,
                        Fester_R = true,
                        Dicke = 0.0002,
                        Wärmeleitfähigkeit = 0.00,
                        Rohdichte = 1000.0,
                        Kapillar = true,
                        sonstiges = true,
                        KeineLuft = true,
                        Sd_Min = 20.00,
                        Sd_Max = 20.00,
                        Fester_sd = true,
                        Dampfdiffusionswiderstand_Min = 1000000.00,
                        Dampfdiffusionswiderstand_Max = 1000000.00
                    });
                    BasisList.Add(new Basis()
                    {
                        ID_Sort = 3,
                        Bezeichnung = "Mineralfaser 038",
                        R = 5.263158,
                        Dicke = 0.2,
                        Wärmeleitfähigkeit = 0.038,
                        Rohdichte = 120.0,
                        Kapillar = true,
                        sonstiges = true,
                        EvntlLuft = true,
                        Sd_Min = 0.20,
                        Sd_Max = 0.20,
                        Dampfdiffusionswiderstand_Min = 1.00,
                        Dampfdiffusionswiderstand_Max = 1.00
                    });
                    BasisList.Add(new Basis()
                    {
                        ID_Sort = 4,
                        Bezeichnung = "Glasvlies",
                        R = 0.000100,
                        Dicke = 0.001,
                        Wärmeleitfähigkeit = 10.000000,
                        Rohdichte = 400.0,
                        sonstiges = true,
                        KeineLuft = true,
                        Sd_Min = 0.000,
                        Sd_Max = 0.00,
                        Dampfdiffusionswiderstand_Min = 1.0,
                        Dampfdiffusionswiderstand_Max = 1.0
                    });
                    BasisList.Add(new Basis()
                    {
                        ID_Sort = 5,
                        Bezeichnung = "PYE PV200 S4",
                        R = 0.023529,
                        Dicke = 0.004,
                        Wärmeleitfähigkeit = 0.17,
                        Rohdichte = 1200.0,
                        sonstiges = true,
                        Kapillar = true,
                        KeineLuft = true,
                        Sd_Min = 86.000,
                        Sd_Max = 86.00,
                        Dampfdiffusionswiderstand_Min = 21500.0,
                        Dampfdiffusionswiderstand_Max = 21500.0
                    });
                    BasisList.Add(new Basis()
                    {
                        ID_Sort = 6,
                        Bezeichnung = "PYE PV200 S5 beschiefert",
                        R = 0.029412,
                        Dicke = 0.005,
                        Wärmeleitfähigkeit = 0.17,
                        Rohdichte = 1000.0,
                        Kapillar = true,
                        sonstiges = true,
                        KeineLuft = true,
                        Sd_Min = 100.000,
                        Sd_Max = 150.00,
                        Dampfdiffusionswiderstand_Min = 20000,
                        Dampfdiffusionswiderstand_Max = 30000
                    });
                    foreach (Basis i in BasisList)
                    {
                        i.ModelID = main_model.ID;
                    }
                    for (int i = 0; i < BasisList.Count; i++)
                    {
                        App.Database.SaveBauteilAsync(BasisList[i]);
                    }
                }
                Konstruktionstyp = "unbelüftetes Dach (Warmdach) \r\n(unverschattet mit dunkler Deckung/Abdichtung)";

            }

            //Umkehrdach
            else if (main_model.MusterName == "Umkehrdach")
            {
                if (main_model.Selected == 0)
                {

                    BasisList.Add(new Basis()
                    {
                        ID_Sort = 1,
                        Bezeichnung = "Beton armiert (1% Stahl)",
                        R = 0.078261,
                        Dicke = 0.18,
                        Wärmeleitfähigkeit = 2.3,
                        Rohdichte = 2300,
                        Kapillar = true,
                        sonstiges = true,
                        KeineLuft = true,
                        Dampfdiffusionswiderstand_Min = 80,
                        Dampfdiffusionswiderstand_Max = 130,
                        Sd_Min = 14.40,
                        Sd_Max = 23.40,
                    });
                    BasisList.Add(new Basis()
                    {
                        ID_Sort = 2,
                        Bezeichnung = "PYE G200 S4",
                        R = 0.023529,
                        Dicke = 0.004,
                        Wärmeleitfähigkeit = 0.17,
                        Rohdichte = 1000.0,
                        Kapillar = true,
                        sonstiges = true,
                        KeineLuft = true,
                        Sd_Min = 80.00,
                        Sd_Max = 120.00,
                        Dampfdiffusionswiderstand_Min = 20000.00,
                        Dampfdiffusionswiderstand_Max = 30000.00
                    });
                    BasisList.Add(new Basis()
                    {
                        ID_Sort = 3,
                        Bezeichnung = "PYE PV200 S5 beschiefert",
                        R = 0.029412,
                        Dicke = 0.005,
                        Wärmeleitfähigkeit = 0.17,
                        Rohdichte = 1000.0,
                        Kapillar = true,
                        sonstiges = true,
                        KeineLuft = true,
                        Sd_Min = 100.000,
                        Sd_Max = 150.00,
                        Dampfdiffusionswiderstand_Min = 20000,
                        Dampfdiffusionswiderstand_Max = 30000
                    });
                    BasisList.Add(new Basis()
                    {
                        ID_Sort = 4,
                        Bezeichnung = "XPS 035, mit Stufenfalz / mehrlagig",
                        R = 5.142857,
                        Dicke = 0.18,
                        Wärmeleitfähigkeit = 0.035,
                        Rohdichte = 30.0,
                        Kapillar = true,
                        sonstiges = true,
                        KeineLuft = true,
                        Sd_Min = 14.40,
                        Sd_Max = 45.0,
                        Dampfdiffusionswiderstand_Min = 80.00,
                        Dampfdiffusionswiderstand_Max = 250.00
                    });
                    BasisList.Add(new Basis()
                    {
                        ID_Sort = 5,
                        Bezeichnung = "Polyestervlies",
                        R = 0.000100,
                        Dicke = 0.001,
                        Wärmeleitfähigkeit = 10.000000,
                        Rohdichte = 400.0,
                        sonstiges = true,
                        KeineLuft = true,
                        Sd_Min = 0.000,
                        Sd_Max = 0.00,
                        Dampfdiffusionswiderstand_Min = 1.0,
                        Dampfdiffusionswiderstand_Max = 1.0
                    });
                    BasisList.Add(new Basis()
                    {
                        ID_Sort = 6,
                        Bezeichnung = "Kies",
                        R = 0.0000,
                        Fester_R = true,
                        Dicke = 0.05,
                        Wärmeleitfähigkeit = 0.0,
                        Rohdichte = 2000.0,
                        sonstiges = true,
                        KeineLuft = true,
                        Sd_Min = 0.15,
                        Sd_Max = 0.15,
                        Dampfdiffusionswiderstand_Min = 3.0,
                        Dampfdiffusionswiderstand_Max = 3.0
                    });
                    
                    foreach (Basis i in BasisList)
                    {
                        i.ModelID = main_model.ID;
                    }
                    for (int i = 0; i < BasisList.Count; i++)
                    {
                        App.Database.SaveBauteilAsync(BasisList[i]);
                    }
                }
                Konstruktionstyp = "unbelüftetes Dach (Warmdach) \r\n(unverschattet mit dunkler Deckung/Abdichtung)";
            }

            //WDVS
            else if (main_model.MusterName == "WDVS_EPS-032")
            {
                if (main_model.Selected == 0)
                {

                    BasisList.Add(new Basis()
                    {
                        ID_Sort = 1,
                        Bezeichnung = "Innenputz",
                        R = 0.021429,
                        Dicke = 0.015,
                        Wärmeleitfähigkeit = 0.699986,
                        Rohdichte = 1400,
                        sonstiges = true,
                        KeineLuft = true,
                        Dampfdiffusionswiderstand_Min = 10,
                        Dampfdiffusionswiderstand_Max = 10,
                        Sd_Min = 0.15,
                        Sd_Max = 0.15,
                    });
                    BasisList.Add(new Basis()
                    {
                        ID_Sort = 2,
                        Bezeichnung = "Mauerwerk",
                        R = 0.60000,
                        Dicke = 0.24,
                        Wärmeleitfähigkeit = 0.4,
                        Rohdichte = 2000.0,
                        sonstiges = true,
                        KeineLuft = true,
                        Sd_Min = 1.92,
                        Sd_Max = 1.92,
                        Dampfdiffusionswiderstand_Min = 8.0,
                        Dampfdiffusionswiderstand_Max = 8.0
                    });
                    BasisList.Add(new Basis()
                    {
                        ID_Sort = 3,
                        Bezeichnung = "EPS 032",
                        R = 3.750,
                        Dicke = 0.12,
                        Wärmeleitfähigkeit = 0.032,
                        Rohdichte = 30.0,
                        Kapillar = true,
                        sonstiges = true,
                        KeineLuft = true,
                        Sd_Min = 2.40,
                        Sd_Max = 12.00,
                        Dampfdiffusionswiderstand_Min = 20.0,
                        Dampfdiffusionswiderstand_Max = 100.0
                    });
                    BasisList.Add(new Basis()
                    {
                        ID_Sort = 4,
                        Bezeichnung = "Außenputz",
                        R = 0.02,
                        Dicke = 0.02,
                        Wärmeleitfähigkeit = 1.0,
                        Rohdichte = 1400.0,
                        sonstiges = true,
                        EvntlLuft = true,
                        Sd_Min = 0.80,
                        Sd_Max = 0.80,
                        Dampfdiffusionswiderstand_Min = 40.0,
                        Dampfdiffusionswiderstand_Max = 40.0
                    });

                    foreach (Basis i in BasisList)
                    {
                        i.ModelID = main_model.ID;
                    }
                    for (int i = 0; i < BasisList.Count; i++)
                    {
                        App.Database.SaveBauteilAsync(BasisList[i]);
                    }
                }
                Konstruktionstyp = "nicht hinterlüftete Wand";
            }
            else if (main_model.MusterName == "WDVS_EPS-035")
            {
                if (main_model.Selected == 0)
                {

                    BasisList.Add(new Basis()
                    {
                        ID_Sort = 1,
                        Bezeichnung = "Innenputz",
                        R = 0.021429,
                        Dicke = 0.015,
                        Wärmeleitfähigkeit = 0.699986,
                        Rohdichte = 1400,
                        sonstiges = true,
                        KeineLuft = true,
                        Dampfdiffusionswiderstand_Min = 10,
                        Dampfdiffusionswiderstand_Max = 10,
                        Sd_Min = 0.15,
                        Sd_Max = 0.15,
                    });
                    BasisList.Add(new Basis()
                    {
                        ID_Sort = 2,
                        Bezeichnung = "Mauerwerk",
                        R = 0.60000,
                        Dicke = 0.24,
                        Wärmeleitfähigkeit = 0.4,
                        Rohdichte = 2000.0,
                        sonstiges = true,
                        KeineLuft = true,
                        Sd_Min = 1.92,
                        Sd_Max = 1.92,
                        Dampfdiffusionswiderstand_Min = 8.0,
                        Dampfdiffusionswiderstand_Max = 8.0
                    });
                    BasisList.Add(new Basis()
                    {
                        ID_Sort = 3,
                        Bezeichnung = "EPS 035",
                        R = 3.428571,
                        Dicke = 0.12,
                        Wärmeleitfähigkeit = 0.035,
                        Rohdichte = 30.0,
                        Kapillar = true,
                        sonstiges = true,
                        KeineLuft = true,
                        Sd_Min = 2.40,
                        Sd_Max = 12.00,
                        Dampfdiffusionswiderstand_Min = 20.0,
                        Dampfdiffusionswiderstand_Max = 100.0
                    });
                    BasisList.Add(new Basis()
                    {
                        ID_Sort = 4,
                        Bezeichnung = "Außenputz",
                        R = 0.02,
                        Dicke = 0.02,
                        Wärmeleitfähigkeit = 1.0,
                        Rohdichte = 1400.0,
                        sonstiges = true,
                        EvntlLuft = true,
                        Sd_Min = 0.80,
                        Sd_Max = 0.80,
                        Dampfdiffusionswiderstand_Min = 40.0,
                        Dampfdiffusionswiderstand_Max = 40.0
                    });

                    foreach (Basis i in BasisList)
                    {
                        i.ModelID = main_model.ID;
                    }
                    for (int i = 0; i < BasisList.Count; i++)
                    {
                        App.Database.SaveBauteilAsync(BasisList[i]);
                    }
                }
                Konstruktionstyp = "nicht hinterlüftete Wand";
            }

            //
            else if (main_model.MusterName == "Beton_KSD_EPS035_PVC")
            {
                if (main_model.Selected == 0)
                {

                    BasisList.Add(new Basis()
                    {
                        ID_Sort = 1,
                        Bezeichnung = "Beton armiert (1% Stahl)",
                        R = 0.078261,
                        Dicke = 0.18,
                        Wärmeleitfähigkeit = 2.3,
                        Rohdichte = 2300,
                        Kapillar = true,
                        sonstiges = true,
                        KeineLuft = true,
                        Dampfdiffusionswiderstand_Min = 80,
                        Dampfdiffusionswiderstand_Max = 130,
                        Sd_Min = 14.40,
                        Sd_Max = 23.40,
                    });
                    BasisList.Add(new Basis()
                    {
                        ID_Sort = 2,
                        Bezeichnung = "KSD",
                        R = 0.008824,
                        Dicke = 0.0015,
                        Wärmeleitfähigkeit = 0.17,
                        Rohdichte = 1000.0,
                        Kapillar = true,
                        sonstiges = true,
                        KeineLuft = true,
                        Sd_Min = 1500.00,
                        Sd_Max = 1500.00,
                        Fester_sd = true,
                        Dampfdiffusionswiderstand_Min = 1000000.00,
                        Dampfdiffusionswiderstand_Max = 1000000.00
                    });
                    BasisList.Add(new Basis()
                    {
                        ID_Sort = 3,
                        Bezeichnung = "EPS 035, mit Stufenfalz",
                        R = 5.142857,
                        Dicke = 0.18,
                        Wärmeleitfähigkeit = 0.03500,
                        Rohdichte = 30.0,
                        Kapillar = true,
                        sonstiges = true,
                        KeineLuft = true,
                        Sd_Min = 3.6,
                        Sd_Max = 18.00,
                        Dampfdiffusionswiderstand_Min = 20.00,
                        Dampfdiffusionswiderstand_Max = 100.00
                    });
                    BasisList.Add(new Basis()
                    {
                        ID_Sort = 4,
                        Bezeichnung = "Rohglasvlies",
                        R = 0.000100,
                        Dicke = 0.001,
                        Wärmeleitfähigkeit = 10.000000,
                        Rohdichte = 400.0,
                        sonstiges = true,
                        KeineLuft = true,
                        Sd_Min = 0.000,
                        Sd_Max = 0.00,
                        Dampfdiffusionswiderstand_Min = 1.0,
                        Dampfdiffusionswiderstand_Max = 1.0
                    });
                    BasisList.Add(new Basis()
                    {
                        ID_Sort = 5,
                        Bezeichnung = "PVC-P (DIN 16730)",
                        R = 0.00000,
                        Dicke = 0.0015,
                        Wärmeleitfähigkeit = 0.000000,
                        Rohdichte = 1200.0,
                        sonstiges = true,
                        Kapillar = true,
                        KeineLuft = true,
                        Fester_R = true,
                        Sd_Min = 15.000,
                        Sd_Max = 45.00,
                        Dampfdiffusionswiderstand_Min = 10000.00,
                        Dampfdiffusionswiderstand_Max = 30000.00
                    });
                    foreach (Basis i in BasisList)
                    {
                        i.ModelID = main_model.ID;
                    }
                    for (int i = 0; i < BasisList.Count; i++)
                    {
                        App.Database.SaveBauteilAsync(BasisList[i]);
                    }
                }
                Konstruktionstyp = "unbelüftetes Dach (Warmdach) \r\n(unverschattet mit dunkler Deckung/Abdichtung)";


            }
            else if (main_model.MusterName == "OSB_KSD_EPS035_PVC")
            {
                if (main_model.Selected == 0)
                {

                    BasisList.Add(new Basis()
                    {
                        ID_Sort = 1,
                        Bezeichnung = "OSB-Platten",
                        R = 0.169231,
                        Dicke = 0.022,
                        Wärmeleitfähigkeit = 0.13,
                        Rohdichte = 650.00,
                        Kapillar = true,
                        Holzwerkstoff = true,
                        KeineLuft = true,
                        Dampfdiffusionswiderstand_Min = 30.0,
                        Dampfdiffusionswiderstand_Max = 50.0,
                        Sd_Min = 0.66,
                        Sd_Max = 1.10,
                    });
                    BasisList.Add(new Basis()
                    {
                        ID_Sort = 2,
                        Bezeichnung = "KSD",
                        R = 0.008824,
                        Dicke = 0.0015,
                        Wärmeleitfähigkeit = 0.17,
                        Rohdichte = 1000.0,
                        Kapillar = true,
                        sonstiges = true,
                        KeineLuft = true,
                        Sd_Min = 1500.00,
                        Sd_Max = 1500.00,
                        Fester_sd = true,
                        Dampfdiffusionswiderstand_Min = 1000000.00,
                        Dampfdiffusionswiderstand_Max = 1000000.00
                    });
                    BasisList.Add(new Basis()
                    {
                        ID_Sort = 3,
                        Bezeichnung = "EPS 035, mit Stufenfalz",
                        R = 5.142857,
                        Dicke = 0.18,
                        Wärmeleitfähigkeit = 0.03500,
                        Rohdichte = 30.0,
                        Kapillar = true,
                        sonstiges = true,
                        KeineLuft = true,
                        Sd_Min = 3.6,
                        Sd_Max = 18.00,
                        Dampfdiffusionswiderstand_Min = 20.00,
                        Dampfdiffusionswiderstand_Max = 100.00
                    });
                    BasisList.Add(new Basis()
                    {
                        ID_Sort = 4,
                        Bezeichnung = "Rohglasvlies",
                        R = 0.000100,
                        Dicke = 0.001,
                        Wärmeleitfähigkeit = 10.000000,
                        Rohdichte = 400.0,
                        sonstiges = true,
                        KeineLuft = true,
                        Sd_Min = 0.000,
                        Sd_Max = 0.00,
                        Dampfdiffusionswiderstand_Min = 1.0,
                        Dampfdiffusionswiderstand_Max = 1.0
                    });
                    BasisList.Add(new Basis()
                    {
                        ID_Sort = 5,
                        Bezeichnung = "PVC-P (DIN 16730)",
                        R = 0.00000,
                        Dicke = 0.0015,
                        Wärmeleitfähigkeit = 0.000000,
                        Rohdichte = 1200.0,
                        sonstiges = true,
                        Kapillar = true,
                        KeineLuft = true,
                        Fester_R = true,
                        Sd_Min = 15.000,
                        Sd_Max = 45.00,
                        Dampfdiffusionswiderstand_Min = 10000.00,
                        Dampfdiffusionswiderstand_Max = 30000.00
                    });
                    foreach (Basis i in BasisList)
                    {
                        i.ModelID = main_model.ID;
                    }
                    for (int i = 0; i < BasisList.Count; i++)
                    {
                        App.Database.SaveBauteilAsync(BasisList[i]);
                    }
                }
                Konstruktionstyp = "unbelüftetes Dach (Warmdach) \r\n(unverschattet mit dunkler Deckung/Abdichtung)";

            }

            //Visible von Datentabelle Deckblatt
            BefestigerFrame.IsVisible = false;

            //Visible von Button U_wert
            UwertBasisButton.IsVisible = true;

            //Visible von Datentabelle U-Wert
            BasisUwert.IsVisible = false;

            //Rges und Uwert Berechnung
            BindingContext = this;
            BefestigerBoolean = false;
            BefestigerBooleanEmpty = true;
            SizeDefault = Setting.Size_Default;
            SizeMedium = Setting.Size_Medium;
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            GetBasis();
            if (NachweisGrid.IsVisible == true)
            {
                MeldungDicke.IsVisible = false;
            }
        }
        private async void GetBasis()
        {
            //Bauteil
            var item = await App.Database.GetBauteilAsync();
            
            foreach (Basis i in item)
            {                i.SizeClass = Setting.Size_Default;

                if (i.ModelID == main_model.ID)
                {
                    main_model.Bauteil_Basis.Add(i);

                    for (int m = 0; m <= main_model.Bauteil_Basis.Count - 1; m++)
                    {
                        for (int n = m + 1; n <= main_model.Bauteil_Basis.Count - 1; n++)
                        {                           
                            if (main_model.Bauteil_Basis[m].ID_Bauteil == main_model.Bauteil_Basis[n].ID_Bauteil)
                            {
                                main_model.Bauteil_Basis.Remove(main_model.Bauteil_Basis[m]);
                            }                            
                           
                        }
                    }
                }
                i.Gewicht = i.Dicke * i.Rohdichte;
            }

            if (main_model.Selected == 0)
            {
                for (int i = 0; i < main_model.Bauteil_Basis.Count - 1; i++)
                {
                    for (int j = i + 1; j < main_model.Bauteil_Basis.Count; j++)
                    {
                        if (main_model.Bauteil_Basis[i].ID_Sort > main_model.Bauteil_Basis[j].ID_Sort)
                        {
                            var oldItem = main_model.Bauteil_Basis[i];
                            var newItem = main_model.Bauteil_Basis[j];
                            main_model.Bauteil_Basis[i] = newItem;
                            main_model.Bauteil_Basis[j] = oldItem;
                            await App.Database.UpdateBauteilAsync(main_model.Bauteil_Basis[i]);
                            await App.Database.UpdateBauteilAsync(main_model.Bauteil_Basis[j]);
                        }

                        if (main_model.Bauteil_Basis[i].ID_Bauteil > main_model.Bauteil_Basis[j].ID_Bauteil)
                        {
                            int oldID = main_model.Bauteil_Basis[i].ID_Bauteil;
                            int newID = main_model.Bauteil_Basis[j].ID_Bauteil;
                            main_model.Bauteil_Basis[i].ID_Bauteil = newID;
                            main_model.Bauteil_Basis[j].ID_Bauteil = oldID;
                            await App.Database.UpdateBauteilAsync(main_model.Bauteil_Basis[i]);
                            await App.Database.UpdateBauteilAsync(main_model.Bauteil_Basis[j]);
                        }
                    }
                }
            }
            listBasis.ItemsSource = main_model.Bauteil_Basis;
            listBasisUwert.ItemsSource = main_model.Bauteil_Basis;
            CalculateSum_Basis();


            //Befestiger
            var fix = await App.Database.GetFixAsync();
            foreach (BefestigerBasis j in fix)
            {
                if (j.ModelID == main_model.ID)
                {
                    j.SizeClass = Setting.Size_Default;
                    main_model.Befestiger_Basis.Add(j);
                    for (int m = 0; m <= main_model.Befestiger_Basis.Count - 1; m++)
                    {
                        for (int n = m + 1; n <= main_model.Befestiger_Basis.Count - 1; n++)
                        {
                            //Update Befestiger 
                            if (main_model.Befestiger_Basis[m].ID_Befestiger == main_model.Befestiger_Basis[n].ID_Befestiger)
                            {
                                main_model.Befestiger_Basis.Remove(main_model.Befestiger_Basis[m]);
                            }

                        }
                    }
                }
            }
            listBefestiger.ItemsSource = main_model.Befestiger_Basis;
            if (main_model.Befestiger_Basis.Count != 0)
            {
                FrameBefestigerBasis.IsVisible = true;
            }
            else
            {
                FrameBefestigerBasis.IsVisible = false;
            }        
            Calculate_Uf();
            Calculate_Ug();
            Calculate_DeltaU();          
        }        

        //Method zur Berechung von Rges und Uwert in jeder Konstruktion DIN 4108-3:2018-10 Anhang B.3
        public void CalculateSum_Basis()
        {

            if (Konstruktionstyp == "unbelüftetes Dach (Warmdach) \r\n(unverschattet mit dunkler Deckung/Abdichtung)"
                || Konstruktionstyp == "unbelüftetes Dach (Warmdach) \r\n(verschattet bzw. helle Deckung/Abdichtung)")
            {
                Pc = 2000;
            }
            else
            {
                Pc = 1700;
            }
            Rbasis = main_model.Bauteil_Basis.Sum(p => p.R) + Aufwärts + Außen;
            Summe_Dicke_Basis = main_model.Bauteil_Basis.Sum(p => p.Dicke);
            Ubasis = 1 / Rbasis;


            //Sd-Wert in Tauperiode = sd-Min
            //Bestimmen von Temperatur, Sättigungsdampfdruck, Tauwassermasse, 
            foreach (KlimadatenClass i in Klimadaten)
            {               
                Wärmestromdichte = (i.InnenTemp - i.AußenTemp) / (main_model.Bauteil_Basis.Sum(p => p.R) + Innen_TWN + Außen);
                double? innen_feuchtenachweis = i.InnenTemp - Innen_TWN * Wärmestromdichte;
                main_model.Bauteil_Basis[0].Tempverlauf = innen_feuchtenachweis - Wärmestromdichte * main_model.Bauteil_Basis[0].R;

                //Dampfsättigungsdruck DIN 4108-3:2018-10 Anhang C.3
                main_model.Bauteil_Basis[0].Dampfsättigungsdruck = 610.5 * Math.Exp(Convert.ToDouble(17.269 * main_model.Bauteil_Basis[0].Tempverlauf / (237.3 + main_model.Bauteil_Basis[0].Tempverlauf)));


                main_model.Bauteil_Basis[0].Sd = main_model.Bauteil_Basis[0].Sd_Min;
                Gesamt_SdBasis = main_model.Bauteil_Basis.Sum(p => p.Sd_Min);
                Faktor_Dampfdruckverteilung = (i.InnenWasserdampfdruck - i.AußenWasserdampfdruck) / Gesamt_SdBasis;
                main_model.Bauteil_Basis[0].Dampfteildruck = i.InnenWasserdampfdruck - Faktor_Dampfdruckverteilung * main_model.Bauteil_Basis[0].Sd;

                int Ebene = 0;

                for (int m = 1; m <= main_model.Bauteil_Basis.Count - 1; m++)
                {
                    //Temperaturverlauf
                    main_model.Bauteil_Basis[m].Tempverlauf = main_model.Bauteil_Basis[m - 1].Tempverlauf - main_model.Bauteil_Basis[m].R * Wärmestromdichte;
                    //Sättigungsdampfdruck Psat (DIN 4108-3:2018-10 Anhang C.4)                    
                    if (main_model.Bauteil_Basis[m].Tempverlauf >= 0)
                    {
                        main_model.Bauteil_Basis[m].Dampfsättigungsdruck = 610.5 * Math.Exp(Convert.ToDouble(17.269 * main_model.Bauteil_Basis[m].Tempverlauf / (237.3 + main_model.Bauteil_Basis[m].Tempverlauf)));
                    }
                    else
                    {
                        main_model.Bauteil_Basis[m].Dampfsättigungsdruck = 610.5 * Math.Exp(Convert.ToDouble(21.875 * main_model.Bauteil_Basis[m].Tempverlauf / (265.5 + main_model.Bauteil_Basis[m].Tempverlauf)));
                    }

                    //Wasserdampfteildruck
                    main_model.Bauteil_Basis[m].Sd = main_model.Bauteil_Basis[m].Sd_Min;
                    main_model.Bauteil_Basis[m].Dampfteildruck = main_model.Bauteil_Basis[(m - 1)].Dampfteildruck - Faktor_Dampfdruckverteilung * main_model.Bauteil_Basis[m].Sd;
                    //Tauwasserausfall
                    if (main_model.Bauteil_Basis[m].Dampfteildruck > main_model.Bauteil_Basis[m].Dampfsättigungsdruck)
                    {
                        NachweisGrid.IsVisible = true;
                        Ebene++;
                        main_model.Bauteil_Basis[m].Dampfteildruck = main_model.Bauteil_Basis[m].Dampfsättigungsdruck;
                        main_model.Bauteil_Basis[m].TW = true;

                        //max. zulässige Tauwassermasse
                        //kapillar nicht wasseraufnahmefähig
                        if (main_model.Bauteil_Basis[m].Kapillar == true || main_model.Bauteil_Basis[m + 1].Kapillar == true)
                        {
                            ZulTauwasser = 500;

                            //Holz 5%
                            if (main_model.Bauteil_Basis[m].Holz == true && main_model.Bauteil_Basis[m + 1].Holz == true)
                            {
                                double w1 = (double) main_model.Bauteil_Basis[m].Gewicht * 1000 * 0.05;
                                double w2 = (double) main_model.Bauteil_Basis[m + 1].Gewicht * 1000 * 0.05;
                                ZulTauwasser = Math.Min(w1, w2);
                            }
                            else
                            {
                                if (main_model.Bauteil_Basis[m].Holz == true)
                                {
                                    ZulTauwasser = main_model.Bauteil_Basis[m].Gewicht * 1000 * 0.05;
                                }
                                else if (main_model.Bauteil_Basis[m + 1].Holz == true)
                                {
                                    ZulTauwasser = main_model.Bauteil_Basis[m + 1].Gewicht * 1000 * 0.05;
                                }
                            }

                            //Holzwerkstoff mit 3%
                            if (main_model.Bauteil_Basis[m].Holzwerkstoff == true && main_model.Bauteil_Basis[m + 1].Holzwerkstoff == true)
                            {
                                double w1 = (double)main_model.Bauteil_Basis[m].Gewicht * 1000 * 0.03;
                                double w2 = (double)main_model.Bauteil_Basis[m + 1].Gewicht * 1000 * 0.03;
                                ZulTauwasser = Math.Min(w1, w2);
                            }
                            else
                            {
                                if (main_model.Bauteil_Basis[m].Holzwerkstoff == true)
                                {
                                    ZulTauwasser = main_model.Bauteil_Basis[m].Gewicht * 1000 * 0.03;                                   
                                }
                                else if (main_model.Bauteil_Basis[m + 1].Holzwerkstoff == true)
                                {
                                    ZulTauwasser = main_model.Bauteil_Basis[m + 1].Gewicht * 1000 * 0.03;
                                }
                            }

                            //Kombination von Holz und Holzwerkstoff
                            if (main_model.Bauteil_Basis[m].Holz == true && main_model.Bauteil_Basis[m + 1].Holzwerkstoff == true)
                            {
                                double w1 = (double)(main_model.Bauteil_Basis[m].Gewicht * 1000 * 0.05);
                                double w2 = (double)(main_model.Bauteil_Basis[m + 1].Gewicht * 1000 * 0.03);
                                ZulTauwasser = Math.Min(w1, w2);
                            }
                            else if (main_model.Bauteil_Basis[m].Holzwerkstoff == true && main_model.Bauteil_Basis[m + 1].Holz == true)
                            {
                                double w1 = (double)main_model.Bauteil_Basis[m].Gewicht * 1000 * 0.03;
                                double w2 = (double)main_model.Bauteil_Basis[m + 1].Gewicht * 1000 * 0.05;
                                ZulTauwasser = Math.Min(w1, w2);
                            }
                        }

                        //kapillar wasseraufnahmefähig
                        else
                        {
                            ZulTauwasser = 1000;

                            //Holz 5%
                            if (main_model.Bauteil_Basis[m].Holz == true && main_model.Bauteil_Basis[m + 1].Holz == true)
                            {
                                double w1 = (double)main_model.Bauteil_Basis[m].Gewicht * 1000 * 0.05;
                                double w2 = (double)main_model.Bauteil_Basis[m + 1].Gewicht * 1000 * 0.05;
                                ZulTauwasser = Math.Min(w1, w2);
                            }
                            else
                            {
                                if (main_model.Bauteil_Basis[m].Holz == true)
                                {
                                    ZulTauwasser = main_model.Bauteil_Basis[m].Gewicht * 1000 * 0.05;
                                }
                                else if (main_model.Bauteil_Basis[m + 1].Holz == true)
                                {
                                    ZulTauwasser = main_model.Bauteil_Basis[m + 1].Gewicht * 1000 * 0.05;
                                }
                            }

                            //Holzwerkstoff mit 3%
                            if (main_model.Bauteil_Basis[m].Holzwerkstoff == true && main_model.Bauteil_Basis[m + 1].Holzwerkstoff == true)
                            {
                                double w1 = (double)main_model.Bauteil_Basis[m].Gewicht * 1000 * 0.03;
                                double w2 = (double)main_model.Bauteil_Basis[m + 1].Gewicht * 1000 * 0.03;
                                ZulTauwasser = Math.Min(w1, w2);
                            }
                            else
                            {
                                if (main_model.Bauteil_Basis[m].Holzwerkstoff == true)
                                {
                                    ZulTauwasser = main_model.Bauteil_Basis[m].Gewicht * 1000 * 0.03;
                                }
                                else if (main_model.Bauteil_Basis[m + 1].Holzwerkstoff == true)
                                {
                                    ZulTauwasser = main_model.Bauteil_Basis[m + 1].Gewicht * 1000 * 0.03;
                                }
                            }

                            //Kombination von Holz und Holzwerkstoff
                            if (main_model.Bauteil_Basis[m].Holz == true && main_model.Bauteil_Basis[m + 1].Holzwerkstoff == true)
                            {
                                double w1 = (double)(main_model.Bauteil_Basis[m].Gewicht * 1000 * 0.05);
                                double w2 = (double)(main_model.Bauteil_Basis[m + 1].Gewicht * 1000 * 0.03);
                                ZulTauwasser = Math.Min(w1, w2);
                            }
                            else if (main_model.Bauteil_Basis[m].Holzwerkstoff == true && main_model.Bauteil_Basis[m + 1].Holz == true)
                            {
                                double w1 = (double)main_model.Bauteil_Basis[m].Gewicht * 1000 * 0.03;
                                double w2 = (double)main_model.Bauteil_Basis[m + 1].Gewicht * 1000 * 0.05;
                                ZulTauwasser = Math.Min(w1, w2);
                            }
                        }

                        //Draft List speichert die Sum Sd bis Tauwasserebene und ab Tauwasserebene
                        double? sum_sd = 0;
                        for (int j = 0; j <= m - 1; j++)
                        {                           
                            sum_sd = sum_sd + main_model.Bauteil_Basis[j].Sd;
                            Faktor_Dampfdruckverteilung = (main_model.Bauteil_Basis[m].Dampfteildruck - i.AußenWasserdampfdruck) / (Gesamt_SdBasis - (main_model.Bauteil_Basis[m].Sd + sum_sd));
                        }
                        Schichtgrenze.Add(new Schichtgrenzen()
                        {
                            Dampfteildruck = (double)main_model.Bauteil_Basis[m].Dampfteildruck,
                            Sd = (double)main_model.Bauteil_Basis[m].Sd,
                            SumSd = (double)sum_sd,                          
                            
                        });

                        //Tauwasserausfall in einer Ebene A.2.5.3 DIN 4108-3 Fall b
                        if (Ebene == 1)
                        {
                            Meldung3Ebene.IsVisible = false;
                            double? tev = i.VerdunstungsDauer * 3600 * Math.Pow(10, 3);
                            double? sdc = 0;
                            sdc = Schichtgrenze[Schichtgrenze.Count - 1].SumSd + Schichtgrenze[Schichtgrenze.Count - 1].Sd;
                            Tauwassermasse = Delta0 * tev * (((i.InnenWasserdampfdruck - main_model.Bauteil_Basis[m].Dampfteildruck) / sdc) - ((main_model.Bauteil_Basis[m].Dampfteildruck - i.AußenWasserdampfdruck) / (Gesamt_SdBasis - sdc)));
                            Verdunstungsmasse = Delta0 * tev * (((Pc - i.InnenDruckVerdunstung) / sdc) + ((Pc - i.AußenDruckVerdunstung) / (Gesamt_SdBasis - sdc)));

                        }

                        //Tauwasserausfall in zwei Ebenen A.2.5.5 Din 4108-3 Fall d
                        else if (Ebene == 2)
                        {
                            NachweisGrid.IsVisible = true;
                            Meldung3Ebene.IsVisible = false;

                            //Tauperiode
                            double? Mc1 = 0;
                            double? Mc2 = 0;
                            double? sdc1 = 0;
                            double? sdc2 = 0;
                            double? sdc_sub = 0;
                            sdc1 = Schichtgrenze[Schichtgrenze.Count - 2].SumSd + Schichtgrenze[Schichtgrenze.Count - 2].Sd;
                            sdc2 = Schichtgrenze[Schichtgrenze.Count - 1].SumSd + Schichtgrenze[Schichtgrenze.Count - 1].Sd;

                            //Hier Sdc1 = sdc2
                            if (sdc1 == sdc2)
                            {
                                sdc_sub = sdc1;
                            }
                            else
                            {
                                sdc_sub = sdc2 - sdc1;
                            }

                            Mc1 = Delta0 * i.TauDauer * 3600 * Math.Pow(10, 3) * (((i.InnenWasserdampfdruck - Schichtgrenze[Schichtgrenze.Count - 2].Dampfteildruck) / sdc1) - ((Schichtgrenze[Schichtgrenze.Count - 2].Dampfteildruck - Schichtgrenze[Schichtgrenze.Count - 1].Dampfteildruck) / sdc_sub));
                            Mc2 = Delta0 * i.TauDauer * 3600 * Math.Pow(10, 3) * (((Schichtgrenze[Schichtgrenze.Count - 2].Dampfteildruck - Schichtgrenze[Schichtgrenze.Count - 1].Dampfteildruck) / sdc_sub) - ((Schichtgrenze[Schichtgrenze.Count - 1].Dampfteildruck - i.AußenWasserdampfdruck) / (Gesamt_SdBasis - sdc2)));

                            Tauwassermasse = Mc1 + Mc2;                            

                            //Verdunstungsperiode
                            double? tev = i.VerdunstungsDauer * 3600 * Math.Pow(10, 3);
                            double? tev1 = 0;
                            double? tev2 = 0;
                            double? gev1 = 0;
                            double? gev2 = 0;

                            gev1 = Delta0 * (Pc - i.InnenDruckVerdunstung) / sdc1;
                            gev2 = Delta0 * (Pc - i.AußenDruckVerdunstung) / (Gesamt_SdBasis - sdc2);
                            tev1 = Mc1 / gev1;
                            tev2 = Mc2 / gev2;

                            double? Mev1 = 0;
                            double? Mev2 = 0;

                            //Fallunterscheidung
                            if (tev1 > tev && tev2 > tev)
                            {
                                Mev1 = gev1 * tev;
                                Mev2 = gev2 * tev;
                            }
                            else
                            {
                                if (tev1 < tev2)
                                {
                                    Mev1 = gev1 * tev;
                                    Mev2 = gev2 * tev + (Delta0 * (Pc - i.InnenDruckVerdunstung) / sdc2 + gev2) * (tev - tev1);
                                }
                                else
                                {
                                    Mev2 = gev2 * tev2;
                                    Mev1 = gev1 * tev2 + (gev1 + Delta0 * (Pc - i.AußenDruckVerdunstung) / (Gesamt_SdBasis - sdc1)) * (tev - tev2);
                                }
                            }
                            Verdunstungsmasse = Mev1 + Mev2;
                        }

                        else if (Ebene > 2)
                        {
                            NachweisGrid.IsVisible = false;
                            Meldung3Ebene.IsVisible = true;
                            Feuchtenachweis.Text = "bitte überprüfen";
                            NachweisBasis = false;
                        }



                    }

                    //Kein Tauwasserausfall
                    else
                    {
                        main_model.Bauteil_Basis[m].TW = false;
                    }
                }

                //Feuchtenachweis
                if (Ebene == 0)
                {
                    NachweisGrid.IsVisible = false;
                    MeldungDicke.IsVisible = true;
                    Meldung3Ebene.IsVisible = false;
                    Feuchtenachweis.Text = "ok";
                    //NachweisBasis = true;
                }
                else if (Ebene == 1 || Ebene == 2)
                {
                    if (Tauwassermasse < ZulTauwasser && Tauwassermasse < Verdunstungsmasse)
                    {
                        Feuchtenachweis.Text = "ok";
                        Schluss.Text = "Die Tauwasserbildung ist im Sinne von DIN 4108, Teil 3 (Oktober 2018), Abs. 5.2.2. unschädlich, da WT < zul WT und WV > WT.";
                        NachweisBasis = true;
                        Meldung3Ebene.IsVisible = false;
                        MeldungDicke.IsVisible = false;
                    }

                    else
                    {
                        Feuchtenachweis.Text = "bitte überprüfen";
                        Schluss.Text = "WT > WV => ändern";
                        NachweisBasis = false;
                        MeldungDicke.IsVisible = false;
                        Meldung3Ebene.IsVisible = false;
                    }
                }  
                else if (Ebene > 2)
                {
                    Meldung3Ebene.IsVisible = true;
                    MeldungDicke.IsVisible = false;
                    NachweisGrid.IsVisible = false;
                }
            }
        }

        //Berechnen DeltaUg
        public void Calculate_Ug()
        { 
            foreach (Basis i in main_model.Bauteil_Basis)
            {
                if (i.R == main_model.Bauteil_Basis.Max(p => p.R))
                {
                    if (i.KeineLuft == true)
                    {
                        DU_g = 0;
                        BefestigerBoolean = false;
                        BefestigerBooleanEmpty = true;
                    }
                    else if (i.EvntlLuft == true)
                    {
                        DU_g = 0.01;
                        BefestigerBoolean = true;
                        BefestigerBooleanEmpty = false;
                    }
                    else if (i.MitLuft == true)
                    {
                        DU_g = 0.04;
                        BefestigerBoolean = true;
                        BefestigerBooleanEmpty = false;
                    }
                    Ug = DU_g * Math.Pow((Convert.ToDouble(i.R / Rbasis)), 2);
                }
            }
        }

        //Berechnen DeltaUf
        private void Calculate_Uf()
        {
            foreach (Basis i in main_model.Bauteil_Basis)
            {
                if (i.Wärmeleitfähigkeit == main_model.Bauteil_Basis.Where(m => m.Wärmeleitfähigkeit != 0).Min(m => m.Wärmeleitfähigkeit))
                {
                    foreach (BefestigerBasis j in main_model.Befestiger_Basis)
                    {
                       if (j.Länge == null)
                        {
                            j.Uf_i = (0.8 * j.Wärmeleitfähigkeit_f * Math.PI * Math.Pow((Convert.ToDouble(j.Durchmesser / 1000) / 2), 2) * j.Anzahl * Math.Pow((Convert.ToDouble(i.R / Rbasis)), 2)) / i.Dicke;

                        }
                        else
                        {
                            j.Uf_i = (0.8 * j.Wärmeleitfähigkeit_f * Math.PI * Math.Pow((Convert.ToDouble(j.Durchmesser / 1000) / 2), 2) * j.Anzahl * Math.Pow((Convert.ToDouble(i.R / Rbasis)), 2)) / (j.Länge / 1000);
                        }
                    }
                    Uf = main_model.Befestiger_Basis.Sum(p => p.Uf_i);
                }
            }
        }

        //Berechnen DeltaU
        private void Calculate_DeltaU()
        {
            Gesamt_du = Uf + Ug;
            Delta_U = Ubasis + Uf + Ug;
            AnteilKorrektur = ((Gesamt_du) / Ubasis) * 100;
            //Anzeige des Ergebnis nach Korrektur direkt in der U-wert Tabelle
            if (main_model.Befestiger_Basis.Count == 0)
            {
                if(Ug ==  0)
                {
                    BefestigerBoolean = false;
                    BefestigerBooleanEmpty = true;
                }
                else
                {
                    if (AnteilKorrektur <= 3)
                    {
                        BefestigerBoolean = false;
                        BefestigerBooleanEmpty = true;
                    }
                    else
                    {
                        BefestigerBoolean = true;
                        BefestigerBooleanEmpty = false;
                    }
                }
            }
            else
            {
                if (AnteilKorrektur <= 3)
                {
                    BefestigerBoolean = false;
                    BefestigerBooleanEmpty = true;
                }
                else
                {
                    BefestigerBoolean = true;
                    BefestigerBooleanEmpty = false;
                }
            }
      
        }

        //Basiskonstruktion item selected
         private async void OnSelected_ItemSelected_Basis(object sender, SelectedItemChangedEventArgs e)
         {
            if (BasisUwert.IsVisible == true)
            {
                if (listBasisUwert.SelectedItem == null)
                    return;
                var selectedBauteil = (e.SelectedItem as Basis)!;

                //unselected
                listBasisUwert.SelectedItem = null;

                var Basis = new BasisDetailPage(selectedBauteil);

                Basis.BasisUpdated += (source, bauteil) =>
                {
                    selectedBauteil.ID_Bauteil = bauteil.ID_Bauteil;
                    selectedBauteil.Bezeichnung = bauteil.Bezeichnung;
                    selectedBauteil.Dicke = bauteil.Dicke;
                    selectedBauteil.Wärmeleitfähigkeit = bauteil.Wärmeleitfähigkeit;
                    selectedBauteil.Rohdichte = bauteil.Rohdichte;
                    selectedBauteil.Kapillar = bauteil.Kapillar;
                    selectedBauteil.Holz = bauteil.Holz;
                    selectedBauteil.Holzwerkstoff = bauteil.Holzwerkstoff;
                    selectedBauteil.sonstiges = bauteil.sonstiges;
                    selectedBauteil.KeineLuft = bauteil.KeineLuft;
                    selectedBauteil.EvntlLuft = bauteil.EvntlLuft;
                    selectedBauteil.MitLuft = bauteil.MitLuft;
                    selectedBauteil.Dampfdiffusionswiderstand_Min = bauteil.Dampfdiffusionswiderstand_Min;
                    selectedBauteil.Dampfdiffusionswiderstand_Max = bauteil.Dampfdiffusionswiderstand_Max;
                    selectedBauteil.Sd_Min = bauteil.Sd_Min;
                    selectedBauteil.Sd_Max = bauteil.Sd_Max;
                    selectedBauteil.Sd = bauteil.Sd;
                    selectedBauteil.Tempverlauf = bauteil.Tempverlauf;
                    selectedBauteil.Dampfteildruck = bauteil.Dampfteildruck;
                    selectedBauteil.Dampfsättigungsdruck = bauteil.Dampfsättigungsdruck;
                    selectedBauteil.TW = bauteil.TW;
                    selectedBauteil.Fester_R = bauteil.Fester_R;
                    selectedBauteil.Fester_sd = bauteil.Fester_sd;
                    selectedBauteil.ModelID = bauteil.ModelID;
                    selectedBauteil.DLR1 = bauteil.DLR1;
                    selectedBauteil.DLR2 = bauteil.DLR2;
                    selectedBauteil.DLR3 = bauteil.DLR3;
                    selectedBauteil.DLR4 = bauteil.DLR4;
                    selectedBauteil.DLR5 = bauteil.DLR5;
                    selectedBauteil.LR1 = bauteil.LR1;
                    selectedBauteil.LR2 = bauteil.LR2;
                    selectedBauteil.LR3 = bauteil.LR3;
                    selectedBauteil.LR4 = bauteil.LR4;
                    selectedBauteil.LR5 = bauteil.LR5;
                    selectedBauteil.R = bauteil.R;
                    selectedBauteil.Gewicht = bauteil.Gewicht;
                    foreach (Basis i in main_model.Bauteil_Basis)
                    {
                        if (Double.IsInfinity(Convert.ToDouble(i.R)) || Double.IsNaN(Convert.ToDouble(i.R)))
                        {
                            i.R = 0;
                        }
                    }
                    CalculateSum_Basis();
                    Calculate_Ug();
                    Calculate_Uf();
                    Calculate_DeltaU();
                };

                main_model.Bauteil_Basis.Remove(selectedBauteil);
                CalculateSum_Basis();
                Calculate_Ug();
                Calculate_Uf();
                Calculate_DeltaU();
                main_model.Date = DateTime.Now;
                await App.Database.UpdateItemAsync(main_model);
                await Navigation.PushAsync(new BasisDetailPage(selectedBauteil)
                {
                    BindingContext = selectedBauteil,
                });
            }
        }
        

        //Tab Selected
        public void Deckblatt_Tapped(object sender, EventArgs e)
        {
            //Anzeige von Datentabelle in Deckblatt
            DeckblattTab.IsVisible = true;

            //Anzeige von U-Wert Frame
            BasisUwert.IsVisible = false;
            BefestigerFrame.IsVisible = false;

            //Anzeige von Analyse Tauperiode
            //TestTab.IsVisible = false;

            //Text Bold wenn Tab ausgewählt wurde
            DeckblattButton.FontAttributes = FontAttributes.Bold;
            UwertBasisButton.FontAttributes = FontAttributes.None;
            BefestigerButton.FontAttributes = FontAttributes.None;
            //TestButton.FontAttributes = FontAttributes.None;

            //Text underlined wenn Tab ausgewählt wurde
            DeckblattButton.TextDecorations = TextDecorations.Underline;
            UwertBasisButton.TextDecorations = TextDecorations.None;
            BefestigerButton.TextDecorations = TextDecorations.None;
            //TestButton.TextDecorations = TextDecorations.None;

            //Plusbutton
            BoxPlus.IsVisible = false;
            ButtonPlus.IsVisible = false;

            CalculateSum_Basis();
            if (NachweisGrid.IsVisible == true)
            {
                MeldungDicke.IsVisible = false;
            }
        }
        public void Basis_Tapped(object sender, EventArgs e)
        {
            main_model.Selected = 1;
            //Anzeige von Datentabelle in Deckblatt
            DeckblattTab.IsVisible = false;

            //Anzeige von U-Wert Frame
            BasisUwert.IsVisible = true;
            BefestigerFrame.IsVisible = false;

            //Anzeige von Analyse Tauperiode
            //TestTab.IsVisible = false;
      
            //Text Bold wenn Tab ausgewählt wurde
            DeckblattButton.FontAttributes = FontAttributes.None;
            UwertBasisButton.FontAttributes = FontAttributes.Bold;
            BefestigerButton.FontAttributes = FontAttributes.None;
            //TestButton.FontAttributes = FontAttributes.None;

            //Text underlined wenn Tab ausgewählt wurde
            DeckblattButton.TextDecorations = TextDecorations.None;
            UwertBasisButton.TextDecorations = TextDecorations.Underline;
            BefestigerButton.TextDecorations = TextDecorations.None;
            //TestButton.TextDecorations = TextDecorations.None;

            //Plusbutton
            BoxPlus.IsVisible = true;
            ButtonPlus.IsVisible = true;
            if (main_model.Bauteil_Basis.Count == 0)
            {
                BefestigerBoolean = false;
                BefestigerBooleanEmpty = true;
            }
            else
            {
                Calculate_Uf();
                Calculate_Ug();
                Calculate_DeltaU();
            }
            
        }
        private void Befestiger_Tapped(object sender, EventArgs e)
        {
            //Anzeige von Datentabelle in Deckblatt
            DeckblattTab.IsVisible = false;

            //Anzeige von U-Wert Frame
            BasisUwert.IsVisible = false;
            BefestigerFrame.IsVisible = true;
            //Anzeige von Analyse Tauperiode
            //TestTab.IsVisible = false;

            //Text Bold wenn Tab ausgewählt wurde
            DeckblattButton.FontAttributes = FontAttributes.None;
            UwertBasisButton.FontAttributes = FontAttributes.None;
            BefestigerButton.FontAttributes = FontAttributes.Bold;
            //TestButton.FontAttributes = FontAttributes.None;

            //Text underlined wenn Tab ausgewählt wurde
            DeckblattButton.TextDecorations = TextDecorations.None;
            UwertBasisButton.TextDecorations = TextDecorations.None;
            BefestigerButton.TextDecorations = TextDecorations.Underline;
            //TestButton.TextDecorations = TextDecorations.None;
                        

            //Plusbutton
            BoxPlus.IsVisible = false;
            ButtonPlus.IsVisible = false;
            if (main_model.Befestiger_Basis.Count != 0)
            {
                FrameBefestigerBasis.IsVisible = true;
            }
            else
            {
                FrameBefestigerBasis.IsVisible = false;
            }
        }          

        //Einfügen von Befestiger
        private async void Befestiger_Einfügen_Clicked(object sender, EventArgs e)
        {            
            var newBasisBefestiger = new BefestigerPage();
            newBasisBefestiger.BefestigerAdded += async (source, befestiger) =>
            {
                befestiger.ModelID = main_model.ID;
                newItem_Befestiger = new BefestigerBasis()
                {
                    ID_Befestiger = befestiger.ID_Befestiger,
                    Bezeichnung = befestiger.Bezeichnung,
                    Durchmesser = befestiger.Durchmesser,
                    Wärmeleitfähigkeit_f = befestiger.Wärmeleitfähigkeit_f,
                    Eindringtiefe = befestiger.Eindringtiefe,
                    Uf_i = befestiger.Uf_i,
                    ModelID = befestiger.ModelID,
                    Anzahl = befestiger.Anzahl,
                };
                _befestiger.Add(newItem_Befestiger);
                await App.Database.SaveFixAsync(newItem_Befestiger);
                GetBasis();

                if (_befestiger.Count == 0)
                {
                    BefestigerBoolean = false;
                    BefestigerBooleanEmpty = true;
                }
                else
                {
                    Calculate_Uf();
                    Calculate_Ug();
                    Calculate_DeltaU();
                }

            };
            main_model.Date = DateTime.Now;
            await App.Database.UpdateItemAsync(main_model);
            await Navigation.PushAsync(newBasisBefestiger);
        }
        //Einfügen von Bauteile
        private async void Bauteil_Einfügen_Clicked(object sender, EventArgs e)
        {
            if (UwertBasisButton.FontAttributes == FontAttributes.Bold)
            {              
                var newBasisBauteil = new BauteilPage();
                newBasisBauteil.BauteilAdded += async (source, bauteil) =>
                {
                    bauteil.ID_Sort = BasisList.Count() + 1;
                    bauteil.ModelID = main_model.ID;
                    newItem_Bauteil = new Basis()
                    {
                        ID_Bauteil = bauteil.ID_Bauteil,
                        R = bauteil.R,
                        Bezeichnung = bauteil.Bezeichnung,
                        Dicke = bauteil.Dicke,
                        Wärmeleitfähigkeit = bauteil.Wärmeleitfähigkeit,
                        Rohdichte = bauteil.Rohdichte,
                        Kapillar = bauteil.Kapillar,
                        Holz = bauteil.Holz,
                        Holzwerkstoff = bauteil.Holzwerkstoff,
                        sonstiges = bauteil.sonstiges,
                        KeineLuft = bauteil.KeineLuft,
                        EvntlLuft = bauteil.EvntlLuft,
                        MitLuft = bauteil.MitLuft,
                        Dampfdiffusionswiderstand_Min = bauteil.Dampfdiffusionswiderstand_Min,
                        Dampfdiffusionswiderstand_Max = bauteil.Dampfdiffusionswiderstand_Max,
                        Sd_Min = bauteil.Sd_Min,
                        Sd_Max = bauteil.Sd_Max,
                        Sd = bauteil.Sd,
                        Tempverlauf = bauteil.Tempverlauf,
                        Dampfteildruck = bauteil.Dampfteildruck,
                        Dampfsättigungsdruck = bauteil.Dampfsättigungsdruck,
                        TW = bauteil.TW,
                        Fester_R = bauteil.Fester_R,
                        Fester_sd = bauteil.Fester_sd,
                        ModelID = bauteil.ModelID,
                        DLR1 = bauteil.DLR1,
                        DLR2 = bauteil.DLR2,
                        DLR3 = bauteil.DLR3,
                        DLR4 = bauteil.DLR4,
                        DLR5 = bauteil.DLR5,
                        LR1 = bauteil.LR1,
                        LR2 = bauteil.LR2,
                        LR3 = bauteil.LR3,
                        LR4 = bauteil.LR4,
                        LR5 = bauteil.LR5,
                        Gewicht = bauteil.Gewicht,
                    };                                            
                    _basis.Add(newItem_Bauteil);
                    await App.Database.SaveBauteilAsync(newItem_Bauteil);
                    GetBasis();
                    CalculateSum_Basis();
                    Calculate_Ug();
                    Calculate_Uf();
                    Calculate_DeltaU();
                };
                main_model.Date = DateTime.Now;
                await App.Database.UpdateItemAsync(main_model);
                await Navigation.PushAsync(newBasisBauteil);
            }             
        } 

        //ItemSelected Befestiger
        private async void OnSelected_Befestiger_Basis(object sender, SelectedItemChangedEventArgs e)
        {
            if (listBefestiger.SelectedItem == null)
                return;
            var selectedBefestiger = (e.SelectedItem as BefestigerBasis)!;
            listBefestiger.SelectedItem = null;
            var BasisBefestiger = new BasisEinfügen(selectedBefestiger);
            BasisBefestiger.BefestigerUpdated += (source, befestiger) =>
            {
                selectedBefestiger.ID_Befestiger = befestiger.ID_Befestiger;
                selectedBefestiger.Anzahl = befestiger.Anzahl;
                selectedBefestiger.Wärmeleitfähigkeit_f = befestiger.Wärmeleitfähigkeit_f;
                selectedBefestiger.Durchmesser = befestiger.Durchmesser;
                selectedBefestiger.Eindringtiefe = befestiger.Eindringtiefe;
                selectedBefestiger.Länge = befestiger.Länge;
                selectedBefestiger.ModelID = befestiger.ModelID;
                Calculate_Uf();
                Calculate_Ug();
                Calculate_DeltaU();
            };

            main_model.Befestiger_Basis.Remove(selectedBefestiger);
            Calculate_Uf();
            Calculate_Ug();
            Calculate_DeltaU();
            main_model.Date = DateTime.Now;
            await App.Database.UpdateItemAsync(main_model);
            await Navigation.PushAsync(new BasisEinfügen(selectedBefestiger)
            {
                BindingContext = selectedBefestiger
            });
        }

        private void Konstruktion_Clicked(object sender, EventArgs e)
        {
            var konstruktionsUpdate = new KonstruktionPage(Konstruktionstyp);
            konstruktionsUpdate.KonstruktionChanged += (source, konstruktion) =>
            {
                Konstruktionstyp = konstruktion;
            };
            Navigation.PushAsync(konstruktionsUpdate);
            CalculateSum_Basis();
        }
      
        //Öffnen des Menüs
        private async void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            /*var project = main_model;     
             await Navigation.PushAsync(new ProjektMenu(project));*/
            var menu = main_model as MainModel;
            await Navigation.PushAsync(new ProjektMenu(menu)
            {
                BindingContext = menu as MainModel
            });
        
        }

        //Zurück zur Vorderseite
        private async void Back_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopToRootAsync();

        }

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            await DisplayAlert("Achtung!", "Bauteile können nur unter U-Wert bearbeitet werden", "OK");
        }
        private async void Up_Clicked(object sender, EventArgs e)
        {
            ImageButton imagebutton = (sender as ImageButton)!;
            var item = (imagebutton.BindingContext as Basis)!;

            int old_id = item.ID_Bauteil;
            int itemToInsertBefore_old_ID = old_id - 1;
            foreach (Basis i in main_model.Bauteil_Basis)
            {
                if (i.ID_Bauteil == itemToInsertBefore_old_ID)
                {
                    i.ID_Bauteil = old_id;
                    item.ID_Bauteil = itemToInsertBefore_old_ID;
                    await App.Database.UpdateBauteilAsync(i);
                    break;
                }
            }
            await App.Database.UpdateBauteilAsync(item);
            OnAppearing();
            main_model.Date = DateTime.Now;
            await App.Database.UpdateItemAsync(main_model);
        }

        private async void Down_Clicked(object sender, EventArgs e)
        {
            ImageButton imageButton = (sender as ImageButton)!;
            var item = (imageButton.BindingContext as Basis)!;

            int old_id = item.ID_Bauteil;
            int itemToInsertBefore_old_ID = old_id + 1;
            foreach (Basis i in main_model.Bauteil_Basis)
            {
                if (i.ID_Bauteil == itemToInsertBefore_old_ID)
                {
                    i.ID_Bauteil = old_id;
                    item.ID_Bauteil = itemToInsertBefore_old_ID;
                    await App.Database.UpdateBauteilAsync(i);
                    break;
                }
            }
            await App.Database.UpdateBauteilAsync(item);
            OnAppearing();
            main_model.Date = DateTime.Now;
            await App.Database.UpdateItemAsync(main_model);
        }

        private void Edit_Clicked(object sender, EventArgs e)
        {
        }
    }
}
