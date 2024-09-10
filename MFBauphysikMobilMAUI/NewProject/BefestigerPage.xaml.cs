using MFBauphysikMobilMAUI.Helpers;
using MFBauphysikMobilMAUI.Models;
using MFBauphysikMobilMAUI.NewProject;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;
using Microsoft.Maui.Controls.Xaml;
using Microsoft.Maui.Controls;
using Microsoft.Maui;
using MFBauphysikMobilMAUI;
using System.ComponentModel;
using System.Windows.Input;
using System.Runtime.CompilerServices;


namespace MFBauphysikMobilMAUI.NewProject
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BefestigerPage : ContentPage
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
        private double _size_default;
        public double SizeDefault
        {
            get { return _size_default; }
            set
            {
                if ( _size_default == value)
                    return;
                _size_default = value; OnPropertyChanged(nameof(SizeDefault));
            }
        }
        public Befestiger? NewItem { get; set; }
        public EventHandler<Befestiger>? BefestigerAdded;
        List<BF>? _befestiger;
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
        public string SelectedAufbauMessage { get; private set; }

       // public ICommand AufbauSelectionChangedCommand => new Command(AufbauSelectionChanged);
        public BefestigerPage()
        {
            InitializeComponent();
            this.BindingContext = this;
            var assembly = (typeof(App)).Assembly;
            Stream stream = assembly.GetManifestResourceStream("MFBauphysikMobil.BefestigerExport.xml");
            using (var reader = new StreamReader(stream))
            {
                var serializer = new XmlSerializer(typeof(List<BF>));
                _befestiger = (List<BF>)serializer.Deserialize(reader);
            }
            listBefestiger.ItemsSource = _befestiger.OrderBy(p => p.B);
             foreach (BF i in _befestiger)
             {
                 i.SizeClass = Setting.Size_Default;
             }
            SizeTitle = Setting.Size_Title;
            
           // source = new List<Befestiger>();
            /*CreateAufbauCollection();

            selectedAufbau = Aufbau.Skip(3).FirstOrDefault();
            AufbauSelectionChanged();

            SelectedAufbaus = new ObservableCollection<object>()
            {
            Aufbau[1], Aufbau[2], Aufbau[3]
            };*/
        }

       /* protected override async void OnAppearing()
        {
            base.OnAppearing();
            CreateAufbauCollection();

            selectedAufbau = Aufbau.Skip(3).FirstOrDefault();
            AufbauSelectionChanged();

            SelectedAufbaus = new ObservableCollection<object>()
            {
            Aufbau[1], Aufbau[2], Aufbau[3]
            };

        }*/

      /*  void CreateAufbauCollection()
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
            foreach(Befestiger i in Aufbau)
            {
                i.Size_Def = Setting.Size_Default;
            }
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
        #endregion*/

        public async void Back_Clicked (object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
        public async void Check_Clicked(object sender, EventArgs e) 
        {
            if (NewItem == null)
            {
                await DisplayAlert("Achtung", "Bitte wählen Sie einen Befestiger aus", "OK");
            }
            else
            {                
                BefestigerAdded?.Invoke(this, NewItem);
                await Navigation.PopAsync();
            }
        }

        private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            var itemsource = _befestiger!;
            listBefestiger.ItemsSource = itemsource.Where(p => p.B.ToLower().Contains(e.NewTextValue)).OrderBy(p => p.B);
        }

        private void listBefestiger_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {      
            var selectedBefestiger = (e.SelectedItem as BF)!;
            NewItem = new Befestiger()
            {
                Bezeichnung = selectedBefestiger.B,
                Wärmeleitfähigkeit_f = Convert.ToDouble(selectedBefestiger.LR),
                Durchmesser = Convert.ToDouble(selectedBefestiger.DN),
                Eindringtiefe = Convert.ToDouble(selectedBefestiger.ET),
            };
        }

      /*  private void listBefestiger_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Befestiger selectedItem = (e.CurrentSelection.FirstOrDefault() as Befestiger)!;
            NewItem = selectedItem;
        }*/
    }
}