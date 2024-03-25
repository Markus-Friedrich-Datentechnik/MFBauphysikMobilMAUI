using MFBauphysikMobilMAUI.Helpers;
using MFBauphysikMobilMAUI.Models;
using MFBauphysikMobilMAUI.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Maui.Controls.Xaml;
using Microsoft.Maui.Controls;
using Microsoft.Maui;
using MFBauphysikMobilMAUI;

namespace MFBauphysikMobilMAUI.NewProject
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BasisEinfügen : ContentPage
    {
        public MainModel main_model { get; set; }
        private double _size_title;
        public double SizeTitle
        {
            get { return _size_title; }
            set
            {
                if (_size_title == value)
                    return;
                _size_title = value;
                OnPropertyChanged(nameof(SizeTitle));
            }
        }

        public event EventHandler<BefestigerBasis> BefestigerUpdated;
        public event EventHandler<BefestigerBasis> BefestigerAdded;
        public event EventHandler<BefestigerBasis> BefestigerRemoved;
        public BasisEinfügen(BefestigerBasis befestiger)
        {
            if (befestiger == null)
                throw new ArgumentException(nameof(befestiger));
            InitializeComponent();

            BindingContext = new BefestigerBasis
            {
                Bezeichnung = befestiger.Bezeichnung,
                ID_Befestiger = befestiger.ID_Befestiger,
                Anzahl = befestiger.Anzahl,
                Wärmeleitfähigkeit_f = befestiger.Wärmeleitfähigkeit_f,
                Durchmesser = befestiger.Durchmesser,
                Eindringtiefe = befestiger.Eindringtiefe,
                Länge = befestiger.Länge,
                ModelID = befestiger.ModelID,
                SizeClass = Setting.Size_Default,
            };
            SizeTitle = Setting.Size_Title;

        }

        private void Back_Clicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }
        private async void Check_Clicked(object sender, EventArgs e)
        {
            var befestiger = BindingContext as BefestigerBasis;
           
            if (befestiger.Anzahl == 0 || string.IsNullOrEmpty(Anzahl_entry.Text))
            {
                await DisplayAlert("Achtung", "Bitte geben Sie eine Zahl zwischen 1 und 9999999999 ein.", "OK");
                Anzahl_entry.Focus();
                return;
            }
            if (befestiger.Wärmeleitfähigkeit_f == null)
            {
                await DisplayAlert("Achtung", "Bitte geben Sie eine Zahl zwischen 1 und 9999999999 ein.", "OK");
                Lambda_entry.Focus();
                return;
            }
            else if (befestiger.Durchmesser == null)
            {
                await DisplayAlert("Achtung", "Bitte geben Sie eine Zahl zwischen 1 und 9999999999 ein.", "OK");
                Durchmesser_entry.Focus();
                return;
            }

            if (befestiger.ID_Befestiger == 0)
            {
                befestiger.ID_Befestiger = 1;
                BefestigerAdded?.Invoke(this, befestiger);
                await Navigation.PopAsync();
            }
            else
            {
                BefestigerUpdated?.Invoke(this, befestiger);
                await App.Database.UpdateFixAsync(befestiger);

            }
            await Navigation.PopAsync();
        }
    

        public async void BefestigerLöschen_Clicked(object sender, EventArgs e)
        {
            var befestiger = (BefestigerBasis)BindingContext;
            var answer = await DisplayAlert("Achtung!", "Befestiger wirklich löschen?", "Ja", "Nein");
            if (answer == true)
            {
                BefestigerRemoved?.Invoke(this, befestiger);
                await App.Database.DeleteFixItems(befestiger);
                await Navigation.PopAsync();
            }
        }

    }
}