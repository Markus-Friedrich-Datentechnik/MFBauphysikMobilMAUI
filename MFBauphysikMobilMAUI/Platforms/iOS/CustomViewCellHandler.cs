using MFBauphysikMobilMAUI.CustomRenderer;
using Microsoft.Maui.Controls.Compatibility.Platform.iOS;
using Microsoft.Maui.Platform;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UIKit;

namespace MFBauphysikMobilMAUI.Platforms.iOS
{
    public class CustomViewCellHandler :ViewCellRenderer
    {
        public override UITableViewCell GetCell(Cell item, UITableViewCell reusableCell, UITableView tv)
        {
            var cell = base.GetCell(item, reusableCell, tv);
            cell.SelectedBackgroundView = new UIView
            {
                BackgroundColor = ((MyViewCell)item).SelectedBackGroundColor.ToPlatform()
            };
            return cell;
        }
    }
}
