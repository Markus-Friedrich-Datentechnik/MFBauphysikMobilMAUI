using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Maui.Controls;
using Microsoft.Maui;

namespace MFBauphysikMobilMAUI.CustomRenderer
{
    public class MyTextCell : TextCell
    {
       public static readonly BindableProperty SelectedBackgroundColorProperty =
       BindableProperty.Create("SelectedBackgroundColor",
                               typeof(Color),
                               typeof(MyTextCell),
                               null);
        public Color SelectedBackGroundColor
        {
            get { return (Color)GetValue(SelectedBackgroundColorProperty); }
            set { SetValue(SelectedBackgroundColorProperty, value); }
        }
    }
}
