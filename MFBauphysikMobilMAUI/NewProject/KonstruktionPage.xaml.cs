using MFBauphysikMobilMAUI.Helpers;
using MFBauphysikMobilMAUI.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Maui.Controls.PlatformConfiguration.AndroidSpecific;
using Microsoft.Maui.Controls.Xaml;
using Microsoft.Maui.Controls;
using Microsoft.Maui;

namespace MFBauphysikMobilMAUI.NewProject
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class KonstruktionPage : ContentPage, INotifyPropertyChanged
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
        public event EventHandler<string> KonstruktionChanged;

        private bool _kaltdach;
        private bool _warmdach;
        private bool _hinterluftWand;
        private bool _kein_hinterlufWand;
        private bool _verschattet;
        private string _type;
        private string _name;
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                if (_name == value) return;
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }
        public string Type
        {
            get { return _type; }
            set 
            {
                if (_type == value) return;
                _type = value;
                OnPropertyChanged(nameof(Type));
            }
        }

        public bool Kaltdach
        {
            get { return _kaltdach; } 
            set
            {
                if (_kaltdach == value) 
                    return;
                _kaltdach = value;
                OnPropertyChanged(nameof(Kaltdach));
            }
        }
        public bool Warmdach
        {
            get { return _warmdach; }
            set
            {
                if (_warmdach == value)
                    return;
                _warmdach = value;
                OnPropertyChanged(nameof(Warmdach));
            }
        }

        public bool HinterluftWand
        {
            get { return _hinterluftWand; }
            set
            {
                if (_hinterluftWand == value)
                    return;
                _hinterluftWand = value;
                OnPropertyChanged(nameof(HinterluftWand));
            }
        }

        public bool Kein_HinterluftWand
        {
            get { return _kein_hinterlufWand; }
            set
            {
                if (_kein_hinterlufWand == value)
                    return;
                _kein_hinterlufWand = value;
                OnPropertyChanged(nameof(Kein_HinterluftWand));
            }
        }
        public bool Verschattet
        {
            get { return _verschattet; }
            set
            {
                if (_verschattet == value)
                    return;
                _verschattet = value;
                OnPropertyChanged(nameof(Verschattet));
            }
        }

        public KonstruktionPage(string konstruktionstyp)
        {           
            InitializeComponent();

            BindingContext = this;
            if (konstruktionstyp == "unbelüftetes Dach (Warmdach) \r\n(unverschattet mit dunkler Deckung/Abdichtung)")
            {
                Warmdach = true;
            }
            else if (konstruktionstyp == "Kaltdach")
            {
                Kaltdach = true;
            }
            else if (konstruktionstyp == "hinterlüftete Wand")
            {
                HinterluftWand = true;
            }
            else if (konstruktionstyp == "nicht hinterlüftete Wand")
            {
                Kein_HinterluftWand = true;
            }
            else if (konstruktionstyp == "unbelüftetes Dach (Warmdach) \r\n(verschattet bzw. helle Deckung/Abdichtung)")
            {
                Verschattet = true;
            }
            kaltdach_label.FontSize = Setting.Size_Default;
            warmdach_label.FontSize = Setting.Size_Default;
            verschattet_label.FontSize = Setting.Size_Default;
            luft_wand_label.FontSize = Setting.Size_Default;
            kein_luft_wand_label.FontSize = Setting.Size_Default;
            konstruktion_label.FontSize = Setting.Size_Default;
            SizeTitle = Setting.Size_Title;
            Name = konstruktionstyp;
        }

        public async void Back_Clicked(object sender, EventArgs e)
        {
            if (Type == "Kaltdach")
            {
                Name = "Kaltdach";
            }
            else if (Type == "Warmdach")
            {
                Name = "unbelüftetes Dach (Warmdach) \r\n(unverschattet mit dunkler Deckung/Abdichtung)";
            }
            else if (Type == "verschattet")
            {
                Name = "unbelüftetes Dach (Warmdach) \r\n(verschattet bzw. helle Deckung/Abdichtung)";
            }
            else if (Type == "HinterluftWand")
            {
                Name = "hinterlüftete Wand";
            }
            else if (Type == "Kein_HinterluftWand")
            {
                Name = "nicht hinterlüftete Wand";
            }
            KonstruktionChanged?.Invoke(this, Name);
            await Navigation.PopAsync();
        }

        public void Next_Clicked(object sender, EventArgs e)
        {
        }

        private void Konstruktion_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            RadioButton button = sender as RadioButton;
            Type = button.Value.ToString();
        }
    }
}