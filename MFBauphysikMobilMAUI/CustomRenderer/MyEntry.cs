using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Maui.Controls;
using Microsoft.Maui;

namespace MFBauphysikMobilMAUI.CustomRenderer
{
    public class MyEntry : Entry
    {
        public static readonly BindableProperty MyHighlightColorProperty = BindableProperty.Create(nameof(MyHighlightColor), typeof(Color), typeof(MyEntry));
        public static readonly BindableProperty MyHandleColorProperty = BindableProperty.Create(nameof(MyHandleColor), typeof(Color), typeof(MyEntry));
        public static readonly BindableProperty MyTintColorProperty = BindableProperty.Create(nameof(MyTintColor), typeof(Color), typeof(MyEntry));
        public static readonly BindableProperty MyBorderColorProperty = BindableProperty.Create(nameof(MyBorderColor), typeof(Color), typeof(MyEntry));
       

        public Color MyHighlightColor
        {
            get => (Color)GetValue(MyHighlightColorProperty);
            set => SetValue(MyHighlightColorProperty, value);
        }

        public Color MyHandleColor
        {
            get => (Color)GetValue(MyHandleColorProperty);
            set => SetValue(MyHandleColorProperty, value);
        }

        public Color MyTintColor
        {
            get => (Color)GetValue(MyTintColorProperty);
            set => SetValue(MyTintColorProperty, value);
        }

        public Color MyBorderColor
        {
            get => (Color)GetValue(MyBorderColorProperty);
            set => SetValue(MyBorderColorProperty, value);
        }


    }
}