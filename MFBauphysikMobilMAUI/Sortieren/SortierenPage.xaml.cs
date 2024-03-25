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
using MFBauphysikMobilMAUI.Helpers;

namespace MFBauphysikMobilMAUI.Sortieren
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SortierenPage : ContentPage
    {
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
        public event EventHandler<EinstellungModel> SortiertUpdated;
        public SortierenPage(EinstellungModel sortieren)
        {
            if (sortieren == null)
                throw new ArgumentNullException(nameof(sortieren));
            InitializeComponent();
            BindingContext = new EinstellungModel
            {
                DefaultSize = sortieren.DefaultSize,
                MicroSize = sortieren.MicroSize,
                MediumSize = sortieren.MediumSize,
                TitleSize = sortieren.TitleSize,
                LargeSize = sortieren.LargeSize,
                Alt_Jung = sortieren.Alt_Jung,
                Jung_Alt = sortieren.Jung_Alt,
                A_Z = sortieren.A_Z,
                Z_A = sortieren.Z_A,
            };
            label_neueste.FontSize = sortieren.DefaultSize;
            label_älteste.FontSize = sortieren.DefaultSize;
            label_az.FontSize = sortieren.DefaultSize;
            label_za.FontSize = sortieren.DefaultSize;

            neueste.FontSize = sortieren.DefaultSize;
            älteste.FontSize = sortieren.DefaultSize;
            a_z.FontSize = sortieren.DefaultSize;
            z_a.FontSize = sortieren.DefaultSize;
            SizeTitle = Setting.Size_Title;

        }

        public async void Back_Clicked (object sender, EventArgs e)
        {
            var sortieren = BindingContext as EinstellungModel;
            SortiertUpdated?.Invoke(this, sortieren);
            await Navigation.PopAsync();
        }

        private void RadioButton_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            RadioButton button = sender as RadioButton;
        }

        private async void Check_Clicked(object sender, EventArgs e)
        {
            var sortieren = BindingContext as EinstellungModel;
            SortiertUpdated?.Invoke(this, sortieren);
            await Navigation.PopToRootAsync();
        }
    }


}