
using MFBauphysikMobilMAUI.Models;
using MFBauphysikMobilMAUI.NewProject;
using MFBauphysikMobilMAUI.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLitePCL;
using SQLite;
using MFBauphysikMobilMAUI.Interface;
using MFBauphysikMobilMAUI.Data;
using static System.Net.Mime.MediaTypeNames;
using System.Net.Http.Headers;
using System.ComponentModel.Design;
using System.Data;
using System.Reflection;
using System.Xml.Serialization;
using MFBauphysikMobilMAUI.Utils;
using MFBauphysikMobilMAUI.Helpers;
using MFBauphysikMobilMAUI.Konfiguration;
using Microsoft.Maui.Controls;
using Microsoft.Maui;
using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Maui.Controls;
using Microsoft.Maui;
using MFBauphysikMobilMAUI;

namespace MFBauphysikMobilMAUI
{
    public partial class MainPage : ContentPage
    {
        ObservableCollection<Basis> _basis = new ObservableCollection<Basis>();
        public ObservableCollection<Basis> BasisList
        {
            get { return _basis; }
            set
            {
                _basis = value;
                OnPropertyChanged(nameof(BasisList));
            }
        }
        List<EinstellungModel> _setting_mainPage = new List<EinstellungModel>();
        public List<EinstellungModel> Setting_MainPage
        {
            get { return _setting_mainPage; }
            set
            {
                _setting_mainPage = value;
                OnPropertyChanged(nameof(MainPage));
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
        public MainPage()
        {
            InitializeComponent();
            Setting_MainPage = new List<EinstellungModel>()
                {
                    new EinstellungModel()
                    {
                        Alt_Jung = true,
                        Jung_Alt = false,
                        A_Z = false,
                        Z_A = false,
                    },
                };
            SizeDefault = Setting.Size_Default;
            SizeTitle = Setting.Size_Title;
            SizeLarge = Setting.Size_Large;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            var itemsource = await App.Database.GetItemAsync();
            foreach (MainModel i in itemsource)
            {
                i.SizeClass = Setting.Size_Default;                
            }
            var setting = Setting_MainPage[0];
            setting.DefaultSize = Setting.Size_Default;
            setting.MediumSize = Setting.Size_Medium;
            setting.LargeSize = Setting.Size_Large;
            setting.MicroSize = Setting.Size_Micro;
            setting.TitleSize = Setting.Size_Title;
            if (setting.Jung_Alt == true)
            {
                ListProjekt.ItemsSource = itemsource.OrderBy(d => d.Date);
            }
            else if (setting.Alt_Jung == true)
            {
                ListProjekt.ItemsSource = itemsource.OrderByDescending(d => d.Date);
            }
            else if (setting.A_Z == true)
            {
                ListProjekt.ItemsSource = itemsource.OrderBy(d => d.ProjectName);
            }
            else
            {
                ListProjekt.ItemsSource = itemsource.OrderByDescending(d => d.ProjectName);
            }
        }
        //Neues Projekt erstellen
        private async void PlusClicked(object sender, EventArgs e)
        {
            //var project = new MainModel();
            //await Navigation.PushAsync(new NewPage(project));
            await Navigation.PushAsync(new NewPage());
        }

        //Projekt auswählen
        private async void ListProjekt_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {
                var selectedItem = (e.SelectedItem as MainModel)!;
                selectedItem.Selected = 1;
                if (selectedItem.MusterName == "Sparrendach")
                {
                    //selectedItem = (MainModel)BindingContext;
                    await Navigation.PushAsync(new CalculationSparren(selectedItem));
                }
                else if (selectedItem.MusterName == "Ständerwand")
                {
                    //selectedItem = (MainModel)BindingContext;
                    await Navigation.PushAsync(new CalculationStänder(selectedItem));

                }
                else
                {
                    //selectedItem = (MainModel)BindingContext;
                    await Navigation.PushAsync(new CalculationPage(selectedItem));

                }
            }

        }

        //Menü auswählen
        private async void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            var mainpage = Setting_MainPage[0] as EinstellungModel;
            var mainpageUpdated = new MenuPage(mainpage);
            mainpageUpdated.MainPageUpdated += (source, mainpagesetting) =>
            {
                mainpage.DefaultSize = mainpagesetting.DefaultSize;
                mainpage.MediumSize = mainpagesetting.MediumSize;
                mainpage.MicroSize = mainpagesetting.MicroSize;
                mainpage.TitleSize = mainpagesetting.TitleSize;
                mainpage.LargeSize = mainpagesetting.LargeSize;
                mainpage.Alt_Jung = mainpagesetting.Alt_Jung;
                mainpage.Jung_Alt = mainpagesetting.Jung_Alt;
                mainpage.A_Z = mainpagesetting.A_Z;
                mainpage.Z_A = mainpagesetting.Z_A;
            };
            await Navigation.PushAsync(mainpageUpdated);

            
        }

        private async void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            var itemsource = await App.Database.GetItemAsync();
            ListProjekt.ItemsSource = itemsource.Where(p => p.ProjectName.ToLower().Contains(e.NewTextValue)).OrderByDescending(p => p.Date);

        }

        private async void OnItemClicked(object sender, EventArgs e)
        {
            //string action = await DisplayActionSheet("Option", "Abbrechen", null, "Löschen", "Umbenennen");
            //if (action == "Löschen")
            //{
            MenuItem menu_item = (sender as MenuItem)!;
            var item = (menu_item.BindingContext as MainModel)!;
            var answer = await DisplayAlert("Achtung!", "Projekt wirklich löschen?", "Ja", "Nein");
            if (answer == true)
            {
                await App.Database.DeleteItems(item);
                OnAppearing();
            }
            //}
        }
        private async void OnRenameClicked(object sender, EventArgs e)
        {
            //  else if (action == "Umbenennen")
            //{
            var menuItem = (sender as MenuItem)!;
            var item = (menuItem.BindingContext as MainModel)!;
            string result = await DisplayPromptAsync("Projektname", null, "OK", "Abbrechen", initialValue: item.ProjectName.ToString());
            if (item.ProjectName == result)
            {

            }
            else
            {
                if (!string.IsNullOrWhiteSpace(result))
                {
                    item.ProjectName = result;
                    item.Date = DateTime.Now;
                }
                await App.Database.UpdateItemAsync(item);
            }
            OnAppearing();
        }
    }
}
