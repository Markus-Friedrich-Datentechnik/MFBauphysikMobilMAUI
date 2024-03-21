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
    public partial class SparrenEinfügen : ContentPage
    {
        public event EventHandler<BefestigerSparren> BefestigerSparrenUpdated;
        public event EventHandler<BefestigerSparren> BefestigerSparrenAdded;
        public event EventHandler<BefestigerSparren> BefestigerSparrenRemoved;
        public SparrenEinfügen(BefestigerSparren befestigerSparren)
        {
            if (befestigerSparren == null)
                throw new ArgumentException(nameof (befestigerSparren));
            InitializeComponent();

            //Neues Befestiger
            BindingContext = new BefestigerSparren
            {
                ID_Befestiger = befestigerSparren.ID_Befestiger,
                ModelID = befestigerSparren.ModelID,
                Anzahl = befestigerSparren.Anzahl,
                Wärmeleitfähigkeit_f = befestigerSparren.Wärmeleitfähigkeit_f,
                Durchmesser = befestigerSparren.Durchmesser,
                Eindringtiefe = befestigerSparren.Eindringtiefe,
                Länge = befestigerSparren.Länge,
                SizeClass = Setting.Size_Default,
            };
        }
        private void Back_Clicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }

        private async void Check_Clicked(object sender, EventArgs e)
        {
            var befestiger = BindingContext as BefestigerSparren;
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
                BefestigerSparrenAdded?.Invoke(this, befestiger);
                await Navigation.PopAsync();
            }
            else
            {
                BefestigerSparrenUpdated?.Invoke(this, befestiger);
                await App.Database.UpdateFixSparrenAsync(befestiger);
            }
            await Navigation.PopAsync();

        }

        public async void BefestigerLöschen_Clicked(object sender, EventArgs e) 
        {
            var befestiger = (BefestigerSparren)BindingContext;
            var answer = await DisplayAlert("Achtung!", "Befestiger wirklich löschen?", "Ja", "Nein");
            if (answer == true)
            {
                BefestigerSparrenRemoved?.Invoke(this, befestiger);
                await App.Database.DeleteFixSparrenItems(befestiger);
                await Navigation.PopAsync();
            }
        }
    }
}
