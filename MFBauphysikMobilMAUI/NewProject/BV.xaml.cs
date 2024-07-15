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
using MFBauphysikMobilMAUI;

namespace MFBauphysikMobilMAUI.NewProject
{
    public partial class BV : ContentPage
    {
        public MainModel main_model { get; set; }       

        public BV(MainModel project)
        {            
            InitializeComponent();
            //var vm = new MainVM();
            //this.BindingContext = vm;
            main_model = new MainModel
            {
                Selected = project.Selected,
                ID = project.ID,
                MusterName = project.MusterName,
                ProjectName = project.ProjectName,
                Date = project.Date,
                BV = project.BV,
                BV_Ersatz = project.BV_Ersatz,
                Befestiger_Basis = project.Befestiger_Basis,
                Befestiger_Sparren = project.Befestiger_Sparren,
                Befestiger_Gefach = project.Befestiger_Gefach,
                Befestiger_Ständer = project.Befestiger_Ständer,
                Bauteil_Basis = project.Bauteil_Basis,
                Bauteil_Sparren = project.Bauteil_Sparren,
                Bauteil_Gefach = project.Bauteil_Gefach,
                Bauteil_Ständer = project.Bauteil_Ständer,                
            };
            bv_label.FontSize = Setting.Size_Default;
            projekt_label.FontSize = Setting.Size_Default;
            BV_Ersatz.FontSize = Setting.Size_Default;
            entry_label.FontSize = Setting.Size_Default;
            ProjektName.FontSize = Setting.Size_Default;
            Title_BV.FontSize = Setting.Size_Large;
        }
        public async void Back_Clicked (object sender, EventArgs e)
        {            
            await Navigation.PopAsync(); 
        }
        public async void Next_Clicked(object sender, EventArgs e)
        {
            var bv_update = (MainModel)BindingContext;
            bv_update.Date = DateTime.Now;
            await App.Database.UpdateItemAsync(bv_update);
            await Navigation.PopAsync();
            /*if (bv_update.MusterName == "Sparrendach")
            {
                await Navigation.PushAsync(new CalculationSparren(bv_update));
            }
            else if (bv_update.MusterName == "Ständerwand")
            {
                await Navigation.PushAsync(new CalculationStänder(bv_update));
            }
            else
            {
                await Navigation.PushAsync(new CalculationPage(bv_update));
            }*/
        }
    }
}