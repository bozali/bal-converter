﻿using Microsoft.UI.Xaml;
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
using Bal.Converter.Modules.MediaDownloader.ViewModels;
using Bal.Converter.YouTubeDl;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Bal.Converter.Modules.MediaDownloader.Views;
/// <summary>
/// An empty page that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class PlaylistOverviewPage : Page
{
    public PlaylistOverviewPage()
    {
        this.ViewModel = App.GetService<PlaylistOverviewViewModel>();

        this.InitializeComponent();
    }

    public PlaylistOverviewViewModel ViewModel { get; set; }
}
