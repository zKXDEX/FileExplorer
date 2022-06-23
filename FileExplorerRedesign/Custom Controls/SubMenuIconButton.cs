using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace FileExplorerRedesign.Custom_Controls
{
    public class SubMenuIconButton : Button
    {
        public PathGeometry Icon
        {
            get => (PathGeometry) GetValue(IconProperty);
            set => SetValue(IconProperty, value);
        }

        // Using a DependencyProperty as the backing store for Icon.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IconProperty =
            DependencyProperty.Register("Icon", typeof(PathGeometry), typeof(SubMenuIconButton));
    }
}