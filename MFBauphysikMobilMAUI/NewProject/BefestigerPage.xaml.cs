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


namespace MFBauphysikMobilMAUI.NewProject
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BefestigerPage : ContentPage
    {
        public Befestiger? NewItem { get; set; }
        public EventHandler<Befestiger>? BefestigerAdded;
        List<BF>? _befestiger;
        public BefestigerPage()
        {
            InitializeComponent();
            this.BindingContext = this;

            var assembly = typeof(App).Assembly;
            //Error stream ist gerade 
            Stream? stream = assembly.GetManifestResourceStream("MFBauphysikMobilMAUI.Resources.Raw.Befestiger_Export.xml");
            XmlSerializer serializer = new XmlSerializer(typeof(List<BF>));
            _befestiger = (List<BF>)serializer.Deserialize(stream!)!;

            listBefestiger.ItemsSource = _befestiger.OrderBy(p => p.B);
            foreach (BF i in _befestiger)
            {
                i.SizeClass = Setting.Size_Default;
            }
           
        }
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

    }
}