using System.Net.Security;
using System.Reflection;
using System.Xml.Linq;
using System.Xml.Serialization;
using MFBauphysikMobilMAUI.Models;
using MFBauphysikMobilMAUI.Helpers;
using System.Web;
using System.Xml;
using System.Collections.ObjectModel;
namespace MFBauphysikMobilMAUI;

public partial class Test : ContentPage
{
    public EventHandler<Bauteile>? BauteilAdded;
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
    public Test()
    {
        InitializeComponent();


        BasisList.Add(new Basis()
        {
            ID_Sort = 1,
            Bezeichnung = "OSB-Platten",
            R = 0.169231,
            Dicke = 0.022,
            Wärmeleitfähigkeit = 0.13,
            Rohdichte = 650.00,
            Kapillar = true,
            Holzwerkstoff = true,
            KeineLuft = true,
            Dampfdiffusionswiderstand_Min = 30.0,
            Dampfdiffusionswiderstand_Max = 50.0,
            Sd_Min = 0.66,
            Sd_Max = 1.10,
        });
        BasisList.Add(new Basis()
        {
            ID_Sort = 2,
            Bezeichnung = "KSD",
            R = 0.008824,
            Dicke = 0.0015,
            Wärmeleitfähigkeit = 0.17,
            Rohdichte = 1000.0,
            Kapillar = true,
            sonstiges = true,
            KeineLuft = true,
            Sd_Min = 1500.00,
            Sd_Max = 1500.00,
            Fester_sd = true,
            Dampfdiffusionswiderstand_Min = 1000000.00,
            Dampfdiffusionswiderstand_Max = 1000000.00
        });
        BasisList.Add(new Basis()
        {
            ID_Sort = 3,
            Bezeichnung = "EPS 035, mit Stufenfalz",
            R = 5.142857,
            Dicke = 0.18,
            Wärmeleitfähigkeit = 0.03500,
            Rohdichte = 30.0,
            Kapillar = true,
            sonstiges = true,
            KeineLuft = true,
            Sd_Min = 3.6,
            Sd_Max = 18.00,
            Dampfdiffusionswiderstand_Min = 20.00,
            Dampfdiffusionswiderstand_Max = 100.00
        });
        BasisList.Add(new Basis()
        {
            ID_Sort = 4,
            Bezeichnung = "Rohglasvlies",
            R = 0.000100,
            Dicke = 0.001,
            Wärmeleitfähigkeit = 10.000000,
            Rohdichte = 400.0,
            sonstiges = true,
            KeineLuft = true,
            Sd_Min = 0.000,
            Sd_Max = 0.00,
            Dampfdiffusionswiderstand_Min = 1.0,
            Dampfdiffusionswiderstand_Max = 1.0
        });
        BasisList.Add(new Basis()
        {
            ID_Sort = 5,
            Bezeichnung = "PVC-P (DIN 16730)",
            R = 0.00000,
            Dicke = 0.0015,
            Wärmeleitfähigkeit = 0.000000,
            Rohdichte = 1200.0,
            sonstiges = true,
            Kapillar = true,
            KeineLuft = true,
            Fester_R = true,
            Sd_Min = 15.000,
            Sd_Max = 45.00,
            Dampfdiffusionswiderstand_Min = 10000.00,
            Dampfdiffusionswiderstand_Max = 30000.00
        });

        listBefestiger.ItemsSource = BasisList;

    }

    private void listBefestiger_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        var selectedItem = (e.SelectedItem as WLG)!;
        Console.WriteLine(selectedItem.B);

    }
}