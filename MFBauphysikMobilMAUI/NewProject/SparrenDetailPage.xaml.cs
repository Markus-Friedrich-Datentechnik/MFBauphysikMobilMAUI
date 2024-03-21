using MFBauphysikMobilMAUI.Helpers;
using MFBauphysikMobilMAUI.Models;
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
    public partial class SparrenDetailPage : ContentPage
    {
        public event EventHandler<Sparren> SparrenUpdated;
        public event EventHandler<Sparren> SparrenAdded;
        public event EventHandler<Sparren> SparrenRemoved;
        public SparrenDetailPage(Sparren sparren)
        {
            if (sparren  == null)
                throw new ArgumentNullException(nameof(sparren));
            InitializeComponent();

            BindingContext = new Sparren
            {
                ID_Bauteil = sparren.ID_Bauteil,
                ID_Sort = sparren.ID_Sort,
                ModelID = sparren.ModelID,
                Bezeichnung = sparren.Bezeichnung,
                Dicke = sparren.Dicke,
                Wärmeleitfähigkeit = sparren.Wärmeleitfähigkeit,
                Rohdichte = sparren.Rohdichte,
                Kapillar = sparren.Kapillar,
                Holz = sparren.Holz,
                Holzwerkstoff = sparren.Holzwerkstoff,
                sonstiges = sparren.sonstiges,
                KeineLuft = sparren.KeineLuft,
                EvntlLuft = sparren.EvntlLuft,
                MitLuft = sparren.MitLuft,
                Dampfdiffusionswiderstand_Min = sparren.Dampfdiffusionswiderstand_Min,
                Dampfdiffusionswiderstand_Max = sparren.Dampfdiffusionswiderstand_Max,
                Sd_Min = sparren.Sd_Min,
                Sd_Max = sparren.Sd_Max,
                Sd = sparren.Sd,
                Tempverlauf = sparren.Tempverlauf,
                Dampfteildruck = sparren.Dampfteildruck,
                Dampfsättigungsdruck = sparren.Dampfsättigungsdruck,
                TW = sparren.TW,
                Fester_R = sparren.Fester_R,
                Fester_sd = sparren.Fester_sd,
                DLR1 = sparren.DLR1,
                DLR2 = sparren.DLR2,
                DLR3 = sparren.DLR3,
                DLR4 = sparren.DLR4,
                DLR5 = sparren.DLR5,
                LR1 = sparren.LR1,
                LR2 = sparren.LR2,
                LR3 = sparren.LR3,
                LR4 = sparren.LR4,
                LR5 = sparren.LR5,
                Gewicht = sparren.Gewicht,
                SizeClass = Setting.Size_Default,
            };
            if (sparren.Fester_R == true)
            {
                Lambda_Entry.IsEnabled = false;
                Fester_R_Entry.IsEnabled = true;
            }
            else
            {
                Lambda_Entry.IsEnabled = true;
                Fester_R_Entry.IsEnabled = false;
            }

            if (Entry_Bezeichnung.Text == null)
            {
                ButtonLöschen.IsVisible = false;
            }
            else
            {
                ButtonLöschen.IsVisible = true;
            }
            if (sparren.Fester_sd == true)
            {
                Diff_Widerstand_Min.IsEnabled = false;
                Diff_Widerstand_Max.IsEnabled = false;
                Entry_sd_min.IsEnabled = true;
                Entry_sd_max.IsEnabled = true;
            }
            else
            {
                Diff_Widerstand_Min.IsEnabled = true;
                Diff_Widerstand_Max.IsEnabled = true;
                Entry_sd_min.IsEnabled = false;
                Entry_sd_max.IsEnabled = false;
            }
            evntlluft_label.FontSize = Setting.Size_Default;
            ohneluft_label.FontSize = Setting.Size_Default; ;
            mitluft_label.FontSize = Setting.Size_Default; ;

            holz_label.FontSize = Setting.Size_Default; ;
            holzwerkstoff_label.FontSize = Setting.Size_Default; ;
            sonstiges_label.FontSize = Setting.Size_Default;

            ButtonLöschen.FontSize = Setting.Size_Default;
        }

        public async void Back_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
        public async void Check_Clicked(object sender, EventArgs e)
        {
            var sparren = BindingContext as Sparren;
            if (String.IsNullOrEmpty(sparren.Bezeichnung))
            {
                await DisplayAlert("Achtung", "Bitte geben Sie eine Bezeichnung ein.", "OK");
                Entry_Bezeichnung.Focus();
                return;
            }
            if (sparren.Dampfdiffusionswiderstand_Min == null)
            {
                await DisplayAlert("Achtung", "Bitte geben Sie eine Zahl zwischen 0,0001 und 99999999 ein.", "OK");
                Diff_Widerstand_Min.Focus();
                return;
            }
            else if (sparren.Dampfdiffusionswiderstand_Max == null)
            {
                await DisplayAlert("Achtung", "Bitte geben Sie eine Zahl zwischen 0,0001 und 99999999 ein.", "OK");
                Diff_Widerstand_Max.Focus();
                return;
            }
            if (sparren.ID_Bauteil == 0)
            {
                sparren.ID_Bauteil = 1;
                SparrenAdded?.Invoke(this, sparren);
                await Navigation.PopAsync();
            }
            else
            {
                if (sparren.DLR1 != 0 && sparren.Dicke <= sparren.DLR1 / 1000)
                {
                    sparren.Wärmeleitfähigkeit = sparren.LR1;
                }
                else if (sparren.DLR2 != 0 && sparren.DLR1 / 1000 < sparren.Dicke && sparren.Dicke <= sparren.DLR2 / 1000)
                {
                    sparren.Wärmeleitfähigkeit = sparren.LR2;
                }
                else if (sparren.DLR3 != 0 && sparren.DLR2 / 1000 < sparren.Dicke && sparren.Dicke <= sparren.DLR3 / 1000)
                {
                    sparren.Wärmeleitfähigkeit = sparren.LR3;
                }
                else if (sparren.DLR4 != 0 && sparren.DLR3 / 1000 < sparren.Dicke && sparren.Dicke <= sparren.DLR4 / 1000)
                {
                    sparren.Wärmeleitfähigkeit = sparren.LR4;
                }
                else if (sparren.DLR5 != 0 && sparren.DLR4 / 1000 < sparren.Dicke && sparren.Dicke <= sparren.DLR5 / 1000)
                {
                    sparren.Wärmeleitfähigkeit = sparren.LR5;
                }
                SparrenUpdated?.Invoke(this, sparren);
                await App.Database.UpdateBauteilSparrenAsync(sparren);  
            }
            await Navigation.PopAsync();
        }
        private void LuftSpalte_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            RadioButton button = sender as RadioButton;
            //animalLabel.Text = $"You have chosen: {button.Value}";
        }

        private void Kapillar_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            RadioButton button = sender as RadioButton;
        }

        private void CheckBox_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            CheckBox box = sender as CheckBox;
        }

        private async void Löschen_Clicked(object sender, EventArgs e)
        {
            var sparren = (Sparren)BindingContext;
            var answer = await DisplayAlert("Achtung!", "Bauteil wirklich löschen?", "Ja", "Nein");
            if (answer == true)
            {
                SparrenRemoved?.Invoke(this, sparren);
                await App.Database.DeleteBauteilSparrenItems(sparren);
                await Navigation.PopAsync();
            }
        }
        private void Fester_R_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            CheckBox box = sender as CheckBox;
            if (box.IsChecked == true)
            {
                Lambda_Entry.IsEnabled = false;
                Fester_R_Entry.IsEnabled = true;
            }
            else
            {
                Lambda_Entry.IsEnabled = true;
                Fester_R_Entry.IsEnabled = false;
            }
        }

        private void Entry_TextChanged(object sender, TextChangedEventArgs e)
        {
            var bauteil = BindingContext as Sparren;
            //Lambda, Dicke, R
            if (bauteil.Fester_R == true)
            {
                if (bauteil.R == 0)
                {
                    bauteil.Wärmeleitfähigkeit = 0;
                }
                else
                {
                    bauteil.Wärmeleitfähigkeit = bauteil.Dicke / bauteil.R;
                }
            }
            else
            {
                if (bauteil.Wärmeleitfähigkeit == 0)
                {
                    bauteil.R = 0;
                }
                else
                {
                    bauteil.R = bauteil.Dicke / bauteil.Wärmeleitfähigkeit;
                }
            }

            //Mü, Sd
            if (bauteil.Fester_sd == true)
            {
                if (bauteil.Dicke == 0)
                {
                    bauteil.Dampfdiffusionswiderstand_Min = 0;
                    bauteil.Dampfdiffusionswiderstand_Max = 0;
                    bauteil.Sd_Min = bauteil.Sd_Min;
                    bauteil.Sd_Max = bauteil.Sd_Max;
                }
                else
                {
                    bauteil.Dampfdiffusionswiderstand_Min = bauteil.Sd_Min / bauteil.Dicke;
                    bauteil.Dampfdiffusionswiderstand_Max = bauteil.Sd_Max / bauteil.Dicke;
                }
            }
            else
            {
                bauteil.Sd_Min = bauteil.Dampfdiffusionswiderstand_Min * bauteil.Dicke;
                bauteil.Sd_Max = bauteil.Dampfdiffusionswiderstand_Max * bauteil.Dicke;
            }

            //Gewicht
            bauteil.Gewicht = bauteil.Dicke * bauteil.Rohdichte;
        }
        private void Fester_sd_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            CheckBox box = sender as CheckBox;
            if (box.IsChecked == true)
            {
                Diff_Widerstand_Min.IsEnabled = false;
                Diff_Widerstand_Max.IsEnabled = false;
                Entry_sd_min.IsEnabled = true;
                Entry_sd_max.IsEnabled = true;
            }
            else
            {
                Diff_Widerstand_Min.IsEnabled = true;
                Diff_Widerstand_Max.IsEnabled = true;
                Entry_sd_min.IsEnabled = false;
                Entry_sd_max.IsEnabled = false;
            }
        }
    }
}