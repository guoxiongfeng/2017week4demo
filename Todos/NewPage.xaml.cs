using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Popups;


namespace Todos
{
    public sealed partial class NewPage : Page
    {
        public NewPage()
        {
            this.InitializeComponent();
            var viewTitleBar = Windows.UI.ViewManagement.ApplicationView.GetForCurrentView().TitleBar;
            viewTitleBar.BackgroundColor = Windows.UI.Colors.CornflowerBlue;
            viewTitleBar.ButtonBackgroundColor = Windows.UI.Colors.CornflowerBlue;

            //Add Cache Mode
            //this.NavigationCacheMode = Windows.UI.Xaml.Navigation.NavigationCacheMode.Enabled;
        }
        private void Create_Item(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage), "");
        }
        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            if (((App)App.Current).IsSuspend)
            {
                var composite = new ApplicationDataCompositeValue();
                composite["Title"] = Title.Text;
                composite["Details"] = Details.Text;
                composite["DateShower"] = DateShower.Date;
                ApplicationData.Current.LocalSettings.Values["NewPageData"] = composite;
                //Date need to complete
            }
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (ApplicationData.Current.LocalSettings.Values.ContainsKey("NewPageData"))
            {
                var composite = ApplicationData.Current.LocalSettings.Values["NewPageData"] as ApplicationDataCompositeValue;
                if (composite.ContainsKey("Title")) Title.Text = (string)composite["Title"];
                if (composite.ContainsKey("Details")) Details.Text = (string)composite["Details"];
                if (composite.ContainsKey("DateShower")) DateShower.Date = (DateTimeOffset)composite["DateShower"];
            }
            ApplicationData.Current.LocalSettings.Values.Remove("NewPageData");
        }
   }
}
