using MFBauphysikMobilMAUI.Helpers;
using MFBauphysikMobilMAUI.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
    public partial class BasisDetailPage : ContentPage
    {
        public event EventHandler<Basis> BasisUpdated;
        public event EventHandler<Basis> BasisAdded;
        public event EventHandler<Basis> BasisRemoved;

        public BasisDetailPage(Basis bauteil)
        {
            if (bauteil == null)
                throw new ArgumentNullException(nameof(bauteil));

            InitializeComponent();
            BindingContext = new Basis
            {
                ID_Bauteil = bauteil.ID_Bauteil,
                ID_Sort = bauteil.ID_Sort,
                Bezeichnung = bauteil.Bezeichnung,
                Dicke = bauteil.Dicke,
                Wärmeleitfähigkeit = bauteil.Wärmeleitfähigkeit,
                Rohdichte = bauteil.Rohdichte,
                Kapillar = bauteil.Kapillar,
                Holz = bauteil.Holz,
                Holzwerkstoff = bauteil.Holzwerkstoff,
                sonstiges = bauteil.sonstiges,
                KeineLuft = bauteil.KeineLuft,
                EvntlLuft = bauteil.EvntlLuft,
                MitLuft = bauteil.MitLuft,
                Dampfdiffusionswiderstand_Min = bauteil.Dampfdiffusionswiderstand_Min,
                Dampfdiffusionswiderstand_Max = bauteil.Dampfdiffusionswiderstand_Max,
                Sd_Min = bauteil.Sd_Min,
                Sd_Max = bauteil.Sd_Max,
                Sd = bauteil.Sd,
                Tempverlauf = bauteil.Tempverlauf,
                Dampfteildruck = bauteil.Dampfteildruck,
                Dampfsättigungsdruck = bauteil.Dampfsättigungsdruck,
                TW = bauteil.TW,
                Fester_R = bauteil.Fester_R,
                Fester_sd = bauteil.Fester_sd,
                ModelID = bauteil.ModelID,
                DLR1 = bauteil.DLR1,
                DLR2 = bauteil.DLR2,
                DLR3 = bauteil.DLR3,
                DLR4 = bauteil.DLR4,
                DLR5 = bauteil.DLR5,
                LR1 = bauteil.LR1,
                LR2 = bauteil.LR2,
                LR3 = bauteil.LR3,
                LR4 = bauteil.LR4,
                LR5 = bauteil.LR5,
                Gewicht = bauteil.Gewicht,
                SizeClass = Setting.Size_Default,
            };
            if (bauteil.Fester_R == true)
            {
                Lambda_Entry.IsEnabled = false;
                Fester_R_Entry.IsEnabled = true;              
            }
            else
            {
                Lambda_Entry.IsEnabled = true;
                Fester_R_Entry.IsEnabled = false;
            }
           
            if (bauteil.Fester_sd == true)
            {
                Entry_diff_min.IsEnabled = false;
                Entry_diff_max.IsEnabled = false;
                Entry_sd_min.IsEnabled = true;
                Entry_sd_max.IsEnabled = true;
            }
            else
            {
                Entry_diff_min.IsEnabled = true;
                Entry_diff_max.IsEnabled = true;
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
            var bauteil = BindingContext as Basis;
            if (String.IsNullOrEmpty(bauteil.Bezeichnung))
            {
                await DisplayAlert("Achtung", "Bitte geben Sie eine Bezeichnung ein.", "OK");
                Entry_Bezeichnung.Focus();
                return;
            }
            if (bauteil.Dampfdiffusionswiderstand_Min == null)
            {
                await DisplayAlert("Achtung", "Bitte geben Sie eine Zahl zwischen 0,0001 und 99999999 ein.", "OK");
                Entry_diff_min.Focus();
                return;
            }
            else if (bauteil.Dampfdiffusionswiderstand_Max == null)
            {
                await DisplayAlert("Achtung", "Bitte geben Sie eine Zahl zwischen 0,0001 und 99999999 ein.", "OK");
                Entry_diff_max.Focus();
                return;
            }

            if (bauteil.ID_Bauteil == 0)
            {
                bauteil.ID_Bauteil = 1;
                BasisAdded?.Invoke(this, bauteil);
                await Navigation.PopAsync();
            }
            else
            {
                if (bauteil.DLR1 != 0 && bauteil.Dicke <= bauteil.DLR1/1000 )
                {
                    bauteil.Wärmeleitfähigkeit = bauteil.LR1;
                }
                else if (bauteil.DLR2 != 0 && bauteil.DLR1 / 1000 < bauteil.Dicke && bauteil.Dicke <= bauteil.DLR2 / 1000)
                {
                    bauteil.Wärmeleitfähigkeit = bauteil.LR2;
                }
                else if (bauteil.DLR3 != 0 && bauteil.DLR2 / 1000 < bauteil.Dicke && bauteil.Dicke <= bauteil.DLR3 / 1000)
                {
                    bauteil.Wärmeleitfähigkeit = bauteil.LR3;
                }
                else if (bauteil.DLR4 != 0 && bauteil.DLR3 / 1000 < bauteil.Dicke && bauteil.Dicke <= bauteil.DLR4 / 1000)
                {
                    bauteil.Wärmeleitfähigkeit = bauteil.LR4;
                }
                else if (bauteil.DLR5 != 0 && bauteil.DLR4 / 1000 < bauteil.Dicke && bauteil.Dicke <= bauteil.DLR5 / 1000)
                {
                    bauteil.Wärmeleitfähigkeit = bauteil.LR5;
                }
                BasisUpdated?.Invoke(this, bauteil);
                await App.Database.UpdateBauteilAsync(bauteil);
            }
            await Navigation.PopAsync();
        }

        private void LuftSpalte_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            RadioButton button = (sender as RadioButton)!;
        }

        private void Kapillar_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            RadioButton button = (sender as RadioButton)!;
        }

        private void CheckBox_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            CheckBox box = (sender as CheckBox)!;
        }

        private async void Löschen_Clicked(object sender, EventArgs e)
        {
            var bauteil = (Basis)BindingContext;
            var answer = await DisplayAlert("Achtung!", "Bauteil wirklich löschen?", "Ja", "Nein");
            if (answer == true)
            {
                BasisRemoved?.Invoke(this, bauteil);
                await App.Database.DeleteBauteilItems(bauteil);
                await Navigation.PopAsync();
            }
        }

        private void Fester_R_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            CheckBox box = (sender as CheckBox)!;
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
        private void Fester_sd_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            CheckBox box = (sender as CheckBox)!;
            if (box.IsChecked == true)
            {
                Entry_diff_min.IsEnabled = false;
                Entry_diff_max.IsEnabled = false;
                Entry_sd_min.IsEnabled = true;
                Entry_sd_max.IsEnabled = true;
            }
            else
            {
                Entry_diff_min.IsEnabled = true;
                Entry_diff_max.IsEnabled = true;
                Entry_sd_min.IsEnabled = false;
                Entry_sd_max.IsEnabled = false;
            }
        }

        private void Entry_TextChanged(object sender, TextChangedEventArgs e)
        {
            var bauteil = (BindingContext as Basis)!;
            //Lambda, Dicke, R
            if(bauteil.Fester_R == true)
            {
                if(bauteil.R == 0)
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
            if ( bauteil.Fester_sd == true)
            {
                if(bauteil.Dicke == 0)
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

    }    
}