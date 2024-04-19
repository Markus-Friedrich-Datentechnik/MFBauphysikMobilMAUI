using MFBauphysikMobilMAUI.Helpers;
using MFBauphysikMobilMAUI.Models;
using MFBauphysikMobilMAUI.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Maui.Controls.Xaml;
using Microsoft.Maui.Controls;
using Microsoft.Maui;

namespace MFBauphysikMobilMAUI.Konfiguration
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class KonfigurationPage : ContentPage, INotifyPropertyChanged
    {
        public event EventHandler<EinstellungModel> KonfigUpdated;
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
        public double Default_OldValue {  get; set; }
        public double Large_OldValue { get; set; }
        public double Medium_OldValue { get; set; }
        public double Micro_OldValue { get; set; }
        public double Title_OldValue { get; set; }
        public string Mode { get; set; }
        public KonfigurationPage(EinstellungModel konfig)
        {
            if (konfig == null) 
                throw new ArgumentNullException(nameof(konfig));
            InitializeComponent();
            BindingContext = new EinstellungModel
            {
                DefaultSize = konfig.DefaultSize,
                MicroSize = konfig.MicroSize,
                MediumSize = konfig.MediumSize,
                TitleSize = konfig.TitleSize,
                LargeSize = konfig.LargeSize,
            };
            Default_OldValue = konfig.DefaultSize;
            Large_OldValue = konfig.LargeSize;
            Medium_OldValue = konfig.MediumSize;
            Micro_OldValue = konfig.MicroSize;
            Title_OldValue = konfig.TitleSize;

            slider.Value = (konfig.DefaultSize / 14) * 100;

            design_label.FontSize = Setting.Size_Default;
            gerät_label.FontSize = Setting.Size_Default;
            hell_label.FontSize = Setting.Size_Default;
            dark_label.FontSize = Setting.Size_Default;
            darstellung_label.FontSize = Setting.Size_Default;
            slider_label.FontSize = Setting.Size_Default;
            SizeTitle = Setting.Size_Title;
            switch (Setting.Theme)
            {
                case 0:
                    gerätModus.IsChecked = true; break;
                case 1:
                    lightmodus.IsChecked = true; break;
                case 2:
                    darkmodus.IsChecked= true; break;
            }
            Mode = App.Current.UserAppTheme.ToString();
        }


        private async void Back_Clicked(object sender, EventArgs e)
        {
            var answer = await DisplayAlert("Achtung", "Änderungen wirklich verwerfen?", "Ja", "Nein");
            if(answer == true)
            {
                var konfig = (BindingContext as EinstellungModel)!;
                konfig.DefaultSize = Default_OldValue;
                konfig.LargeSize = Large_OldValue;
                konfig.MediumSize = Medium_OldValue;
                konfig.MicroSize = Micro_OldValue;
                konfig.TitleSize = Title_OldValue;
                Setting.Size_Default = Default_OldValue;
                Setting.Size_Large = Large_OldValue;
                Setting.Size_Medium = Medium_OldValue;
                Setting.Size_Micro = Micro_OldValue;
                Setting.Size_Title = Title_OldValue;
                KonfigUpdated?.Invoke(this, konfig);
                if (Mode == "Light")
                {
                    lightmodus.IsChecked = true;
                }
                else if (Mode == "Dark")
                {
                    darkmodus.IsChecked = true;
                }
                else
                {
                    gerätModus.IsChecked = true;
                }
                await Navigation.PopAsync();
            }
        }
        bool loaded;
        protected override void OnAppearing()
        {
            base.OnAppearing();
            loaded = true;
        }
        private void Modus_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            /* RadioButton button = sender as RadioButton;
             var mod = button.Value.ToString();
             if (mod == "hell")
             {
                 App.Current.UserAppTheme = OSAppTheme.Light;
             }
             else if (mod == "dunkel")
             {
                 App.Current.UserAppTheme= OSAppTheme.Dark;
             }            
             var konfig = BindingContext as EinstellungModel;
             KonfigUpdated?.Invoke(this, konfig);*/
            if (!loaded)
                return;

            if (!e.Value)
                return;

            var val = (sender as RadioButton)?.Value as string;
            if (string.IsNullOrWhiteSpace(val))
                return;
            switch (val)
            {
                case "system":
                    Setting.Theme = 0;
                    break;
                case "hell":
                    Setting.Theme = 1;
                    break;
                case "dunkel":
                    Setting.Theme = 2;
                    break;

            }
            TheTheme.SetTheme();
        }
        private void slider_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            var konfig = BindingContext as EinstellungModel;
            double value = e.NewValue;
            konfig.DefaultSize = 14 * (value / 100);
            konfig.MicroSize = 9.8 * (value / 100);
            konfig.MediumSize = 16.8 * (value / 100);
            konfig.TitleSize = 23.8 * (value / 100);
            konfig.LargeSize = 21 * (value / 100);
            Textbeispiel.FontSize = konfig.DefaultSize;
            CheckButton.IsVisible = true;
            KonfigUpdated?.Invoke(this, konfig);
        }

        private void S50_Selected(object sender, EventArgs e)
        {
            slider.Value = 50;             
            var konfig = BindingContext as EinstellungModel;
            konfig.DefaultSize = 14 * 0.5;
            konfig.MicroSize = 9.8 * 0.5;
            konfig.MediumSize = 16.8 * 0.5;
            konfig.TitleSize = 23.8 * 0.5;
            konfig.LargeSize = 21 * 0.5;
            Textbeispiel.FontSize = konfig.DefaultSize;
            KonfigUpdated?.Invoke(this, konfig);
        }
        private void S75_Selected(object sender, EventArgs e)
        {
            slider.Value = 75;
            var konfig = BindingContext as EinstellungModel;
            konfig.DefaultSize = 14 * 0.75;
            konfig.MicroSize = 9.8 * 0.75;
            konfig.MediumSize = 16.8 * 0.75;
            konfig.TitleSize = 23.8 * 0.75;
            konfig.LargeSize = 21 * 0.75;
            Textbeispiel.FontSize = konfig.DefaultSize;
            KonfigUpdated?.Invoke(this, konfig);
        }
        private void S100_Selected(object sender, EventArgs e)
        {
            slider.Value = 100;
            var konfig = BindingContext as EinstellungModel;
            konfig.DefaultSize = 14 ;
            konfig.MicroSize = 9.8 ;
            konfig.MediumSize = 16.8;
            konfig.TitleSize = 23.8;
            konfig.LargeSize = 21;
            Textbeispiel.FontSize = konfig.DefaultSize;
            KonfigUpdated?.Invoke(this, konfig);
        }
        private void S125_Selected(object sender, EventArgs e)
        {
            slider.Value = 125;
            var konfig = BindingContext as EinstellungModel;
            konfig.DefaultSize = 14 * 1.25;
            konfig.MicroSize = 9.8 * 1.25;
            konfig.MediumSize = 16.8 * 1.25;
            konfig.TitleSize = 23.8 * 1.25;
            konfig.LargeSize = 21 * 1.25;
            Textbeispiel.FontSize = konfig.DefaultSize;
            KonfigUpdated?.Invoke(this, konfig);
        }
        private void S150_Selected(object sender, EventArgs e)
        {
            slider.Value = 150;
            var konfig = BindingContext as EinstellungModel;
            konfig.DefaultSize = 14 * 1.5;
            konfig.MicroSize = 9.8 * 1.5;
            konfig.MediumSize = 16.8 * 1.5;
            konfig.TitleSize = 23.8 * 1.5;
            konfig.LargeSize = 21 * 1.5;
            Textbeispiel.FontSize = konfig.DefaultSize;
            KonfigUpdated?.Invoke(this, konfig);
        }

        private async void Checked_Clicked(object sender, EventArgs e)
        {
            var konfig = BindingContext as EinstellungModel;
            Setting.Size_Default = konfig.DefaultSize;
            Setting.Size_Medium = konfig.MediumSize;
            Setting.Size_Micro  = konfig.MicroSize;
            Setting.Size_Large = konfig.LargeSize;
            Setting.Size_Title = konfig.TitleSize;
            KonfigUpdated?.Invoke(this, konfig);
            await Navigation.PopAsync();
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            CheckButton.IsVisible = true;
        }
    }
}