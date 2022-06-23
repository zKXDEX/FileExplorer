using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace FileExplorerRedesign.Custom_Controls
{
    public class DriveAndFolderButton : RadioButton
    {
        public PathGeometry Icon
        {
            get => (PathGeometry) GetValue(IconProperty);
            set => SetValue(IconProperty, value);
        }

        // Using a DependencyProperty as the backing store for Icon.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IconProperty =
            DependencyProperty.Register("Icon", typeof(PathGeometry), typeof(DriveAndFolderButton));


        public Brush IconFill
        {
            get { return (Brush)GetValue(IconFillProperty); }
            set { SetValue(IconFillProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IconFill.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IconFillProperty =
            DependencyProperty.Register("IconFill", typeof(Brush), typeof(DriveAndFolderButton));


        public ICommand UnPinCommand
        {
            get { return (ICommand)GetValue(UnPinCommandProperty); }
            set { SetValue(UnPinCommandProperty, value); }
        }

        // Using a DependencyProperty as the backing store for UnPinCommand.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty UnPinCommandProperty =
            DependencyProperty.Register("UnPinCommand", typeof(ICommand), typeof(DriveAndFolderButton));


    }
}