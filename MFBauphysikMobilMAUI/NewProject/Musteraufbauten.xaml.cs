using MFBauphysikMobilMAUI.Helpers;
using MFBauphysikMobilMAUI.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.Maui.Controls.Xaml;
using Microsoft.Maui.Controls;
using Microsoft.Maui;
using MFBauphysikMobilMAUI;

namespace MFBauphysikMobilMAUI.NewProject
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Musteraufbauten : ContentPage, INotifyPropertyChanged
    { 
        public MainModel main_model {  get; set; }
        List<MainModel> _muster;

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
        public  Musteraufbauten(MainModel project)
        {
            BindingContext = this;
            InitializeComponent();
            _muster = new List<MainModel>
            {
                new MainModel {MusterName= "Beton_G200_EPS_PYE",
                    ProjectName = project.ProjectName,
                    Date = DateTime.Now,
                    BV_Ersatz = project.BV_Ersatz,
                    ID = project.ID},
                new MainModel {MusterName = "Sparrendach",
                    ProjectName = project.ProjectName,
                    Date = DateTime.Now,
                    BV_Ersatz = project.BV_Ersatz,
                    ID = project.ID},
                new MainModel {MusterName = "Ständerwand",
                    ProjectName = project.ProjectName,
                    Date = DateTime.Now,
                    BV_Ersatz = project.BV_Ersatz,
                    ID = project.ID,
                },  
                
                //Beton KSD EPS035 PVC
                new MainModel
                {
                    MusterName = "Beton_KSD_EPS035_PVC",
                    ProjectName = project.ProjectName,
                    Date  = DateTime.Now,
                    BV_Ersatz = project.BV_Ersatz,
                    ID = project.ID,
                },

                //OSB KSD EPS035 PVC
                new MainModel
                {
                    MusterName = "OSB_KSD_EPS035_PVC",
                    ProjectName = project.ProjectName,
                    Date  = DateTime.Now,
                    BV_Ersatz = project.BV_Ersatz,
                    ID = project.ID,
                },

                //Beton
                new MainModel
                {
                    MusterName = "Beton_KSD_EPS_EPDM",
                    ProjectName = project.ProjectName,
                    Date = DateTime.Now,
                    BV_Ersatz=project.BV_Ersatz,
                    ID = project.ID,
                },
                new MainModel
                {
                    MusterName = "Beton_KSD_EPS_Evalon",
                    ProjectName = project.ProjectName,
                    Date = DateTime.Now,
                    BV_Ersatz=project.BV_Ersatz,
                    ID = project.ID,
                },
                new MainModel
                {
                    MusterName = "Beton_KSD_EPS_FPO",
                    ProjectName = project.ProjectName,
                    Date = DateTime.Now,
                    BV_Ersatz=project.BV_Ersatz,
                    ID = project.ID,
                },
                new MainModel
                {
                    MusterName = "Beton_KSD_EPS_PVC",
                    ProjectName = project.ProjectName,
                    Date = DateTime.Now,
                    BV_Ersatz=project.BV_Ersatz,
                    ID = project.ID,
                },
                new MainModel
                {
                    MusterName = "Beton_KSD_EPS 023_PYE_einlagig",
                    ProjectName = project.ProjectName,
                    Date = DateTime.Now,
                    BV_Ersatz=project.BV_Ersatz,
                    ID = project.ID,
                },
                new MainModel
                {
                    MusterName = "Beton_KSD_EPS 023_PYE_zweilagig",
                    ProjectName = project.ProjectName,
                    Date = DateTime.Now,
                    BV_Ersatz=project.BV_Ersatz,
                    ID = project.ID,
                },
                new MainModel
                {
                    MusterName = "Beton_KSD_MIFA_EPDM",
                    ProjectName = project.ProjectName,
                    Date = DateTime.Now,
                    BV_Ersatz=project.BV_Ersatz,
                    ID = project.ID,
                },
                new MainModel
                {
                    MusterName = "Beton_KSD_MIFA_Evalon",
                    ProjectName = project.ProjectName,
                    Date = DateTime.Now,
                    BV_Ersatz=project.BV_Ersatz,
                    ID = project.ID,
                },
                new MainModel
                {
                    MusterName = "Beton_KSD_MIFA_FPO",
                    ProjectName = project.ProjectName,
                    Date = DateTime.Now,
                    BV_Ersatz=project.BV_Ersatz,
                    ID = project.ID,
                },
                new MainModel
                {
                    MusterName = "Beton_KSD_MIFA_PVC",
                    ProjectName = project.ProjectName,
                    Date = DateTime.Now,
                    BV_Ersatz=project.BV_Ersatz,
                    ID = project.ID,
                },
                new MainModel
                {
                    MusterName = "Beton_KSD_MIFA 023_PYE_einlagig",
                    ProjectName = project.ProjectName,
                    Date = DateTime.Now,
                    BV_Ersatz=project.BV_Ersatz,
                    ID = project.ID,
                },
                new MainModel
                {
                    MusterName = "Beton_KSD_MIFA 023_PYE_zweilagig",
                    ProjectName = project.ProjectName,
                    Date = DateTime.Now,
                    BV_Ersatz=project.BV_Ersatz,
                    ID = project.ID,
                },
                new MainModel
                {
                    MusterName = "Beton_KSD_PUR_EPDM",
                    ProjectName = project.ProjectName,
                    Date = DateTime.Now,
                    BV_Ersatz=project.BV_Ersatz,
                    ID = project.ID,
                },
                new MainModel
                {
                    MusterName = "Beton_KSD_PUR_Evalon",
                    ProjectName = project.ProjectName,
                    Date = DateTime.Now,
                    BV_Ersatz=project.BV_Ersatz,
                    ID = project.ID,
                },
                new MainModel
                {
                    MusterName = "Beton_KSD_PUR_FPO",
                    ProjectName = project.ProjectName,
                    Date = DateTime.Now,
                    BV_Ersatz=project.BV_Ersatz,
                    ID = project.ID,
                },
                new MainModel
                {
                    MusterName = "Beton_KSD_PUR_PVC",
                    ProjectName = project.ProjectName,
                    Date = DateTime.Now,
                    BV_Ersatz=project.BV_Ersatz,
                    ID = project.ID,
                },
                new MainModel
                {
                    MusterName = "Beton_KSD_PUR 023_PYE_einlagig",
                    ProjectName = project.ProjectName,
                    Date = DateTime.Now,
                    BV_Ersatz=project.BV_Ersatz,
                    ID = project.ID,
                },
                new MainModel
                {
                    MusterName = "Beton_KSD_PUR 023_PYE_zweilagig",
                    ProjectName = project.ProjectName,
                    Date = DateTime.Now,
                    BV_Ersatz=project.BV_Ersatz,
                    ID = project.ID,
                },
                //OSB
                new MainModel
                {
                    MusterName = "OSB_KSD_EPS_EPDM",
                    ProjectName = project.ProjectName,
                    Date = DateTime.Now,
                    BV_Ersatz=project.BV_Ersatz,
                    ID = project.ID,
                },
                new MainModel
                {
                    MusterName = "OSB_KSD_EPS_Evalon",
                    ProjectName = project.ProjectName,
                    Date = DateTime.Now,
                    BV_Ersatz=project.BV_Ersatz,
                    ID = project.ID,
                },
                new MainModel
                {
                    MusterName = "OSB_KSD_EPS_FPO",
                    ProjectName = project.ProjectName,
                    Date = DateTime.Now,
                    BV_Ersatz=project.BV_Ersatz,
                    ID = project.ID,
                },
                new MainModel
                {
                    MusterName = "OSB_KSD_EPS_PVC",
                    ProjectName = project.ProjectName,
                    Date = DateTime.Now,
                    BV_Ersatz=project.BV_Ersatz,
                    ID = project.ID,
                },
                new MainModel
                {
                    MusterName = "OSB_KSD_EPS 023_PYE_einlagig",
                    ProjectName = project.ProjectName,
                    Date = DateTime.Now,
                    BV_Ersatz=project.BV_Ersatz,
                    ID = project.ID,
                },
                new MainModel
                {
                    MusterName = "OSB_KSD_EPS 023_PYE_zweilagig",
                    ProjectName = project.ProjectName,
                    Date = DateTime.Now,
                    BV_Ersatz=project.BV_Ersatz,
                    ID = project.ID,
                },
                new MainModel
                {
                    MusterName = "OSB_KSD_MIFA_EPDM",
                    ProjectName = project.ProjectName,
                    Date = DateTime.Now,
                    BV_Ersatz=project.BV_Ersatz,
                    ID = project.ID,
                },
                new MainModel
                {
                    MusterName = "OSB_KSD_MIFA_Evalon",
                    ProjectName = project.ProjectName,
                    Date = DateTime.Now,
                    BV_Ersatz=project.BV_Ersatz,
                    ID = project.ID,
                },
                new MainModel
                {
                    MusterName = "OSB_KSD_MIFA_FPO",
                    ProjectName = project.ProjectName,
                    Date = DateTime.Now,
                    BV_Ersatz=project.BV_Ersatz,
                    ID = project.ID,
                },
                new MainModel
                {
                    MusterName = "OSB_KSD_MIFA_PVC",
                    ProjectName = project.ProjectName,
                    Date = DateTime.Now,
                    BV_Ersatz=project.BV_Ersatz,
                    ID = project.ID,
                },
                new MainModel
                {
                    MusterName = "OSB_KSD_MIFA 023_PYE_einlagig",
                    ProjectName = project.ProjectName,
                    Date = DateTime.Now,
                    BV_Ersatz=project.BV_Ersatz,
                    ID = project.ID,
                },
                new MainModel
                {
                    MusterName = "OSB_KSD_MIFA 023_PYE_zweilagig",
                    ProjectName = project.ProjectName,
                    Date = DateTime.Now,
                    BV_Ersatz=project.BV_Ersatz,
                    ID = project.ID,
                },
                new MainModel
                {
                    MusterName = "OSB_KSD_PUR_EPDM",
                    ProjectName = project.ProjectName,
                    Date = DateTime.Now,
                    BV_Ersatz=project.BV_Ersatz,
                    ID = project.ID,
                },
                new MainModel
                {
                    MusterName = "OSB_KSD_PUR_Evalon",
                    ProjectName = project.ProjectName,
                    Date = DateTime.Now,
                    BV_Ersatz=project.BV_Ersatz,
                    ID = project.ID,
                },
                new MainModel
                {
                    MusterName = "OSB_KSD_PUR_FPO",
                    ProjectName = project.ProjectName,
                    Date = DateTime.Now,
                    BV_Ersatz=project.BV_Ersatz,
                    ID = project.ID,
                },
                new MainModel
                {
                    MusterName = "OSB_KSD_PUR_PVC",
                    ProjectName = project.ProjectName,
                    Date = DateTime.Now,
                    BV_Ersatz=project.BV_Ersatz,
                    ID = project.ID,
                },
                new MainModel
                {
                    MusterName = "OSB_KSD_PUR 023_PYE_einlagig",
                    ProjectName = project.ProjectName,
                    Date = DateTime.Now,
                    BV_Ersatz=project.BV_Ersatz,
                    ID = project.ID,
                },
                new MainModel
                {
                    MusterName = "OSB_KSD_PUR 023_PYE_zweilagig",
                    ProjectName = project.ProjectName,
                    Date = DateTime.Now,
                    BV_Ersatz=project.BV_Ersatz,
                    ID = project.ID,
                },

                //Trapezblech KSD
                new MainModel
                {
                    MusterName = "Trapezblech_KSD_EPS_EPDM",
                    ProjectName = project.ProjectName,
                    Date = DateTime.Now,
                    BV_Ersatz=project.BV_Ersatz,
                    ID = project.ID,
                },
                new MainModel
                {
                    MusterName = "Trapezblech_KSD_EPS_Evalon",
                    ProjectName = project.ProjectName,
                    Date = DateTime.Now,
                    BV_Ersatz=project.BV_Ersatz,
                    ID = project.ID,
                },
                new MainModel
                {
                    MusterName = "Trapezblech_KSD_EPS_FPO",
                    ProjectName = project.ProjectName,
                    Date = DateTime.Now,
                    BV_Ersatz=project.BV_Ersatz,
                    ID = project.ID,
                },
                new MainModel
                {
                    MusterName = "Trapezblech_KSD_EPS_PVC",
                    ProjectName = project.ProjectName,
                    Date = DateTime.Now,
                    BV_Ersatz=project.BV_Ersatz,
                    ID = project.ID,
                },
                new MainModel
                {
                    MusterName = "Trapezblech_KSD_EPS 023_PYE_einlagig",
                    ProjectName = project.ProjectName,
                    Date = DateTime.Now,
                    BV_Ersatz=project.BV_Ersatz,
                    ID = project.ID,
                },
                new MainModel
                {
                    MusterName = "Trapezblech_KSD_EPS 023_PYE_zweilagig",
                    ProjectName = project.ProjectName,
                    Date = DateTime.Now,
                    BV_Ersatz=project.BV_Ersatz,
                    ID = project.ID,
                },
                new MainModel
                {
                    MusterName = "Trapezblech_KSD_MIFA_EPDM",
                    ProjectName = project.ProjectName,
                    Date = DateTime.Now,
                    BV_Ersatz=project.BV_Ersatz,
                    ID = project.ID,
                },
                new MainModel
                {
                    MusterName = "Trapezblech_KSD_MIFA_Evalon",
                    ProjectName = project.ProjectName,
                    Date = DateTime.Now,
                    BV_Ersatz=project.BV_Ersatz,
                    ID = project.ID,
                },
                new MainModel
                {
                    MusterName = "Trapezblech_KSD_MIFA_FPO",
                    ProjectName = project.ProjectName,
                    Date = DateTime.Now,
                    BV_Ersatz=project.BV_Ersatz,
                    ID = project.ID,
                },
                new MainModel
                {
                    MusterName = "Trapezblech_KSD_MIFA_PVC",
                    ProjectName = project.ProjectName,
                    Date = DateTime.Now,
                    BV_Ersatz=project.BV_Ersatz,
                    ID = project.ID,
                },
                new MainModel
                {
                    MusterName = "Trapezblech_KSD_MIFA 023_PYE_einlagig",
                    ProjectName = project.ProjectName,
                    Date = DateTime.Now,
                    BV_Ersatz=project.BV_Ersatz,
                    ID = project.ID,
                },
                new MainModel
                {
                    MusterName = "Trapezblech_KSD_MIFA 023_PYE_zweilagig",
                    ProjectName = project.ProjectName,
                    Date = DateTime.Now,
                    BV_Ersatz=project.BV_Ersatz,
                    ID = project.ID,
                },
                new MainModel
                {
                    MusterName = "Trapezblech_KSD_PUR_EPDM",
                    ProjectName = project.ProjectName,
                    Date = DateTime.Now,
                    BV_Ersatz=project.BV_Ersatz,
                    ID = project.ID,
                },
                new MainModel
                {
                    MusterName = "Trapezblech_KSD_PUR_Evalon",
                    ProjectName = project.ProjectName,
                    Date = DateTime.Now,
                    BV_Ersatz=project.BV_Ersatz,
                    ID = project.ID,
                },
                new MainModel
                {
                    MusterName = "Trapezblech_KSD_PUR_FPO",
                    ProjectName = project.ProjectName,
                    Date = DateTime.Now,
                    BV_Ersatz=project.BV_Ersatz,
                    ID = project.ID,
                },
                new MainModel
                {
                    MusterName = "Trapezblech_KSD_PUR_PVC",
                    ProjectName = project.ProjectName,
                    Date = DateTime.Now,
                    BV_Ersatz=project.BV_Ersatz,
                    ID = project.ID,
                },
                new MainModel
                {
                    MusterName = "Trapezblech_KSD_PUR 023_PYE_einlagig",
                    ProjectName = project.ProjectName,
                    Date = DateTime.Now,
                    BV_Ersatz=project.BV_Ersatz,
                    ID = project.ID,
                },
                new MainModel
                {
                    MusterName = "Trapezblech_KSD_PUR 023_PYE_zweilagig",
                    ProjectName = project.ProjectName,
                    Date = DateTime.Now,
                    BV_Ersatz=project.BV_Ersatz,
                    ID = project.ID,
                },
                //Trapezblech PE
                new MainModel
                {
                    MusterName = "Trapezblech_PE_MIFA_EPDM",
                    ProjectName = project.ProjectName,
                    Date = DateTime.Now,
                    BV_Ersatz=project.BV_Ersatz,
                    ID = project.ID,
                },
                new MainModel
                {
                    MusterName = "Trapezblech_PE_MIFA_Evalon",
                    ProjectName = project.ProjectName,
                    Date = DateTime.Now,
                    BV_Ersatz=project.BV_Ersatz,
                    ID = project.ID,
                },
                new MainModel
                {
                    MusterName = "Trapezblech_PE_MIFA_FPO",
                    ProjectName = project.ProjectName,
                    Date = DateTime.Now,
                    BV_Ersatz=project.BV_Ersatz,
                    ID = project.ID,
                },
                new MainModel
                {
                    MusterName = "Trapezblech_PE_MIFA_PVC",
                    ProjectName = project.ProjectName,
                    Date = DateTime.Now,
                    BV_Ersatz=project.BV_Ersatz,
                    ID = project.ID,
                },
                new MainModel
                {
                    MusterName = "Trapezblech_PE_MIFA 023_PYE_einlagig",
                    ProjectName = project.ProjectName,
                    Date = DateTime.Now,
                    BV_Ersatz=project.BV_Ersatz,
                    ID = project.ID,
                },
                new MainModel
                {
                    MusterName = "Trapezblech_PE_MIFA 023_PYE_zweilagig",
                    ProjectName = project.ProjectName,
                    Date = DateTime.Now,
                    BV_Ersatz=project.BV_Ersatz,
                    ID = project.ID,
                },

                //Umkehrdach
                new MainModel
                {
                    MusterName = "Umkehrdach",
                    ProjectName = project.ProjectName,
                    Date = DateTime.Now,
                    BV_Ersatz = project.BV_Ersatz,
                    ID=project.ID,
                },
                //WDVS
                new MainModel
                {
                    MusterName = "WDVS_EPS-032",
                    ProjectName = project.ProjectName,
                    Date = DateTime.Now,
                    BV_Ersatz = project.BV_Ersatz,
                    ID = project.ID,
                },
                new MainModel
                {
                    MusterName = "WDVS_EPS-035",
                    ProjectName = project.ProjectName,
                    Date = DateTime.Now,
                    BV_Ersatz = project.BV_Ersatz,
                    ID = project.ID,
                }
            };
            listView.ItemsSource = _muster.OrderBy(p => p.MusterName);
            foreach(MainModel i in _muster)
            {
                i.SizeClass = Setting.Size_Default;
            }
        }
      
        public void Back_Clicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }
        private void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (listView.SelectedItem == null)
            {
                return;
            }
            var selectedAufbau = (e.SelectedItem as MainModel)!;
            //listView.SelectedItem = null;
            selectedAufbau.Selected = 0;
            main_model = selectedAufbau;           
        }

        private async void  Next_Clicked(object sender, EventArgs e)
        {
            if (main_model == null)
            {
                await DisplayAlert("Achtung", "Bitte einen Musteraufbau auswählen", "OK");
            }
            else
            {
                if (main_model.MusterName == "Sparrendach")
                {
                    await App.Database.SaveItemAsync(main_model);
                    var page = new CalculationSparren(main_model);
                    await Navigation.PushAsync(page);
                }
                else if (main_model.MusterName == "Ständerwand")
                {
                    await App.Database.SaveItemAsync(main_model);
                    var page = new CalculationStänder(main_model);
                    await Navigation.PushAsync(page);
                }
                else
                {
                    await App.Database.SaveItemAsync(main_model);
                    var page = new CalculationPage(main_model);
                    await Navigation.PushAsync(page);
                }
            }
        }

        private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            var item = _muster;
            listView.ItemsSource = item.Where(p => p.MusterName.ToLower().Contains(e.NewTextValue)).OrderBy(p => p.MusterName);
        }
    }
}