
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

namespace MFBauphysikMobilMAUI.Einstellungen
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EinstellungPage : ContentPage
    {
        public event EventHandler<EinstellungModel> EinstellungUpdated;
              
        public EinstellungPage(EinstellungModel einstellung)
        {
            if (einstellung == null)
                throw new ArgumentNullException(nameof(einstellung));
            //BindingContext = new MainPageModel();
            InitializeComponent();
            //Auswahl im Zeit-Tab
            BindingContext = new EinstellungModel
            {
                Alt_Jung = einstellung.Alt_Jung,
                Jung_Alt = einstellung.Jung_Alt,
                A_Z = einstellung.A_Z,
                Z_A = einstellung.Z_A,
            };           
        }

        private async void Back_Clicked(object sender, EventArgs e)
        {
            if (checkButton.IsVisible == true)
            {
                var answer = await DisplayAlert("Achtung!", "Änderungen wirklich verwerfen?", "Ja", "Nein");

                if (answer == true)
                {
                    await Navigation.PopAsync();
                }
            }
            else
            {
                await Navigation.PopAsync();
            }

        }

        //Auswahl Tab
       /* private void Zeit_Clicked(object sender, EventArgs e)
        {

            FrameZeit.IsVisible = true;
            DokumentvomPC.IsVisible = false;
            DokumentzumPC.IsVisible = false;
            FrameStammdaten.IsVisible = false;
            ZeitButton.TextDecorations = TextDecorations.Underline;
            ZeitButton.FontAttributes= FontAttributes.Bold;
            DokumentButton.TextDecorations = TextDecorations.None;
            DokumentButton.FontAttributes= FontAttributes.None;
            StammdatenButton.TextDecorations = TextDecorations.None;
            StammdatenButton.FontAttributes= FontAttributes.None;
        }

        private void Dokumente_Clicked(object sender, EventArgs e)
        {
            DokumentvomPC.IsVisible = true;
            DokumentzumPC.IsVisible= true;
            FrameZeit.IsVisible = false;
            FrameStammdaten.IsVisible = false;
            ZeitButton.TextDecorations = TextDecorations.None;
            ZeitButton.FontAttributes = FontAttributes.None;
            DokumentButton.TextDecorations = TextDecorations.Underline;
            DokumentButton.FontAttributes = FontAttributes.Bold;
            StammdatenButton.TextDecorations = TextDecorations.None;
            StammdatenButton.FontAttributes = FontAttributes.None;

            
        }

        private void Stammdaten_Clicked(object sender, EventArgs e)
        {
            FrameStammdaten.IsVisible = true;
            FrameZeit.IsVisible = false;
            DokumentvomPC.IsVisible = false;
            DokumentzumPC.IsVisible = false;
            ZeitButton.TextDecorations = TextDecorations.None;
            ZeitButton.FontAttributes = FontAttributes.None;
            DokumentButton.TextDecorations = TextDecorations.None;
            DokumentButton.FontAttributes = FontAttributes.None;
            StammdatenButton.TextDecorations = TextDecorations.Underline;
            StammdatenButton.FontAttributes = FontAttributes.Bold;
        }*/

        //Speichern von Änderungen
        private async void Check_Clicked(object sender, EventArgs e)
        {
            var einstellung = BindingContext as EinstellungModel;
            EinstellungUpdated?.Invoke(this, einstellung);
            await Navigation.PopAsync();
        }
        private void Selected_IndexChanged(object sender, EventArgs e)
        {
            if (checkButton.IsVisible == false)
            {
                checkButton.IsVisible = true;
            }
            
        }

        /*private void VomPC_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            RadioButton button = sender as RadioButton;
            
            //Dokumente = button.Value.ToString();
            /*var item = button.Value.ToString();


            if (item == "immer" || item == "wifi" || item == "nichtSyn")
            {
                checkButton.IsVisible = true;
            }
            
        }
        private void ZumPC_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            RadioButton button = sender as RadioButton;
            var zumPC = button.Value.ToString();

            if (zumPC == "immerzumPC" || zumPC == "nichtzumPC" || zumPC == "wifizumPC")
            {
                checkButton.IsVisible = true;
            }
        }
        private void Element_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            RadioButton button = sender as RadioButton;
            var element = button.Value.ToString();
            if (element == "immerElement" || element == "wifiElement" || element == "nichtElement")
            {
                checkButton.IsVisible = true;
            }
        }*/
                
        //Zeiteinstellung für die automatische Aktualisierung
        //Nach unten wischen
       /* private void Swiped_Down(object sender, SwipedEventArgs e)
        {
            string takt = text1.Text;
            switch (takt)
            {
                case "ausgeschaltet":
                    text1.Text = "alle 240 min";
                    text2.Text = "ausgeschaltet";
                    text3.Text = "alle 10 min";
                    break;
                case "alle 240 min":
                    text1.Text = "alle 180 min";
                    text2.Text = "alee 240 min";
                    text3.Text = "ausgeschaltet";
                    break;
                case "alle 180 min":
                    text1.Text = "alle 120 min";
                    text2.Text = "alee 180 min";
                    text3.Text = "alle 240 min";
                    break;
                case "alle 120 min":
                    text1.Text = "alle 60 min";
                    text2.Text = "alee 120 min";
                    text3.Text = "alle 180 min";
                    break;
                case "alle 60 min":
                    text1.Text = "alle 30 min";
                    text2.Text = "alee 60 min";
                    text3.Text = "alle 120 min";
                    break;
                case "alle 30 min":
                    text1.Text = "alle 15 min";
                    text2.Text = "alee 30 min";
                    text3.Text = "alle 60 min";
                    break;
                case "alle 15 min":
                    text1.Text = "alle 10 min";
                    text2.Text = "alee 15 min";
                    text3.Text = "alle 30 min";
                    break;
                case "alle 10 min":
                    text1.Text = "ausgeschaltet";
                    text2.Text = "alee 10 min";
                    text3.Text = "alle 15 min";
                    break;
            }
        }*/

        //Nach oben wischen
       /* private void Swiped_Up(object sender, SwipedEventArgs e)
        {
            string takt = text1.Text;
            switch (takt)
            {
                case "ausgeschaltet":
                    text1.Text = "alle 10 min";
                    text2.Text = "alle 15 min";
                    text3.Text = "alle 30 min";
                    break;
                case "alle 10 min":
                    text1.Text = "alle 15 min";
                    text2.Text = "alee 30 min";
                    text3.Text = "alle 60 min";
                    break;
                case "alle 15 min":
                    text1.Text = "alle 30 min";
                    text2.Text = "alee 60 min";
                    text3.Text = "alle 120 min";
                    break;
                case "alle 30 min":
                    text1.Text = "alle 60 min";
                    text2.Text = "alee 120 min";
                    text3.Text = "alle 180 min";
                    break;
                case "alle 60 min":
                    text1.Text = "alle 120 min";
                    text2.Text = "alee 180 min";
                    text3.Text = "alle 240 min";
                    break;
                case "alle 120 min":
                    text1.Text = "alle 180 min";
                    text2.Text = "alee 240 min";
                    text3.Text = "ausgeschaltet";
                    break;
                case "alle 180 min":
                    text1.Text = "alle 240 min";
                    text2.Text = "ausgeschaltet";
                    text3.Text = "alle 10 min";
                    break;
                case "alle 240 min":
                    text1.Text = "ausgeschaltet";
                    text2.Text = "alle 10 min";
                    text3.Text = "alle 15 min";
                    break;
            }
        }*/
    }
}