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

namespace MFBauphysikMobilMAUI.NewProject
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BauteilPage : ContentPage
    {
        public Bauteile? NewItem { get; set; }
        public EventHandler<Bauteile>? BauteilAdded;
        List<WLG>? _basis;
        
        public BauteilPage()
        {
            InitializeComponent();           
           var assembly = typeof(App).Assembly;
            Stream? stream = assembly.GetManifestResourceStream("MFBauphysikMobilMAUI.Resources.Raw.Bauteile.xml");
            XmlSerializer serializer = new XmlSerializer(typeof(List<WLG>));
            _basis = (List<WLG>)serializer.Deserialize(stream!)!;
           
            listView.ItemsSource = _basis;
            foreach(WLG i in _basis)
            {
                i.SizeClass = Setting.Size_Default;
            }
        }

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
    }    
}