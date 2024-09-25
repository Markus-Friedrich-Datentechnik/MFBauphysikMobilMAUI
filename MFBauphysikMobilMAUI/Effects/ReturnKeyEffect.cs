using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MFBauphysikMobil.Effects
{
    public class ReturnKeyEffect : RoutingEffect
    {
        public string ReturnText { get; set; }

        public ReturnKeyEffect()
            : base("Brax.ReturnKeyEffect")
        { }
    }
/*#if ANDROID
    internal class ReturnKeyPlatformEffect : PlatformEffect
    {
        protected override void OnAttached()
        {
            // Customize the control here
        }

        protected override void OnDetached()
        {
            // Cleanup the control customization here
        }
    }
#elif IOS
    internal class ReturnKeyPlatformEffect : PlatformEffect
    {
        protected override void OnAttached()
        {
            // Customize the control here
        }

        protected override void OnDetached()
        {
            // Cleanup the control customization here
        }
    }
#elif WINDOWS
    internal class ReturnKeyPlatformEffect : PlatformEffect
    {
        protected override void OnAttached()
        {
            // Customize the control here
        }

        protected override void OnDetached()
        {
            // Cleanup the control customization here
        }
    }
#endif*/

}



