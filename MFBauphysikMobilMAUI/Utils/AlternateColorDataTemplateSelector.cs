using MFBauphysikMobilMAUI.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Microsoft.Maui.Controls;
using Microsoft.Maui;

namespace MFBauphysikMobilMAUI.Utils
{
    public class AlternateColorDataTemplateSelector : DataTemplateSelector
    {
        public DataTemplate EvenTemplate { get; set; }
        public DataTemplate UnevenTemplate { get; set; }
        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            return ((ObservableCollection<Bauteile>)((ListView)container).ItemsSource).IndexOf(item as Bauteile) % 2 == 0 ? EvenTemplate : UnevenTemplate;
        }
    }
}
