using MFBauphysikMobil.ViewModels;
using MFBauphysikMobilMAUI.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace MFBauphysikMobil.NewProject;

public partial class TestMusteraufbauten : ContentPage, INotifyPropertyChanged
{

    public MainModel main_model { get; set; }
    List<MainModel> _muster;
    readonly IList<MainModel> source;
    MainModel selectedAufbau;
    int selectionCount = 1;

    public ObservableCollection<MainModel> Aufbau { get; private set; }
    public IList<MainModel> EmptyAufbau { get; private set; }

    public MainModel SelectedAufbau
    {
        get
        {
            return selectedAufbau;
        }
        set
        {
            if (selectedAufbau != value)
            {
                selectedAufbau = value;
            }
        }
    }

    ObservableCollection<object> selectedAufbaus;
    public ObservableCollection<object> SelectedAufbaus
    {
        get
        {
            return selectedAufbaus;
        }
        set
        {
            if (selectedAufbaus != value)
            {
                selectedAufbaus = value;
            }
        }
    }


    public string SelectedAufbauMessage { get; private set; }

    public ICommand AufbauSelectionChangedCommand => new Command(AufbauSelectionChanged);
    public TestMusteraufbauten(MainModel project)
    {
        //BindingContext = new MusterViewModel();
        
        InitializeComponent();
        main_model = new MainModel
        {
            ProjectName = project.ProjectName,
            BV = project.BV,
            Date = DateTime.Now,
            BV_Ersatz = project.BV_Ersatz,
            ID = project.ID
        };
        source = new List<MainModel>();
        CreateAufbauCollection();

        selectedAufbau = Aufbau.Skip(3).FirstOrDefault();
        AufbauSelectionChanged();

        SelectedAufbaus = new ObservableCollection<object>()
        {
            Aufbau[1], Aufbau[3], Aufbau[4]
        };

    }
    void CreateAufbauCollection()
    {
        MainModel project = main_model;
        source.Add(new MainModel
        {
            MusterName = "Beton_G200_EPS_PYE",
            ProjectName = project.ProjectName,
            BV = project.BV,
            Date = DateTime.Now,
            BV_Ersatz = project.BV_Ersatz,
            ID = project.ID
        });
        source.Add(new MainModel
        {
            MusterName = "Sparrendach",
            ProjectName = project.ProjectName,
            BV = project.BV,
            Date = DateTime.Now,
            BV_Ersatz = project.BV_Ersatz,
            ID = project.ID
        });
        source.Add(new MainModel
        {
            MusterName = "Ständerwand",
            ProjectName = project.ProjectName,
            BV = project.BV,
            Date = DateTime.Now,
            BV_Ersatz = project.BV_Ersatz,
            ID = project.ID,
        });

        //Beton KSD EPS035 PVC
        source.Add(new MainModel
        {
            MusterName = "Beton_KSD_EPS035_PVC",
            ProjectName = project.ProjectName,
            BV = project.BV,
            Date = DateTime.Now,
            BV_Ersatz = project.BV_Ersatz,
            ID = project.ID,
        });

        //OSB KSD EPS035 PVC
        source.Add(new MainModel
        {
            MusterName = "OSB_KSD_EPS035_PVC",
            ProjectName = project.ProjectName,
            BV = project.BV,
            Date = DateTime.Now,
            BV_Ersatz = project.BV_Ersatz,
            ID = project.ID,
        });

        //Beton
        source.Add(new MainModel
        {
            MusterName = "Beton_KSD_EPS_EPDM",
            ProjectName = project.ProjectName,
            BV = project.BV,
            Date = DateTime.Now,
            BV_Ersatz = project.BV_Ersatz,
            ID = project.ID,
        });
        source.Add(new MainModel
        {
            MusterName = "Beton_KSD_EPS_Evalon",
            ProjectName = project.ProjectName,
            BV = project.BV,
            Date = DateTime.Now,
            BV_Ersatz = project.BV_Ersatz,
            ID = project.ID,
        });
        source.Add(new MainModel
        {
            MusterName = "Beton_KSD_EPS_FPO",
            ProjectName = project.ProjectName,
            BV = project.BV,
            Date = DateTime.Now,
            BV_Ersatz = project.BV_Ersatz,
            ID = project.ID,
        });
        source.Add(new MainModel
        {
            MusterName = "Beton_KSD_EPS_PVC",
            ProjectName = project.ProjectName,
            BV = project.BV,
            Date = DateTime.Now,
            BV_Ersatz = project.BV_Ersatz,
            ID = project.ID,
        });
        source.Add(new MainModel
        {
            MusterName = "Beton_KSD_EPS_PYE_einlagig",
            ProjectName = project.ProjectName,
            BV = project.BV,
            Date = DateTime.Now,
            BV_Ersatz = project.BV_Ersatz,
            ID = project.ID,
        });
        source.Add(new MainModel
        {
            MusterName = "Beton_KSD_EPS_PYE_zweilagig",
            ProjectName = project.ProjectName,
            BV = project.BV,
            Date = DateTime.Now,
            BV_Ersatz = project.BV_Ersatz,
            ID = project.ID,
        });
        source.Add(new MainModel
        {
            MusterName = "Beton_KSD_MIFA_EPDM",
            ProjectName = project.ProjectName,
            BV = project.BV,
            Date = DateTime.Now,
            BV_Ersatz = project.BV_Ersatz,
            ID = project.ID,
        });
        source.Add(new MainModel
        {
            MusterName = "Beton_KSD_MIFA_Evalon",
            ProjectName = project.ProjectName,
            BV = project.BV,
            Date = DateTime.Now,
            BV_Ersatz = project.BV_Ersatz,
            ID = project.ID,
        });
        source.Add(new MainModel
        {
            MusterName = "Beton_KSD_MIFA_FPO",
            ProjectName = project.ProjectName,
            BV = project.BV,
            Date = DateTime.Now,
            BV_Ersatz = project.BV_Ersatz,
            ID = project.ID,
        });
        source.Add(new MainModel
        {
            MusterName = "Beton_KSD_MIFA_PVC",
            ProjectName = project.ProjectName,
            BV = project.BV,
            Date = DateTime.Now,
            BV_Ersatz = project.BV_Ersatz,
            ID = project.ID,
        });
        source.Add(new MainModel
        {
            MusterName = "Beton_KSD_MIFA_PYE_einlagig",
            ProjectName = project.ProjectName,
            BV = project.BV,
            Date = DateTime.Now,
            BV_Ersatz = project.BV_Ersatz,
            ID = project.ID,
        });
        source.Add(new MainModel
        {
            MusterName = "Beton_KSD_MIFA_PYE_zweilagig",
            ProjectName = project.ProjectName,
            BV = project.BV,
            Date = DateTime.Now,
            BV_Ersatz = project.BV_Ersatz,
            ID = project.ID,
        });
        source.Add(new MainModel
        {
            MusterName = "Beton_KSD_PUR_EPDM",
            ProjectName = project.ProjectName,
            BV = project.BV,
            Date = DateTime.Now,
            BV_Ersatz = project.BV_Ersatz,
            ID = project.ID,
        });
        source.Add(new MainModel
        {
            MusterName = "Beton_KSD_PUR_Evalon",
            ProjectName = project.ProjectName,
            BV = project.BV,
            Date = DateTime.Now,
            BV_Ersatz = project.BV_Ersatz,
            ID = project.ID,
        });
        source.Add(new MainModel
        {
            MusterName = "Beton_KSD_PUR_FPO",
            ProjectName = project.ProjectName,
            BV = project.BV,
            Date = DateTime.Now,
            BV_Ersatz = project.BV_Ersatz,
            ID = project.ID,
        });
        source.Add(new MainModel
        {
            MusterName = "Beton_KSD_PUR_PVC",
            ProjectName = project.ProjectName,
            BV = project.BV,
            Date = DateTime.Now,
            BV_Ersatz = project.BV_Ersatz,
            ID = project.ID,
        });
        source.Add(new MainModel
        {
            MusterName = "Beton_KSD_PUR_PYE_einlagig",
            ProjectName = project.ProjectName,
            BV = project.BV,
            Date = DateTime.Now,
            BV_Ersatz = project.BV_Ersatz,
            ID = project.ID,
        });
        source.Add(new MainModel
        {
            MusterName = "Beton_KSD_PUR_PYE_zweilagig",
            ProjectName = project.ProjectName,
            BV = project.BV,
            Date = DateTime.Now,
            BV_Ersatz = project.BV_Ersatz,
            ID = project.ID,
        });
        //OSB
        source.Add(new MainModel
        {
            MusterName = "OSB_KSD_EPS_EPDM",
            ProjectName = project.ProjectName,
            BV = project.BV,
            Date = DateTime.Now,
            BV_Ersatz = project.BV_Ersatz,
            ID = project.ID,
        });
        source.Add(new MainModel
        {
            MusterName = "OSB_KSD_EPS_Evalon",
            ProjectName = project.ProjectName,
            BV = project.BV,
            Date = DateTime.Now,
            BV_Ersatz = project.BV_Ersatz,
            ID = project.ID,
        });
        source.Add(new MainModel
        {
            MusterName = "OSB_KSD_EPS_FPO",
            ProjectName = project.ProjectName,
            BV = project.BV,
            Date = DateTime.Now,
            BV_Ersatz = project.BV_Ersatz,
            ID = project.ID,
        });
        source.Add(new MainModel
        {
            MusterName = "OSB_KSD_EPS_PVC",
            ProjectName = project.ProjectName,
            BV = project.BV,
            Date = DateTime.Now,
            BV_Ersatz = project.BV_Ersatz,
            ID = project.ID,
        });
        source.Add(new MainModel
        {
            MusterName = "OSB_KSD_EPS_PYE_einlagig",
            ProjectName = project.ProjectName,
            BV = project.BV,
            Date = DateTime.Now,
            BV_Ersatz = project.BV_Ersatz,
            ID = project.ID,
        });
        source.Add(new MainModel
        {
            MusterName = "OSB_KSD_EPS_PYE_zweilagig",
            ProjectName = project.ProjectName,
            BV = project.BV,
            Date = DateTime.Now,
            BV_Ersatz = project.BV_Ersatz,
            ID = project.ID,
        });
        source.Add(new MainModel
        {
            MusterName = "OSB_KSD_MIFA_EPDM",
            ProjectName = project.ProjectName,
            BV = project.BV,
            Date = DateTime.Now,
            BV_Ersatz = project.BV_Ersatz,
            ID = project.ID,
        });
        source.Add(new MainModel
        {
            MusterName = "OSB_KSD_MIFA_Evalon",
            ProjectName = project.ProjectName,
            BV = project.BV,
            Date = DateTime.Now,
            BV_Ersatz = project.BV_Ersatz,
            ID = project.ID,
        });
        source.Add(new MainModel
        {
            MusterName = "OSB_KSD_MIFA_FPO",
            ProjectName = project.ProjectName,
            BV = project.BV,
            Date = DateTime.Now,
            BV_Ersatz = project.BV_Ersatz,
            ID = project.ID,
        });
        source.Add(new MainModel
        {
            MusterName = "OSB_KSD_MIFA_PVC",
            ProjectName = project.ProjectName,
            BV = project.BV,
            Date = DateTime.Now,
            BV_Ersatz = project.BV_Ersatz,
            ID = project.ID,
        });
        source.Add(new MainModel
        {
            MusterName = "OSB_KSD_MIFA_PYE_einlagig",
            ProjectName = project.ProjectName,
            BV = project.BV,
            Date = DateTime.Now,
            BV_Ersatz = project.BV_Ersatz,
            ID = project.ID,
        });
        source.Add(new MainModel
        {
            MusterName = "OSB_KSD_MIFA_PYE_zweilagig",
            ProjectName = project.ProjectName,
            BV = project.BV,
            Date = DateTime.Now,
            BV_Ersatz = project.BV_Ersatz,
            ID = project.ID,
        });
        source.Add(new MainModel
        {
            MusterName = "OSB_KSD_PUR_EPDM",
            ProjectName = project.ProjectName,
            BV = project.BV,
            Date = DateTime.Now,
            BV_Ersatz = project.BV_Ersatz,
            ID = project.ID,
        });
        source.Add(new MainModel
        {
            MusterName = "OSB_KSD_PUR_Evalon",
            ProjectName = project.ProjectName,
            BV = project.BV,
            Date = DateTime.Now,
            BV_Ersatz = project.BV_Ersatz,
            ID = project.ID,
        });
        source.Add(new MainModel
        {
            MusterName = "OSB_KSD_PUR_FPO",
            ProjectName = project.ProjectName,
            BV = project.BV,
            Date = DateTime.Now,
            BV_Ersatz = project.BV_Ersatz,
            ID = project.ID,
        });
        source.Add(new MainModel
        {
            MusterName = "OSB_KSD_PUR_PVC",
            ProjectName = project.ProjectName,
            BV = project.BV,
            Date = DateTime.Now,
            BV_Ersatz = project.BV_Ersatz,
            ID = project.ID,
        });
        source.Add(new MainModel
        {
            MusterName = "OSB_KSD_PUR_PYE_einlagig",
            ProjectName = project.ProjectName,
            BV = project.BV,
            Date = DateTime.Now,
            BV_Ersatz = project.BV_Ersatz,
            ID = project.ID,
        });
        source.Add(new MainModel
        {
            MusterName = "OSB_KSD_PUR_PYE_zweilagig",
            ProjectName = project.ProjectName,
            BV = project.BV,
            Date = DateTime.Now,
            BV_Ersatz = project.BV_Ersatz,
            ID = project.ID,
        });

        //Trapezblech KSD
        source.Add(new MainModel
        {
            MusterName = "Trapezblech_KSD_EPS_EPDM",
            ProjectName = project.ProjectName,
            BV = project.BV,
            Date = DateTime.Now,
            BV_Ersatz = project.BV_Ersatz,
            ID = project.ID,
        });
        source.Add(new MainModel
        {
            MusterName = "Trapezblech_KSD_EPS_Evalon",
            ProjectName = project.ProjectName,
            BV = project.BV,
            Date = DateTime.Now,
            BV_Ersatz = project.BV_Ersatz,
            ID = project.ID,
        });
        source.Add(new MainModel
        {
            MusterName = "Trapezblech_KSD_EPS_FPO",
            ProjectName = project.ProjectName,
            BV = project.BV,
            Date = DateTime.Now,
            BV_Ersatz = project.BV_Ersatz,
            ID = project.ID,
        });
        source.Add(new MainModel
        {
            MusterName = "Trapezblech_KSD_EPS_PVC",
            ProjectName = project.ProjectName,
            BV = project.BV,
            Date = DateTime.Now,
            BV_Ersatz = project.BV_Ersatz,
            ID = project.ID,
        });
        source.Add(new MainModel
        {
            MusterName = "Trapezblech_KSD_EPS_PYE_einlagig",
            ProjectName = project.ProjectName,
            BV = project.BV,
            Date = DateTime.Now,
            BV_Ersatz = project.BV_Ersatz,
            ID = project.ID,
        });
        source.Add(new MainModel
        {
            MusterName = "Trapezblech_KSD_EPS_PYE_zweilagig",
            ProjectName = project.ProjectName,
            BV = project.BV,
            Date = DateTime.Now,
            BV_Ersatz = project.BV_Ersatz,
            ID = project.ID,
        });
        source.Add(new MainModel
        {
            MusterName = "Trapezblech_KSD_MIFA_EPDM",
            ProjectName = project.ProjectName,
            BV = project.BV,
            Date = DateTime.Now,
            BV_Ersatz = project.BV_Ersatz,
            ID = project.ID,
        });
        source.Add(new MainModel
        {
            MusterName = "Trapezblech_KSD_MIFA_Evalon",
            ProjectName = project.ProjectName,
            BV = project.BV,
            Date = DateTime.Now,
            BV_Ersatz = project.BV_Ersatz,
            ID = project.ID,
        });
        source.Add(new MainModel
        {
            MusterName = "Trapezblech_KSD_MIFA_FPO",
            ProjectName = project.ProjectName,
            BV = project.BV,
            Date = DateTime.Now,
            BV_Ersatz = project.BV_Ersatz,
            ID = project.ID,
        });
        source.Add(new MainModel
        {
            MusterName = "Trapezblech_KSD_MIFA_PVC",
            ProjectName = project.ProjectName,
            BV = project.BV,
            Date = DateTime.Now,
            BV_Ersatz = project.BV_Ersatz,
            ID = project.ID,
        });
        source.Add(new MainModel
        {
            MusterName = "Trapezblech_KSD_MIFA_PYE_einlagig",
            ProjectName = project.ProjectName,
            BV = project.BV,
            Date = DateTime.Now,
            BV_Ersatz = project.BV_Ersatz,
            ID = project.ID,
        });
        source.Add(new MainModel
        {
            MusterName = "Trapezblech_KSD_MIFA_PYE_zweilagig",
            ProjectName = project.ProjectName,
            BV = project.BV,
            Date = DateTime.Now,
            BV_Ersatz = project.BV_Ersatz,
            ID = project.ID,
        });
        source.Add(new MainModel
        {
            MusterName = "Trapezblech_KSD_PUR_EPDM",
            ProjectName = project.ProjectName,
            BV = project.BV,
            Date = DateTime.Now,
            BV_Ersatz = project.BV_Ersatz,
            ID = project.ID,
        });
        source.Add(new MainModel
        {
            MusterName = "Trapezblech_KSD_PUR_Evalon",
            ProjectName = project.ProjectName,
            BV = project.BV,
            Date = DateTime.Now,
            BV_Ersatz = project.BV_Ersatz,
            ID = project.ID,
        });
        source.Add(new MainModel
        {
            MusterName = "Trapezblech_KSD_PUR_FPO",
            ProjectName = project.ProjectName,
            BV = project.BV,
            Date = DateTime.Now,
            BV_Ersatz = project.BV_Ersatz,
            ID = project.ID,
        });
        source.Add(new MainModel
        {
            MusterName = "Trapezblech_KSD_PUR_PVC",
            ProjectName = project.ProjectName,
            BV = project.BV,
            Date = DateTime.Now,
            BV_Ersatz = project.BV_Ersatz,
            ID = project.ID,
        });
        source.Add(new MainModel
        {
            MusterName = "Trapezblech_KSD_PUR_PYE_einlagig",
            ProjectName = project.ProjectName,
            BV = project.BV,
            Date = DateTime.Now,
            BV_Ersatz = project.BV_Ersatz,
            ID = project.ID,
        });
           source.Add(new MainModel
           {
               MusterName = "Trapezblech_KSD_PUR_PYE_zweilagig",
               ProjectName = project.ProjectName,
               BV = project.BV,
               Date = DateTime.Now,
               BV_Ersatz = project.BV_Ersatz,
               ID = project.ID,
           });
        //Trapezblech PE
        source.Add(new MainModel
        {
            MusterName = "Trapezblech_PE_MIFA_EPDM",
            ProjectName = project.ProjectName,
            BV = project.BV,
            Date = DateTime.Now,
            BV_Ersatz = project.BV_Ersatz,
            ID = project.ID,
        });
        source.Add(new MainModel
        {
            MusterName = "Trapezblech_PE_MIFA_Evalon",
            ProjectName = project.ProjectName,
            BV = project.BV,
            Date = DateTime.Now,
            BV_Ersatz = project.BV_Ersatz,
            ID = project.ID,
        });
        source.Add(new MainModel
        {
            MusterName = "Trapezblech_PE_MIFA_FPO",
            ProjectName = project.ProjectName,
            BV = project.BV,
            Date = DateTime.Now,
            BV_Ersatz = project.BV_Ersatz,
            ID = project.ID,
        });
        source.Add(new MainModel
        {
            MusterName = "Trapezblech_PE_MIFA_PVC",
            ProjectName = project.ProjectName,
            BV = project.BV,
            Date = DateTime.Now,
            BV_Ersatz = project.BV_Ersatz,
            ID = project.ID,
        });
        source.Add(new MainModel
        {
            MusterName = "Trapezblech_PE_MIFA_PYE_einlagig",
            ProjectName = project.ProjectName,
            BV = project.BV,
            Date = DateTime.Now,
            BV_Ersatz = project.BV_Ersatz,
            ID = project.ID,
        });
        source.Add(new MainModel
        {
            MusterName = "Trapezblech_PE_MIFA_PYE_zweilagig",
            ProjectName = project.ProjectName,
            BV = project.BV,
            Date = DateTime.Now,
            BV_Ersatz = project.BV_Ersatz,
            ID = project.ID,
        });

        //Umkehrdach
        source.Add(new MainModel
        {
            MusterName = "Umkehrdach",
            ProjectName = project.ProjectName,
            BV = project.BV,
            Date = DateTime.Now,
            BV_Ersatz = project.BV_Ersatz,
            ID = project.ID,
        });
        //WDVS
        source.Add(new MainModel
        {
            MusterName = "WDVS_EPS-032",
            ProjectName = project.ProjectName,
            BV = project.BV,
            Date = DateTime.Now,
            BV_Ersatz = project.BV_Ersatz,
            ID = project.ID,
        });
        source.Add(new MainModel
        {
            MusterName = "WDVS_EPS-035",
            ProjectName = project.ProjectName,
            BV = project.BV,
            Date = DateTime.Now,
            BV_Ersatz = project.BV_Ersatz,
            ID = project.ID,
        });

        source.Add(new MainModel
        {
            MusterName = "KSD_EPS035_PVC",
            ProjectName = project.ProjectName,
            BV = project.BV,
            Date = DateTime.Now,
            BV_Ersatz = project.BV_Ersatz,
            ID = project.ID,
        });

        Aufbau = new ObservableCollection<MainModel>(source);
    }
    void AufbauSelectionChanged()
    {
        SelectedAufbauMessage = $"Selection {selectionCount}:{SelectedAufbau.MusterName}";
        OnPropertyChanged("SelectedAufbauMessage");
        selectionCount++;
    }

    #region INotifyPropertyChanged
    public event PropertyChangedEventHandler PropertyChanged;

    void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
    #endregion

    public void Back_Clicked(object sender, EventArgs e)
    {
        Navigation.PopAsync();
    }
    private async void Next_Clicked(object sender, EventArgs e)
    {
    }
}