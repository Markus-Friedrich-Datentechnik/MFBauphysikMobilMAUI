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

namespace MFBauphysikMobilMAUI.NewProject
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class KlimadatenPage : ContentPage
    {
       
        public KlimadatenPage()
        {
            InitializeComponent();
            BindingContext = new KlimadatenClass
            {
                //Tauperiode
                TauDauer = 2160,
                InnenTemp = 20,
                InnenFeuchte = 50,

                AußenTemp = -5,
                AußenFeuchte = 80,

                //Verdunstungsperiode
                VerdunstungsDauer = 2160,
                InnenDruckVerdunstung = 1200,
                AußenDruckVerdunstung = 1200,

                Wände = 1700,
                Dächer = 2000,

                //Wasserdampfteildruck Tauperiode
                InnenWasserdampfdruck = 1168,
                AußenWasserdampfdruck = 321,

                SizeDefault = Setting.Size_Default,
                SizeTitle = Setting.Size_Title,
                
            };
        }

        private async void Back_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}