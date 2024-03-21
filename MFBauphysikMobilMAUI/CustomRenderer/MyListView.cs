using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Maui.Controls;
using Microsoft.Maui;

namespace MFBauphysikMobilMAUI.CustomRenderer
{
    public class MyListView : ListView
    {
        public static readonly BindableProperty IsScrollingEnableProperty =
            BindableProperty.Create(nameof(IsScrollingEnable), typeof(bool), typeof(MyListView), true);
        public static readonly BindableProperty CircularProperty = 
            BindableProperty.Create(nameof(Circular), typeof(bool), typeof(MyListView), true);

        public bool IsScrollingEnable 
        {
            get { return (bool)GetValue(IsScrollingEnableProperty); }
            set { SetValue(IsScrollingEnableProperty, value); }
        }

        public bool Circular
        {
            get { return (bool)GetValue(CircularProperty); }
            set { SetValue(CircularProperty, value); }
        }
    }
}
