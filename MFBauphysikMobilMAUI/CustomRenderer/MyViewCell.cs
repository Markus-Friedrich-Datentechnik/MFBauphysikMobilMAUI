using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Maui.Controls;
using Microsoft.Maui;

namespace MFBauphysikMobilMAUI.CustomRenderer
{
    public class MyViewCell : Microsoft.Maui.Controls.ViewCell
    {
        public static readonly BindableProperty SelectedBackgroundColorProperty = BindableProperty.Create(
            nameof(SelectedBackGroundColor), typeof(Color), typeof(MyViewCell), Colors.White);
        public static readonly BindableProperty UnselectedBackgroundColorProperty = BindableProperty.Create(
            nameof(UnselectedBackgroundColor), typeof(Color), typeof(MyViewCell), Colors.White);
        public Color SelectedBackGroundColor
        {
            get { return (Color)GetValue(SelectedBackgroundColorProperty); }
            set { SetValue(SelectedBackgroundColorProperty, value); }
        }

        public Color UnselectedBackgroundColor
        {
            get { return (Color)GetValue(UnselectedBackgroundColorProperty); }
            set { SetValue(UnselectedBackgroundColorProperty, value); }
        }

        public MyViewCell() { }
    }
}
