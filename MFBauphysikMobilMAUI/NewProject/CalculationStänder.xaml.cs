using MFBauphysikMobilMAUI.Helpers;
using MFBauphysikMobilMAUI.Models;
using MFBauphysikMobilMAUI.NewProject;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    public partial class CalculationStänder : ContentPage
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
        private string _name_def;
        public string NameDef
        {
            get { return _name_def; }
            set
            {
                if (_name_def == value) return;
                _name_def = value;
                OnPropertyChanged(nameof(NameDef));
            }
        }
        public Gefach NewItem_Gefach_Bauteil { get; set; }
        public Ständer NewItem_Ständer_Bauteil { get; set; }
        public BefestigerGefach NewItem_Gefach_Befestiger { get; set; }
        public BefestigerStänder NewItem_Ständer_Befestiger { get; set; }

        public int Grid_Gefach { get; set; }
        public int Grid_Ständer { get; set; }

        ObservableCollection<Gefach> _gefach = new ObservableCollection<Gefach>();
        public ObservableCollection<Gefach> GefachList
        {
            get { return _gefach; }
            set
            {
                _gefach = value;
                OnPropertyChanged(nameof(Gefach));
            }
        }
        ObservableCollection<Ständer> _ständer = new ObservableCollection<Ständer>();
        public ObservableCollection<Ständer> StänderList
        {
            get { return _ständer; }
            set
            {
                _ständer = value;
                OnPropertyChanged(nameof(Ständer));
            }
        }

        ObservableCollection<BefestigerGefach> _befestigerGefach = new ObservableCollection<BefestigerGefach>();
        public ObservableCollection<BefestigerGefach> BefestigerGefachList
        {
            get { return _befestigerGefach; }
            set
            {
                _befestigerGefach = value;
                OnPropertyChanged(nameof(BefestigerGefach));
            }
        }
        ObservableCollection<BefestigerStänder> _befestigerStänder = new ObservableCollection<BefestigerStänder>();
        public ObservableCollection<BefestigerStänder> BefestigerStänderList
        {
            get { return _befestigerStänder; }
            set
            {
                _befestigerStänder = value;
                OnPropertyChanged(nameof(BefestigerStänder));
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

        private double? _rgefach;
        public double? Rgefach
        {
            get { return _rgefach; }
            set
            {
                if (_rgefach == value)
                    return;
                _rgefach = value;
                OnPropertyChanged(nameof(Rgefach));
                Rupper = 1 / (((_anteilGefach / _gesamtflächen) / _rgefach) + ((_anteilStänder / _gesamtflächen) / _rständer));
            }
        }
        private double? _rständer;
        public double? Rständer
        {
            get { return _rständer; }
            set
            {
                if (_rständer == value)
                    return;
                _rständer = value;
                OnPropertyChanged(nameof(Rständer));
                Rupper = 1 / (((_anteilGefach / _gesamtflächen) / _rgefach) + ((_anteilStänder / _gesamtflächen) / _rständer));
            }
        }

        private double? _ugefach;
        public double? Ugefach
        {
            get { return _ugefach; }
            set
            {
                if (_ugefach == value)
                    return;
                _ugefach = value;
                OnPropertyChanged(nameof(Ugefach));
            }
        }
        private double? _uständer;
        public double? Uständer
        {
            get { return _uständer; }
            set
            {
                if (_uständer == value)
                    return;
                _uständer = value;
                OnPropertyChanged(nameof(Uständer));
            }
        }

        private double? _anteilGefach;
        public double? EntryGefach
        {
            get { return _anteilGefach; }
            set
            {
                if (_anteilGefach == value)
                    return;
                _anteilGefach = value;
                OnPropertyChanged(nameof(EntryGefach));
                Gesamtflächen = _anteilGefach + _anteilStänder;
                Rupper = 1 / (((_anteilGefach / _gesamtflächen) / _rgefach) + ((_anteilStänder / _gesamtflächen) / _rständer));
            }
        }
        private double? _anteilStänder;
        public double? EntryStänder
        {
            get { return _anteilStänder; }
            set
            {
                if (_anteilStänder == value)
                    return;
                _anteilStänder = value;
                OnPropertyChanged(nameof(EntryStänder));
                Gesamtflächen = _anteilGefach + _anteilStänder;
                Rupper = 1 / (((_anteilGefach / _gesamtflächen) / _rgefach) + ((_anteilStänder / _gesamtflächen) / _rständer));
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
                Rupper = 1 / (((_anteilGefach / _gesamtflächen) / _rgefach) + ((_anteilStänder / _gesamtflächen) / _rständer));

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
        private double? _anteil;
        public double? Anteil
        {
            get { return _anteil; }
            set
            {
                if (_anteil == value)
                    return;
                _anteil = value;
                OnPropertyChanged(nameof(Anteil));
            }
        }

        private double? _uf_gefach;
        public double? Uf_Gefach
        {
            get { return _uf_gefach; }
            set
            {
                if (_uf_gefach == value)
                    return;
                _uf_gefach = value;
                OnPropertyChanged(nameof(Uf_Gefach));
            }
        }
        private double? _uf_ständer;
        public double? Uf_Ständer
        {
            get { return _uf_ständer; }
            set
            {
                if (_uf_ständer == value)
                    return;
                _uf_ständer = value;
                OnPropertyChanged(nameof(Uf_Ständer));
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


        private double? _ug_gefach;
        public double? Ug_Gefach
        {
            get { return _ug_gefach; }
            set
            {
                if (_ug_gefach == value)
                    return;
                _ug_gefach = value;
                OnPropertyChanged(nameof(Ug_Gefach));
            }
        }
        private double? _ug_ständer;
        public double? Ug_Ständer
        {
            get { return _ug_ständer; }
            set
            {
                if (_ug_ständer == value)
                    return;
                _ug_ständer = value;
                OnPropertyChanged(nameof(Ug_Ständer));
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

        private double? _gesamtSdGefach;
        public double? Gesamt_SdGefach
        {
            get { return _gesamtSdGefach; }
            set
            {
                if (_gesamtSdGefach == value)
                    return;
                _gesamtSdGefach = value;
                OnPropertyChanged(nameof(Gesamt_SdGefach));
            }
        }
        private double? _gesamtSdStänder;
        public double? Gesamt_SdStänder
        {
            get { return _gesamtSdStänder; }
            set
            {
                if (_gesamtSdStänder == value)
                    return;
                _gesamtSdStänder = value;
                OnPropertyChanged(nameof(Gesamt_SdStänder));
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
        private bool _nachweis_gefach;
        public bool NachweisGefach
        {
            get { return _nachweis_gefach; }
            set
            {
                if (_nachweis_gefach == value)
                    return;
                _nachweis_gefach = value;
                OnPropertyChanged(nameof(NachweisGefach));
            }
        }
        private bool _nachweis_ständer;
        public bool NachweisStänder
        {
            get { return _nachweis_ständer; }
            set
            {
                if (_nachweis_ständer == value) return;
                _nachweis_ständer = value;
                OnPropertyChanged(nameof(NachweisStänder));
            }
        }

        private double? _summe_dicke_gefach;
        public double? Summe_Dicke_Gefach
        {
            get { return _summe_dicke_gefach; }
            set
            {
                if (_summe_dicke_gefach == value) return;
                _summe_dicke_gefach = value;
                OnPropertyChanged(nameof(Summe_Dicke_Gefach));
            }
        }
        private double? _summe_dicke_ständer;
        public double? Summe_Dicke_Ständer
        {
            get { return _summe_dicke_ständer; }
            set
            {
                if (_summe_dicke_ständer == value) return;
                _summe_dicke_ständer = value;
                OnPropertyChanged(nameof(Summe_Dicke_Ständer));
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
        public CalculationStänder(MainModel muster)
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
                BV = muster.BV,
                BV_Ersatz = muster.BV_Ersatz,
                Befestiger_Basis = muster.Befestiger_Basis,
                Bauteil_Basis = muster.Bauteil_Basis,
            };
            Aufbau = main_model.MusterName;
            NameDef = main_model.ProjectName;

            if (main_model.Selected == 0)
            {
                //Gefach
                GefachList.Add(new Gefach()
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
                GefachList.Add(new Gefach()
                {
                    ID_Sort = 2,
                    Bezeichnung = "Luft, senkrecht, ruhend, d >= 25mm",
                    Dicke = 0.025,
                    R = 0.16,
                    Wärmeleitfähigkeit = 0.156250,
                    Rohdichte = 1.25,
                    Kapillar = false,
                    sonstiges = true,
                    KeineLuft = true,
                    Fester_R = true,
                    Sd_Min = 0.03,
                    Sd_Max = 0.03,
                    Dampfdiffusionswiderstand_Min = 1,
                    Dampfdiffusionswiderstand_Max = 1
                });
                GefachList.Add(new Gefach()
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
                GefachList.Add(new Gefach()
                {
                    ID_Sort = 4,
                    Bezeichnung = "Mineralfaser 035",
                    R = 4.0000,
                    Dicke = 0.14,
                    Wärmeleitfähigkeit = 0.035,
                    Rohdichte = 120,
                    Kapillar = false,
                    sonstiges = true,
                    EvntlLuft = true,
                    Sd_Min = 0.20,
                    Sd_Max = 0.20,
                    Dampfdiffusionswiderstand_Min = 1.4,
                    Dampfdiffusionswiderstand_Max = 1.4
                });
                GefachList.Add(new Gefach()
                {
                    ID_Sort = 5,
                    Bezeichnung = "OSB-Platte",
                    R = 0.076923,
                    Dicke = 0.01,
                    Wärmeleitfähigkeit = 0.13,
                    Rohdichte = 650,
                    Kapillar = false,
                    Holzwerkstoff = true,
                    KeineLuft = true,
                    Sd_Min = 0.30,
                    Sd_Max = 0.50,
                    Dampfdiffusionswiderstand_Min = 30,
                    Dampfdiffusionswiderstand_Max = 50
                });
                GefachList.Add(new Gefach()
                {
                    ID_Sort = 6,
                    Bezeichnung = "Kunstharzputz",
                    R = 0.021429,
                    Dicke = 0.015,
                    Wärmeleitfähigkeit = 0.7,
                    Rohdichte = 1100,
                    Kapillar = false,
                    sonstiges = true,
                    KeineLuft = true,
                    Sd_Min = 0.75,
                    Sd_Max = 3.00,
                    Dampfdiffusionswiderstand_Min = 50,
                    Dampfdiffusionswiderstand_Max = 200
                });
                foreach (Gefach i in GefachList)
                {
                    i.ModelID = main_model.ID;
                }
                for (int i = 0; i < GefachList.Count; i++)
                {
                    App.Database.SaveBauteilGefachAsync(GefachList[i]);
                }
                //Ständer
                StänderList.Add(new Ständer()
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
                StänderList.Add(new Ständer()
                {
                    ID_Sort = 2,
                    Bezeichnung = "Luft, senkrecht, ruhend, d >= 25mm",
                    Wärmeleitfähigkeit = 0.15625,
                    Dicke = 0.025,
                    R = 0.16,
                    Rohdichte = 1.25,
                    Kapillar = false,
                    sonstiges = true,
                    KeineLuft = true,
                    Fester_R = true,
                    Sd_Min = 0.03,
                    Sd_Max = 0.03,
                    Dampfdiffusionswiderstand_Min = 1,
                    Dampfdiffusionswiderstand_Max = 1
                });
                StänderList.Add(new Ständer()
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
                StänderList.Add(new Ständer()
                {
                    ID_Sort = 4,
                    Bezeichnung = "Fichte",
                    R = 1.076923,
                    Dicke = 0.14,
                    Wärmeleitfähigkeit = 0.13,
                    Rohdichte = 600,
                    Kapillar = false,
                    Holz = true,
                    KeineLuft = true,
                    Sd_Min = 5.60,
                    Sd_Max = 5.60,
                    Dampfdiffusionswiderstand_Min = 40,
                    Dampfdiffusionswiderstand_Max = 40
                });
                StänderList.Add(new Ständer()
                {
                    ID_Sort = 5,
                    Bezeichnung = "OSB-Platte",
                    R = 0.076923,
                    Dicke = 0.01,
                    Wärmeleitfähigkeit = 0.13,
                    Rohdichte = 650,
                    Kapillar = false,
                    Holzwerkstoff = true,
                    KeineLuft = true,
                    Sd_Min = 0.30,
                    Sd_Max = 0.50,
                    Dampfdiffusionswiderstand_Min = 30,
                    Dampfdiffusionswiderstand_Max = 50
                });
                StänderList.Add(new Ständer()
                {
                    ID_Sort = 6,
                    Bezeichnung = "Kunstharzputz",
                    R = 0.021429,
                    Dicke = 0.015,
                    Wärmeleitfähigkeit = 0.7,
                    Rohdichte = 1100,
                    Kapillar = true,
                    sonstiges = true,
                    KeineLuft = true,
                    Sd_Min = 0.75,
                    Sd_Max = 3.00,
                    Dampfdiffusionswiderstand_Min = 50,
                    Dampfdiffusionswiderstand_Max = 200
                });
                foreach (Ständer i in StänderList)
                {
                    i.ModelID = main_model.ID;
                }
                for (int i = 0; i < StänderList.Count; i++)
                {
                    App.Database.SaveBauteilStänderAsync(StänderList[i]);
                }

            };

            Konstruktionstyp = "hinterlüftete Wand";

            EntryGefach = 0.625;
            EntryStänder = 0.08;

            //Visible von Datentabelle Deckblatt
            FrameGefach.IsVisible = true;
            FrameStänder.IsVisible = true;
            BefestigerFrame.IsVisible = false;

            //Visible von Button U-Wert
            UwertGefachButton.IsVisible = true;
            UwertStänderButton.IsVisible = true;
            UgesButton.IsVisible = true;

            //Visible von Datentabelle U-Wert
            GefachUwert.IsVisible = false;
            StänderUwert.IsVisible = false;
            GesUwert.IsVisible = false;

            //Visible von Flächenanteil Angabe

            AnteilGefach.IsVisible = false;
            AnteilStänder.IsVisible = false;
            BindingContext = this;          
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            indicator_view.IsVisible = true;
            main_view.IsVisible = false;
            GetGefach();
            GetStänder();
            if (MeldungDicke.IsVisible == true)
            {
                Meldung3Ebene.IsVisible = false;
            }
            await Task.Delay(500);
            indicator_view.IsVisible = false;
            main_view.IsVisible = true;
        }

        private async void GetGefach()
        {
            //Bauteil
            var itemGefach = await App.Database.GetBauteilGefachAsync();
            main_model.Bauteil_Gefach.Clear();
            foreach (Gefach i in itemGefach)
            {
                i.SizeClass = Setting.Size_Default;
                if (i.ModelID == main_model.ID)
                {
                    main_model.Bauteil_Gefach.Add(i);
                    for (int m = 0; m <= main_model.Bauteil_Gefach.Count - 1; m++)
                    {
                        for (int n = m + 1; n <= main_model.Bauteil_Gefach.Count - 1; n++)
                        {
                            if (main_model.Bauteil_Gefach[m].ID_Bauteil == main_model.Bauteil_Gefach[n].ID_Bauteil)
                            {
                                main_model.Bauteil_Gefach.Remove(main_model.Bauteil_Gefach[m]);
                            }
                        }
                    }
                }
                i.Gewicht = i.Dicke * i.Rohdichte;
            }
            //Sorted List
            if (main_model.Selected == 0)
            {
                for (int i = 0; i < main_model.Bauteil_Gefach.Count - 1; i++)
                {
                    for (int j = i + 1; j < main_model.Bauteil_Gefach.Count; j++)
                    {
                        if (main_model.Bauteil_Gefach[i].ID_Sort > main_model.Bauteil_Gefach[j].ID_Sort)
                        {
                            var oldItem = main_model.Bauteil_Gefach[i];
                            var newItem = main_model.Bauteil_Gefach[j];
                            main_model.Bauteil_Gefach[i] = newItem;
                            main_model.Bauteil_Gefach[j] = oldItem;
                            await App.Database.UpdateBauteilGefachAsync(main_model.Bauteil_Gefach[i]);
                            await App.Database.UpdateBauteilGefachAsync(main_model.Bauteil_Gefach[j]);
                        }
                        if (main_model.Bauteil_Gefach[i].ID_Bauteil > main_model.Bauteil_Gefach[j].ID_Bauteil)
                        {
                            int oldID = main_model.Bauteil_Gefach[i].ID_Bauteil;
                            int newID = main_model.Bauteil_Gefach[j].ID_Bauteil;
                            main_model.Bauteil_Gefach[i].ID_Bauteil = newID;
                            main_model.Bauteil_Gefach[j].ID_Bauteil = oldID;
                            await App.Database.UpdateBauteilGefachAsync(main_model.Bauteil_Gefach[i]);
                            await App.Database.UpdateBauteilGefachAsync(main_model.Bauteil_Gefach[j]);
                        }
                    }
                }
            }
            listGefach.ItemsSource = main_model.Bauteil_Gefach;
            listGefachUwert.ItemsSource = main_model.Bauteil_Gefach;
            CalculateSum_Gefach();

            //Befestiger
            var fixGefach = await App.Database.GetFixGefachAsync();
            main_model.Befestiger_Gefach.Clear();
            foreach (BefestigerGefach j in fixGefach)
            {
                j.SizeClass = Setting.Size_Default;
                if (j.ModelID == main_model.ID)
                {
                    main_model.Befestiger_Gefach.Add(j);
                    for (int m = 0; m <= main_model.Befestiger_Gefach.Count - 1; m++)
                    {
                        for (int n = m + 1; n <= main_model.Befestiger_Gefach.Count - 1; n++)
                        {
                            //Update Befestiger 
                            if (main_model.Befestiger_Gefach[m].ID_Befestiger == main_model.Befestiger_Gefach[n].ID_Befestiger)
                            {
                                main_model.Befestiger_Gefach.Remove(main_model.Befestiger_Gefach[m]);
                            }

                        }
                    }
                }
            }
            listBefestigerGefach.ItemsSource = main_model.Befestiger_Gefach;
            if (main_model.Befestiger_Gefach.Count != 0)
            {
                FrameBefestigerGefach.IsVisible = true;
                //List_Befestiger_Gefach_Deckblatt.ItemsSource = main_model.Befestiger_Gefach;
                //Kein_Befestiger_Gefach_Deckblatt.Text = "";
            }
            else
            {
                FrameBefestigerGefach.IsVisible = false;
                //Kein_Befestiger_Gefach_Deckblatt.Text = "nicht in Gefach vorhanden";
            }

            CalculateFehler_Abschätzung();
            Calculate_Uf();
            Calculate_Ug();
            Calculate_DeltaU();
        }
        private async void GetStänder()
        {
            //Bauteil
            var itemStänder = await App.Database.GetBauteilStänderAsync();
            main_model.Bauteil_Ständer.Clear();
            foreach (Ständer i in itemStänder)
            {
                i.SizeClass = Setting.Size_Default;
                if (i.ModelID == main_model.ID)
                {
                    main_model.Bauteil_Ständer.Add(i);
                    for (int m = 0; m <= main_model.Bauteil_Ständer.Count - 1; m++)
                    {
                        for (int n = m + 1; n <= main_model.Bauteil_Ständer.Count - 1; n++)
                        {
                            if (main_model.Bauteil_Ständer[m].ID_Bauteil == main_model.Bauteil_Ständer[n].ID_Bauteil)
                            {
                                main_model.Bauteil_Ständer.Remove(main_model.Bauteil_Ständer[m]);
                            }
                        }
                    }
                }
                i.Gewicht = i.Dicke * i.Rohdichte;
            }

            //Sorted List
            if (main_model.Selected == 0)
            {
                for (int i = 0; i < main_model.Bauteil_Ständer.Count - 1; i++)
                {
                    for (int j = i + 1; j < main_model.Bauteil_Ständer.Count; j++)
                    {
                        if (main_model.Bauteil_Ständer[i].ID_Sort > main_model.Bauteil_Ständer[j].ID_Sort)
                        {
                            var oldItem = main_model.Bauteil_Ständer[i];
                            var newItem = main_model.Bauteil_Ständer[j];
                            main_model.Bauteil_Ständer[i] = newItem;
                            main_model.Bauteil_Ständer[j] = oldItem;
                            await App.Database.UpdateBauteilStänderAsync(main_model.Bauteil_Ständer[i]);
                            await App.Database.UpdateBauteilStänderAsync(main_model.Bauteil_Ständer[j]);
                        }
                        if (main_model.Bauteil_Ständer[i].ID_Bauteil > main_model.Bauteil_Ständer[j].ID_Bauteil)
                        {
                            int oldID = main_model.Bauteil_Ständer[i].ID_Bauteil;
                            int newID = main_model.Bauteil_Ständer[j].ID_Bauteil;
                            main_model.Bauteil_Ständer[i].ID_Bauteil = newID;
                            main_model.Bauteil_Ständer[j].ID_Bauteil = oldID;
                            await App.Database.UpdateBauteilStänderAsync(main_model.Bauteil_Ständer[i]);
                            await App.Database.UpdateBauteilStänderAsync(main_model.Bauteil_Ständer[j]);
                        }
                    }
                }
            }
            listStänder.ItemsSource = main_model.Bauteil_Ständer;
            listStänderUwert.ItemsSource = main_model.Bauteil_Ständer;
            CalculateSum_Ständer();

            //Befestiger
            var fixStänder = await App.Database.GetFixStänderAsync();
            main_model.Befestiger_Ständer.Clear();
            foreach (BefestigerStänder j in fixStänder)
            {
                j.SizeClass = Setting.Size_Default;
                if (j.ModelID == main_model.ID)
                {
                    main_model.Befestiger_Ständer.Add(j);
                    for (int m = 0; m <= main_model.Befestiger_Ständer.Count - 1; m++)
                    {
                        for (int n = m + 1; n <= main_model.Befestiger_Ständer.Count - 1; n++)
                        {
                            //Update Befestiger 
                            if (main_model.Befestiger_Ständer[m].ID_Befestiger == main_model.Befestiger_Ständer[n].ID_Befestiger)
                            {
                                main_model.Befestiger_Ständer.Remove(main_model.Befestiger_Ständer[m]);
                            }

                        }
                    }
                }
            }
            listBefestigerStänder.ItemsSource = main_model.Befestiger_Ständer;
            if (main_model.Befestiger_Ständer.Count != 0)
            {
                FrameBefestigerStänder.IsVisible = true;
                //List_Befestiger_Ständer_Deckblatt.ItemsSource = main_model.Befestiger_Ständer;
                //Kein_Befestiger_Ständer_Deckblatt.Text = "";
            }
            else
            {
                FrameBefestigerStänder.IsVisible = false;
                //Kein_Befestiger_Ständer_Deckblatt.Text = "nicht in Ständer vorhanden";
            }

            CalculateFehler_Abschätzung();
            Calculate_Uf();
            Calculate_Ug();
            Calculate_DeltaU();
        }        
        public void CalculateSum_Gefach()
        {
            if (Konstruktionstyp == "Warmdach")
            {
                Pc = 2000;
            }
            else
            {
                Pc = 1700;
            }
            Rgefach = main_model.Bauteil_Gefach.Sum(p => p.R) + Horizontal + Außen;
            Summe_Dicke_Gefach = main_model.Bauteil_Gefach.Sum(p => p.Dicke);
            Ugefach = 1 / Rgefach;

            //Sd-Wert in Tauperiode = sd-Min
            //Bestimmen von Temperatur, Sättigungsdampfdruck
            foreach (KlimadatenClass i in Klimadaten)
            {
                Wärmestromdichte = (i.InnenTemp - i.AußenTemp) / (main_model.Bauteil_Gefach.Sum(p => p.R) + Innen_TWN + Außen);
                double? innen_feuchtenachweis = i.InnenTemp - 0.25 * Wärmestromdichte;
                main_model.Bauteil_Gefach[0].Tempverlauf = innen_feuchtenachweis - Wärmestromdichte * main_model.Bauteil_Gefach[0].R;
                main_model.Bauteil_Gefach[0].Dampfsättigungsdruck = 610.5 * Math.Exp(Convert.ToDouble((17.269 * main_model.Bauteil_Gefach[0].Tempverlauf) / (237.3 + main_model.Bauteil_Gefach[0].Tempverlauf)));


                main_model.Bauteil_Gefach[0].Sd = main_model.Bauteil_Gefach[0].Sd_Min;
                Gesamt_SdGefach = main_model.Bauteil_Gefach.Sum(p => p.Sd_Min);
                Faktor_Dampfdruckverteilung = (i.InnenWasserdampfdruck - i.AußenWasserdampfdruck) / Gesamt_SdGefach;
                main_model.Bauteil_Gefach[0].Dampfteildruck = i.InnenWasserdampfdruck - Faktor_Dampfdruckverteilung * main_model.Bauteil_Gefach[0].Sd;

                int Ebene = 0;
                for (int m = 1; m <= main_model.Bauteil_Gefach.Count - 1; m++)
                {
                    //Temperaturverlauf
                    main_model.Bauteil_Gefach[m].Tempverlauf = main_model.Bauteil_Gefach[m - 1].Tempverlauf - main_model.Bauteil_Gefach[m].R * Wärmestromdichte;

                    //Sättigungsdampfdruck Psat (DIN 4108-3:2018-10 Anhang C.4)                    
                    if (main_model.Bauteil_Gefach[m].Tempverlauf >= 0)
                    {
                        main_model.Bauteil_Gefach[m].Dampfsättigungsdruck = 610.5 * Math.Exp(Convert.ToDouble(17.269 * main_model.Bauteil_Gefach[m].Tempverlauf / (237.3 + main_model.Bauteil_Gefach[m].Tempverlauf)));
                    }
                    else
                    {
                        main_model.Bauteil_Gefach[m].Dampfsättigungsdruck = 610.5 * Math.Exp(Convert.ToDouble(21.875 * main_model.Bauteil_Gefach[m].Tempverlauf / (265.5 + main_model.Bauteil_Gefach[m].Tempverlauf)));
                    }

                    //Wasserdampfteildruck
                    main_model.Bauteil_Gefach[m].Sd = main_model.Bauteil_Gefach[m].Sd_Min;
                    main_model.Bauteil_Gefach[m].Dampfteildruck = main_model.Bauteil_Gefach[(m - 1)].Dampfteildruck - Faktor_Dampfdruckverteilung * main_model.Bauteil_Gefach[m].Sd;


                    //Tauwasserausfall
                    if (main_model.Bauteil_Gefach[m].Dampfteildruck > main_model.Bauteil_Gefach[m].Dampfsättigungsdruck)
                    {
                        NachweisGrid.IsVisible = false;
                        Ebene = Ebene + 1;
                        main_model.Bauteil_Gefach[m].Dampfteildruck = main_model.Bauteil_Gefach[m].Dampfsättigungsdruck;
                        main_model.Bauteil_Gefach[m].TW = true;

                        //max. zulässige Tauwassermasse
                        //kapillar nicht wasseraufnahmefähig
                        if (main_model.Bauteil_Gefach[m].Kapillar == true || main_model.Bauteil_Gefach[m + 1].Kapillar == true)
                        {
                            ZulTauwasser = 500;

                            //Holz 5%
                            if (main_model.Bauteil_Gefach[m].Holz == true && main_model.Bauteil_Gefach[m + 1].Holz == true)
                            {
                                double w1 = (double)main_model.Bauteil_Gefach[m].Gewicht * 1000 * 0.05;
                                double w2 = (double)main_model.Bauteil_Gefach[m + 1].Gewicht * 1000 * 0.05;
                                ZulTauwasser = Math.Min(w1, w2);
                            }
                            else
                            {
                                if (main_model.Bauteil_Gefach[m].Holz == true)
                                {
                                    ZulTauwasser = main_model.Bauteil_Gefach[m].Gewicht * 1000 * 0.05;
                                }
                                else if (main_model.Bauteil_Gefach[m + 1].Holz == true)
                                {
                                    ZulTauwasser = main_model.Bauteil_Gefach[m + 1].Gewicht * 1000 * 0.05;
                                }
                            }

                            //Holzwerkstoff mit 3%
                            if (main_model.Bauteil_Gefach[m].Holzwerkstoff == true && main_model.Bauteil_Gefach[m + 1].Holzwerkstoff == true)
                            {
                                double w1 = (double)main_model.Bauteil_Gefach[m].Gewicht * 1000 * 0.03;
                                double w2 = (double)main_model.Bauteil_Gefach[m + 1].Gewicht * 1000 * 0.03;
                                ZulTauwasser = Math.Min(w1, w2);
                            }
                            else
                            {
                                if (main_model.Bauteil_Gefach[m].Holzwerkstoff == true)
                                {
                                    ZulTauwasser = main_model.Bauteil_Gefach[m].Gewicht * 1000 * 0.03;
                                }
                                else if (main_model.Bauteil_Gefach[m + 1].Holzwerkstoff == true)
                                {
                                    ZulTauwasser = main_model.Bauteil_Gefach[m + 1].Gewicht * 1000 * 0.03;
                                }
                            }

                            //Kombination von Holz und Holzwerkstoff
                            if (main_model.Bauteil_Gefach[m].Holz == true && main_model.Bauteil_Gefach[m + 1].Holzwerkstoff == true)
                            {
                                double w1 = (double)(main_model.Bauteil_Gefach[m].Gewicht * 1000 * 0.05);
                                double w2 = (double)(main_model.Bauteil_Gefach[m + 1].Gewicht * 1000 * 0.03);
                                ZulTauwasser = Math.Min(w1, w2);
                            }
                            else if (main_model.Bauteil_Gefach[m].Holzwerkstoff == true && main_model.Bauteil_Gefach[m + 1].Holz == true)
                            {
                                double w1 = (double)main_model.Bauteil_Gefach[m].Gewicht * 1000 * 0.03;
                                double w2 = (double)main_model.Bauteil_Gefach[m + 1].Gewicht * 1000 * 0.05;
                                ZulTauwasser = Math.Min(w1, w2);
                            }
                        }

                        //kapillar wasseraufnahmefähig
                        else
                        {
                            ZulTauwasser = 1000;

                            //Holz 5%
                            if (main_model.Bauteil_Gefach[m].Holz == true && main_model.Bauteil_Gefach[m + 1].Holz == true)
                            {
                                double w1 = (double)main_model.Bauteil_Gefach[m].Gewicht * 1000 * 0.05;
                                double w2 = (double)main_model.Bauteil_Gefach[m + 1].Gewicht * 1000 * 0.05;
                                ZulTauwasser = Math.Min(w1, w2);
                            }
                            else
                            {
                                if (main_model.Bauteil_Gefach[m].Holz == true)
                                {
                                    ZulTauwasser = main_model.Bauteil_Gefach[m].Gewicht * 1000 * 0.05;
                                }
                                else if (main_model.Bauteil_Gefach[m + 1].Holz == true)
                                {
                                    ZulTauwasser = main_model.Bauteil_Gefach[m + 1].Gewicht * 1000 * 0.05;
                                }
                            }

                            //Holzwerkstoff mit 3%
                            if (main_model.Bauteil_Gefach[m].Holzwerkstoff == true && main_model.Bauteil_Gefach[m + 1].Holzwerkstoff == true)
                            {
                                double w1 = (double)main_model.Bauteil_Gefach[m].Gewicht * 1000 * 0.03;
                                double w2 = (double)main_model.Bauteil_Gefach[m + 1].Gewicht * 1000 * 0.03;
                                ZulTauwasser = Math.Min(w1, w2);
                            }
                            else
                            {
                                if (main_model.Bauteil_Gefach[m].Holzwerkstoff == true)
                                {
                                    ZulTauwasser = main_model.Bauteil_Gefach[m].Gewicht * 1000 * 0.03;
                                }
                                else if (main_model.Bauteil_Gefach[m + 1].Holzwerkstoff == true)
                                {
                                    ZulTauwasser = main_model.Bauteil_Gefach[m + 1].Gewicht * 1000 * 0.03;
                                }
                            }

                            //Kombination von Holz und Holzwerkstoff
                            if (main_model.Bauteil_Gefach[m].Holz == true && main_model.Bauteil_Gefach[m + 1].Holzwerkstoff == true)
                            {
                                double w1 = (double)(main_model.Bauteil_Gefach[m].Gewicht * 1000 * 0.05);
                                double w2 = (double)(main_model.Bauteil_Gefach[m + 1].Gewicht * 1000 * 0.03);
                                ZulTauwasser = Math.Min(w1, w2);
                            }
                            else if (main_model.Bauteil_Gefach[m].Holzwerkstoff == true && main_model.Bauteil_Gefach[m + 1].Holz == true)
                            {
                                double w1 = (double)main_model.Bauteil_Gefach[m].Gewicht * 1000 * 0.03;
                                double w2 = (double)main_model.Bauteil_Gefach[m + 1].Gewicht * 1000 * 0.05;
                                ZulTauwasser = Math.Min(w1, w2);
                            }
                        }

                        double? sum_sd = 0;
                        for (int j = 0; j <= m - 1; j++)
                        {
                            sum_sd = sum_sd + main_model.Bauteil_Gefach[j].Sd;
                            Faktor_Dampfdruckverteilung = (main_model.Bauteil_Gefach[m].Dampfteildruck - i.AußenWasserdampfdruck) / (Gesamt_SdGefach - (main_model.Bauteil_Gefach[m].Sd + sum_sd));
                        }

                        Schichtgrenze.Add(new Schichtgrenzen()
                        {
                            Dampfteildruck = (double)main_model.Bauteil_Gefach[m].Dampfteildruck,
                            Sd = (double)main_model.Bauteil_Gefach[m].Sd,
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
                            Tauwassermasse = Delta0 * tev * (((i.InnenWasserdampfdruck - main_model.Bauteil_Gefach[m].Dampfteildruck) / sdc) - ((main_model.Bauteil_Gefach[m].Dampfteildruck - i.AußenWasserdampfdruck) / (Gesamt_SdGefach - sdc)));
                            Verdunstungsmasse = Delta0 * tev * (((Pc - i.InnenDruckVerdunstung) / sdc) + ((Pc - i.AußenDruckVerdunstung) / (Gesamt_SdGefach - sdc)));
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
                            Mc2 = Delta0 * i.TauDauer * 3600 * Math.Pow(10, 3) * (((Schichtgrenze[Schichtgrenze.Count - 2].Dampfteildruck - Schichtgrenze[Schichtgrenze.Count - 1].Dampfteildruck) / sdc_sub) - ((Schichtgrenze[Schichtgrenze.Count - 1].Dampfteildruck - i.AußenWasserdampfdruck) / (Gesamt_SdGefach - sdc2)));

                            Tauwassermasse = Mc1 + Mc2;

                            //Verdunstungsperiode
                            double? tev = i.VerdunstungsDauer * 3600 * Math.Pow(10, 3);
                            double? tev1 = 0;
                            double? tev2 = 0;
                            double? gev1 = 0;
                            double? gev2 = 0;

                            gev1 = Delta0 * (Pc - i.InnenDruckVerdunstung) / sdc1;
                            gev2 = Delta0 * (Pc - i.AußenDruckVerdunstung) / (Gesamt_SdGefach - sdc2);
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
                                    Mev1 = gev1 * tev2 + (gev1 + Delta0 * (Pc - i.AußenDruckVerdunstung) / (Gesamt_SdGefach - sdc1)) * (tev - tev2);
                                }
                            }
                            Verdunstungsmasse = Mev1 + Mev2;
                        }

                        else if (Ebene > 2)
                        {
                            NachweisGrid.IsVisible = false;
                            Meldung3Ebene.IsVisible = true;
                            MeldungDicke.IsVisible = false;
                            Feuchtenachweis.Text = "bitte überprüfen";
                        }

                    }

                    //Kein Tauwasserausfall
                    else
                    {
                        main_model.Bauteil_Gefach[m].TW = false;
                    }
                }
                //Feuchtenachweis
                if (Ebene == 0)
                {
                    NachweisGrid.IsVisible = false;
                    MeldungDicke.IsVisible = false;
                    Meldung3Ebene.IsVisible = false;
                    Feuchtenachweis.Text = "ok";
                    NachweisStänder = true;
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
        public void CalculateSum_Ständer()
        {
            if (Konstruktionstyp == "Warmdach")
            {
                Pc = 2000;
            }
            else
            {
                Pc = 1700;
            }
            Rständer = main_model.Bauteil_Ständer.Sum(p => p.R) + Horizontal + Außen;
            Summe_Dicke_Ständer = main_model.Bauteil_Ständer.Sum(p => p.Dicke);
            Uständer = 1 / Rständer;

            //Sd-Wert in Tauperiode = sd-Min
            //Bestimmen von Temperatur, Sättigungsdampfdruck
            foreach (KlimadatenClass i in Klimadaten)
            {
                Wärmestromdichte = (i.InnenTemp - i.AußenTemp) / (main_model.Bauteil_Ständer.Sum(p => p.R) + Innen_TWN + Außen);
                double? innen_feuchtenachweis = i.InnenTemp - 0.25 * Wärmestromdichte;
                main_model.Bauteil_Ständer[0].Tempverlauf = innen_feuchtenachweis - Wärmestromdichte * main_model.Bauteil_Ständer[0].R;
                main_model.Bauteil_Ständer[0].Dampfsättigungsdruck = 610.5 * Math.Exp(Convert.ToDouble((17.269 * main_model.Bauteil_Ständer[0].Tempverlauf) / (237.3 + main_model.Bauteil_Ständer[0].Tempverlauf)));


                main_model.Bauteil_Ständer[0].Sd = main_model.Bauteil_Ständer[0].Sd_Min;
                Gesamt_SdStänder = main_model.Bauteil_Ständer.Sum(p => p.Sd_Min);
                Faktor_Dampfdruckverteilung = (i.InnenWasserdampfdruck - i.AußenWasserdampfdruck) / Gesamt_SdStänder;
                main_model.Bauteil_Ständer[0].Dampfteildruck = i.InnenWasserdampfdruck - Faktor_Dampfdruckverteilung * main_model.Bauteil_Ständer[0].Sd;

                int Ebene = 0;
                for (int m = 1; m <= main_model.Bauteil_Ständer.Count - 1; m++)
                {
                    //Temperaturverlauf
                    main_model.Bauteil_Ständer[m].Tempverlauf = main_model.Bauteil_Ständer[m - 1].Tempverlauf - main_model.Bauteil_Ständer[m].R * Wärmestromdichte;

                    //Sättigungsdampfdruck Psat (DIN 4108-3:2018-10 Anhang C.4)                    
                    if (main_model.Bauteil_Ständer[m].Tempverlauf >= 0)
                    {
                        main_model.Bauteil_Ständer[m].Dampfsättigungsdruck = 610.5 * Math.Exp(Convert.ToDouble(17.269 * main_model.Bauteil_Ständer[m].Tempverlauf / (237.3 + main_model.Bauteil_Ständer[m].Tempverlauf)));
                    }
                    else
                    {
                        main_model.Bauteil_Ständer[m].Dampfsättigungsdruck = 610.5 * Math.Exp(Convert.ToDouble(21.875 * main_model.Bauteil_Ständer[m].Tempverlauf / (265.5 + main_model.Bauteil_Ständer[m].Tempverlauf)));
                    }

                    //Wasserdampfteildruck
                    main_model.Bauteil_Ständer[m].Sd = main_model.Bauteil_Ständer[m].Sd_Min;
                    main_model.Bauteil_Ständer[m].Dampfteildruck = main_model.Bauteil_Ständer[(m - 1)].Dampfteildruck - Faktor_Dampfdruckverteilung * main_model.Bauteil_Ständer[m].Sd;


                    //Tauwasserausfall
                    if (main_model.Bauteil_Ständer[m].Dampfteildruck > main_model.Bauteil_Ständer[m].Dampfsättigungsdruck)
                    {
                        NachweisGrid.IsVisible = false;
                        Ebene = Ebene + 1;
                        main_model.Bauteil_Ständer[m].Dampfteildruck = main_model.Bauteil_Ständer[m].Dampfsättigungsdruck;
                        main_model.Bauteil_Ständer[m].TW = true;

                        //max. zulässige Tauwassermasse
                        //kapillar nicht wasseraufnahmefähig
                        if (main_model.Bauteil_Ständer[m].Kapillar == true || main_model.Bauteil_Ständer[m + 1].Kapillar == true)
                        {
                            ZulTauwasser = 500;

                            //Holz 5%
                            if (main_model.Bauteil_Ständer[m].Holz == true && main_model.Bauteil_Ständer[m + 1].Holz == true)
                            {
                                double w1 = (double)main_model.Bauteil_Ständer[m].Gewicht * 1000 * 0.05;
                                double w2 = (double)main_model.Bauteil_Ständer[m + 1].Gewicht * 1000 * 0.05;
                                ZulTauwasser = Math.Min(w1, w2);
                            }
                            else
                            {
                                if (main_model.Bauteil_Ständer[m].Holz == true)
                                {
                                    ZulTauwasser = main_model.Bauteil_Ständer[m].Gewicht * 1000 * 0.05;
                                }
                                else if (main_model.Bauteil_Ständer[m + 1].Holz == true)
                                {
                                    ZulTauwasser = main_model.Bauteil_Ständer[m + 1].Gewicht * 1000 * 0.05;
                                }
                            }

                            //Holzwerkstoff mit 3%
                            if (main_model.Bauteil_Ständer[m].Holzwerkstoff == true && main_model.Bauteil_Ständer[m + 1].Holzwerkstoff == true)
                            {
                                double w1 = (double)main_model.Bauteil_Ständer[m].Gewicht * 1000 * 0.03;
                                double w2 = (double)main_model.Bauteil_Ständer[m + 1].Gewicht * 1000 * 0.03;
                                ZulTauwasser = Math.Min(w1, w2);
                            }
                            else
                            {
                                if (main_model.Bauteil_Ständer[m].Holzwerkstoff == true)
                                {
                                    ZulTauwasser = main_model.Bauteil_Ständer[m].Gewicht * 1000 * 0.03;
                                }
                                else if (main_model.Bauteil_Ständer[m + 1].Holzwerkstoff == true)
                                {
                                    ZulTauwasser = main_model.Bauteil_Ständer[m + 1].Gewicht * 1000 * 0.03;
                                }
                            }

                            //Kombination von Holz und Holzwerkstoff
                            if (main_model.Bauteil_Ständer[m].Holz == true && main_model.Bauteil_Ständer[m + 1].Holzwerkstoff == true)
                            {
                                double w1 = (double)(main_model.Bauteil_Ständer[m].Gewicht * 1000 * 0.05);
                                double w2 = (double)(main_model.Bauteil_Ständer[m + 1].Gewicht * 1000 * 0.03);
                                ZulTauwasser = Math.Min(w1, w2);
                            }
                            else if (main_model.Bauteil_Ständer[m].Holzwerkstoff == true && main_model.Bauteil_Ständer[m + 1].Holz == true)
                            {
                                double w1 = (double)main_model.Bauteil_Ständer[m].Gewicht * 1000 * 0.03;
                                double w2 = (double)main_model.Bauteil_Ständer[m + 1].Gewicht * 1000 * 0.05;
                                ZulTauwasser = Math.Min(w1, w2);
                            }
                        }

                        //kapillar wasseraufnahmefähig
                        else
                        {
                            ZulTauwasser = 1000;

                            //Holz 5%
                            if (main_model.Bauteil_Ständer[m].Holz == true && main_model.Bauteil_Ständer[m + 1].Holz == true)
                            {
                                double w1 = (double)main_model.Bauteil_Ständer[m].Gewicht * 1000 * 0.05;
                                double w2 = (double)main_model.Bauteil_Ständer[m + 1].Gewicht * 1000 * 0.05;
                                ZulTauwasser = Math.Min(w1, w2);
                            }
                            else
                            {
                                if (main_model.Bauteil_Ständer[m].Holz == true)
                                {
                                    ZulTauwasser = main_model.Bauteil_Ständer[m].Gewicht * 1000 * 0.05;
                                }
                                else if (main_model.Bauteil_Ständer[m + 1].Holz == true)
                                {
                                    ZulTauwasser = main_model.Bauteil_Ständer[m + 1].Gewicht * 1000 * 0.05;
                                }
                            }

                            //Holzwerkstoff mit 3%
                            if (main_model.Bauteil_Ständer[m].Holzwerkstoff == true && main_model.Bauteil_Ständer[m + 1].Holzwerkstoff == true)
                            {
                                double w1 = (double)main_model.Bauteil_Ständer[m].Gewicht * 1000 * 0.03;
                                double w2 = (double)main_model.Bauteil_Ständer[m + 1].Gewicht * 1000 * 0.03;
                                ZulTauwasser = Math.Min(w1, w2);
                            }
                            else
                            {
                                if (main_model.Bauteil_Ständer[m].Holzwerkstoff == true)
                                {
                                    ZulTauwasser = main_model.Bauteil_Ständer[m].Gewicht * 1000 * 0.03;
                                }
                                else if (main_model.Bauteil_Ständer[m + 1].Holzwerkstoff == true)
                                {
                                    ZulTauwasser = main_model.Bauteil_Ständer[m + 1].Gewicht * 1000 * 0.03;
                                }
                            }

                            //Kombination von Holz und Holzwerkstoff
                            if (main_model.Bauteil_Ständer[m].Holz == true && main_model.Bauteil_Ständer[m + 1].Holzwerkstoff == true)
                            {
                                double w1 = (double)(main_model.Bauteil_Ständer[m].Gewicht * 1000 * 0.05);
                                double w2 = (double)(main_model.Bauteil_Ständer[m + 1].Gewicht * 1000 * 0.03);
                                ZulTauwasser = Math.Min(w1, w2);
                            }
                            else if (main_model.Bauteil_Ständer[m].Holzwerkstoff == true && main_model.Bauteil_Ständer[m + 1].Holz == true)
                            {
                                double w1 = (double)main_model.Bauteil_Ständer[m].Gewicht * 1000 * 0.03;
                                double w2 = (double)main_model.Bauteil_Ständer[m + 1].Gewicht * 1000 * 0.05;
                                ZulTauwasser = Math.Min(w1, w2);
                            }
                        }

                        double? sum_sd = 0;
                        for (int j = 0; j <= m - 1; j++)
                        {
                            sum_sd = sum_sd + main_model.Bauteil_Ständer[j].Sd;
                            Faktor_Dampfdruckverteilung = (main_model.Bauteil_Ständer[m].Dampfteildruck - i.AußenWasserdampfdruck) / (Gesamt_SdStänder - (main_model.Bauteil_Ständer[m].Sd + sum_sd));
                        }

                        Schichtgrenze.Add(new Schichtgrenzen()
                        {
                            Dampfteildruck = (double)main_model.Bauteil_Ständer[m].Dampfteildruck,
                            Sd = (double)main_model.Bauteil_Ständer[m].Sd,
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
                            Tauwassermasse = Delta0 * tev * (((i.InnenWasserdampfdruck - main_model.Bauteil_Ständer[m].Dampfteildruck) / sdc) - ((main_model.Bauteil_Ständer[m].Dampfteildruck - i.AußenWasserdampfdruck) / (Gesamt_SdStänder - sdc)));
                            Verdunstungsmasse = Delta0 * tev * (((Pc - i.InnenDruckVerdunstung) / sdc) + ((Pc - i.AußenDruckVerdunstung) / (Gesamt_SdStänder - sdc)));
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

                            // Hier Sdc1 = sdc2
                            if (sdc1 == sdc2)
                            {
                                sdc_sub = sdc1;
                            }
                            else
                            {
                                sdc_sub = sdc2 - sdc1;
                            }
                            Mc1 = Delta0 * i.TauDauer * 3600 * Math.Pow(10, 3) * (((i.InnenWasserdampfdruck - Schichtgrenze[Schichtgrenze.Count - 2].Dampfteildruck) / sdc1) - ((Schichtgrenze[Schichtgrenze.Count - 2].Dampfteildruck - Schichtgrenze[Schichtgrenze.Count - 1].Dampfteildruck) / sdc_sub));
                            Mc2 = Delta0 * i.TauDauer * 3600 * Math.Pow(10, 3) * (((Schichtgrenze[Schichtgrenze.Count - 2].Dampfteildruck - Schichtgrenze[Schichtgrenze.Count - 1].Dampfteildruck) / sdc_sub) - ((Schichtgrenze[Schichtgrenze.Count - 1].Dampfteildruck - i.AußenWasserdampfdruck) / (Gesamt_SdStänder - sdc2)));

                            Tauwassermasse = Mc1 + Mc2;

                            //Verdunstungsperiode
                            double? tev = i.VerdunstungsDauer * 3600 * Math.Pow(10, 3);
                            double? tev1 = 0;
                            double? tev2 = 0;
                            double? gev1 = 0;
                            double? gev2 = 0;

                            gev1 = Delta0 * (Pc - i.InnenDruckVerdunstung) / sdc1;
                            gev2 = Delta0 * (Pc - i.AußenDruckVerdunstung) / (Gesamt_SdStänder - sdc2);
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
                                    Mev1 = gev1 * tev2 + (gev1 + Delta0 * (Pc - i.AußenDruckVerdunstung) / (Gesamt_SdStänder - sdc1)) * (tev - tev2);
                                }
                            }
                            Verdunstungsmasse = Mev1 + Mev2;
                        }

                        else if (Ebene > 2)
                        {
                            NachweisGrid.IsVisible = false;
                            Meldung3Ebene.IsVisible = true;
                            MeldungDicke.IsVisible = false;
                            Feuchtenachweis.Text = "bitte überprüfen";
                            
                        }
                    }
                    //Kein Tauwasser
                    else
                    {
                        main_model.Bauteil_Ständer[m].TW = false;
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
        }
        public void CalculateFehler_Abschätzung()
        {
            double rj;
            Berechnung_Rj.Clear();

            for (int m = 0; m <= main_model.Bauteil_Gefach.Count - 1; m++)
            {
                for (int n = 0; n <= main_model.Bauteil_Ständer.Count - 1; n++)
                {
                    if (m == n)
                    {
                        rj = (double)(1 / (((EntryGefach / Gesamtflächen) / main_model.Bauteil_Gefach[m].R) + ((EntryStänder / Gesamtflächen) / main_model.Bauteil_Ständer[m].R)));
                        Berechnung_Rj.Add(new Berechnung_R() { Rj = rj });
                    }
                }
            }
            Rlower = Berechnung_Rj.Sum(p => p.Rj) + Horizontal + Außen;


            //Berechnung der Abschätzung und Uges

            Rtot = (Rupper + Rlower) / 2;
            Abschätzung = ((Rupper - Rlower) / (2 * Rtot)) * 100;
            Ugesamt = 1 / Rtot;

        }

        public void Calculate_Ug()
        {

            foreach (Gefach i in main_model.Bauteil_Gefach)
            {
                if (i.R == main_model.Bauteil_Gefach.Max(p => p.R))
                {
                    if (i.KeineLuft == true)
                    {
                        DU_g = 0;
                    }
                    else if (i.EvntlLuft == true)
                    {
                        DU_g = 0.01;
                    }
                    else if (i.MitLuft == true)
                    {
                        DU_g = 0.04;
                    }
                    Ug_Gefach = (EntryGefach / Gesamtflächen) * DU_g * Math.Pow((Convert.ToDouble(i.R / Rgefach)), 2);
                }
            }
            foreach (Ständer i in main_model.Bauteil_Ständer)
            {
                if (i.R == main_model.Bauteil_Ständer.Max(p => p.R))
                {
                    if (i.KeineLuft == true)
                    {
                        DU_g = 0;
                    }
                    else if (i.EvntlLuft == true)
                    {
                        DU_g = 0.01;
                    }
                    else if (i.MitLuft == true)
                    {
                        DU_g = 0.04;
                    }
                    Ug_Ständer = (EntryStänder / Gesamtflächen) * DU_g * Math.Pow((Convert.ToDouble(i.R / Rständer)), 2);
                }
            }
            Ug = Ug_Gefach + Ug_Ständer;
        }

        private void Calculate_Uf()
        {
            //Gefach
            foreach (Gefach i in main_model.Bauteil_Gefach)
            {
                if (i.Wärmeleitfähigkeit == main_model.Bauteil_Gefach.Where(m => m.Wärmeleitfähigkeit != 0).Min(p => p.Wärmeleitfähigkeit))
                {
                    foreach (BefestigerGefach j in main_model.Befestiger_Gefach)
                    {
                        if (j.Länge == null)
                        {
                            j.Uf_i = (EntryGefach / Gesamtflächen) * (0.8 * j.Wärmeleitfähigkeit_f * Math.PI * Math.Pow((Convert.ToDouble(j.Durchmesser / 1000) / 2), 2) * j.Anzahl * Math.Pow(Convert.ToDouble(i.R / Rgefach), 2)) / i.Dicke;
                        }
                        else
                        {
                            j.Uf_i = (EntryGefach / Gesamtflächen) * (0.8 * j.Wärmeleitfähigkeit_f * Math.PI * Math.Pow((Convert.ToDouble(j.Durchmesser / 1000) / 2), 2) * j.Anzahl * Math.Pow(Convert.ToDouble(i.R / Rgefach), 2)) / (j.Länge / 1000);
                        }
                    }
                    Uf_Gefach = main_model.Befestiger_Gefach.Sum(p => p.Uf_i);
                }
            }
            //Ständer nicht relevant, da kein Befestiger betrachtet wird
            /*foreach (Ständer i in main_model.Bauteil_Ständer)
            {
                if (i.Wärmeleitfähigkeit == main_model.Bauteil_Ständer.Min(p => p.Wärmeleitfähigkeit))
                {
                    foreach (BefestigerStänder j in main_model.Befestiger_Ständer)
                    {
                        if (j.Länge == null)
                        {
                            j.Uf_i = (EntryStänder / Gesamtflächen) * (0.8 * j.Wärmeleitfähigkeit_f * Math.PI * Math.Pow((Convert.ToDouble(j.Durchmesser / 1000) / 2), 2) * j.Anzahl * Math.Pow(Convert.ToDouble(i.R / Rständer), 2)) / i.Dicke;

                        }
                        else
                        {
                            j.Uf_i = (EntryStänder / Gesamtflächen) * (0.8 * j.Wärmeleitfähigkeit_f * Math.PI * Math.Pow(Convert.ToDouble((j.Durchmesser / 1000) / 2), 2) * j.Anzahl * Math.Pow(Convert.ToDouble(i.R / Rständer), 2)) / (j.Länge / 1000);
                        }
                    }
                    Uf_Ständer = main_model.Befestiger_Ständer.Sum(p => p.Uf_i);
                }
            }*/
            //Uf = Uf_Gefach + Uf_Ständer;
            Uf = Uf_Gefach;
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

        private async void OnSelected_ItemSelected_Gefach(object sender, SelectedItemChangedEventArgs e)
        {
            if (GefachUwert.IsVisible == true)
            {
                if (listGefachUwert.SelectedItem == null)
                    return;
                var selectedGefach = (e.SelectedItem as Gefach)!;
                listGefachUwert.SelectedItem = null;
                var Gefach = new GefachDetailPage(selectedGefach);
                Gefach.GefachUpdated += (source, gefach) =>
                {
                    selectedGefach.ID_Bauteil = gefach.ID_Bauteil;
                    selectedGefach.ModelID = gefach.ModelID;
                    selectedGefach.Bezeichnung = gefach.Bezeichnung;
                    selectedGefach.R = gefach.R;
                    selectedGefach.Dicke = gefach.Dicke;
                    selectedGefach.Wärmeleitfähigkeit = gefach.Wärmeleitfähigkeit;
                    selectedGefach.Rohdichte = gefach.Rohdichte;
                    selectedGefach.Kapillar = gefach.Kapillar;
                    selectedGefach.Holz = gefach.Holz;
                    selectedGefach.Holzwerkstoff = gefach.Holzwerkstoff;
                    selectedGefach.sonstiges = gefach.sonstiges;
                    selectedGefach.KeineLuft = gefach.KeineLuft;
                    selectedGefach.EvntlLuft = gefach.EvntlLuft;
                    selectedGefach.MitLuft = gefach.MitLuft;
                    selectedGefach.Dampfdiffusionswiderstand_Min = gefach.Dampfdiffusionswiderstand_Min;
                    selectedGefach.Dampfdiffusionswiderstand_Max = gefach.Dampfdiffusionswiderstand_Max;
                    selectedGefach.Sd_Min = gefach.Sd_Min;
                    selectedGefach.Sd_Max = gefach.Sd_Max;
                    selectedGefach.Sd = gefach.Sd;
                    selectedGefach.Tempverlauf = gefach.Tempverlauf;
                    selectedGefach.Dampfteildruck = gefach.Dampfteildruck;
                    selectedGefach.Dampfsättigungsdruck = gefach.Dampfsättigungsdruck;
                    selectedGefach.TW = gefach.TW;
                    selectedGefach.Fester_R = gefach.Fester_R;
                    selectedGefach.Fester_sd = gefach.Fester_sd;
                    selectedGefach.Gewicht = gefach.Gewicht;
                    selectedGefach.DLR1 = gefach.DLR1;
                    selectedGefach.DLR2 = gefach.DLR2;
                    selectedGefach.DLR3 = gefach.DLR3;
                    selectedGefach.DLR4 = gefach.DLR4;
                    selectedGefach.DLR5 = gefach.DLR5;
                    selectedGefach.LR1 = gefach.LR1;
                    selectedGefach.LR2 = gefach.LR2;
                    selectedGefach.LR3 = gefach.LR3;
                    selectedGefach.LR4 = gefach.LR4;
                    selectedGefach.LR5 = gefach.LR5;

                    foreach (Gefach i in GefachList)
                    {
                        if (Double.IsInfinity(Convert.ToDouble(i.R)) || Double.IsNaN(Convert.ToDouble(i.R)))
                        {
                            i.R = 0;
                        }
                    }
                    CalculateSum_Gefach();
                    Calculate_Ug();
                    Calculate_Uf();
                    Calculate_DeltaU();
                };

                //main_model.Bauteil_Gefach.Remove(selectedGefach);
                CalculateSum_Gefach();
                Calculate_Ug();
                Calculate_Uf();
                Calculate_DeltaU();
                main_model.Date = DateTime.Now;
                await App.Database.UpdateItemAsync(main_model);
                await Navigation.PushAsync(new GefachDetailPage(selectedGefach)
                {
                    BindingContext = selectedGefach,
                });

            }
        }
        //Ständer item selected
        private async void OnSelected_ItemSelected_Ständer(object sender, SelectedItemChangedEventArgs e)
        {
            if (StänderUwert.IsVisible == true)
            {
                if (listStänderUwert.SelectedItem == null)
                    return;
                var selectedStänder = (e.SelectedItem as Ständer)!;
                listStänderUwert.SelectedItem = null;
                var Ständer = new StänderDetailPage(selectedStänder);
                Ständer.StänderUpdated += (source, ständer) =>
                {
                    selectedStänder.ID_Bauteil = ständer.ID_Bauteil;
                    selectedStänder.ModelID = ständer.ModelID;
                    selectedStänder.Bezeichnung = ständer.Bezeichnung;
                    selectedStänder.R = ständer.R;
                    selectedStänder.Dicke = ständer.Dicke;
                    selectedStänder.Wärmeleitfähigkeit = ständer.Wärmeleitfähigkeit;
                    selectedStänder.Rohdichte = ständer.Rohdichte;
                    selectedStänder.Kapillar = ständer.Kapillar;
                    selectedStänder.Holz = ständer.Holz;
                    selectedStänder.Holzwerkstoff = ständer.Holzwerkstoff;
                    selectedStänder.sonstiges = ständer.sonstiges;
                    selectedStänder.KeineLuft = ständer.KeineLuft;
                    selectedStänder.EvntlLuft = ständer.EvntlLuft;
                    selectedStänder.MitLuft = ständer.MitLuft;
                    selectedStänder.Dampfdiffusionswiderstand_Min = ständer.Dampfdiffusionswiderstand_Min;
                    selectedStänder.Dampfdiffusionswiderstand_Max = ständer.Dampfdiffusionswiderstand_Max;
                    selectedStänder.Sd_Min = ständer.Sd_Min;
                    selectedStänder.Sd_Max = ständer.Sd_Max;
                    selectedStänder.Sd = ständer.Sd;
                    selectedStänder.Tempverlauf = ständer.Tempverlauf;
                    selectedStänder.Dampfteildruck = ständer.Dampfteildruck;
                    selectedStänder.Dampfsättigungsdruck = ständer.Dampfsättigungsdruck;
                    selectedStänder.TW = ständer.TW;
                    selectedStänder.Fester_R = ständer.Fester_R;
                    selectedStänder.Fester_sd = ständer.Fester_sd;
                    selectedStänder.Gewicht = ständer.Gewicht;
                    selectedStänder.DLR1 = ständer.DLR1;
                    selectedStänder.DLR2 = ständer.DLR2;
                    selectedStänder.DLR3 = ständer.DLR3;
                    selectedStänder.DLR4 = ständer.DLR4;
                    selectedStänder.DLR5 = ständer.DLR5;
                    selectedStänder.LR1 = ständer.LR1;
                    selectedStänder.LR2 = ständer.LR2;
                    selectedStänder.LR3 = ständer.LR3;
                    selectedStänder.LR4 = ständer.LR4;
                    selectedStänder.LR5 = ständer.LR5;
                    foreach (Ständer i in StänderList)
                    {
                        if (Double.IsInfinity(Convert.ToDouble(i.R)) || Double.IsNaN(Convert.ToDouble(i.R)))
                        {
                            i.R = 0;
                        }
                    }
                    CalculateSum_Ständer();
                    Calculate_Ug();
                    Calculate_Uf();
                    Calculate_DeltaU();
                };

                //main_model.Bauteil_Ständer.Remove(selectedStänder);
                CalculateSum_Ständer();
                Calculate_Ug();
                Calculate_Uf();
                Calculate_DeltaU();
                main_model.Date = DateTime.Now;
                await App.Database.UpdateItemAsync(main_model);
                await Navigation.PushAsync(new StänderDetailPage(selectedStänder)
                {
                    BindingContext = selectedStänder,
                });
            }

        }

        public void Deckblatt_Tapped(object sender, EventArgs e)
        {
            //Anzeige von Datentabelle in Deckblatt
            DeckblattTab.IsVisible = true;
            //TauTab.IsVisible = false;

            //Anzeige von U-Wert Frame
            GefachUwert.IsVisible = false;
            StänderUwert.IsVisible = false;
            GesUwert.IsVisible = false;
            BefestigerFrame.IsVisible = false;

            //Anzeige von Flächenanteil
            AnteilGefach.IsVisible = false;
            AnteilStänder.IsVisible = false;

            //Text Bold wenn Tab ausgewählt wurde
            DeckblattButton.FontAttributes = FontAttributes.Bold;
            UwertGefachButton.FontAttributes = FontAttributes.None;
            UwertStänderButton.FontAttributes = FontAttributes.None;
            UgesButton.FontAttributes = FontAttributes.None;
            BefestigerButton.FontAttributes = FontAttributes.None;

            //Plusbutton
            BoxPlus.IsVisible = false;
            ButtonPlus.IsVisible = false;

            NachweisGrid.IsVisible = false;
            MeldungDicke.IsVisible = false;
            if (main_model.Bauteil_Gefach.Count != main_model.Bauteil_Ständer.Count)
            {
                MeldungDicke.IsVisible = true;
                Meldung3Ebene.IsVisible = false;
                NachweisGrid.IsVisible = false;                
                Feuchtenachweis.Text = "bitte überprüfen";
            }
            else
            {
                Summe_Dicke_Gefach = main_model.Bauteil_Gefach.Sum(p => p.Dicke);
                Summe_Dicke_Ständer = main_model.Bauteil_Ständer.Sum(p => p.Dicke);
                if (Math.Abs((double)(Summe_Dicke_Gefach - Summe_Dicke_Ständer)) > 0.000000000001)
                {
                    NachweisGrid.IsVisible = false;
                    MeldungDicke.IsVisible = true;
                    Feuchtenachweis.Text = "bitte überprüfen";
                }
                else
                {
                    CalculateSum_Gefach();
                    CalculateSum_Ständer();                   
                }
            }
        }
        private void Gefach_Tapped(object sender, EventArgs e)
        {
            main_model.Selected = 1;
            //Anzeige von Datentabelle in Deckblatt
            DeckblattTab.IsVisible = false;
            // TauTab.IsVisible = false;
            //Anzeige von U-Wert Frame
            GefachUwert.IsVisible = true;
            StänderUwert.IsVisible = false;
            GesUwert.IsVisible = false;
            BefestigerFrame.IsVisible = false;

            //Anzeige von Flächenanteil
            AnteilGefach.IsVisible = true;
            AnteilStänder.IsVisible = false;

            //Text Bold wenn Tab ausgewählt wurde
            DeckblattButton.FontAttributes = FontAttributes.None;
            UwertGefachButton.FontAttributes = FontAttributes.Bold;
            UwertStänderButton.FontAttributes = FontAttributes.None;
            UgesButton.FontAttributes = FontAttributes.None;
            BefestigerButton.FontAttributes = FontAttributes.None;

            //Plusbutton
            BoxPlus.IsVisible = true;
            ButtonPlus.IsVisible = true;
        }
        private void Ständer_Tapped(object sender, EventArgs e)
        {
            main_model.Selected = 1;
            //Anzeige von Datentabelle in Deckblatt
            DeckblattTab.IsVisible = false;
            //TauTab.IsVisible = false;
            //Anzeige von U-Wert Frame
            GefachUwert.IsVisible = false;
            StänderUwert.IsVisible = true;
            GesUwert.IsVisible = false;
            BefestigerFrame.IsVisible = false;

            //Anzeige von Flächenanteil
            AnteilGefach.IsVisible = false;
            AnteilStänder.IsVisible = true;

            //Text Bold wenn Tab ausgewählt wurde
            DeckblattButton.FontAttributes = FontAttributes.None;
            UwertGefachButton.FontAttributes = FontAttributes.None;
            UwertStänderButton.FontAttributes = FontAttributes.Bold;
            UgesButton.FontAttributes = FontAttributes.None;
            BefestigerButton.FontAttributes = FontAttributes.None;

            //Plusbutton
            BoxPlus.IsVisible = true;
            ButtonPlus.IsVisible = true;
        }
        private void Uges_Tapped(object sender, EventArgs e)
        {
            //Anzeige von Datentabelle in Deckblatt
            DeckblattTab.IsVisible = false;
            //TauTab.IsVisible = false;

            //Anzeige von U-Wert Frame
            GefachUwert.IsVisible = false;
            StänderUwert.IsVisible = false;
            GesUwert.IsVisible = true;
            BefestigerFrame.IsVisible = false;

            //Anzeige von Flächenanteil
            AnteilGefach.IsVisible = false;
            AnteilStänder.IsVisible = false;

            //Text Bold wenn Tab ausgewählt wurde
            DeckblattButton.FontAttributes = FontAttributes.None;
            UwertGefachButton.FontAttributes = FontAttributes.None;
            UwertStänderButton.FontAttributes = FontAttributes.None;
            UgesButton.FontAttributes = FontAttributes.Bold;
            BefestigerButton.FontAttributes = FontAttributes.None;

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

            Summe_Dicke_Gefach = main_model.Bauteil_Gefach.Sum(p => p.Dicke);
            Summe_Dicke_Ständer = main_model.Bauteil_Ständer.Sum(p => p.Dicke);

            if (main_model.Bauteil_Gefach.Count != main_model.Bauteil_Ständer.Count || Math.Abs((double)(Summe_Dicke_Gefach - Summe_Dicke_Ständer)) > 0.000000000001)
            {
                Frame_Ergebnis.IsVisible = false;
                Frame_Warnung.IsVisible = true;
            }
            Gesamt_Gewicht = ((EntryGefach / Gesamtflächen) * main_model.Bauteil_Gefach.Sum(p => p.Gewicht) + (EntryStänder / Gesamtflächen) * main_model.Bauteil_Ständer.Sum(p => p.Gewicht));
        }

        private void Befestiger_Tapped(object sender, EventArgs e)
        {
            //Anzeige von Datentabelle in Deckblatt
            DeckblattTab.IsVisible = false;
            //TauTab.IsVisible = false;
            //Anzeige von U-Wert Frame
            GefachUwert.IsVisible = false;
            StänderUwert.IsVisible = false;
            GesUwert.IsVisible = false;
            BefestigerFrame.IsVisible = true;

            //Anzeige von Flächenanteil
            AnteilGefach.IsVisible = false;
            AnteilStänder.IsVisible = false;

            //Text Bold wenn Tab ausgewählt wurde
            DeckblattButton.FontAttributes = FontAttributes.None;
            UwertGefachButton.FontAttributes = FontAttributes.None;
            UwertStänderButton.FontAttributes = FontAttributes.None;
            UgesButton.FontAttributes = FontAttributes.None;
            BefestigerButton.FontAttributes = FontAttributes.Bold;

            //Plusbutton
            BoxPlus.IsVisible = false;
            ButtonPlus.IsVisible = false;
            if (main_model.Befestiger_Gefach.Count == 0)
            {
                FrameBefestigerGefach.IsVisible = false;
            }
            else { FrameBefestigerGefach.IsVisible = true; }
            if (main_model.Befestiger_Ständer.Count == 0)
            {
                FrameBefestigerStänder.IsVisible = false;
            }
            else { FrameBefestigerStänder.IsVisible = true; }
        }

        private async void Befestiger_Einfügen_Clicked(object sender, EventArgs e)
        {
            string action = await DisplayActionSheet("Befestiger hinzufügen in", "Abbrechen", null, "Gefach", "Ständer");

            //Befestiger in Gefach hinzufügen
            //if (UwertGefachButton.FontAttributes == FontAttributes.Bold)
            if (action == "Gefach")
            {
                var newGefach = new BefestigerPage();
                newGefach.BefestigerAdded += async (source, befestigerGefach) =>
                {
                    befestigerGefach.ModelID = main_model.ID;
                    NewItem_Gefach_Befestiger = new BefestigerGefach()
                    {
                        ID_Befestiger = befestigerGefach.ID_Befestiger,
                        Bezeichnung = befestigerGefach.Bezeichnung,
                        Durchmesser = befestigerGefach.Durchmesser,
                        Wärmeleitfähigkeit_f = befestigerGefach.Wärmeleitfähigkeit_f,
                        Eindringtiefe = befestigerGefach.Eindringtiefe,
                        Uf_i = befestigerGefach.Uf_i,
                        ModelID = befestigerGefach.ModelID,
                        Anzahl = befestigerGefach.Anzahl,
                    };
                    _befestigerGefach.Add(NewItem_Gefach_Befestiger);
                    await App.Database.SaveFixGefachAsync(NewItem_Gefach_Befestiger);
                    GetGefach();
                    Calculate_Uf();
                    Calculate_Ug();
                    Calculate_DeltaU();
                    BefestigerButton.IsVisible = true;
                };
                main_model.Date = DateTime.Now;
                await App.Database.UpdateItemAsync(main_model);
                await Navigation.PushAsync(newGefach);
            }
            //Befestiger in Ständer hinzufügen
            //else if (UwertStänderButton.FontAttributes == FontAttributes.Bold)
            else if (action == "Ständer")
            {
                var newStänder = new BefestigerPage();
                newStänder.BefestigerAdded += async (source, befestigerStänder) =>
                {
                    befestigerStänder.ModelID = main_model.ID;
                    NewItem_Ständer_Befestiger = new BefestigerStänder()
                    {
                        ID_Befestiger = befestigerStänder.ID_Befestiger,
                        Bezeichnung = befestigerStänder.Bezeichnung,
                        Durchmesser = befestigerStänder.Durchmesser,
                        Wärmeleitfähigkeit_f = befestigerStänder.Wärmeleitfähigkeit_f,
                        Eindringtiefe = befestigerStänder.Eindringtiefe,
                        Uf_i = befestigerStänder.Uf_i,
                        ModelID = befestigerStänder.ModelID,
                        Anzahl = befestigerStänder.Anzahl,
                    };
                    _befestigerStänder.Add(NewItem_Ständer_Befestiger);
                    await App.Database.SaveFixStänderAsync(NewItem_Ständer_Befestiger);
                    GetStänder();
                    Calculate_Uf();
                    Calculate_Ug();
                    Calculate_DeltaU();
                    BefestigerButton.IsVisible = true;
                };
                main_model.Date = DateTime.Now;
                await App.Database.UpdateItemAsync(main_model);
                await Navigation.PushAsync(newStänder);

            }
        }
        private async void Bauteil_Einfügen_Clicked(object sender, EventArgs e)
        {
            //In Gefach einfügen
            if (UwertGefachButton.FontAttributes == FontAttributes.Bold)
            {
                var newGefachBauteil = new BauteilPage();
                newGefachBauteil.BauteilAdded += async (source, gefach) =>
                {
                    gefach.ID_Sort = GefachList.Count() + 1;
                    gefach.ModelID = main_model.ID;
                    NewItem_Gefach_Bauteil = new Gefach()
                    {
                        ID_Bauteil = gefach.ID_Bauteil,
                        Bezeichnung = gefach.Bezeichnung,
                        R = gefach.R,
                        Dicke = gefach.Dicke,
                        Wärmeleitfähigkeit = gefach.Wärmeleitfähigkeit,
                        Rohdichte = gefach.Rohdichte,
                        Kapillar = gefach.Kapillar,
                        Holz = gefach.Holz,
                        Holzwerkstoff = gefach.Holzwerkstoff,
                        sonstiges = gefach.sonstiges,
                        KeineLuft = gefach.KeineLuft,
                        EvntlLuft = gefach.EvntlLuft,
                        MitLuft = gefach.MitLuft,
                        Dampfdiffusionswiderstand_Min = gefach.Dampfdiffusionswiderstand_Min,
                        Dampfdiffusionswiderstand_Max = gefach.Dampfdiffusionswiderstand_Max,
                        Sd_Min = gefach.Sd_Min,
                        Sd_Max = gefach.Sd_Max,
                        Sd = gefach.Sd,
                        Tempverlauf = gefach.Tempverlauf,
                        Dampfteildruck = gefach.Dampfteildruck,
                        Dampfsättigungsdruck = gefach.Dampfsättigungsdruck,
                        TW = gefach.TW,
                        Fester_R = gefach.Fester_R,
                        Fester_sd = gefach.Fester_sd,
                        Gewicht = gefach.Gewicht,
                        ModelID = gefach.ModelID,
                        DLR1 = gefach.DLR1,
                        DLR2 = gefach.DLR2,
                        DLR3 = gefach.DLR3,
                        DLR4 = gefach.DLR4,
                        DLR5 = gefach.DLR5,
                        LR1 = gefach.LR1,
                        LR2 = gefach.LR2,
                        LR3 = gefach.LR3,
                        LR4 = gefach.LR4,
                        LR5 = gefach.LR5,
                    };
                    _gefach.Add(NewItem_Gefach_Bauteil);
                    await App.Database.SaveBauteilGefachAsync(NewItem_Gefach_Bauteil);
                    GetGefach();
                    CalculateSum_Gefach();
                    Calculate_Ug();
                    Calculate_Uf();
                    Calculate_DeltaU();
                };
                main_model.Date = DateTime.Now;
                await App.Database.UpdateItemAsync(main_model);
                await Navigation.PushAsync(newGefachBauteil);
            }
            //In Ständer einfügen
            else if (UwertStänderButton.FontAttributes == FontAttributes.Bold)
            {
                var newStänderBauteil = new BauteilPage();
                newStänderBauteil.BauteilAdded += async (source, ständer) =>
                {
                    ständer.ID_Sort = StänderList.Count() + 1;
                    ständer.ModelID = main_model.ID;
                    NewItem_Ständer_Bauteil = new Ständer()
                    {
                        ID_Bauteil = ständer.ID_Bauteil,
                        Bezeichnung = ständer.Bezeichnung,
                        R = ständer.R,
                        Dicke = ständer.Dicke,
                        Wärmeleitfähigkeit = ständer.Wärmeleitfähigkeit,
                        Rohdichte = ständer.Rohdichte,
                        Kapillar = ständer.Kapillar,
                        Holz = ständer.Holz,
                        Holzwerkstoff = ständer.Holzwerkstoff,
                        sonstiges = ständer.sonstiges,
                        KeineLuft = ständer.KeineLuft,
                        EvntlLuft = ständer.EvntlLuft,
                        MitLuft = ständer.MitLuft,
                        Dampfdiffusionswiderstand_Min = ständer.Dampfdiffusionswiderstand_Min,
                        Dampfdiffusionswiderstand_Max = ständer.Dampfdiffusionswiderstand_Max,
                        Sd_Min = ständer.Sd_Min,
                        Sd_Max = ständer.Sd_Max,
                        Sd = ständer.Sd,
                        Tempverlauf = ständer.Tempverlauf,
                        Dampfteildruck = ständer.Dampfteildruck,
                        Dampfsättigungsdruck = ständer.Dampfsättigungsdruck,
                        TW = ständer.TW,
                        Fester_R = ständer.Fester_R,
                        Fester_sd = ständer.Fester_sd,
                        Gewicht = ständer.Gewicht,
                        ModelID = ständer.ModelID,
                        DLR1 = ständer.DLR1,
                        DLR2 = ständer.DLR2,
                        DLR3 = ständer.DLR3,
                        DLR4 = ständer.DLR4,
                        DLR5 = ständer.DLR5,
                        LR1 = ständer.LR1,
                        LR2 = ständer.LR2,
                        LR3 = ständer.LR3,
                        LR4 = ständer.LR4,
                        LR5 = ständer.LR5,
                    };
                    _ständer.Add(NewItem_Ständer_Bauteil);
                    await App.Database.SaveBauteilStänderAsync(NewItem_Ständer_Bauteil);
                    GetStänder();
                    CalculateSum_Ständer();
                    Calculate_Ug();
                    Calculate_Uf();
                    Calculate_DeltaU();
                };
                main_model.Date = DateTime.Now;
                await App.Database.UpdateItemAsync(main_model);
                await Navigation.PushAsync(newStänderBauteil);
            }
        }

        private async void OnSelected_Befestiger_Gefach(object sender, SelectedItemChangedEventArgs e)
        {
            if (listBefestigerGefach.SelectedItem == null)
                return;
            var selectedBefestiger = (e.SelectedItem as BefestigerGefach)!;
            listBefestigerGefach.SelectedItem = null;
            var GefachBefestiger = new GefachEinfügen(selectedBefestiger);
            GefachBefestiger.BefestigerGefachUpdated += (source, befestiger) =>
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

            //main_model.Befestiger_Gefach.Remove(selectedBefestiger);
            Calculate_Uf();
            Calculate_Ug();
            Calculate_DeltaU();
            main_model.Date = DateTime.Now;
            await App.Database.UpdateItemAsync(main_model);
            await Navigation.PushAsync(new GefachEinfügen(selectedBefestiger)
            {
                BindingContext = selectedBefestiger,
            });
        }
        private async void OnSelected_Befestiger_Ständer(object sender, SelectedItemChangedEventArgs e)
        {
            if (listBefestigerStänder.SelectedItem == null)
                return;
            var selectedBefestiger = (e.SelectedItem as BefestigerStänder)!;
            listBefestigerStänder.SelectedItem = null;
            var StänderBefestiger = new StänderEinfügen(selectedBefestiger);
            StänderBefestiger.BefestigerStänderUpdated += (source, befestiger) =>
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

          //  main_model.Befestiger_Ständer.Remove(selectedBefestiger);
            Calculate_Uf();
            Calculate_Ug();
            Calculate_DeltaU();
            main_model.Date = DateTime.Now;
            await App.Database.UpdateItemAsync(main_model);
            await Navigation.PushAsync(new StänderEinfügen(selectedBefestiger)
            {
                BindingContext = selectedBefestiger,
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
            CalculateSum_Gefach();
            CalculateSum_Ständer();

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
            ImageButton imagebutton = (sender as ImageButton)!;
            if (UwertGefachButton.FontAttributes == FontAttributes.Bold)
            {
                var item = (imagebutton.BindingContext as Gefach)!;
                int old_id = item.ID_Bauteil;
                int next_id = 0;
                for (int i = main_model.Bauteil_Gefach.Count() - 1; i >= 0; i--)
                {
                    if (main_model.Bauteil_Gefach[i].ID_Bauteil < old_id)
                    {
                        next_id = main_model.Bauteil_Gefach[i].ID_Bauteil;
                        break;
                    }
                }
                int itemToInsertBefore_old_ID = next_id; foreach (Gefach i in main_model.Bauteil_Gefach)
                {
                    if (i.ID_Bauteil == itemToInsertBefore_old_ID)
                    {
                        i.ID_Bauteil = old_id;
                        item.ID_Bauteil = itemToInsertBefore_old_ID;
                        await App.Database.UpdateBauteilGefachAsync(i);
                        break;
                    }
                }
                await App.Database.UpdateBauteilGefachAsync(item);
                GetGefach();
            }
            else if (UwertStänderButton.FontAttributes == FontAttributes.Bold)
            {
                var item = (imagebutton.BindingContext as Ständer)!;
                int old_id = item.ID_Bauteil;
                int next_id = 0;
                for (int i = main_model.Bauteil_Ständer.Count() - 1; i >= 0; i--)
                {
                    if (main_model.Bauteil_Ständer[i].ID_Bauteil < old_id)
                    {
                        next_id = main_model.Bauteil_Ständer[i].ID_Bauteil;
                        break;
                    }
                }
                int itemToInsertBefore_old_ID = next_id; foreach (Ständer i in main_model.Bauteil_Ständer)
                {
                    if (i.ID_Bauteil == itemToInsertBefore_old_ID)
                    {
                        i.ID_Bauteil = old_id;
                        item.ID_Bauteil = itemToInsertBefore_old_ID;
                        await App.Database.UpdateBauteilStänderAsync(i);
                        break;
                    }
                }
                await App.Database.UpdateBauteilStänderAsync(item);
                GetStänder();
            }
            main_model.Date = DateTime.Now;
            await App.Database.UpdateItemAsync(main_model);
        }

        private async void Down_Clicked(object sender, EventArgs e)
        {
            ImageButton imagebutton = (sender as ImageButton)!;
            if (UwertGefachButton.FontAttributes == FontAttributes.Bold)
            {
                var item = (imagebutton.BindingContext as Gefach)!;
                int old_id = item.ID_Bauteil;
                int next_id = 0;
                foreach (Gefach i in main_model.Bauteil_Gefach)
                {
                    if (i.ID_Bauteil > old_id)
                    {
                        next_id = i.ID_Bauteil;
                        break;
                    }
                }
                int itemToInsertBefore_old_ID = next_id; 
                foreach (Gefach i in main_model.Bauteil_Gefach)
                {
                    if (i.ID_Bauteil == itemToInsertBefore_old_ID)
                    {
                        i.ID_Bauteil = old_id;
                        item.ID_Bauteil = itemToInsertBefore_old_ID;
                        await App.Database.UpdateBauteilGefachAsync(i);
                        break;
                    }
                }
                await App.Database.UpdateBauteilGefachAsync(item);
                GetGefach();
            }
            else if (UwertStänderButton.FontAttributes == FontAttributes.Bold)
            {
                var item = (imagebutton.BindingContext as Ständer)!;
                int old_id = item.ID_Bauteil;
                int next_id = 0;
                foreach (Ständer i in main_model.Bauteil_Ständer)
                {
                    if (i.ID_Bauteil > old_id)
                    {
                        next_id = i.ID_Bauteil;
                        break;
                    }
                }
                int itemToInsertBefore_old_ID = next_id; 
                foreach (Ständer i in main_model.Bauteil_Ständer)
                {
                    if (i.ID_Bauteil == itemToInsertBefore_old_ID)
                    {
                        i.ID_Bauteil = old_id;
                        item.ID_Bauteil = itemToInsertBefore_old_ID;
                        await App.Database.UpdateBauteilStänderAsync(i);
                        break;
                    }
                }
                await App.Database.UpdateBauteilStänderAsync(item);
                GetStänder();
            }
            main_model.Date = DateTime.Now;
            await App.Database.UpdateItemAsync(main_model);
        }
    }
}
