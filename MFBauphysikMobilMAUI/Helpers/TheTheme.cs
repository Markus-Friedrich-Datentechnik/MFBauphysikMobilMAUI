using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Microsoft.Maui.ApplicationModel;
using Microsoft.Maui.Controls;
using Microsoft.Maui;
using TestBauphysikMaui;

namespace MFBauphysikMobilMAUI.Helpers
{
    public static class TheTheme
    {
        public static void SetTheme()
        {
            switch (Setting.Theme)
            {
                case 0:
                    App.Current.UserAppTheme = AppTheme.Unspecified;                    
                    break;
                case 1:
                    App.Current.UserAppTheme = AppTheme.Light;
                    break;
                case 2:
                    App.Current.UserAppTheme= AppTheme.Dark;
                    break;
            }
        }
    }
}
