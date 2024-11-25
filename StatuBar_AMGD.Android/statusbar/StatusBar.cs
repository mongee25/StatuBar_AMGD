using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Plugin.CurrentActivity;
using StatuBar_AMGD.Droid.statusbar;
using StatuBar_AMGD.VistaModelo;
using Xamarin.Forms;

[assembly:Dependency(typeof(StatusBar))]

namespace StatuBar_AMGD.Droid.statusbar
{
    internal class StatusBar : VMstatusbar
    {
        WindowManagerFlags _originalFlag;

        Window GetCurrentwindow()
        {
            var window = CrossCurrentActivity.Current.Activity.Window;
            window.ClearFlags(WindowManagerFlags.TranslucentStatus);
            window.AddFlags(WindowManagerFlags.DrawsSystemBarBackgrounds);
            return window;
        }
        public void CambiarColor()
        {
            if (Build.VERSION.SdkInt >= BuildVersionCodes.M)
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    var currentWindow = GetCurrentwindow();
                    currentWindow.DecorView.SystemUiVisibility = (StatusBarVisibility)SystemUiFlags.LayoutStable;
                    currentWindow.SetStatusBarColor(Android.Graphics.Color.Transparent);
                });               
            }
            throw new NotImplementedException();
        }
        public void MostrarStatusBar() 
        {
            var activity = (Activity)Forms.Context;
            var attrs = activity.Window.Attributes;
            attrs.Flags = _originalFlag;
            activity.Window.Attributes = attrs;
        }
        public void OcultarStatusBar() 
        {
            var activity = (Activity)Forms.Context;
            var attrs = activity.Window.Attributes;
            _originalFlag = attrs.Flags;
            attrs.Flags |= WindowManagerFlags.Fullscreen;
            activity.Window.Attributes = attrs;
        }
        public void Transparente()
        {
            MostrarStatusBar();
            if (Build.VERSION.SdkInt >= BuildVersionCodes.M)
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    var currentWindow = GetCurrentwindow();
                    currentWindow.DecorView.SystemUiVisibility = (StatusBarVisibility)SystemUiFlags.LayoutFullscreen;
                    currentWindow.SetStatusBarColor(Android.Graphics.Color.Transparent);
                });
            }
        }
        public void Traslucido()
        {
            MostrarStatusBar();
            var activity = (Activity)Forms.Context;
            var attrs = activity.Window.Attributes;
            _originalFlag = attrs.Flags;
            attrs.Flags |= WindowManagerFlags.TranslucentStatus;
            activity.Window.Attributes = attrs;
        }
    }
}