using MFBauphysikMobilMAUI.Helpers;
using MFBauphysikMobilMAUI.Models;
using MFBauphysikMobilMAUI.NewProject;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.Maui.Controls.Xaml;
using Microsoft.Maui.Controls;
using Microsoft.Maui;
using MFBauphysikMobilMAUI;


namespace MFBauphysikMobilMAUI.NewProject
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CalculationSparren : ContentPage, INotifyPropertyChanged
    {
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
        public Basis newItem_Basis_Bauteil { get; set; }
        public Sparren newItem_Sparren_Bauteil { get; set; }
        public BefestigerBasis newItem_Basis_Befestiger { get; set; }
        public BefestigerSparren newItem_Sparren_Befestiger { get; set; }

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
        ObservableCollection<Sparren> _sparren = new ObservableCollection<Sparren>();
        public ObservableCollection<Sparren> SparrenList
        {
            get { return _sparren; }
            set
            {
                _sparren = value;
                OnPropertyChanged(nameof(Sparren));
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
        ObservableCollection<BefestigerSparren> _befestigerSparren = new ObservableCollection<BefestigerSparren>();
        public ObservableCollection<BefestigerSparren> BefestigerSparrenList
        {
            get { return _befestigerSparren; }
            set
            {
                _befestigerSparren = value;
                OnPropertyChanged(nameof(BefestigerSparren));
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
                Rupper = 1 / (((_anteilBasis / _gesamtflächen) / _rbasis) + ((_anteilSparren / _gesamtflächen) / _rsparren));
            }
        }
        private double? _rsparren;
        public double? Rsparren
        {
            get { return _rsparren; }
            set
            {
                if (_rsparren == value)
                    return;
                _rsparren = value;
                OnPropertyChanged(nameof(Rsparren));
                Rupper = 1 / (((_anteilBasis / _gesamtflächen) / _rbasis) + ((_anteilSparren / _gesamtflächen) / _rsparren));
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
        private double? _usparren;
        public double? Usparren
        {
            get { return _usparren; }
            set
            {
                if (_usparren == value)
                    return;
                _usparren = value;
                OnPropertyChanged(nameof(Usparren));
            }
        }

        //Definieren Flächenanteil OnPropertyChanged
        private double? _anteilBasis;
        public double? EntryBasis
        {
            get { return _anteilBasis; }
            set
            {
                if (_anteilBasis == value)
                    return;
                _anteilBasis = value;
                OnPropertyChanged(nameof(EntryBasis));
                Gesamtflächen = _anteilBasis + _anteilSparren;
                Rupper = 1 / (((_anteilBasis / _gesamtflächen) / _rbasis) + ((_anteilSparren / _gesamtflächen) / _rsparren));

            }
        }
        private double? _anteilSparren;
        public double? EntrySparren
        {
            get { return _anteilSparren; }
            set
            {
                if (_anteilSparren == value)
                    return;
                _anteilSparren = value;
                OnPropertyChanged(nameof(EntrySparren));
                Gesamtflächen = _anteilBasis + _anteilSparren;
                Rupper = 1 / (((_anteilBasis / _gesamtflächen) / _rbasis) + ((_anteilSparren / _gesamtflächen) / _rsparren));

            }
        }
        private double? _gesamtflächen;
        public double? Gesamtflächen
        {
            get { return _gesamtflächen; }
            set
            {
                if (_gesamtflächen == value)
                    return;
                _gesamtflächen = value;
                OnPropertyChanged(nameof(Gesamtflächen));
                Rupper = 1 / (((_anteilBasis / _gesamtflächen) / _rbasis) + ((_anteilSparren / _gesamtflächen) / _rsparren));
            }
        }

        //Definieren Fehlerabschätzung OnPropertyChanged
        private double? _rupper;
        public double? Rupper
        {
            get { return _rupper; }
            set
            {
                if (_rupper == value)
                    return;
                _rupper = value;
                OnPropertyChanged(nameof(Rupper));
                Rtot = (_rupper + _rlower) / 2;
                Abschätzung = ((_rupper - _rlower) / (2 * _rtot)) * 100;
            }
        }

        private double? _rlower;
        public double? Rlower
        {
            get { return _rlower; }
            set
            {
                if (_rlower == value)
                    return;
                _rlower = value;
                OnPropertyChanged(nameof(Rlower));
                Rtot = (_rupper + _rlower) / 2;
                Abschätzung = ((_rupper - _rlower) / (2 * _rtot)) * 100;

            }
        }
        private double? _rtot;
        public double? Rtot
        {
            get { return _rtot; }
            set
            {
                if (_rtot == value)
                    return;
                _rtot = value;
                OnPropertyChanged(nameof(Rtot));
            }
        }
        private double? _e;
        public double? Abschätzung
        {
            get { return _e; }
            set
            {
                if (_e == value)
                    return;
                _e = value;
                OnPropertyChanged(nameof(Abschätzung));
            }
        }
        private double? _ugesamt;
        public double? Ugesamt
        {
            get { return _ugesamt; }
            set
            {
                if (_ugesamt == value)
                    return;
                _ugesamt = value;
                OnPropertyChanged(nameof(Ugesamt));
                Delta_U = _ugesamt + _uf + _ug;
                AnteilKorrektur = (_uf + _ug) / _ugesamt;

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
        private double? _uf_sparren;
        public double? Uf_Sparren
        {
            get { return _uf_sparren; }
            set
            {
                if (_uf_sparren == value)
                    return;
                _uf_sparren = value;
                OnPropertyChanged(nameof(Uf_Sparren));
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
                Delta_U = _ugesamt + _uf + _ug;
                AnteilKorrektur = (_uf + _ug) / _ugesamt;
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
        private double? _ug_sparren;
        public double? Ug_Sparren
        {
            get { return _ug_sparren; }
            set
            {
                if (_ug_sparren == value)
                    return;
                _ug_sparren = value;
                OnPropertyChanged(nameof(Ug_Sparren));
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
                Delta_U = _ugesamt + _uf + _ug;
                AnteilKorrektur = (_uf + _ug) / _ugesamt;
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
        private double? _gesamtSdSparren;
        public double? Gesamt_SdSparren
        {
            get { return _gesamtSdSparren; }
            set
            {
                if (_gesamtSdSparren == value)
                    return;
                _gesamtSdSparren = value;
                OnPropertyChanged(nameof(Gesamt_SdSparren));
            }
        }
        private double? _delta0;
        public double? Delta0
        {
            get { return _delta0; }
            set
            {
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
        private string _konstruktionstyp;
        public string Konstruktionstyp
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
        private bool _nachweis_sparren;
        public bool NachweisSparren
        {
            get { return _nachweis_sparren; }
            set
            {
                if (_nachweis_sparren == value) return;
                _nachweis_sparren = value;
                OnPropertyChanged(nameof(NachweisSparren));
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
        private double? _summe_dicke_sparren;
        public double? Summe_Dicke_Sparren
        {
            get { return _summe_dicke_sparren; }
            set
            {
                if (_summe_dicke_sparren == value)
                    return;
                _summe_dicke_sparren = value;
                OnPropertyChanged(nameof(Summe_Dicke_Sparren));
            }
        }

        private double? _gesamt_gewicht;
        public double? Gesamt_Gewicht
        {
            get { return _gesamt_gewicht; }
            set
            {
                if (_gesamt_gewicht == value) return;
                _gesamt_gewicht = value;
                OnPropertyChanged(nameof(Gesamt_Gewicht));
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
        public CalculationSparren(MainModel muster)
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

            SizeDefault = Setting.Size_Default;
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
                Bauteil_Sparren = muster.Bauteil_Sparren,
                Befestiger_Sparren = muster.Befestiger_Sparren,
            };
            Aufbau = main_model.MusterName;
            if (main_model.Selected == 0)
            {
                BasisList.Add(new Basis()
                {
                    ID_Sort = 1,
                    Bezeichnung = "Gipskartonplatten",
                    R = 0.05000,
                    Dicke = 0.0125,
                    Wärmeleitfähigkeit = 0.25,
                    Rohdichte = 900,
                    Kapillar = false,
                    sonstiges = true,
                    KeineLuft = true,
                    Sd_Min = 0.10,
                    Sd_Max = 0.10,
                    Dampfdiffusionswiderstand_Min = 8,
                    Dampfdiffusionswiderstand_Max = 8
                });
                BasisList.Add(new Basis()
                {
                    ID_Sort = 2,
                    Bezeichnung = "Luft, waagrecht, schwach belüftet, d >= 25mm",
                    Dicke = 0.025,
                    Rohdichte = 1.25,
                    R = 0.08,
                    Wärmeleitfähigkeit = 0.3125,
                    Kapillar = false,
                    sonstiges = true,
                    KeineLuft = true,
                    Fester_R = true,
                    Sd_Min = 0.03,
                    Sd_Max = 0.03,
                    Dampfdiffusionswiderstand_Min = 1,
                    Dampfdiffusionswiderstand_Max = 1
                });
                BasisList.Add(new Basis()
                {
                    ID_Sort = 3,
                    Bezeichnung = "PE-Folie d >= 0.1 mm",
                    R = 0.000571,
                    Dicke = 0.0002,
                    Wärmeleitfähigkeit = 0.350,
                    Rohdichte = 1500,
                    Kapillar = true,
                    sonstiges = true,
                    KeineLuft = true,
                    Sd_Min = 20.00,
                    Sd_Max = 20.00,
                    Dampfdiffusionswiderstand_Min = 100000,
                    Dampfdiffusionswiderstand_Max = 100000
                });
                BasisList.Add(new Basis()
                {
                    ID_Sort = 4,
                    Bezeichnung = "Mineralfaser 032",
                    R = 5.6250,
                    Dicke = 0.18,
                    Wärmeleitfähigkeit = 0.032,
                    Rohdichte = 120.0,
                    Kapillar = true,
                    sonstiges = true,
                    KeineLuft = true,
                    Sd_Min = 0.18,
                    Sd_Max = 0.18,
                    Dampfdiffusionswiderstand_Min = 1,
                    Dampfdiffusionswiderstand_Max = 1
                });
                BasisList.Add(new Basis()
                {
                    ID_Sort = 5,
                    Bezeichnung = "Delta-Maxx",
                    R = 0.002353,
                    Dicke = 0.0004,
                    Wärmeleitfähigkeit = 0.17,
                    Rohdichte = 0.19,
                    Kapillar = true,
                    sonstiges = true,
                    KeineLuft = true,
                    Sd_Min = 0.15,
                    Sd_Max = 0.15,
                    Dampfdiffusionswiderstand_Min = 375,
                    Dampfdiffusionswiderstand_Max = 375
                });
                foreach (Basis i in BasisList)
                {
                    i.ModelID = main_model.ID;
                }
                for (int i = 0; i < BasisList.Count; i++)
                {
                    App.Database.SaveBauteilAsync(BasisList[i]);
                }

                SparrenList.Add(new Sparren()
                {
                    ID_Sort = 1,
                    Bezeichnung = "Gipskartonplatten",
                    R = 0.0500,
                    Dicke = 0.0125,
                    Wärmeleitfähigkeit = 0.25,
                    Rohdichte = 900,
                    Kapillar = false,
                    sonstiges = true,
                    KeineLuft = true,
                    Sd_Min = 0.10,
                    Sd_Max = 0.10,
                    Dampfdiffusionswiderstand_Min = 8,
                    Dampfdiffusionswiderstand_Max = 8
                });
                SparrenList.Add(new Sparren()
                {
                    ID_Sort = 2,
                    Bezeichnung = "Luft, waagrecht, schwach belüftet, d >= 25mm",
                    Dicke = 0.025,
                    Wärmeleitfähigkeit = 0.3125,
                    Rohdichte = 1.25,
                    R = 0.08,
                    Kapillar = false,
                    sonstiges = true,
                    KeineLuft = true,
                    Fester_R = true,
                    Sd_Min = 0.03,
                    Sd_Max = 0.03,
                    Dampfdiffusionswiderstand_Min = 1,
                    Dampfdiffusionswiderstand_Max = 1
                });
                SparrenList.Add(new Sparren()
                {
                    ID_Sort = 3,
                    Bezeichnung = "PE-Folie d >= 0.1 mm",
                    R = 0.000571,
                    Dicke = 0.0002,
                    Wärmeleitfähigkeit = 0.35,
                    Rohdichte = 1500,
                    Kapillar = true,
                    sonstiges = true,
                    KeineLuft = true,
                    Sd_Min = 20.00,
                    Sd_Max = 20.00,
                    Dampfdiffusionswiderstand_Min = 100000,
                    Dampfdiffusionswiderstand_Max = 100000
                });
                SparrenList.Add(new Sparren()
                {
                    ID_Sort = 4,
                    Bezeichnung = "Fichte",
                    R = 1.384615,
                    Dicke = 0.18,
                    Wärmeleitfähigkeit = 0.13,
                    Rohdichte = 600,
                    Kapillar = false,
                    Holz = true,
                    KeineLuft = true,
                    Sd_Min = 7.20,
                    Sd_Max = 7.20,
                    Dampfdiffusionswiderstand_Min = 40,
                    Dampfdiffusionswiderstand_Max = 40
                });
                SparrenList.Add(new Sparren()
                {
                    ID_Sort = 5,
                    Bezeichnung = "Delta-Maxx",
                    R = 0.002353,
                    Dicke = 0.0004,
                    Wärmeleitfähigkeit = 0.17,
                    Rohdichte = 0.19,
                    Kapillar = true,
                    sonstiges = true,
                    KeineLuft = true,
                    Sd_Min = 0.15,
                    Sd_Max = 0.15,
                    Dampfdiffusionswiderstand_Min = 375,
                    Dampfdiffusionswiderstand_Max = 375
                });
                foreach (Sparren i in SparrenList)
                {
                    i.ModelID = main_model.ID;
                }
                for (int i = 0; i < SparrenList.Count; i++)
                {
                    App.Database.SaveBauteilSparrenAsync(SparrenList[i]);
                }
            };

            Konstruktionstyp = "Kaltdach";
            EntryBasis = 0.60;
            EntrySparren = 0.08;

            BefestigerBoolean = false;
            BefestigerBooleanEmpty = true;

            //Visible von Datentabelle Deckblatt
            FrameBasis.IsVisible = true;
            FrameSparren.IsVisible = true;
            BefestigerFrame.IsVisible = false;

            //Visible von Button U-Wert
            UwertBasisButton.IsVisible = true;
            UwertSparrenButton.IsVisible = true;
            UgesButton.IsVisible = true;

            //Visible von Datentabelle U-Wert
            BasisUwert.IsVisible = false;
            SparrenUwert.IsVisible = false;
            GesUwert.IsVisible = false;

            //Visible von Flächenanteil Angabe
            AnteilBasis.IsVisible = false;
            AnteilSparren.IsVisible = false;

            //Calculate_Ug();
            BindingContext = this;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            GetBasis();
            GetSparren();
            if (MeldungDicke.IsVisible == true)
            {
                Meldung3Ebene.IsVisible = false;
            }
            
        }

        private async void GetBasis()
        {
            //base.OnAppearing();
            //Bauteil
            var item = await App.Database.GetBauteilAsync();
            foreach (Basis i in item)
            {
                i.SizeClass = Setting.Size_Default;
                if (i.ModelID == main_model.ID)
                {
                    main_model.Bauteil_Basis.Add(i);
                    // main_model.Bauteil_Basis.OrderBy(p => p.ID_Bauteil);

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
            //Sorted List
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
                j.SizeClass = Setting.Size_Default;
                if (j.ModelID == main_model.ID)
                {
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
            CalculateFehler_Abschätzung();
            Calculate_Uf();
            Calculate_Ug();
            Calculate_DeltaU();
        }

        private async void GetSparren()
        {
            //Bauteil
            var itemSparren = await App.Database.GetBauteilSparrenAsync();
            foreach (Sparren i in itemSparren)
            {
                i.SizeClass = Setting.Size_Default;
                if (i.ModelID == main_model.ID)
                {
                    main_model.Bauteil_Sparren.Add(i);
                    for (int m = 0; m <= main_model.Bauteil_Sparren.Count - 1; m++)
                    {
                        for (int n = m + 1; n <= main_model.Bauteil_Sparren.Count - 1; n++)
                        {
                            if (main_model.Bauteil_Sparren[m].ID_Bauteil == main_model.Bauteil_Sparren[n].ID_Bauteil)
                            {
                                main_model.Bauteil_Sparren.Remove(main_model.Bauteil_Sparren[m]);
                            }
                        }
                    }
                }
                i.Gewicht = i.Dicke * i.Rohdichte;
            }
            //Sorted List
            if (main_model.Selected == 0)
            {
                for (int i = 0; i < main_model.Bauteil_Sparren.Count - 1; i++)
                {
                    for (int j = i + 1; j < main_model.Bauteil_Sparren.Count; j++)
                    {
                        if (main_model.Bauteil_Sparren[i].ID_Sort > main_model.Bauteil_Sparren[j].ID_Sort)
                        {
                            var oldItem = main_model.Bauteil_Sparren[i];
                            var newItem = main_model.Bauteil_Sparren[j];
                            main_model.Bauteil_Sparren[i] = newItem;
                            main_model.Bauteil_Sparren[j] = oldItem;
                            await App.Database.UpdateBauteilSparrenAsync(main_model.Bauteil_Sparren[i]);
                            await App.Database.UpdateBauteilSparrenAsync(main_model.Bauteil_Sparren[j]);
                        }
                        if (main_model.Bauteil_Sparren[i].ID_Bauteil > main_model.Bauteil_Sparren[j].ID_Bauteil)
                        {
                            int oldID = main_model.Bauteil_Sparren[i].ID_Bauteil;
                            int newID = main_model.Bauteil_Sparren[j].ID_Bauteil;
                            main_model.Bauteil_Sparren[i].ID_Bauteil = newID;
                            main_model.Bauteil_Sparren[j].ID_Bauteil = oldID;
                            await App.Database.UpdateBauteilSparrenAsync(main_model.Bauteil_Sparren[i]);
                            await App.Database.UpdateBauteilSparrenAsync(main_model.Bauteil_Sparren[j]);
                        }
                    }
                }
            }
            listSparren.ItemsSource = main_model.Bauteil_Sparren;
            listSparrenUwert.ItemsSource = main_model.Bauteil_Sparren;
            CalculateSum_Sparren();

            //Befestiger
            var fixSparren = await App.Database.GetFixSparrenAsync();
            foreach (BefestigerSparren j in fixSparren)
            {
                j.SizeClass = Setting.Size_Default;
                if (j.ModelID == main_model.ID)
                {
                    main_model.Befestiger_Sparren.Add(j);
                    for (int m = 0; m <= main_model.Befestiger_Sparren.Count - 1; m++)
                    {
                        for (int n = m + 1; n <= main_model.Befestiger_Sparren.Count - 1; n++)
                        {
                            //Update Befestiger 
                            if (main_model.Befestiger_Sparren[m].ID_Befestiger == main_model.Befestiger_Sparren[n].ID_Befestiger)
                            {
                                main_model.Befestiger_Sparren.Remove(main_model.Befestiger_Sparren[m]);
                            }

                        }
                    }
                }
            }
            listBefestigerSparren.ItemsSource = main_model.Befestiger_Sparren;
            if (main_model.Befestiger_Sparren.Count != 0)
            {
                FrameBefestigerSparren.IsVisible = true;
            }
            else
            {
                FrameBefestigerSparren.IsVisible = false;
            }
            CalculateFehler_Abschätzung();
            Calculate_Uf();
            Calculate_Ug();
            Calculate_DeltaU();
        }  
       
        public void CalculateSum_Basis()
        {

            if (Konstruktionstyp == "Warmdach")
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
                        NachweisGrid.IsVisible = false;
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
                            MeldungDicke.IsVisible = false;
                            double? tev = i.VerdunstungsDauer * 3600 * Math.Pow(10, 3);
                            double? sdc = 0;
                            sdc = Schichtgrenze[Schichtgrenze.Count - 1].SumSd + Schichtgrenze[Schichtgrenze.Count - 1].Sd;
                            Tauwassermasse = Delta0 * tev * (((i.InnenWasserdampfdruck - main_model.Bauteil_Basis[m].Dampfteildruck) / sdc) - ((main_model.Bauteil_Basis[m].Dampfteildruck - i.AußenWasserdampfdruck) / (Gesamt_SdBasis - sdc)));
                            Verdunstungsmasse = Delta0 * tev * (((Pc - i.InnenDruckVerdunstung) / sdc) + ((Pc - i.AußenDruckVerdunstung) / (Gesamt_SdBasis - sdc)));
                        }

                        //Tauwasserausfall in zwei Ebenen A.2.5.5 Din 4108-3 Fall d
                        else if (Ebene == 2)
                        {
                            NachweisGrid.IsVisible = false;
                            Meldung3Ebene.IsVisible = false;
                            MeldungDicke.IsVisible = false;

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
                    MeldungDicke.IsVisible = false;
                    Meldung3Ebene.IsVisible = false;
                    Feuchtenachweis.Text = "ok";
                }
                else if (Ebene == 1 || Ebene == 2)
                {
                    if (Meldung3Ebene.IsVisible == true || MeldungDicke.IsVisible == true)
                    {
                        Feuchtenachweis.Text = "bitte überprüfen";
                    }
                    else
                    {
                        if (Tauwassermasse < ZulTauwasser && Tauwassermasse < Verdunstungsmasse)
                        {
                            Feuchtenachweis.Text = "ok";
                        }

                        else
                        {
                            Feuchtenachweis.Text = "bitte überprüfen";
                        }
                    }
                }
                else if (Ebene > 2)
                {
                    Meldung3Ebene.IsVisible = true;
                    MeldungDicke.IsVisible = false;
                }
            }
        }
        public void CalculateSum_Sparren()
        {

            if (Konstruktionstyp == "Warmdach")
            {
                Pc = 2000;
            }
            else
            {
                Pc = 1700;
            }
            Rsparren = main_model.Bauteil_Sparren.Sum(p => p.R) + Aufwärts + Außen;
            Summe_Dicke_Sparren = main_model.Bauteil_Sparren.Sum(p => p.Dicke);
            Usparren = 1 / Rsparren;


            //Sd-Wert in Tauperiode = sd-Min
            //Bestimmen von Temperatur, Sättigungsdampfdruck
            foreach (KlimadatenClass i in Klimadaten)
            {
                Wärmestromdichte = (i.InnenTemp - i.AußenTemp) / (main_model.Bauteil_Sparren.Sum(p => p.R) + Innen_TWN + Außen);
                double? innen_feuchtenachweis = i.InnenTemp - 0.25 * Wärmestromdichte;
                main_model.Bauteil_Sparren[0].Tempverlauf = innen_feuchtenachweis - Wärmestromdichte * main_model.Bauteil_Sparren[0].R;
                main_model.Bauteil_Sparren[0].Dampfsättigungsdruck = 610.5 * Math.Exp(Convert.ToDouble((17.269 * main_model.Bauteil_Sparren[0].Tempverlauf) / (237.3 + main_model.Bauteil_Sparren[0].Tempverlauf)));


                main_model.Bauteil_Sparren[0].Sd = main_model.Bauteil_Sparren[0].Sd_Min;
                Gesamt_SdSparren = main_model.Bauteil_Sparren.Sum(p => p.Sd_Min);
                Faktor_Dampfdruckverteilung = (i.InnenWasserdampfdruck - i.AußenWasserdampfdruck) / Gesamt_SdSparren;
                main_model.Bauteil_Sparren[0].Dampfteildruck = i.InnenWasserdampfdruck - Faktor_Dampfdruckverteilung * main_model.Bauteil_Sparren[0].Sd;

                int Ebene = 0;
                for (int m = 1; m <= main_model.Bauteil_Sparren.Count - 1; m++)
                {
                    //Temperaturverlauf
                    main_model.Bauteil_Sparren[m].Tempverlauf = main_model.Bauteil_Sparren[m - 1].Tempverlauf - main_model.Bauteil_Sparren[m].R * Wärmestromdichte;

                    //Sättigungsdampfdruck Psat (DIN 4108-3:2018-10 Anhang C.4)                    
                    if (main_model.Bauteil_Sparren[m].Tempverlauf >= 0)
                    {
                        main_model.Bauteil_Sparren[m].Dampfsättigungsdruck = 610.5 * Math.Exp(Convert.ToDouble(17.269 * main_model.Bauteil_Sparren[m].Tempverlauf / (237.3 + main_model.Bauteil_Sparren[m].Tempverlauf)));
                    }
                    else
                    {
                        main_model.Bauteil_Sparren[m].Dampfsättigungsdruck = 610.5 * Math.Exp(Convert.ToDouble(21.875 * main_model.Bauteil_Sparren[m].Tempverlauf / (265.5 + main_model.Bauteil_Sparren[m].Tempverlauf)));
                    }

                    //Wasserdampfteildruck
                    main_model.Bauteil_Sparren[m].Sd = main_model.Bauteil_Sparren[m].Sd_Min;
                    main_model.Bauteil_Sparren[m].Dampfteildruck = main_model.Bauteil_Sparren[(m - 1)].Dampfteildruck - Faktor_Dampfdruckverteilung * main_model.Bauteil_Sparren[m].Sd;


                    //Tauwasserausfall
                    if (main_model.Bauteil_Sparren[m].Dampfteildruck > main_model.Bauteil_Sparren[m].Dampfsättigungsdruck)
                    {
                        NachweisGrid.IsVisible = false;
                        Ebene = Ebene + 1;
                        main_model.Bauteil_Sparren[m].Dampfteildruck = main_model.Bauteil_Sparren[m].Dampfsättigungsdruck;
                        main_model.Bauteil_Sparren[m].TW = true;

                        //max. zulässige Tauwassermasse
                        //kapillar nicht wasseraufnahmefähig
                        if (main_model.Bauteil_Sparren[m].Kapillar == true || main_model.Bauteil_Sparren[m + 1].Kapillar == true)
                        {
                            ZulTauwasser = 500;

                            //Holz 5%
                            if (main_model.Bauteil_Sparren[m].Holz == true && main_model.Bauteil_Sparren[m + 1].Holz == true)
                            {
                                double w1 = (double)main_model.Bauteil_Sparren[m].Gewicht * 1000 * 0.05;
                                double w2 = (double)main_model.Bauteil_Sparren[m + 1].Gewicht * 1000 * 0.05;
                                ZulTauwasser = Math.Min(w1, w2);
                            }
                            else
                            {
                                if (main_model.Bauteil_Sparren[m].Holz == true)
                                {
                                    ZulTauwasser = main_model.Bauteil_Sparren[m].Gewicht * 1000 * 0.05;
                                }
                                else if (main_model.Bauteil_Sparren[m + 1].Holz == true)
                                {
                                    ZulTauwasser = main_model.Bauteil_Sparren[m + 1].Gewicht * 1000 * 0.05;
                                }
                            }

                            //Holzwerkstoff mit 3%
                            if (main_model.Bauteil_Sparren[m].Holzwerkstoff == true && main_model.Bauteil_Sparren[m + 1].Holzwerkstoff == true)
                            {
                                double w1 = (double)main_model.Bauteil_Sparren[m].Gewicht * 1000 * 0.03;
                                double w2 = (double)main_model.Bauteil_Sparren[m + 1].Gewicht * 1000 * 0.03;
                                ZulTauwasser = Math.Min(w1, w2);
                            }
                            else if (main_model.Bauteil_Sparren[m].Holzwerkstoff == true)
                            {
                                ZulTauwasser = main_model.Bauteil_Sparren[m].Gewicht * 1000 * 0.03;
                            }
                            else if (main_model.Bauteil_Sparren[m + 1].Holzwerkstoff == true)
                            {
                                ZulTauwasser = main_model.Bauteil_Sparren[m + 1].Gewicht * 1000 * 0.03;
                            }

                            //Kombination von Holz und Holzwerkstoff
                            if (main_model.Bauteil_Sparren[m].Holz == true && main_model.Bauteil_Sparren[m + 1].Holzwerkstoff == true)
                            {
                                double w1 = (double)(main_model.Bauteil_Sparren[m].Gewicht * 1000 * 0.05);
                                double w2 = (double)(main_model.Bauteil_Sparren[m + 1].Gewicht * 1000 * 0.03);
                                ZulTauwasser = Math.Min(w1, w2);
                            }
                            else if (main_model.Bauteil_Sparren[m].Holzwerkstoff == true && main_model.Bauteil_Sparren[m + 1].Holz == true)
                            {
                                double w1 = (double)main_model.Bauteil_Sparren[m].Gewicht * 1000 * 0.03;
                                double w2 = (double)main_model.Bauteil_Sparren[m + 1].Gewicht * 1000 * 0.05;
                                ZulTauwasser = Math.Min(w1, w2);
                            }
                        }

                        //kapillar wasseraufnahmefähig
                        else
                        {
                            ZulTauwasser = 1000;
                            //Holz 5%
                            if (main_model.Bauteil_Sparren[m].Holz == true && main_model.Bauteil_Sparren[m + 1].Holz == true)
                            {
                                double w1 = (double)main_model.Bauteil_Sparren[m].Gewicht * 1000 * 0.05;
                                double w2 = (double)main_model.Bauteil_Sparren[m + 1].Gewicht * 1000 * 0.05;
                                ZulTauwasser = Math.Min(w1, w2);
                            }
                            else
                            {
                                if (main_model.Bauteil_Sparren[m].Holz == true)
                                {
                                    ZulTauwasser = main_model.Bauteil_Sparren[m].Gewicht * 1000 * 0.05;
                                }
                                else if (main_model.Bauteil_Sparren[m + 1].Holz == true)
                                {
                                    ZulTauwasser = main_model.Bauteil_Sparren[m + 1].Gewicht * 1000 * 0.05;
                                }
                            }

                            //Holzwerkstoff mit 3%
                            if (main_model.Bauteil_Sparren[m].Holzwerkstoff == true && main_model.Bauteil_Sparren[m + 1].Holzwerkstoff == true)
                            {
                                double w1 = (double)main_model.Bauteil_Sparren[m].Gewicht * 1000 * 0.03;
                                double w2 = (double)main_model.Bauteil_Sparren[m + 1].Gewicht * 1000 * 0.03;
                                ZulTauwasser = Math.Min(w1, w2);
                            }
                            else
                            {
                                if (main_model.Bauteil_Sparren[m].Holzwerkstoff == true)
                                {
                                    ZulTauwasser = main_model.Bauteil_Sparren[m].Gewicht * 1000 * 0.03;
                                }
                                else if (main_model.Bauteil_Sparren[m + 1].Holzwerkstoff == true)
                                {
                                    ZulTauwasser = main_model.Bauteil_Sparren[m + 1].Gewicht * 1000 * 0.03;
                                }
                            }

                            //Kombination von Holz und Holzwerkstoff
                            if (main_model.Bauteil_Sparren[m].Holz == true && main_model.Bauteil_Sparren[m + 1].Holzwerkstoff == true)
                            {
                                double w1 = (double)(main_model.Bauteil_Sparren[m].Gewicht * 1000 * 0.05);
                                double w2 = (double)(main_model.Bauteil_Sparren[m + 1].Gewicht * 1000 * 0.03);
                                ZulTauwasser = Math.Min(w1, w2);
                            }
                            else if (main_model.Bauteil_Sparren[m].Holzwerkstoff == true && main_model.Bauteil_Sparren[m + 1].Holz == true)
                            {
                                double w1 = (double)main_model.Bauteil_Sparren[m].Gewicht * 1000 * 0.03;
                                double w2 = (double)main_model.Bauteil_Sparren[m + 1].Gewicht * 1000 * 0.05;
                                ZulTauwasser = Math.Min(w1, w2);
                            }
                        }

                        double? sum_sd = 0;
                        for (int j = 0; j <= m - 1; j++)
                        {
                            sum_sd = sum_sd + main_model.Bauteil_Sparren[j].Sd;
                            Faktor_Dampfdruckverteilung = (main_model.Bauteil_Sparren[m].Dampfteildruck - i.AußenWasserdampfdruck) / (Gesamt_SdSparren - (main_model.Bauteil_Sparren[m].Sd + sum_sd));
                        }

                        Schichtgrenze.Add(new Schichtgrenzen()
                        {
                            Dampfteildruck = (double)main_model.Bauteil_Sparren[m].Dampfteildruck,
                            Sd = (double)main_model.Bauteil_Sparren[m].Sd,
                            SumSd = (double)sum_sd,

                        });
                        //Tauwasserausfall in einer Ebene A.2.5.3 DIN 4108-3 Fall b
                        if (Ebene == 1)
                        {
                            Meldung3Ebene.IsVisible = false;
                            MeldungDicke.IsVisible = false;
                            double? tev = i.VerdunstungsDauer * 3600 * Math.Pow(10, 3);
                            double? sdc = 0;
                            sdc = Schichtgrenze[Schichtgrenze.Count - 1].SumSd + Schichtgrenze[Schichtgrenze.Count - 1].Sd;
                            Tauwassermasse = Delta0 * tev * (((i.InnenWasserdampfdruck - main_model.Bauteil_Sparren[m].Dampfteildruck) / sdc) - ((main_model.Bauteil_Sparren[m].Dampfteildruck - i.AußenWasserdampfdruck) / (Gesamt_SdSparren - sdc)));
                            Verdunstungsmasse = Delta0 * tev * (((Pc - i.InnenDruckVerdunstung) / sdc) + ((Pc - i.AußenDruckVerdunstung) / (Gesamt_SdSparren - sdc)));
                        }

                        //Tauwasserausfall in zwei Ebenen A.2.5.5 Din 4108-3 Fall d
                        else if (Ebene == 2)
                        {
                            Meldung3Ebene.IsVisible = false;
                            MeldungDicke.IsVisible = false;
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
                            Mc2 = Delta0 * i.TauDauer * 3600 * Math.Pow(10, 3) * (((Schichtgrenze[Schichtgrenze.Count - 2].Dampfteildruck - Schichtgrenze[Schichtgrenze.Count - 1].Dampfteildruck) / sdc_sub) - ((Schichtgrenze[Schichtgrenze.Count - 1].Dampfteildruck - i.AußenWasserdampfdruck) / (Gesamt_SdSparren - sdc2)));

                            Tauwassermasse = Mc1 + Mc2;

                            //Verdunstungsperiode
                            double? tev = i.VerdunstungsDauer * 3600 * Math.Pow(10, 3);
                            double? tev1 = 0;
                            double? tev2 = 0;
                            double? gev1 = 0;
                            double? gev2 = 0;

                            gev1 = Delta0 * (Pc - i.InnenDruckVerdunstung) / sdc1;
                            gev2 = Delta0 * (Pc - i.AußenDruckVerdunstung) / (Gesamt_SdSparren - sdc2);
                            tev1 = Mc1 / gev1;
                            tev2 = Mc2 / gev2;

                            double? Mev1 = 0;
                            double? Mev2 = 0;

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
                                    Mev1 = gev1 * tev2 + (gev1 + Delta0 * (Pc - i.AußenDruckVerdunstung) / (Gesamt_SdSparren - sdc1)) * (tev - tev2);
                                }
                            }
                            Verdunstungsmasse = Mev1 + Mev2;
                        }

                        else if (Ebene > 2)
                        {
                            NachweisGrid.IsVisible = false;
                            Meldung3Ebene.IsVisible = true;
                            Feuchtenachweis.Text = "bitte überprüfen";
                            NachweisSparren = false;
                        }
                    }

                    //Kein Tauwasserausfall
                    else
                    {
                        main_model.Bauteil_Sparren[m].TW = false;
                    }
                }
                //Feuchtenachweis
                if (Ebene == 0)
                {
                    NachweisGrid.IsVisible = false;
                    Feuchtenachweis.Text = "ok";
                }
                else if (Ebene == 1 || Ebene == 2)
                {
                    if (Meldung3Ebene.IsVisible == true || MeldungDicke.IsVisible == true)
                    {
                        Feuchtenachweis.Text = "bitte überprüfen";
                    }
                    else
                    {
                        if (Tauwassermasse < ZulTauwasser && Tauwassermasse < Verdunstungsmasse)
                        {
                            Feuchtenachweis.Text = "ok";
                        }

                        else
                        {
                            Feuchtenachweis.Text = "bitte überprüfen";
                        }
                    }
                }
                else if (Ebene > 2)
                {
                    Meldung3Ebene.IsVisible = true;
                    MeldungDicke.IsVisible = false;
                }
            }
        }

        public void CalculateFehler_Abschätzung()
        {
            double rj;
            Berechnung_Rj.Clear();

            for (int m = 0; m <= main_model.Bauteil_Basis.Count - 1; m++)
            {
                for (int n = 0; n <= main_model.Bauteil_Sparren.Count - 1; n++)
                {
                    if (m == n)
                    {
                        rj = (double)(1 / (((EntryBasis / Gesamtflächen) / main_model.Bauteil_Basis[m].R) + ((EntrySparren / Gesamtflächen) / main_model.Bauteil_Sparren[m].R)));
                        Berechnung_Rj.Add(new Berechnung_R() { Rj = rj });
                    }
                }
            }

            Rlower = Berechnung_Rj.Sum(p => p.Rj) + Aufwärts + Außen;
            Rtot = (Rupper + Rlower) / 2;
            Abschätzung = ((Rupper - Rlower) / (2 * Rtot)) * 100;
            Ugesamt = 1 / Rtot;
        }

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
                    Ug_Basis = (EntryBasis / Gesamtflächen) * DU_g * Math.Pow(Convert.ToDouble(i.R / Rbasis), 2);
                }
            }
            foreach (Sparren i in main_model.Bauteil_Sparren)
            {
                if (i.R == main_model.Bauteil_Sparren.Max(p => p.R))
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
                    Ug_Sparren = (EntrySparren / Gesamtflächen) * DU_g * Math.Pow((Convert.ToDouble(i.R / Rsparren)), 2);
                }
            }
            Ug = Ug_Sparren + Ug_Basis;
        }

        //Befestiger dUf
        private void Calculate_Uf()
        {
            //Basis
            foreach (Basis i in main_model.Bauteil_Basis)
            {
                if (i.Wärmeleitfähigkeit == main_model.Bauteil_Basis.Where(m => m.Wärmeleitfähigkeit != 0).Min(m => m.Wärmeleitfähigkeit))
                {
                    foreach (BefestigerBasis j in main_model.Befestiger_Basis)
                    {
                        if (j.Länge == null)
                        {
                            j.Uf_i = (EntryBasis / Gesamtflächen) * (0.8 * j.Wärmeleitfähigkeit_f * Math.PI * Math.Pow((Convert.ToDouble(j.Durchmesser / 1000) / 2), 2) * j.Anzahl * Math.Pow(Convert.ToDouble(i.R / Rbasis), 2)) / i.Dicke;
                        }
                        else
                        {
                            j.Uf_i = (EntryBasis / Gesamtflächen) * (0.8 * j.Wärmeleitfähigkeit_f * Math.PI * Math.Pow((Convert.ToDouble(j.Durchmesser / 1000) / 2), 2) * j.Anzahl * Math.Pow(Convert.ToDouble(i.R / Rbasis), 2)) / (j.Länge / 1000);
                        }
                    }
                    Uf_Basis = main_model.Befestiger_Basis.Sum(p => p.Uf_i);
                }
            }
            //Sparren nicht relevant, da kein Befestiger betrachtet wird
            /*foreach (Sparren i in main_model.Bauteil_Sparren)
             {
                 if (i.Wärmeleitfähigkeit == main_model.Bauteil_Sparren.Min(m => m.Wärmeleitfähigkeit))
                 {
                     foreach (BefestigerSparren j in main_model.Befestiger_Sparren)
                     {
                         if (j.Länge == null)
                         {
                             j.Uf_i = (EntrySparren / Gesamtflächen) * (0.8 * j.Wärmeleitfähigkeit_f * Math.PI * Math.Pow((Convert.ToDouble(j.Durchmesser / 1000) / 2), 2) * j.Anzahl * Math.Pow(Convert.ToDouble(i.R / Rsparren), 2)) / i.Dicke;

                         }
                         else
                         {
                             j.Uf_i = (EntrySparren / Gesamtflächen) * (0.8 * j.Wärmeleitfähigkeit_f * Math.PI * Math.Pow((Convert.ToDouble(j.Durchmesser / 1000) / 2), 2) * j.Anzahl * Math.Pow(Convert.ToDouble(i.R / Rsparren), 2)) / (j.Länge / 1000);
                         }
                     }
                     Uf_Sparren = main_model.Befestiger_Sparren.Sum(p => p.Uf_i);
                 }

             }*/
            //Uf = Uf_Basis + Uf_Sparren;
            Uf = Uf_Basis;

        }
        private void Calculate_DeltaU()
        {
            Gesamt_du = Uf + Ug;
            Delta_U = Ugesamt + Uf + Ug;
            AnteilKorrektur = ((Uf + Ug) / Ugesamt) * 100;

            if (AnteilKorrektur <= 3)
            {
                Grid_dU.IsVisible = false;
                Grid_Uwert_ohne_Fugen.IsVisible = false;
            }
            else
            {
                Grid_dU.IsVisible = true;
                Grid_Uwert_ohne_Fugen.IsVisible = true;
            }

        }

        private async void OnSelected_ItemSelected_Basis(object sender, SelectedItemChangedEventArgs e)
        {
            if (BasisUwert.IsVisible == true)
            {
                if (listBasisUwert.SelectedItem == null)
                    return;
                var selectedBauteil = e.SelectedItem as Basis;

                //unselected
                listBasisUwert.SelectedItem = null;

                var Basis = new BasisDetailPage(selectedBauteil);

                Basis.BasisUpdated += (source, bauteil) =>
                {
                    selectedBauteil.ID_Bauteil = bauteil.ID_Bauteil;
                    selectedBauteil.Bezeichnung = bauteil.Bezeichnung;
                    selectedBauteil.R = bauteil.R;
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
        //Sparren item selected
        private async void OnSelected_ItemSelected_Sparren(object sender, SelectedItemChangedEventArgs e)

        {
            if (SparrenUwert.IsVisible == true)
            {
                if (listSparrenUwert.SelectedItem == null)
                    return;
                var selectedSparren = (e.SelectedItem as Sparren)!;
                listSparrenUwert.SelectedItem = null;
                var Sparren = new SparrenDetailPage(selectedSparren);
                Sparren.SparrenUpdated += (source, sparren) =>
                {
                    selectedSparren.ID_Bauteil = sparren.ID_Bauteil;
                    selectedSparren.ModelID = sparren.ModelID;
                    selectedSparren.Bezeichnung = sparren.Bezeichnung;
                    selectedSparren.R = sparren.R;
                    selectedSparren.Dicke = sparren.Dicke;
                    selectedSparren.Wärmeleitfähigkeit = sparren.Wärmeleitfähigkeit;
                    selectedSparren.Rohdichte = sparren.Rohdichte;
                    selectedSparren.Kapillar = sparren.Kapillar;
                    selectedSparren.Holz = sparren.Holz;
                    selectedSparren.Holzwerkstoff = sparren.Holzwerkstoff;
                    selectedSparren.sonstiges = sparren.sonstiges;
                    selectedSparren.KeineLuft = sparren.KeineLuft;
                    selectedSparren.EvntlLuft = sparren.EvntlLuft;
                    selectedSparren.MitLuft = sparren.MitLuft;
                    selectedSparren.Dampfdiffusionswiderstand_Min = sparren.Dampfdiffusionswiderstand_Min;
                    selectedSparren.Dampfdiffusionswiderstand_Max = sparren.Dampfdiffusionswiderstand_Max;
                    selectedSparren.Sd_Min = sparren.Sd_Min;
                    selectedSparren.Sd_Max = sparren.Sd_Max;
                    selectedSparren.Sd = sparren.Sd;
                    selectedSparren.Tempverlauf = sparren.Tempverlauf;
                    selectedSparren.Dampfteildruck = sparren.Dampfteildruck;
                    selectedSparren.Dampfsättigungsdruck = sparren.Dampfsättigungsdruck;
                    selectedSparren.TW = sparren.TW;
                    selectedSparren.Fester_R = sparren.Fester_R;
                    selectedSparren.Gewicht = sparren.Gewicht;
                    selectedSparren.Fester_sd = sparren.Fester_sd;
                    selectedSparren.DLR1 = sparren.DLR1;
                    selectedSparren.DLR2 = sparren.DLR2;
                    selectedSparren.DLR3 = sparren.DLR3;
                    selectedSparren.DLR4 = sparren.DLR4;
                    selectedSparren.DLR5 = sparren.DLR5;
                    selectedSparren.LR1 = sparren.LR1;
                    selectedSparren.LR2 = sparren.LR2;
                    selectedSparren.LR3 = sparren.LR3;
                    selectedSparren.LR4 = sparren.LR4;
                    selectedSparren.LR5 = sparren.LR5;

                    foreach (Sparren i in SparrenList)
                    {
                        if (Double.IsInfinity(Convert.ToDouble(i.R)) || Double.IsNaN(Convert.ToDouble(i.R)))
                        {
                            i.R = 0;
                        }
                    }
                    CalculateSum_Sparren();
                    Calculate_Ug();
                    Calculate_Uf();
                    Calculate_DeltaU();

                };

                main_model.Bauteil_Sparren.Remove(selectedSparren);
                CalculateSum_Sparren();
                Calculate_Ug();
                Calculate_Uf();
                Calculate_DeltaU();
                main_model.Date = DateTime.Now;
                await App.Database.UpdateItemAsync(main_model);
                await Navigation.PushAsync(new SparrenDetailPage(selectedSparren)
                {
                    BindingContext = selectedSparren,
                });

            }
        }

        public void Deckblatt_Tapped(object sender, EventArgs e)
        {
            //Anzeige von Datentabelle in Deckblatt
            DeckblattTab.IsVisible = true;

            //Anzeige von U-Wert Frame
            BasisUwert.IsVisible = false;
            SparrenUwert.IsVisible = false;
            GesUwert.IsVisible = false;
            BefestigerFrame.IsVisible = false;

            //Anzeige von Flächenanteil
            AnteilBasis.IsVisible = false;
            AnteilSparren.IsVisible = false;

            //Text Bold wenn Tab ausgewählt wurde
            DeckblattButton.FontAttributes = FontAttributes.Bold;
            UwertBasisButton.FontAttributes = FontAttributes.None;
            UwertSparrenButton.FontAttributes = FontAttributes.None;
            UgesButton.FontAttributes = FontAttributes.None;
            BefestigerButton.FontAttributes = FontAttributes.None;
            //Tau_Button.FontAttributes = FontAttributes.None;

            //Text underlined wenn Tab ausgewählt wurde
            DeckblattButton.TextDecorations = TextDecorations.Underline;
            UwertBasisButton.TextDecorations = TextDecorations.None;
            UwertSparrenButton.TextDecorations = TextDecorations.None;
            UgesButton.TextDecorations = TextDecorations.None;
            BefestigerButton.TextDecorations = TextDecorations.None;
            //Tau_Button.TextDecorations = TextDecorations.None;

            //Plusbutton
            BoxPlus.IsVisible = false;
            ButtonPlus.IsVisible = false;

            //Sparren

            NachweisGrid.IsVisible = false;
            MeldungDicke.IsVisible = false;

            if (main_model.Bauteil_Basis.Count != main_model.Bauteil_Sparren.Count)
            {
                NachweisGrid.IsVisible = false;
                MeldungDicke.IsVisible = true;
                Feuchtenachweis.Text = "bitte überprüfen";
            }
            else
            {
                Summe_Dicke_Basis = main_model.Bauteil_Basis.Sum(p => p.Dicke);
                Summe_Dicke_Sparren = main_model.Bauteil_Sparren.Sum(p => p.Dicke);
                if (Math.Abs((double)(Summe_Dicke_Basis - Summe_Dicke_Sparren)) > 0.000000000001)
                {
                    NachweisGrid.IsVisible = false;
                    MeldungDicke.IsVisible = true;
                    Feuchtenachweis.Text = "bitte überprüfen";
                }
                else
                {
                    CalculateSum_Basis();
                    CalculateSum_Sparren();
                }
            }

        }
        public void Basis_Tapped(Object sender, EventArgs e)
        {
            main_model.Selected = 1;
            //Anzeige von Datentabelle in Deckblatt
            DeckblattTab.IsVisible = false;

            //Anzeige von U-Wert Frame
            BasisUwert.IsVisible = true;
            SparrenUwert.IsVisible = false;
            GesUwert.IsVisible = false;
            BefestigerFrame.IsVisible = false;

            //Anzeige von Analyse Tauperiode
            //TauTab.IsVisible = false;

            //Anzeige von Flächenanteil            
            AnteilBasis.IsVisible = true;
            AnteilSparren.IsVisible = false;

            //Text Bold wenn Tab ausgewählt wurde
            DeckblattButton.FontAttributes = FontAttributes.None;
            UwertBasisButton.FontAttributes = FontAttributes.Bold;
            UwertSparrenButton.FontAttributes = FontAttributes.None;
            UgesButton.FontAttributes = FontAttributes.None;
            BefestigerButton.FontAttributes = FontAttributes.None;
            //Tau_Button.FontAttributes = FontAttributes.None;

            //Text underlined wenn Tab ausgewählt wurde
            DeckblattButton.TextDecorations = TextDecorations.None;
            UwertBasisButton.TextDecorations = TextDecorations.Underline;
            UwertSparrenButton.TextDecorations = TextDecorations.None;
            UgesButton.TextDecorations = TextDecorations.None;
            BefestigerButton.TextDecorations = TextDecorations.None;
            //Tau_Button.TextDecorations = TextDecorations.None;

            //Plusbutton
            BoxPlus.IsVisible = true;
            ButtonPlus.IsVisible = true;

            BefestigerBoolean = false;
            BefestigerBooleanEmpty = true;

        }
        private void Sparren_Tapped(object sender, EventArgs e)
        {
            main_model.Selected = 1;
            //Anzeige von Datentabelle in Deckblatt
            DeckblattTab.IsVisible = false;

            //Anzeige von Analyse Tauperiode
            // TauTab.IsVisible = false;

            //Anzeige von U-Wert Frame
            BasisUwert.IsVisible = false;
            SparrenUwert.IsVisible = true;
            GesUwert.IsVisible = false;
            BefestigerFrame.IsVisible = false;

            //Anzeige von Flächenanteil
            AnteilBasis.IsVisible = false;
            AnteilSparren.IsVisible = true;

            //Text Bold wenn Tab ausgewählt wurde
            DeckblattButton.FontAttributes = FontAttributes.None;
            UwertBasisButton.FontAttributes = FontAttributes.None;
            UwertSparrenButton.FontAttributes = FontAttributes.Bold;
            UgesButton.FontAttributes = FontAttributes.None;
            BefestigerButton.FontAttributes = FontAttributes.None;
            //Tau_Button.FontAttributes = FontAttributes.None;

            //Text underlined wenn Tab ausgewählt wurde
            DeckblattButton.TextDecorations = TextDecorations.None;
            UwertBasisButton.TextDecorations = TextDecorations.None;
            UwertSparrenButton.TextDecorations = TextDecorations.Underline;
            UgesButton.TextDecorations = TextDecorations.None;
            BefestigerButton.TextDecorations = TextDecorations.None;
            //Tau_Button.TextDecorations = TextDecorations.None;

            //Plusbutton
            BoxPlus.IsVisible = true;
            ButtonPlus.IsVisible = true;

        }
        private void Uges_Tapped(object sender, EventArgs e)
        {
            //Anzeige von Datentabelle in Deckblatt
            DeckblattTab.IsVisible = false;

            //Anzeige von U-Wert Frame
            BasisUwert.IsVisible = false;
            SparrenUwert.IsVisible = false;
            GesUwert.IsVisible = true;
            BefestigerFrame.IsVisible = false;

            //Anzeige von Analyse Tauperiode
            //TauTab.IsVisible = false;

            //Anzeige von Flächenanteil
            AnteilBasis.IsVisible = false;
            AnteilSparren.IsVisible = false;

            //Text Bold wenn Tab ausgewählt wurde
            DeckblattButton.FontAttributes = FontAttributes.None;
            UwertBasisButton.FontAttributes = FontAttributes.None;
            UwertSparrenButton.FontAttributes = FontAttributes.None;
            UgesButton.FontAttributes = FontAttributes.Bold;
            BefestigerButton.FontAttributes = FontAttributes.None;
            //Tau_Button.FontAttributes = FontAttributes.None;

            //Text underlined wenn Tab ausgewählt wurde
            DeckblattButton.TextDecorations = TextDecorations.None;
            UwertBasisButton.TextDecorations = TextDecorations.None;
            UwertSparrenButton.TextDecorations = TextDecorations.None;
            UgesButton.TextDecorations = TextDecorations.Underline;
            BefestigerButton.TextDecorations = TextDecorations.None;
            //Tau_Button.TextDecorations = TextDecorations.None;

            //Plusbutton
            BoxPlus.IsVisible = false;
            ButtonPlus.IsVisible = false;

            CalculateFehler_Abschätzung();
            Calculate_Uf();
            Calculate_Ug();
            Calculate_DeltaU();
            //Fehlermeldung wenn Nebenkonstruktion unterschiedliche Dicke haben

            Frame_Ergebnis.IsVisible = true;
            Frame_Warnung.IsVisible = false;
            Summe_Dicke_Basis = main_model.Bauteil_Basis.Sum(p => p.Dicke);
            Summe_Dicke_Sparren = main_model.Bauteil_Sparren.Sum(p => p.Dicke);
            if (main_model.Bauteil_Basis.Count != main_model.Bauteil_Sparren.Count || Math.Abs((double)(Summe_Dicke_Basis - Summe_Dicke_Sparren)) > 0.000000000001)
            {
                Frame_Ergebnis.IsVisible = false;
                Frame_Warnung.IsVisible = true;
            }
            Gesamt_Gewicht = ((EntryBasis / Gesamtflächen) * main_model.Bauteil_Basis.Sum(p => p.Gewicht) + (EntrySparren / Gesamtflächen) * main_model.Bauteil_Sparren.Sum(p => p.Gewicht));
        }

        private void Befestiger_Tapped(object sender, EventArgs e)
        {
            //Anzeige von Datentabelle in Deckblatt
            DeckblattTab.IsVisible = false;

            //Anzeige von U-Wert Frame
            BasisUwert.IsVisible = false;
            SparrenUwert.IsVisible = false;
            GesUwert.IsVisible = false;
            BefestigerFrame.IsVisible = true;

            //Anzeige von Analyse Tauperiode
            //TauTab.IsVisible = false;

            //Anzeige von Flächenanteil
            AnteilBasis.IsVisible = false;
            AnteilSparren.IsVisible = false;

            //Text Bold wenn Tab ausgewählt wurde
            DeckblattButton.FontAttributes = FontAttributes.None;
            UwertBasisButton.FontAttributes = FontAttributes.None;
            UwertSparrenButton.FontAttributes = FontAttributes.None;
            UgesButton.FontAttributes = FontAttributes.None;
            BefestigerButton.FontAttributes = FontAttributes.Bold;
            //Tau_Button.FontAttributes = FontAttributes.None;

            //Text underlined wenn Tab ausgewählt wurde
            DeckblattButton.TextDecorations = TextDecorations.None;
            UwertBasisButton.TextDecorations = TextDecorations.None;
            UwertSparrenButton.TextDecorations = TextDecorations.None;
            UgesButton.TextDecorations = TextDecorations.None;
            BefestigerButton.TextDecorations = TextDecorations.Underline;
            //Tau_Button.TextDecorations = TextDecorations.None;


            //Plusbutton
            BoxPlus.IsVisible = false;
            ButtonPlus.IsVisible = false;
            if (main_model.Befestiger_Basis.Count != 0)
            {
                FrameBefestigerBasis.IsVisible = true;
            }
            else { FrameBefestigerBasis.IsVisible = false; }

            if (main_model.Befestiger_Sparren.Count != 0)
            {
                FrameBefestigerSparren.IsVisible = true;
            }
            else { FrameBefestigerSparren.IsVisible = false; }

        }

        private async void Befestiger_Einfügen_Clicked(object sender, EventArgs e)
        {
            string action = await DisplayActionSheet("Befestiger hinzufügen in", "Abbrechen", null, "Basiskonstruktion", "Sparren");
            //Befestiger in Basis hinzufügen
            // if (UwertBasisButton.FontAttributes == FontAttributes.Bold)
            if (action == "Basiskonstruktion")
            {
                var newBasisBefestiger = new BefestigerPage();
                newBasisBefestiger.BefestigerAdded += async (source, befestiger) =>
                {
                    befestiger.ModelID = main_model.ID;
                    newItem_Basis_Befestiger = new BefestigerBasis()
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
                    _befestiger.Add(newItem_Basis_Befestiger);
                    await App.Database.SaveFixAsync(newItem_Basis_Befestiger);
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
                        BefestigerButton.IsVisible = true;
                    }
                };
                main_model.Date = DateTime.Now;
                await App.Database.UpdateItemAsync(main_model);
                await Navigation.PushAsync(newBasisBefestiger);
            }
            //Befestiger in Sparren hinzufügen
            //else if (UwertSparrenButton.FontAttributes == FontAttributes.Bold)
            else if (action == "Sparren")
            {
                var newSparrenBefestiger = new BefestigerPage();
                newSparrenBefestiger.BefestigerAdded += async (source, befestiger) =>
                {
                    befestiger.ModelID = main_model.ID;
                    newItem_Sparren_Befestiger = new BefestigerSparren()
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
                    _befestigerSparren.Add(newItem_Sparren_Befestiger);
                    await App.Database.SaveFixSparrenAsync(newItem_Sparren_Befestiger);
                    GetSparren();
                    Calculate_Uf();
                    Calculate_Ug();
                    Calculate_DeltaU();
                    BefestigerButton.IsVisible = true;
                };
                main_model.Date = DateTime.Now;
                await App.Database.UpdateItemAsync(main_model);
                await Navigation.PushAsync(newSparrenBefestiger);
            }
        }

        private async void Bauteil_Einfügen_Clicked(object sender, EventArgs e)
        {
            //In Basis einfügen
            if (UwertBasisButton.FontAttributes == FontAttributes.Bold)
            {
                var newBasisBauteil = new BauteilPage();
                newBasisBauteil.BauteilAdded += async (source, bauteil) =>
                {
                    bauteil.ID_Sort = BasisList.Count() + 1;
                    bauteil.ModelID = main_model.ID;
                    newItem_Basis_Bauteil = new Basis()
                    {
                        ID_Bauteil = bauteil.ID_Bauteil,
                        Bezeichnung = bauteil.Bezeichnung,
                        R = bauteil.R,
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
                        Gewicht = bauteil.Gewicht,
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
                    };
                    _basis.Add(newItem_Basis_Bauteil);
                    await App.Database.SaveBauteilAsync(newItem_Basis_Bauteil);
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
            //In Sparren einfügen
            else if (UwertSparrenButton.FontAttributes == FontAttributes.Bold)
            {
                var newSparrenBauteil = new BauteilPage();
                newSparrenBauteil.BauteilAdded += async (source, bauteil) =>
                {
                    bauteil.ID_Sort = SparrenList.Count() + 1;
                    bauteil.ModelID = main_model.ID;
                    newItem_Sparren_Bauteil = new Sparren()
                    {
                        ID_Bauteil = bauteil.ID_Bauteil,
                        Bezeichnung = bauteil.Bezeichnung,
                        R = bauteil.R,
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
                        Gewicht = bauteil.Gewicht,
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
                    };
                    _sparren.Add(newItem_Sparren_Bauteil);
                    await App.Database.SaveBauteilSparrenAsync(newItem_Sparren_Bauteil);
                    GetBasis();
                    CalculateSum_Basis();
                    Calculate_Ug();
                    Calculate_Uf();
                    Calculate_DeltaU();
                };
                main_model.Date = DateTime.Now;
                await App.Database.UpdateItemAsync(main_model);
                await Navigation.PushAsync(newSparrenBauteil);
            }
        }

        //ItemSelected Befestiger
        private async void OnSelected_Befestiger_Basis(object sender, SelectedItemChangedEventArgs e)
        {
            if (listBefestiger.SelectedItem == null)
                return;
            var selectedBefestiger = e.SelectedItem as BefestigerBasis;
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
        private async void OnSelected_Befestiger_Sparren(object sender, SelectedItemChangedEventArgs e)
        {
            if (listBefestigerSparren.SelectedItem == null)
                return;
            var selectedBefestiger = e.SelectedItem as BefestigerSparren;
            listBefestigerSparren.SelectedItem = null;
            var SparrenBefestiger = new SparrenEinfügen(selectedBefestiger);
            SparrenBefestiger.BefestigerSparrenUpdated += (source, befestiger) =>
            {
                selectedBefestiger.ID_Befestiger = befestiger.ID_Befestiger;
                selectedBefestiger.ModelID = befestiger.ModelID;
                selectedBefestiger.Anzahl = befestiger.Anzahl;
                selectedBefestiger.Wärmeleitfähigkeit_f = befestiger.Wärmeleitfähigkeit_f;
                selectedBefestiger.Durchmesser = befestiger.Durchmesser;
                selectedBefestiger.Eindringtiefe = befestiger.Eindringtiefe;
                selectedBefestiger.Länge = befestiger.Länge;
                Calculate_Uf();
                Calculate_Ug();
                Calculate_DeltaU();
            };
            main_model.Befestiger_Sparren.Remove(selectedBefestiger);
            if (main_model.Befestiger_Sparren.Count == 0)
            {
                BefestigerBoolean = false;
                BefestigerBooleanEmpty = true;
            }
            Calculate_Uf();
            Calculate_Ug();
            Calculate_DeltaU();
            main_model.Date = DateTime.Now;
            await App.Database.UpdateItemAsync(main_model);
            await Navigation.PushAsync(new SparrenEinfügen(selectedBefestiger)
            {
                BindingContext = selectedBefestiger,
            });
        }

        private void Konstruktion_Clicked(object sender, EventArgs e)
        {
            //var type = sender as Konstruktion;
            //var konstruktionsUpdate = new KonstruktionPage(type);
            var konstruktionsUpdate = new KonstruktionPage(Konstruktionstyp);
            konstruktionsUpdate.KonstruktionChanged += (source, konstruktion) =>
            {
                Konstruktionstyp = konstruktion;
            };
            Navigation.PushAsync(konstruktionsUpdate);
            CalculateSum_Basis();
            CalculateSum_Sparren();

        }
        private void DragGestureRecognizer_DragStarting(object sender, DragStartingEventArgs e)
        {
        }
        private void DropGestureRecognizer_Drop(object sender, DropEventArgs e)
        {
            e.Handled = true;
        }

        //Öffnen des Menüs
        public async void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            var menu = main_model as MainModel;
            await Navigation.PushAsync(new ProjektMenu(menu)
            {
                BindingContext = menu as MainModel
            });
        }

        //Zurück zur Vorderseite
        public async void Back_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopToRootAsync();
        }

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            await DisplayAlert("Achtung!", "Bauteile können nur unter U-Wert bearbeitet werden", "OK");
        }
        private async void Up_Clicked(object sender, EventArgs e)
        {
            ImageButton imagebutton = (sender as ImageButton);
            if (UwertBasisButton.FontAttributes == FontAttributes.Bold)
            {
                var item = (imagebutton.BindingContext as Basis);
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
            }
            else if (UwertSparrenButton.FontAttributes == FontAttributes.Bold)
            {
                var item = (imagebutton.BindingContext as Sparren);
                int old_id = item.ID_Bauteil;
                int itemToInsertBefore_old_ID = old_id - 1;
                foreach (Sparren i in main_model.Bauteil_Sparren)
                {
                    if (i.ID_Bauteil == itemToInsertBefore_old_ID)
                    {
                        i.ID_Bauteil = old_id;
                        item.ID_Bauteil = itemToInsertBefore_old_ID;
                        await App.Database.UpdateBauteilSparrenAsync(i);
                        break;
                    }
                }
                await App.Database.UpdateBauteilSparrenAsync(item);
                OnAppearing();
            }
            main_model.Date = DateTime.Now;
            await App.Database.UpdateItemAsync(main_model);
        }

        private async void Down_Clicked(object sender, EventArgs e)
        {
            ImageButton imagebutton = (sender as ImageButton)!;
            if (UwertBasisButton.FontAttributes == FontAttributes.Bold)
            {
                var item = (imagebutton.BindingContext as Basis)!;
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
            }
            else if (UwertSparrenButton.FontAttributes == FontAttributes.Bold)
            {
                var item = (imagebutton.BindingContext as Sparren)!;
                int old_id = item.ID_Bauteil;
                int itemToInsertBefore_old_ID = old_id + 1;
                foreach (Sparren i in main_model.Bauteil_Sparren)
                {
                    if (i.ID_Bauteil == itemToInsertBefore_old_ID)
                    {
                        i.ID_Bauteil = old_id;
                        item.ID_Bauteil = itemToInsertBefore_old_ID;
                        await App.Database.UpdateBauteilSparrenAsync(i);
                        break;
                    }
                }
                await App.Database.UpdateBauteilSparrenAsync(item);
                OnAppearing();
            }
            main_model.Date = DateTime.Now;
            await App.Database.UpdateItemAsync(main_model);
        }

        private void Edit_Button_Clicked(object sender, EventArgs e)
        {

        }
    }
}