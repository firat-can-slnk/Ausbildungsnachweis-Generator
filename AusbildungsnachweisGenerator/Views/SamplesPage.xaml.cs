using AusbildungsnachweisGenerator.ViewModel;
using CommunityToolkit.WinUI.UI.Controls;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace AusbildungsnachweisGenerator.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SamplesPage : Page
    {
        public SamplesPage()
        {
            InitializeComponent();
            DataContext = new SamplesPageViewModel();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            // Workaround because the app goes to an infinite loop (PanelItem.Title.Get() gets called infinitely)
            // if the layout is UniformGridLayout on start
            //PanelItemsRepeater.Layout = this.Resources["MyUniformGridLayout"] as UniformGridLayout; // Not working good either
        }

        private void DataGrid_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyName == "Meaning")
            {
                e.Column.Header = "Bedeutung";
            }
        }
    }
}
