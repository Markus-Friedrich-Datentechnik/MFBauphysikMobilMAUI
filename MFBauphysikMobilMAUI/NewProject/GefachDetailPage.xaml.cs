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
    public partial class GefachDetailPage : ContentPage
    {
        public event EventHandler<Gefach> GefachUpdated;
        public event EventHandler<Gefach> GefachAdded;
        public event EventHandler<Gefach> GefachRemoved;
        public GefachDetailPage(Gefach gefach)
        {
           if (gefach == null)
                throw new ArgumentNullException(nameof(gefach));
            InitializeComponent();

            BindingContext = new Gefach
            {
                ID_Bauteil = gefach.ID_Bauteil,
                ID_Sort = gefach.ID_Sort,
                ModelID = gefach.ModelID,
                Bezeichnung = gefach.Bezeichnung,
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
                Gewicht = gefach.Gewicht,
                SizeClass = Setting.Size_Default,
            };
            if (gefach.Fester_R == true)
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
            if (gefach.Fester_sd == true)
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
            var gefach = BindingContext as Gefach;
            if (String.IsNullOrEmpty(gefach.Bezeichnung))
            {
                await DisplayAlert("Achtung", "Bitte geben Sie eine Bezeichnung ein.", "OK");
                Entry_Bezeichnung.Focus();
                return;
            }
            if (gefach.Dampfdiffusionswiderstand_Min == null)
            {
                await DisplayAlert("Achtung", "Bitte geben Sie eine Zahl zwischen 0,0001 und 99999999 ein.", "OK");
                Diff_Widerstand_Min.Focus();
                return;
            }
            else if (gefach.Dampfdiffusionswiderstand_Max == null)
            {
                await DisplayAlert("Achtung", "Bitte geben Sie eine Zahl zwischen 0,0001 und 99999999 ein.", "OK");
                Diff_Widerstand_Max.Focus();
                return;
            }
            if (gefach.ID_Bauteil == 0)
            {
                gefach.ID_Bauteil = 1;
                GefachAdded?.Invoke(this, gefach);
                await Navigation.PopAsync();
            }
            else
            {
                if (gefach.DLR1 != 0 && gefach.Dicke <= gefach.DLR1 / 1000)
                {
                    gefach.Wärmeleitfähigkeit = gefach.LR1;
                }
                else if (gefach.DLR2 != 0 && gefach.DLR1 / 1000 < gefach.Dicke && gefach.Dicke <= gefach.DLR2 / 1000)
                {
                    gefach.Wärmeleitfähigkeit = gefach.LR2;
                }
                else if (gefach.DLR3 != 0 && gefach.DLR2 / 1000 < gefach.Dicke && gefach.Dicke <= gefach.DLR3 / 1000)
                {
                    gefach.Wärmeleitfähigkeit = gefach.LR3;
                }
                else if (gefach.DLR4 != 0 && gefach.DLR3 / 1000 < gefach.Dicke && gefach.Dicke <= gefach.DLR4 / 1000)
                {
                    gefach.Wärmeleitfähigkeit = gefach.LR4;
                }
                else if (gefach.DLR5 != 0 && gefach.DLR4 / 1000 < gefach.Dicke && gefach.Dicke <= gefach.DLR5 / 1000)
                {
                    gefach.Wärmeleitfähigkeit = gefach.LR5;
                }
                GefachUpdated?.Invoke(this, gefach);
                await App.Database.UpdateBauteilGefachAsync(gefach);
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
            var gefach = BindingContext as Gefach;
            var answer = await DisplayAlert("Achtung!", "Bauteil wirklich löschen?", "Ja", "Nein");
            if (answer == true)
            {
                GefachRemoved?.Invoke(this, gefach);
                await App.Database.DeleteBauteilGefachItems(gefach);
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
            var bauteil = BindingContext as Gefach;
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