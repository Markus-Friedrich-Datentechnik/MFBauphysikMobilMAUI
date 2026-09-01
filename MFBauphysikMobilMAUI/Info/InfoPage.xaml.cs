using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.Maui.Controls.Xaml;
using System.Runtime.CompilerServices;
using MFBauphysikMobilMAUI.Models;
using MFBauphysikMobilMAUI.Helpers;
using Microsoft.Maui.Controls;
using Microsoft.Maui;
using Microsoft.Maui.Controls.PlatformConfiguration;

namespace MFBauphysikMobilMAUI.Info

{
    
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class InfoPage : ContentPage
    {
        

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
      
        public InfoPage()
        {
            InitializeComponent();
            BindingContext = this;
            if (DeviceInfo.Platform == DevicePlatform.iOS)
            {
                Plattform.Text = "iOS";
            }
            else if (DeviceInfo.Platform == DevicePlatform.Android)
            {
                Plattform.Text = "Android";
            }
            SizeDefault = Setting.Size_Default;
            SizeLarge = Setting.Size_Large;
            SizeMicro = Setting.Size_Micro;
            SizeMedium = Setting.Size_Medium;
            SizeTitle = Setting.Size_Title;

            ProgrammInfoLabel.Text = $"MFBauphysik mobil {AppInfo.Current.VersionString}\n" +
                // $"Datum: {AppVersionInfo.ReleaseDate}\n" +
                $"(C)opyright: 2017 - 2026";

        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }
        private async void Back_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        private void Info_Clicked(object sender, EventArgs e)
        {
            InfoTab.IsVisible = true;
            DatenschutzTab.IsVisible = false;
            InfoButton.FontAttributes= FontAttributes.Bold;
            DatenschutzButton.FontAttributes = FontAttributes.None;
        }

        private void Datenschutz_Clicked(object sender, EventArgs e)
        {
            InfoTab.IsVisible = false;
            DatenschutzTab.IsVisible= true;
            InfoButton.FontAttributes = FontAttributes.None;
            DatenschutzButton.FontAttributes = FontAttributes.Bold;
        }

        private async void OpenBrowser(object sender, EventArgs e)
        {
           await Browser.OpenAsync("https://www.mf-dach.de/", BrowserLaunchMode.SystemPreferred);
                           
        }

        private async void OpenEmail(object sender, EventArgs e)
        {
            string[] recipients = new[] { "info@friedrich-datentechnik.de" };

            var message = new EmailMessage
            {
                To = new List<string>(recipients)    
            };
            await Email.Default.ComposeAsync(message);
        }

        private void OpenCall(object sender, EventArgs e)
        {
            //PhoneDialer.Open(PhoneNumber.Text);
            PhoneDialer.Default.Open("030-667 023 5 - 0");

        }
        private void Open_AppInfo (object sender, EventArgs e)
        {
            AppInfo.ShowSettingsUI();
        }

        private async void OpenMap_Firmen(object sender, EventArgs e)
        {
            // TODO Xamarin.Forms.Device.RuntimePlatform is no longer supported. Use Microsoft.Maui.Devices.DeviceInfo.Platform instead. For more details see https://learn.microsoft.com/en-us/dotnet/maui/migration/forms-projects#device-changes
            if(DeviceInfo.Platform == DevicePlatform.Android)
            {
                await Launcher.OpenAsync("geo:0,0?q=Bahnhofstrasse+74+Eichwalde");
            }
            else if (DeviceInfo.Platform == DevicePlatform.iOS)
            {
                await Launcher.OpenAsync("http://maps.apple.com/?q=74+Bahnhofstrasse+Eichwalde");
            }
        }
        private  async void OpenMap_Besucher(object sender, EventArgs e)
        {
            if (DeviceInfo.Platform == DevicePlatform.Android)
            {
                await Launcher.OpenAsync("geo:0,0?q=Friesenstrasse+2+Zeuthen");
            }
            else if (DeviceInfo.Platform == DevicePlatform.iOS)
            {
                await Launcher.OpenAsync("http://maps.apple.com/?q=Friesenstrasse+2+Zeuthen");
            }
        }
    }
}