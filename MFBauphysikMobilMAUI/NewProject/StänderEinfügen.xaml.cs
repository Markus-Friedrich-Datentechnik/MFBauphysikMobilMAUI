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
    public partial class StänderEinfügen : ContentPage
    {
        public event EventHandler<BefestigerStänder> BefestigerStänderUpdated;
        public event EventHandler<BefestigerStänder> BefestigerStänderAdded;
        public event EventHandler<BefestigerStänder> BefestigerStänderRemoved;

        public StänderEinfügen(BefestigerStänder befestigerStänder)
        {
            if (befestigerStänder == null)
                throw new ArgumentException(nameof(befestigerStänder));
            InitializeComponent();

            //Neuer Befestiger
            BindingContext = new BefestigerStänder
            {
                ID_Befestiger = befestigerStänder.ID_Befestiger,
                ModelID = befestigerStänder.ModelID,
                Anzahl = befestigerStänder.Anzahl,
                Wärmeleitfähigkeit_f = befestigerStänder.Wärmeleitfähigkeit_f,
                Durchmesser = befestigerStänder.Durchmesser,
                Eindringtiefe = befestigerStänder.Eindringtiefe,
                Länge = befestigerStänder.Länge,
                SizeClass = Setting.Size_Default,
            };            
        }

        private void Back_Clicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }

        private async void Check_Clicked(object sender, EventArgs e)
        {
            var befestiger = BindingContext as BefestigerStänder;
            //Fehlermeldung bei der Eingabe
            if (befestiger.Anzahl == 0 || string.IsNullOrEmpty(Anzahl_entry.Text))
            {
                await DisplayAlert("Achtung", "Bitte geben Sie eine Zahl zwischen 1 und 9999999999 ein.", "OK", " ");
                Anzahl_entry.Focus();
                return;
            }
            if (befestiger.Wärmeleitfähigkeit_f == null)
            {
                await DisplayAlert("Achtung", "Bitte geben Sie eine Zahl zwischen 1 und 9999999999 ein.", "OK", " ");
                Lambda_entry.Focus();
                return;
            }
            else if (befestiger.Durchmesser == null)
            {
                await DisplayAlert("Achtung", "Bitte geben Sie eine Zahl zwischen 1 und 9999999999 ein.", "OK", " ");
                Durchmesser_entry.Focus();
                return;
            }

            //Befestiger hinzufügen
            if (befestiger.ID_Befestiger == 0)
            {
                befestiger.ID_Befestiger = 1;
                BefestigerStänderAdded?.Invoke(this, befestiger);
                await Navigation.PopAsync();
            }
            //Befestiger Update
            else
            {
                BefestigerStänderUpdated?.Invoke(this, befestiger);
                await App.Database.UpdateFixStänderAsync(befestiger);
            }
            await Navigation.PopAsync();
        }
        public async void BefestigerLöschen_Clicked (object sender, EventArgs e) 
        {
            var befestiger = (BefestigerStänder)BindingContext;
            var answer = await DisplayAlert("Achtung!", "Befestiger wirklich löschen?", "Ja", "Nein");
            if (answer == true)
            {
                BefestigerStänderRemoved?.Invoke(this, befestiger);
                await App.Database.DeleteFixStänderItems(befestiger);
                await Navigation.PopAsync();
            }
        }
    }
}







