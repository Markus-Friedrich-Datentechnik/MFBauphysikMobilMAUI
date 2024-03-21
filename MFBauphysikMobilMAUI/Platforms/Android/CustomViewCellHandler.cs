using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AView = Android.Views.View;
using AContext = Android.Content.Context;
using AViewGroup = Android.Views.ViewGroup;
using Android.Graphics.Drawables;
using System.ComponentModel;
using Microsoft.Maui.Controls.Platform;
using MFBauphysikMobilMAUI.CustomRenderer;
using Microsoft.Maui.Controls.Compatibility.Platform.Android;

namespace MFBauphysikMobilMAUI.Platforms.Android
{
    public class CustomViewCellHandler : Microsoft.Maui.Controls.Handlers.Compatibility.ViewCellRenderer
    {
        private AView pCellCore;
        private bool pSelected;
        private Drawable pUnselectedBackground;

        protected override AView GetCellCore(Cell item, AView convertView, AViewGroup parent, AContext context)
        {
            pCellCore = base.GetCellCore(item, convertView, parent, context);
            this.pSelected = false;
            this.pUnselectedBackground = pCellCore.Background;
            return pCellCore;
        }
        protected override void OnCellPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnCellPropertyChanged(sender, e);
            if(e.PropertyName == "IsSelected")
            {
                pSelected = !(pSelected);
                if (pSelected)
                {
                    pCellCore.SetBackgroundColor(((MyViewCell)sender).SelectedBackGroundColor.ToAndroid());
                }
                else
                {
                    pCellCore.SetBackground(this.pUnselectedBackground);
                }
            }
        }
    }
}
