using System.Net.Security;
using System.Reflection;
using System.Xml.Linq;
using System.Xml.Serialization;
using MFBauphysikMobilMAUI.Models;
using MFBauphysikMobilMAUI.Helpers;
using System.Web;
using System.Xml;
namespace MFBauphysikMobilMAUI;

public partial class Test : ContentPage
{
    public EventHandler<Bauteile>? BauteilAdded;

    public Test()
	{
        InitializeComponent();
        this.BindingContext = this;
        var assembly = typeof(App).Assembly;
        Stream? stream = assembly.GetManifestResourceStream("MFBauphysikMobilMAUI.NewProject.Bauteile.xml");
        XDocument doc = XDocument.Load(stream);
        var list = doc.Root.Elements("WLG").Select(element =>
        new WLG
        {
            B = element.Element("B").Value,
        }).ToList();
        listBefestiger.ItemsSource = list;

       /*this.BindingContext = this;
        var assembly = typeof(App).Assembly;
        Stream? stream = assembly.GetManifestResourceStream("MFBauphysikMobilMAUI.Resources.Raw.Bauteil.xml");
        XmlSerializer serializer = new XmlSerializer(typeof(List<WLG>));
        var list = (List<WLG>)serializer.Deserialize(stream!)!;
        listBefestiger.ItemsSource = list;*/
    }

    private void listBefestiger_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        var selectedItem = (e.SelectedItem as WLG)!;
        Console.WriteLine(selectedItem.B);

    }
}