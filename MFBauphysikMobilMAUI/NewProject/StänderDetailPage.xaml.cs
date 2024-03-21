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
    public partial class StänderDetailPage : ContentPage
    {
        public event EventHandler<Ständer> StänderUpdated;
        public event EventHandler<Ständer> StänderAdded;
        public event EventHandler<Ständer> StänderRemoved;
        public StänderDetailPage(Ständer ständer)
        {
            if (ständer == null)
                throw new ArgumentNullException(nameof(ständer));
            InitializeComponent();

            BindingContext = new Ständer
            {
                ID_Bauteil = ständer.ID_Bauteil,
                ID_Sort = ständer.ID_Sort,
                ModelID = ständer.ModelID,
                Bezeichnung = ständer.Bezeichnung,
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
                Gewicht = ständer.Gewicht,
                SizeClass = Setting.Size_Default,
            };
            if (ständer.Fester_R == true)
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
            if (ständer.Fester_sd == true)
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
            var ständer = BindingContext as Ständer;
            if (String.IsNullOrEmpty(ständer.Bezeichnung))
            {
                await DisplayAlert("Achtung", "Bitte geben Sie eine Bezeichnung ein.", "OK");
                Entry_Bezeichnung.Focus();
                return;
            }
            if (ständer.Dampfdiffusionswiderstand_Min == null)
            {
                await DisplayAlert("Achtung", "Bitte geben Sie eine Zahl zwischen 0,0001 und 99999999 ein.", "OK");
                Diff_Widerstand_Min.Focus();
                return;
            }
            else if (ständer.Dampfdiffusionswiderstand_Max == null)
            {
                await DisplayAlert("Achtung", "Bitte geben Sie eine Zahl zwischen 0,0001 und 99999999 ein.", "OK");
                Diff_Widerstand_Max.Focus();
                return;
            }
            if (ständer.ID_Bauteil == 0)
            {
                ständer.ID_Bauteil = 1;
                StänderAdded?.Invoke(this, ständer);
                await Navigation.PopAsync();
            }
            else
            {
                if (ständer.DLR1 != 0 && ständer.Dicke <= ständer.DLR1 / 1000)
                {
                    ständer.Wärmeleitfähigkeit = ständer.LR1;
                }
                else if (ständer.DLR2 != 0 && ständer.DLR1 / 1000 < ständer.Dicke && ständer.Dicke <= ständer.DLR2 / 1000)
                {
                    ständer.Wärmeleitfähigkeit = ständer.LR2;
                }
                else if (ständer.DLR3 != 0 && ständer.DLR2 / 1000 < ständer.Dicke && ständer.Dicke <= ständer.DLR3 / 1000)
                {
                    ständer.Wärmeleitfähigkeit = ständer.LR3;
                }
                else if (ständer.DLR4 != 0 && ständer.DLR3 / 1000 < ständer.Dicke && ständer.Dicke <= ständer.DLR4 / 1000)
                {
                    ständer.Wärmeleitfähigkeit = ständer.LR4;
                }
                else if (ständer.DLR5 != 0 && ständer.DLR4 / 1000 < ständer.Dicke && ständer.Dicke <= ständer.DLR5 / 1000)
                {
                    ständer.Wärmeleitfähigkeit = ständer.LR5;
                }
                StänderUpdated?.Invoke(this, ständer);
                await App.Database.UpdateBauteilStänderAsync(ständer);
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
            var ständer = BindingContext as Ständer;
            var answer = await DisplayAlert("Achtung!", "Bauteil wirklich löschen?", "Ja", "Nein");
            if (answer == true)
            {
                StänderRemoved?.Invoke(this, ständer);
                await App.Database.DeleteBauteilStänderItems(ständer);
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
            var bauteil = BindingContext as Ständer;
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