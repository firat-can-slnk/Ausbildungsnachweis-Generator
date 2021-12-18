using AusbildungsnachweisGenerator.Model;
using AusbildungsnachweisGenerator.ViewModel;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    public sealed partial class ProfilePage : Page
    {
        public ProfilePage()
        {
            this.InitializeComponent();
            this.DataContext = new ProfilePageViewModel();
        }

        private void ScrollViewer_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (e.NewSize.Width < 960)
            {
                Grid.SetColumn(GridRight, 0);
                Grid.SetRow(GridRight, 2);
            }
            else
            {
                Grid.SetColumn(GridRight, 1);
                Grid.SetRow(GridRight, 1);
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            var dataContext = (ProfilePageViewModel)DataContext;

            var newProfile = new Profile(dataContext.Job, dataContext.Instructor, dataContext.Company, dataContext.Apprenticeship, dataContext.Apprentice, dataContext.Address, DateTime.Now);

            if(dataContext.SelectedProfile == null || dataContext.SelectedProfile.Timestamp == Profile.DefaultTimestamp)
                AppHelper.AddProfile(newProfile);
            else
            {
                AppHelper.AddProfile(newProfile);
                AppHelper.DeleteProfile(dataContext.SelectedProfile);
            }

            dataContext.LoadProfiles();


        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            ((ProfilePageViewModel)DataContext).LoadProfiles();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var dataContext = (ProfilePageViewModel)DataContext;
            dataContext.RefreshFormValidation();
        }

        private void DeleteConfirmation_Click(object sender, RoutedEventArgs e)
        {
            var dataContext = (ProfilePageViewModel)DataContext;
            if (dataContext.SelectedProfile is Profile p && !p.IsForCreation)
            {
                AppHelper.DeleteProfile(p);
                dataContext.LoadProfiles();
            }
        }
    }
}
