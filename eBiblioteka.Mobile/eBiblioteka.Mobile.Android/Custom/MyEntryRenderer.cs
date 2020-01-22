using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.Content.Res;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using eBiblioteka.Mobile;
using eBiblioteka.Mobile.Droid;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(CustomEntry), typeof(MyEntryRenderer))]
namespace eBiblioteka.Mobile.Droid
{
    class MyEntryRenderer : EntryRenderer
    {
        public MyEntryRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                Control.SetMaxLines(1);
                Control.SetMinLines(1);
                Control.BackgroundTintList = ColorStateList.ValueOf(Android.Graphics.Color.White);
                Control.SetTextColor(Android.Graphics.Color.White);
                Control.SetHintTextColor(Resources.GetColor(Resource.Color.colorTransparent60));
            }
        }
    }
}