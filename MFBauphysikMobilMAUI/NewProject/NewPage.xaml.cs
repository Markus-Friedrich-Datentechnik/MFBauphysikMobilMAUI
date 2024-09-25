using MFBauphysikMobilMAUI.Data;
using MFBauphysikMobilMAUI.Helpers;
using MFBauphysikMobilMAUI.Models;
using MFBauphysikMobilMAUI.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Maui.Controls.Xaml;
using Microsoft.Maui.Controls;
using Microsoft.Maui;
using MFBauphysikMobil.NewProject;

namespace MFBauphysikMobilMAUI.NewProject
{
    [XamlCompilation(XamlCompilationOptions.Compile)]

    public partial class NewPage : ContentPage, INotifyPropertyChanged
    {
        public MainModel _mainmodel { get; set; }
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
       // public NewPage(MainModel project)
       public NewPage()
        {            
            InitializeComponent();
          /*  _mainmodel = new MainModel
            {
                ID = project.ID,
                MusterName = project.MusterName,
                ProjectName = project.ProjectName,
                BV_Ersatz = project.BV_Ersatz,
                Befestiger_Basis = project.Befestiger_Basis,
                Date = project.Date,
            };*/
            SizeDefault = Setting.Size_Default;
            SizeLarge = Setting.Size_Large;
            SizeMicro = Setting.Size_Micro;
            SizeMedium = Setting.Size_Medium;
            SizeTitle = Setting.Size_Title;
            projektname_label.FontSize = Setting.Size_Default;
            projektname_entry.FontSize = Setting.Size_Default;
            bv_label.FontSize = SizeDefault;
            bv_entry.FontSize = SizeDefault;
            bv_ersatz.FontSize = SizeDefault;
        }

        //Back to MainPage
        private void Back_Clicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }

        //Go to next page
        private async void Next_Clicked(object sender, EventArgs e)
        {
           if ((string.IsNullOrEmpty(projektname_entry.Text)) || (string.IsNullOrWhiteSpace(projektname_entry.Text)))
            {
                await DisplayAlert("Achtung", "Bitte \"Projektname\" eingeben", "OK");                
            }
            else
            {
                /* string name = projektname_entry.Text;
                 var neu = new MainModel()
                 {
                     ProjectName = name,
                 };*/
                string name = projektname_entry.Text;
                string bv = "";

                if (string.IsNullOrEmpty(bv_entry.Text))
                {
                    bv = projektname_entry.Text;
                }
                else
                {
                     bv = bv_entry.Text;
                }
                var neu = new MainModel()
                {
                    ProjectName = name,
                    BV = bv,
                };
                await Navigation.PushAsync(new TestMusteraufbauten(neu));
            }
        }

        private  void Entry_TextChanged(object sender, TextChangedEventArgs e)
        {
            bv_entry.Text = projektname_entry.Text;
        }
    }
}