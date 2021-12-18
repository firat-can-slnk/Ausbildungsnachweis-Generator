using AusbildungsnachweisGenerator.Extensions;
using AusbildungsnachweisGenerator.Model;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace AusbildungsnachweisGenerator
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : Window
    {
        public MainWindow()
        {
            this.InitializeComponent();
        }

        private void myButton_Click(object sender, RoutedEventArgs e)
        {
            Proof.Sample.GenerateDocument();
        }

        private void NavigationView_ItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
        {
            FrameNavigationOptions navOptions = new FrameNavigationOptions
            {
                TransitionInfoOverride = args.RecommendedNavigationTransitionInfo
            };

            if (sender.PaneDisplayMode == NavigationViewPaneDisplayMode.Top)
            {
                navOptions.IsNavigationStackEnabled = false;
            }
            Type pageType = typeof(StartPage);

            if (args.InvokedItem == StartPageItem)
            {
                pageType = typeof(StartPage);
            }
            else if (args.IsSettingsInvoked)
            {
                pageType= typeof(SettingsPage);
            }

            contentFrame.NavigateToType(pageType, null, navOptions);

        }

        private void NavigationViewMain_Loaded(object sender, RoutedEventArgs e)
        {
            contentFrame.NavigateToType(typeof(StartPage), null, null);
        }
    }
}
