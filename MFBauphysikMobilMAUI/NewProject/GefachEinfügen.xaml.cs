using MFBauphysikMobilMAUI.Helpers;
using MFBauphysikMobilMAUI.Models;
using MFBauphysikMobilMAUI.ViewModels;
using System;
using System.Collections.Generic;
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
    public partial class GefachEinfügen : ContentPage
    {
        public event EventHandler<BefestigerGefach> BefestigerGefachUpdated;
        public event EventHandler<BefestigerGefach> BefestigerGefachAdded;
        public event EventHandler<BefestigerGefach> BefestigerGefachRemoved;
        public GefachEinfügen(BefestigerGefach befestigerGefach)
        {
            if (befestigerGefach == null)
                throw new ArgumentException(nameof (befestigerGefach));
            InitializeComponent();

            //Neuer Befestiger
            BindingContext = new BefestigerGefach
            {
                ID_Befestiger = befestigerGefach.ID_Befestiger,
                ModelID = befestigerGefach.ModelID,
                Anzahl = befestigerGefach.Anzahl,
                Wärmeleitfähigkeit_f = befestigerGefach.Wärmeleitfähigkeit_f,
                Durchmesser = befestigerGefach.Durchmesser,
                Eindringtiefe = befestigerGefach.Eindringtiefe,
                Länge = befestigerGefach.Länge,
                SizeClass = Setting.Size_Default,
            };
        }

        private void Back_Clicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();

        }
        private async void Check_Clicked(object sender, EventArgs e)
        {
            var befestiger = BindingContext as BefestigerGefach;
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

            if (befestiger.ID_Befestiger == 0)
            {
                befestiger.ID_Befestiger = 1;
                BefestigerGefachAdded?.Invoke(this, befestiger);
                await Navigation.PopAsync();
            }
            else
            {
                BefestigerGefachUpdated?.Invoke(this, befestiger);
                await App.Database.UpdateFixGefachAsync(befestiger);
            }
            await Navigation.PopAsync();
        }

        public async void BefestigerLöschen_Clicked(object sender, EventArgs e) 
        {
            var befestiger = BindingContext as BefestigerGefach;
            var answer = await DisplayAlert("Achtung!", "Befestiger wirklich löschen?", "Ja", "Nein");
            if (answer == true)
            {
                BefestigerGefachRemoved?.Invoke(this, befestiger);
                await App.Database.DeleteFixGefachItems(befestiger);
                await Navigation.PopAsync();
            }
        }
    }
}