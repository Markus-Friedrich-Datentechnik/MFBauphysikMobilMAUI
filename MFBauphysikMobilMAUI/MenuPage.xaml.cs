using MFBauphysikMobilMAUI.Einstellungen;
using MFBauphysikMobilMAUI.Helpers;
using MFBauphysikMobilMAUI.Info;
using MFBauphysikMobilMAUI.Konfiguration;
using MFBauphysikMobilMAUI.Models;
using MFBauphysikMobilMAUI.Sortieren;
using MFBauphysikMobilMAUI.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Maui.Controls.Xaml;
using Microsoft.Maui.Controls;
using MFBauphysikMobilMAUI;

namespace MFBauphysikMobilMAUI
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MenuPage : ContentPage
    {
        ObservableCollection<EinstellungModel> _einstellung = new ObservableCollection<EinstellungModel> ();
        public ObservableCollection<EinstellungModel> Einstellung
        {
            get { return _einstellung;}
            set
            {
                _einstellung = value;
                OnPropertyChanged(nameof(Einstellung));
            }
        }
        private double _size_default;
        public double SizeDefault
        {
            get { return _size_default; }
            set
            {
                if (_size_default == value)
                    return;
                _size_default = value;
                OnPropertyChanged(nameof(SizeDefault));
            }
        }
        private double _size_medium;
        public double SizeMedium
        {
            get { return _size_medium; }
            set
            {
                if (_size_medium == value)
                    return;
                _size_medium = value;
                OnPropertyChanged(nameof(SizeMedium));
            }
        }
        private double _size_large;
        public double SizeLarge
        {
            get { return _size_large; }
            set
            {
                if (_size_large == value)
                    return;
                _size_large = value;
                OnPropertyChanged(nameof(SizeLarge));
            }
        }
        private double _size_micro;
        public double SizeMicro
        {
            get { return _size_micro; }
            set
            {
                if (_size_micro == value)
                    return;
                _size_micro = value;
                OnPropertyChanged(nameof(SizeMicro));
            }
        }
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
        public event EventHandler<EinstellungModel> MainPageUpdated;

        public MenuPage(EinstellungModel mainpagesetting)
        {
            InitializeComponent();

           BindingContext = new EinstellungModel
            {
                DefaultSize = mainpagesetting.DefaultSize,
                MicroSize = mainpagesetting.MicroSize,
                MediumSize = mainpagesetting.MediumSize,
                TitleSize = mainpagesetting.TitleSize,
                LargeSize = mainpagesetting.LargeSize,
                Alt_Jung = mainpagesetting.Alt_Jung,
                Jung_Alt = mainpagesetting.Jung_Alt,
                A_Z = mainpagesetting.A_Z,
                Z_A = mainpagesetting.Z_A,
            };  
            SizeDefault = Setting.Size_Default;
            SizeLarge = Setting.Size_Large;
            SizeMicro = Setting.Size_Micro;
            SizeMedium = Setting.Size_Medium;
            SizeTitle = Setting.Size_Title;
        }

        private void Back_Clicked(object sender, EventArgs e)
        {
            var setting = BindingContext as EinstellungModel;
            MainPageUpdated?.Invoke(this, setting);
            //Navigation.PushAsync(new MainPage());
            Navigation.PopAsync();

        }       
        private void Sortieren_Clicked(object sender, EventArgs e)
        {
            var sortieren = BindingContext as EinstellungModel;
            var vomPCUpdated = new SortierenPage(sortieren);
            vomPCUpdated.SortiertUpdated += (source, sort) =>
            {
                sortieren.DefaultSize = sort.DefaultSize;
                sortieren.MicroSize = sort.MicroSize;
                sortieren.MediumSize = sort.MediumSize;
                sortieren.TitleSize = sort.TitleSize;
                sortieren.LargeSize = sort.LargeSize;
                sortieren.Alt_Jung = sort.Alt_Jung;
                sortieren.Jung_Alt = sort.Jung_Alt;
                sortieren.A_Z = sort.A_Z;
                sortieren.Z_A = sort.Z_A;
            };
            Navigation.PushAsync(vomPCUpdated);
        }
        private void Konfiguration_Clicked(object sender, EventArgs e)
        {
            var konfiguration = BindingContext as EinstellungModel;
            var konfigUpdated = new KonfigurationPage(konfiguration);
            konfigUpdated.KonfigUpdated += (source, konfig) =>
            {
                konfiguration.DefaultSize = konfig.DefaultSize;
                konfiguration.MicroSize = konfig.MicroSize;
                konfiguration.MediumSize = konfig.MediumSize;
                konfiguration.TitleSize = konfig.TitleSize;
                konfiguration.LargeSize = konfig.LargeSize;
            };
            Navigation.PushAsync(konfigUpdated);
        }

        private void Info_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new InfoPage());
        }

        private  async void Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Test());
        }
    }
}