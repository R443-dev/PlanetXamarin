﻿using System;
using System.Windows;
using System.Windows.Controls;
using CrossPlatformInVS.Portable.Models;
using CrossPlatformInVS.Portable.ViewModels;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace CrossPlatformInVS.Phone
{
  public partial class MainPage : PhoneApplicationPage
  {
    private static MasterViewModel viewModel;

    public static MasterViewModel ViewModel
    {
      get { return viewModel ?? (viewModel = new MasterViewModel()); }
    }
    // Constructor
    public MainPage()
    {
      InitializeComponent();
      this.Loaded += OnLoaded;
    }

    private void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
    {
      DataContext = ViewModel;

      ((ApplicationBarIconButton) ApplicationBar.Buttons[0]).Click += (o, args) => {
        ViewModel.LoadItemsCommand.Execute(null);
      };

    }

    private void FeedList_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
      if (FeedList.SelectedItem == null)
        return;

      ViewModel.SelectedFeedItem = (RSSFeedItem)FeedList.SelectedItem;

      NavigationService.Navigate(new Uri("/DetailsPage.xaml?id=" + ViewModel.SelectedFeedItem.Id, UriKind.Relative));
      FeedList.SelectedItem = null;
    }
  }
}