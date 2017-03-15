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

//“空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409 上有介绍

namespace Todos
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            var viewTitleBar = Windows.UI.ViewManagement.ApplicationView.GetForCurrentView().TitleBar;
            viewTitleBar.BackgroundColor = Windows.UI.Colors.CornflowerBlue;
            viewTitleBar.ButtonBackgroundColor = Windows.UI.Colors.CornflowerBlue;
            //Add Cache Mode
            this.NavigationCacheMode = Windows.UI.Xaml.Navigation.NavigationCacheMode.Enabled;
        }

        private void AddAppBarButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(NewPage), "");
        }
        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            /*if(((App)App.Current).IsSuspend)*/
            {
                var composite = new ApplicationDataCompositeValue();
                composite["CheckBox1"] = CheckBox1.IsChecked;
                composite["CheckBox2"] = CheckBox2.IsChecked;

                /*If it is necessary ?
                composite["Line1"] = Line1.Visibility;
                composite["Line2"] = Line2.Visibility;
                */
                ApplicationData.Current.LocalSettings.Values["MainPageData"] = composite;
                //Date need to complete
            }
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (ApplicationData.Current.LocalSettings.Values.ContainsKey("MainPageData"))
            {
                var composite = ApplicationData.Current.LocalSettings.Values["MainPageData"] as ApplicationDataCompositeValue;
                if (composite.ContainsKey("CheckBox1")) CheckBox1.IsChecked = (bool)composite["CheckBox1"];
                if (composite.ContainsKey("CheckBox2")) CheckBox2.IsChecked = (bool)composite["CheckBox2"];

                //If it is necessary ?
                /*
                if (composite.ContainsKey("Line1")) Line1.Visibility = (Visibility)composite["Line1"];
                if (composite.ContainsKey("Line2")) Line2.Visibility = (Visibility)composite["Line2"];
                */
            }
            CheckClickState();
            ApplicationData.Current.LocalSettings.Values.Remove("MainPageData");
        }

        private void OnCheckBox1Clicked(object sender, RoutedEventArgs e)
        {
            if (Line1.Visibility == Visibility.Visible) Line1.Visibility = Visibility.Collapsed;
            else Line1.Visibility = Visibility.Visible;
        }
        private void OnCheckBox2Clicked(object sender, RoutedEventArgs e)
        {
            if (Line2.Visibility == Visibility.Visible) Line2.Visibility = Visibility.Collapsed;
            else Line2.Visibility = Visibility.Visible;
        }
        private void CheckClickState()
        {
            if (CheckBox1.IsChecked == true) Line1.Visibility = Visibility.Visible;
            if (CheckBox2.IsChecked == true) Line2.Visibility = Visibility.Visible;
        }
    }
}
