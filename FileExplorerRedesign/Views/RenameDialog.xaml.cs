using FileExplorerRedesign.Views.Clase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace FileExplorerRedesign.Views
{
    /// <summary>
    /// Interaction logic for RenameDialog.xaml
    /// </summary>
    public partial class RenameDialog : Window
    {
        public RenameDialog()
        {
            InitializeComponent();
        }



        public string OldFolderName
        {
            get { return (string)GetValue(OldFolderNameProperty); }
            set { SetValue(OldFolderNameProperty, value); }
        }

        // Using a DependencyProperty as the backing store for OldFolderName.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty OldFolderNameProperty =
            DependencyProperty.Register("OldFolderName", typeof(string), typeof(RenameDialog));

        private void renameDialog_Loaded(object sender, RoutedEventArgs e)
        {
            object _datacontext;
            _datacontext = DataContext;
            _datacontext = new WindowBlureffect(this, AccentState.ACCENT_ENABLE_ACRYLICBLURBEHIND) { BlurOpacity = 10 };
        }
    }
}
