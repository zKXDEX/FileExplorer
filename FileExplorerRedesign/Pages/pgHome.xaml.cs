using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using FileExplorerRedesign.Annotations;
using FileExplorerRedesign.Commands;
using FileExplorerRedesign.Models;
using FileExplorerRedesign.Views;
using FileExplorerRedesign.Views.Clase;
using Microsoft.VisualBasic.FileIO;
using Syroot.Windows.IO;
using SearchOption = System.IO.SearchOption;

namespace FileExplorerRedesign.Pages
{
    /// <summary>
    /// Lógica de interacción para pgHome.xaml
    /// </summary>
    public partial class pgHome : Page
    {
        public pgHome()
        {
            InitializeComponent();
            

        }


        private void btndownload_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }
    }
}
