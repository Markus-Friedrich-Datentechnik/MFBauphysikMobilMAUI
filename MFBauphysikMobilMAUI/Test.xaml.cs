using System.Net.Security;
using System.Reflection;
using System.Xml.Linq;
using System.Xml.Serialization;
using MFBauphysikMobilMAUI.Models;
using MFBauphysikMobilMAUI.Helpers;
using System.Web;
using System.Xml;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using System.Runtime.CompilerServices;
using MFBauphysikMobilMAUI.NewProject;
namespace MFBauphysikMobilMAUI;

public partial class Test : ContentPage, INotifyPropertyChanged
{
    readonly IList<Befestiger> source;
    Befestiger selectedAufbau;
    int selectionCount = 1;

    public ObservableCollection<Befestiger> Aufbau { get; private set; }
    public IList<Befestiger> EmptyAufbau { get; private set; }

    public Befestiger SelectedAufbau
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
    List<BF>? _befestiger;


    public string SelectedAufbauMessage { get; private set; }

    public ICommand AufbauSelectionChangedCommand => new Command(AufbauSelectionChanged);
    public Test()
    {
        InitializeComponent();

        var assembly = (typeof(App)).Assembly;
        Stream stream = assembly.GetManifestResourceStream("MFBauphysikMobil.BefestigerExport.xml");
        using (var reader = new StreamReader(stream))
        {
            var serializer = new XmlSerializer(typeof(List<BF>));
            _befestiger = (List<BF>)serializer.Deserialize(reader);
        }
        source = new List<Befestiger>();
        CreateAufbauCollection();

        selectedAufbau = Aufbau.Skip(3).FirstOrDefault();
        AufbauSelectionChanged();

        SelectedAufbaus = new ObservableCollection<object>()
        {
            Aufbau[1], Aufbau[2], Aufbau[3]
        };
    }
    void CreateAufbauCollection()
    {
        List<Befestiger> testList = new List<Befestiger>();
        foreach (BF i in _befestiger)
        {
            source.Add(new Befestiger
            {
                Bezeichnung = i.B,
                Wärmeleitfähigkeit_f = Convert.ToDouble(i.LR),
                Durchmesser = Convert.ToDouble(i.DN),
            });
        }
        Aufbau = new ObservableCollection<Befestiger>(source.OrderBy(p => p.Bezeichnung));
        BindingContext = this;
    }
    void AufbauSelectionChanged()
    {
        SelectedAufbauMessage = $"Selection {selectionCount}:{SelectedAufbau.Bezeichnung}";
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
    private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
    {
        var item = source;
        collection_view.ItemsSource = item.Where(p => p.Bezeichnung.ToLower().Contains(e.NewTextValue)).OrderBy(p => p.Bezeichnung);
        //listView.ItemsSource = item.Where(p => p.MusterName.ToLower().Contains(e.NewTextValue)).OrderBy(p => p.MusterName);
    }

    private void collection_view_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {      
    }
}

