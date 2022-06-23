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
    /// Interaction logic for PropertiesDialog.xaml
    /// </summary>
    public partial class PropertiesDialog : Window
    {
        public PropertiesDialog()
        {
            InitializeComponent();
        }



        public PathGeometry Icon
        {
            get { return (PathGeometry)GetValue(IconProperty); }
            set { SetValue(IconProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Icon.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IconProperty =
            DependencyProperty.Register("Icon", typeof(PathGeometry), typeof(PropertiesDialog));



        public string FileName
        {
            get { return (string)GetValue(FileNameProperty); }
            set { SetValue(FileNameProperty, value); }
        }

        // Using a DependencyProperty as the backing store for FileName.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty FileNameProperty =
            DependencyProperty.Register("FileName", typeof(string), typeof(PropertiesDialog));



        public string FileExtension
        {
            get { return (string)GetValue(FileExtensionProperty); }
            set { SetValue(FileExtensionProperty, value); }
        }

        // Using a DependencyProperty as the backing store for FileExtension.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty FileExtensionProperty =
            DependencyProperty.Register("FileExtension", typeof(string), typeof(PropertiesDialog));

        public string Extension
        {
            get => (string)GetValue(ExtensionProperty);
            set => SetValue(ExtensionProperty, value);
        }

        // Using a DependencyProperty as the backing store for Extension.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ExtensionProperty =
            DependencyProperty.Register("Extension", typeof(string), typeof(PropertiesDialog));



        public string FullPath
        {
            get => (string)GetValue(FullPathProperty);
            set => SetValue(FullPathProperty, value);
        }

        // Using a DependencyProperty as the backing store for FullPath.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty FullPathProperty =
            DependencyProperty.Register("FullPath", typeof(string), typeof(PropertiesDialog));



        public string Size
        {
            get => (string)GetValue(SizeProperty);
            set => SetValue(SizeProperty, value);
        }

        // Using a DependencyProperty as the backing store for Size.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SizeProperty =
            DependencyProperty.Register("Size", typeof(string), typeof(PropertiesDialog));



        public string CreatedOn
        {
            get => (string)GetValue(CreatedOnProperty);
            set => SetValue(CreatedOnProperty, value);
        }

        // Using a DependencyProperty as the backing store for CreatedOn.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CreatedOnProperty =
            DependencyProperty.Register("CreatedOn", typeof(string), typeof(PropertiesDialog));



        public string DateModified
        {
            get => (string)GetValue(DateModifiedProperty);
            set => SetValue(DateModifiedProperty, value);
        }

        // Using a DependencyProperty as the backing store for DateModified.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DateModifiedProperty =
            DependencyProperty.Register("DateModified", typeof(string), typeof(PropertiesDialog));



        public string AccessedOn
        {
            get => (string)GetValue(AccessedOnProperty);
            set => SetValue(AccessedOnProperty, value);
        }

        // Using a DependencyProperty as the backing store for AccessedOn.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AccessedOnProperty =
            DependencyProperty.Register("AccessedOn", typeof(string), typeof(PropertiesDialog));



        public bool IsReadOnly
        {
            get => (bool)GetValue(IsReadOnlyProperty);
            set => SetValue(IsReadOnlyProperty, value);
        }

        // Using a DependencyProperty as the backing store for IsReadOnly.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsReadOnlyProperty =
            DependencyProperty.Register("IsReadOnly", typeof(bool), typeof(PropertiesDialog));



        public bool IsHidden
        {
            get => (bool)GetValue(IsHiddenProperty);
            set => SetValue(IsHiddenProperty, value);
        }

        // Using a DependencyProperty as the backing store for IsHidden.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsHiddenProperty =
            DependencyProperty.Register("IsHidden", typeof(bool), typeof(PropertiesDialog));

        private void PropertyDialog_Loaded(object sender, RoutedEventArgs e)
        {
            object _datacontext;
            _datacontext = DataContext;
            _datacontext = new WindowBlureffect(this, AccentState.ACCENT_ENABLE_ACRYLICBLURBEHIND) { BlurOpacity = 10 };
        }
    }
}
