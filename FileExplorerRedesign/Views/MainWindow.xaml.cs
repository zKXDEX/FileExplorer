using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Shapes;
using FileExplorerRedesign.ViewModels;
using FileExplorerRedesign.Views.Clase;

namespace FileExplorerRedesign.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            btnhome.IsChecked = true;
            userpriv.Content = Environment.UserName;
        }

        private void MinimizeButton_OnClick(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void MaximizeButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (WindowState == WindowState.Maximized)
                WindowState = WindowState.Normal;
            else
            {
                WindowState = WindowState.Maximized;
            }
        }

        private void CloseButton_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void DarkLightModeToggleButton_OnClick(object sender, RoutedEventArgs e)
        {
            //Properties.Settings.Default.Save();
        }

        Point _InitPos;
        bool _LeftMouseHeld = false;
        private List<object> _ResultsList = new List<object>();

        private void selectionpanel_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var Limit = (FrameworkElement)sender;
            foreach (Rectangle rectangle in _ResultsList)
            {
                rectangle.StrokeThickness = 1;
            }
            _ResultsList.Clear();

            // Get position relative to the grid content
            _InitPos = e.GetPosition(Limit);

            // With this, selection is more fluid but you can draw beyond limit of your container
            Limit.CaptureMouse();

            // Initialization of the SelectBox
            Canvas.SetLeft(SelectBox, _InitPos.X);
            Canvas.SetTop(SelectBox, _InitPos.X);
            SelectBox.Visibility = Visibility.Visible;

            // Set left mouse button state because mosemove will continue to draw if not filtered
            _LeftMouseHeld = true;

            Debug.WriteLine($"{_InitPos.X}, {_InitPos.Y}");
        }

        private void selectionpanel_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var Limit = (FrameworkElement)sender;

            // Set left mouse button state to released
            _LeftMouseHeld = false;

            Limit.ReleaseMouseCapture();

            // Hide all the listbox (if you forget to specify width and height you will have remanent coordinates
            SelectBox.Visibility = Visibility.Collapsed;
            SelectBox.Width = 0;
            SelectBox.Height = 0;

            if (_ResultsList.Count > 0)
            {
                foreach (FrameworkElement r in _ResultsList)
                    Debug.WriteLine(r.Name);
            }
        }

        private void selectionpanel_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            Grid Limit = (Grid)sender;

            // to calculate only if left mouse button is held;
            if (_LeftMouseHeld)
            {
                // Get current position relative to the grid content
                Point currentPos = e.GetPosition(Limit);

                /*
                    Parameters can't be negative then we will invert base according to mouse value
                */

                // X coordinates
                if (currentPos.X > _InitPos.X)
                {
                    Canvas.SetLeft(SelectBox, _InitPos.X);
                    SelectBox.Width = currentPos.X - _InitPos.X;
                }
                else
                {
                    Canvas.SetLeft(SelectBox, currentPos.X);
                    SelectBox.Width = _InitPos.X - currentPos.X;
                }

                // Y coordinates
                if (currentPos.Y > _InitPos.Y)
                {
                    Canvas.SetTop(SelectBox, _InitPos.Y);
                    SelectBox.Height = currentPos.Y - _InitPos.Y;
                }
                else
                {
                    Canvas.SetTop(SelectBox, currentPos.Y);
                    SelectBox.Height = _InitPos.Y - currentPos.Y;
                }

                /*
                 * With a rectangle geometry you could add every shapes INSIDE the rectangle
                 * With a point geometry you must go over the shape to select it.
                 */
                VisualTreeHelper.HitTest(ListViewControl,
                    new HitTestFilterCallback(Filter),
                    new HitTestResultCallback(MyHitTestResult),
                    /*new PointHitTestParameters(currentPos)*/
                    new GeometryHitTestParameters(new RectangleGeometry(new Rect(_InitPos, currentPos)))
                    );
            }
        }

        private HitTestFilterBehavior Filter(DependencyObject potentialHitTestTarget)
        {
            if (potentialHitTestTarget is Rectangle)
            {
                if (!_ResultsList.Contains(potentialHitTestTarget))
                {

                    _ResultsList.Add(potentialHitTestTarget);
                    ((Rectangle)potentialHitTestTarget).StrokeThickness = 5;
                }
                return HitTestFilterBehavior.ContinueSkipChildren;
            }

            return HitTestFilterBehavior.Continue;

        }


        // Return the result of the hit test to the callback.
        public HitTestResultBehavior MyHitTestResult(HitTestResult result)
        {
            // Set the behavior to return visuals at all z-order levels.
            return HitTestResultBehavior.Continue;
        }


        private void btnhome_Checked(object sender, RoutedEventArgs e)
        {
            if (btnhome.IsChecked == true)
            {
                ListViewControl.Visibility = Visibility.Hidden;
                AddressBar.Text = "Inicio";
                navigatepagehome.Visibility = Visibility.Visible;
                navigatepagehome.Navigate(new Uri("Pages/pgHome.xaml", UriKind.RelativeOrAbsolute));
                dockitemcount.Visibility = Visibility.Hidden;

            }
            else if (btnhome.IsChecked == false)
            {
                ListViewControl.Visibility = Visibility.Visible;
                navigatepagehome.Visibility = Visibility.Hidden;
                dockitemcount.Visibility = Visibility.Visible;
            }
        }

        private void drivefolder_Checked(object sender, RoutedEventArgs e)
        {
            bool btcheck = false;
            if (btnhome.IsChecked == false)
            {
                ListViewControl.Visibility = Visibility.Visible;
                navigatepagehome.Visibility = Visibility.Hidden;
                btcheck = true;
                dockitemcount.Visibility = Visibility.Visible;
                

            }
            else
            {
                ListViewControl.Visibility = Visibility.Hidden;
                navigatepagehome.Visibility = Visibility.Visible;
                dockitemcount.Visibility = Visibility.Hidden;
                btcheck = true;
            }
        }

        private void btnhome_Checked_1(object sender, RoutedEventArgs e)
        {

        }

        private void mainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            object datacontext;
            datacontext = DataContext;
            datacontext = new WindowBlureffect(this, AccentState.ACCENT_ENABLE_ACRYLICBLURBEHIND) { BlurOpacity = 10 };
        }

        private void MIRename_Click(object sender, RoutedEventArgs e)
        {
            //click button
            
        }

        public void RenameFunction()
        {
            ViewModel viewModel = new ViewModel();
            viewModel.Rename();
        }
    }

    public class BoolToVisibilityConverter:IValueConverter
    {
        #region Implementation of IValueConverter

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value != null && (bool) value ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
