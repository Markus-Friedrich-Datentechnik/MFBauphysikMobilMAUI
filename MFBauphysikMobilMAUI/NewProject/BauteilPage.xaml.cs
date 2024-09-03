using MFBauphysikMobilMAUI;
using MFBauphysikMobilMAUI.Helpers;
using MFBauphysikMobilMAUI.Models;
using SQLitePCL;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using Microsoft.Maui.Controls.Xaml;
using Microsoft.Maui.Controls;
using Microsoft.Maui;
using System.Net.Security;
using System.Web;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using System.ComponentModel;

namespace MFBauphysikMobilMAUI.NewProject
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BauteilPage : ContentPage
    {
        public Bauteile? NewItem { get; set; }
        public EventHandler<Bauteile>? BauteilAdded;
        public string Kapillar;
        List<WLG>? _basis;
        readonly IList<Bauteile>? source;
        Bauteile selectedAufbau;
        int selectionCount = 1;


        public ObservableCollection<Bauteile> Aufbau { get; private set; }
        public IList<Bauteile> EmptyAufbau { get; private set; }   

        public Bauteile SelectedAufbau
        {
            get
            {
                return selectedAufbau;
            }
            set
            {
                if (selectedAufbau != value)
                {
                    selectedAufbau = value;
                }
            }
        }

        ObservableCollection<object> selectedAufbaus;
        public ObservableCollection<object> SelectedAufbaus
        {
            get
            {
                return selectedAufbaus;
            }
            set
            {
                if (selectedAufbaus != value)
                {
                    selectedAufbaus = value;
                }
            }
        }
        public string SelectedAufbauMessage { get; private set; }

        //public ICommand AufbauSelectionChangedCommand => new Command(AufbauSelectionChanged);
        //public class Test { public string name; public string rho;};
        public BauteilPage()
        {
            InitializeComponent();
            this.BindingContext = this;                                 
            var assembly = typeof(App).Assembly;
            Stream? stream = (assembly.GetManifestResourceStream("MFBauphysikMobil.NewProject.Bauteile.xml"))!;
            //XDocument doc = XDocument.Load(stream);

            using(var reader = new StreamReader(stream))
            {
                var serializer = new XmlSerializer(typeof(List<WLG>));
                _basis = (List<WLG>)serializer.Deserialize(reader);
            }
            /*_basis = doc.Descendants("WLG").
                Where(x => !x.Elements("NK").Any()
                        && !x.Elements("KF").Any()
                        && !x.Elements("VD").Any()
                        && !x.Elements("LR").Any()
                        && !x.Elements("HA").Any()
                        && !x.Elements("FSD").Any()
                        && !x.Elements("FWDW").Any()
                        && !x.Elements("LR").Any()
                        && !x.Elements("LR1").Any()
                        && !x.Elements("LR2").Any()
                        && !x.Elements("LR3").Any()
                        && !x.Elements("LR4").Any()
                        && !x.Elements("LR5").Any()
                        && !x.Elements("DLR1").Any()
                        && !x.Elements("DLR2").Any()
                        && !x.Elements("DLR3").Any()
                        && !x.Elements("DLR4").Any()
                        && !x.Elements("DLR5").Any())
                .Select(element =>
            new WLG
            {
                B = element.Element("B").Value,
                WDW = element.Element("WDW").Value,
                RHO = element.Element("RHO").Value,
                D = element.Element("D").Value,
                MMin = element.Element("MMin").Value,
                MMax = element.Element("MMax").Value,
                SDMin = element.Element("SDMin").Value,
                SDMax = element.Element("SDMax").Value,
                NK = "0",
                KF = "0",
                VD = "0",
                //LR = element.Element("LR").Value,
                HA = "0",
                FWDW = "0",
                FSD = "0",
                DLR1 = "0",
                DLR2 = "0",
                DLR3 = "0",
                DLR4 = "0",
                DLR5 = "0",
                LR1 = "0",
                LR2 = "0",
                LR3 = "0",
                LR4 = "0",
                LR5 = "0",

            }).ToList();

            List<WLG> nonzero_LR = doc.Descendants("WLG")
                .Where(x => x.Elements("LR").Any())
                .Select(element =>
            new WLG
            {
                B = element.Element("B").Value,
                WDW = element.Element("WDW").Value,
                RHO = element.Element("RHO").Value,
                D = element.Element("D").Value,
                MMin = element.Element("MMin").Value,
                MMax = element.Element("MMax").Value,
                SDMin = element.Element("SDMin").Value,
                SDMax = element.Element("SDMax").Value,
                HA = element.Element("HA").Value,
                LR = element.Element("LR").Value,
            }).ToList();

            List<WLG> zero_LR = doc.Descendants("WLG")
                .Where(x => !x.Elements("LR").Any())
                .Select(element =>
            new WLG
            {
                B = element.Element("B").Value,
                WDW = element.Element("WDW").Value,
                RHO = element.Element("RHO").Value,
                D = element.Element("D").Value,
                MMin = element.Element("MMin").Value,
                MMax = element.Element("MMax").Value,
                SDMin = element.Element("SDMin").Value,
                SDMax = element.Element("SDMax").Value,
                HA = element.Element("HA").Value,
                LR = "0.000",
            }).ToList();

            /*List<WLG> zero_LR5 = doc.Descendants("WLG")
                .Where(x => x.Elements("LR5").Any())
                .Select(element =>
                new WLG
                {
                    B = element.Element("B").Value,
                    WDW = element.Element("WDW").Value,
                    RHO = element.Element("RHO").Value,
                    D = element.Element("D").Value,
                    MMin = element.Element("MMin").Value,
                    MMax = element.Element("MMax").Value,
                    SDMin = element.Element("SDMin").Value,
                    SDMax = element.Element("SDMax").Value,
                    HA = element.Element("HA").Value,
                    LR = element.Element("LR").Value,
                }).ToList();
            _basis.AddRange(nonzero_LR);
            _basis.AddRange(zero_LR);*/
            listView.ItemsSource = _basis.OrderBy(p => p.B);
            foreach (WLG i in _basis)
            {
                i.SizeClass = Setting.Size_Default;
            }
            /* source = new List<Bauteile>();
             CreateAufbauCollection();

             selectedAufbau = Aufbau.Skip(3).FirstOrDefault();
             AufbauSelectionChanged();

             SelectedAufbaus = new ObservableCollection<object>()
             {
             Aufbau[1], Aufbau[2], Aufbau[3]
             };*/
        }
        /*void CreateAufbauCollection()
        {
            foreach (WLG i in _basis)
            {
                source.Add(new Bauteile
                {
                    Bezeichnung = i.B,
                    Wärmeleitfähigkeit = Convert.ToDouble(i.LR),
                    Dicke = Convert.ToDouble(i.D),
                    R = Convert.ToDouble(i.WDW),
                    Sd_Min = Convert.ToDouble(i.SDMin),
                    Sd_Max = Convert.ToDouble(i.SDMax),
                    Dampfdiffusionswiderstand_Min = Convert.ToDouble(i.MMin),
                    Dampfdiffusionswiderstand_Max = Convert.ToDouble(i.MMax),
                    Rohdichte = Convert.ToDouble(i.RHO),
                    LR1 = Convert.ToDouble(i.LR1),
                    LR2 = Convert.ToDouble(i.LR2),
                    LR3 = Convert.ToDouble(i.LR3),
                    LR4 = Convert.ToDouble(i.LR4),
                    LR5 = Convert.ToDouble(i.LR5),
                    DLR1 = Convert.ToDouble(i.DLR1),
                    DLR2 = Convert.ToDouble(i.DLR2),
                    DLR3 = Convert.ToDouble(i.DLR3),
                    DLR4 = Convert.ToDouble(i.DLR4),
                    DLR5 = Convert.ToDouble(i.DLR5),
                });
            }
            Aufbau = new ObservableCollection<Bauteile>(source.OrderBy(p => p.Bezeichnung));
            BindingContext = this;
        }*/

     /*   void AufbauSelectionChanged()
        {
            SelectedAufbauMessage = $"Selection {selectionCount}:{SelectedAufbau.Bezeichnung}";
            OnPropertyChanged("SelectedAufbauMessage");
            selectionCount++;
        }*/

      /*  #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion*/

        private async void Back_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
        private async void Check_Clicked(object sender, EventArgs e)
        {
            if (NewItem == null)
            {
                await DisplayAlert("Achtung", "Bitte wählen Sie einen Bauteil aus", "OK");
            }
            else
            {
                BauteilAdded?.Invoke(this, NewItem);
                await Navigation.PopAsync();
            }           
        }

        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var selectedItem = (e.SelectedItem as WLG)!;

            NewItem = new Bauteile()
            {
                Bezeichnung = selectedItem.B,
                Wärmeleitfähigkeit = Convert.ToDouble(selectedItem.LR),
                Dicke = Convert.ToDouble(selectedItem.D),
                R = Convert.ToDouble(selectedItem.WDW),
                Sd_Min = Convert.ToDouble(selectedItem.SDMin),
                Sd_Max = Convert.ToDouble(selectedItem.SDMax),
                Dampfdiffusionswiderstand_Min = Convert.ToDouble(selectedItem.MMin),
                Dampfdiffusionswiderstand_Max = Convert.ToDouble(selectedItem.MMax),
                Rohdichte = Convert.ToDouble(selectedItem.RHO),
                LR1 = Convert.ToDouble(selectedItem.LR1),
                LR2 = Convert.ToDouble(selectedItem.LR2),
                LR3 = Convert.ToDouble(selectedItem.LR3),
                LR4 = Convert.ToDouble(selectedItem.LR4),
                LR5 = Convert.ToDouble(selectedItem.LR5),
                DLR1 = Convert.ToDouble(selectedItem.DLR1),
                DLR2 = Convert.ToDouble(selectedItem.DLR2),
                DLR3 = Convert.ToDouble(selectedItem.DLR3),
                DLR4 = Convert.ToDouble(selectedItem.DLR4),
                DLR5 = Convert.ToDouble(selectedItem.DLR5),
            };
            if(selectedItem.HA == "1")
            {
                NewItem.Holzwerkstoff = true;
            }
            else if(selectedItem.HA == "2")
            {
                NewItem.sonstiges = true;
            }
            else
            {
                NewItem.Holz = true;
            }
            //Fester Wärmedurchlasswiderstand
            if(selectedItem.FWDW == "1")
            {
                NewItem.Fester_R = true;
            }
            else { NewItem.Fester_R = false; }
            //Fester Sd
            if (selectedItem.FSD == "1")
            {
                NewItem.Fester_sd = true;
            }
            else { NewItem.Fester_sd = false; }
            //Kapillar nicht wasser aufnahmefähig
            if(selectedItem.NK == "1")
            {
                NewItem.Kapillar = true;
            }
            else { NewItem.Kapillar = false; }
            //KorrekturLuft
            if(selectedItem.KF == "1")
            {
                NewItem.EvntlLuft = true;
            }
            else if(selectedItem.KF == "2")
            {
                NewItem.MitLuft = true;
            }
            else { NewItem.KeineLuft = true; }
            NewItem.Gewicht = NewItem.Dicke * NewItem.Rohdichte;
        }
        private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            var itemsource = _basis!;
            listView.ItemsSource = itemsource.Where(p => p.B.ToLower().Contains(e.NewTextValue)).OrderBy(p => p.B);
        }

      /*  private void listView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            WLG selectedItem = e.CurrentSelection.FirstOrDefault() as WLG;
            NewItem = new Bauteile()
            {
                Bezeichnung = selectedItem.B,
                Wärmeleitfähigkeit = Convert.ToDouble(selectedItem.LR),
                Dicke = Convert.ToDouble(selectedItem.D),
                R = Convert.ToDouble(selectedItem.WDW),
                Sd_Min = Convert.ToDouble(selectedItem.SDMin),
                Sd_Max = Convert.ToDouble(selectedItem.SDMax),
                Dampfdiffusionswiderstand_Min = Convert.ToDouble(selectedItem.MMin),
                Dampfdiffusionswiderstand_Max = Convert.ToDouble(selectedItem.MMax),
                Rohdichte = Convert.ToDouble(selectedItem.RHO),
                LR1 = Convert.ToDouble(selectedItem.LR1),
                LR2 = Convert.ToDouble(selectedItem.LR2),
                LR3 = Convert.ToDouble(selectedItem.LR3),
                LR4 = Convert.ToDouble(selectedItem.LR4),
                LR5 = Convert.ToDouble(selectedItem.LR5),
                DLR1 = Convert.ToDouble(selectedItem.DLR1),
                DLR2 = Convert.ToDouble(selectedItem.DLR2),
                DLR3 = Convert.ToDouble(selectedItem.DLR3),
                DLR4 = Convert.ToDouble(selectedItem.DLR4),
                DLR5 = Convert.ToDouble(selectedItem.DLR5),
            };
            if (selectedItem.HA == "1")
            {
                NewItem.Holzwerkstoff = true;
            }
            else if (selectedItem.HA == "2")
            {
                NewItem.sonstiges = true;
            }
            else
            {
                NewItem.Holz = true;
            }
            //Fester Wärmedurchlasswiderstand
            if (selectedItem.FWDW == "1")
            {
                NewItem.Fester_R = true;
            }
            else { NewItem.Fester_R = false; }
            //Fester Sd
            if (selectedItem.FSD == "1")
            {
                NewItem.Fester_sd = true;
            }
            else { NewItem.Fester_sd = false; }
            //Kapillar nicht wasser aufnahmefähig
            if (selectedItem.NK == "1")
            {
                NewItem.Kapillar = true;
            }
            else { NewItem.Kapillar = false; }
            //KorrekturLuft
            if (selectedItem.KF == "1")
            {
                NewItem.EvntlLuft = true;
            }
            else if (selectedItem.KF == "2")
            {
                NewItem.MitLuft = true;
            }
            else { NewItem.KeineLuft = true; }
            NewItem.Gewicht = NewItem.Dicke * NewItem.Rohdichte;
        }*/
    }    
}