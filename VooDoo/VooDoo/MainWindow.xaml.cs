using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using VooDoo.ViewModel;

namespace VooDoo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //VooDoo.ViewModel.MainViewModel vm = new VooDoo.ViewModel.MainViewModel();

        public MainWindow()
        {
            InitializeComponent();
            //DataContext = vm;

            Height = Settings.AppSettings.Instance.Size.Height;
            Width = Settings.AppSettings.Instance.Size.Width;
        }

        private void VooDooMainWindow_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            (DataContext as MainViewModel).OnSizeChanged(sender, e);
        }





        private void Canvas_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            (DataContext as MainViewModel).OnPreviewMouseLeftButtonDown(taskListGrid, Canvas, e);
        }

        
        private void Canvas_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            (DataContext as MainViewModel).OnPreviewMouseMove(taskListGrid, Canvas, e);
        }

        private void Canvas_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            (DataContext as MainViewModel).OnPreviewMouseLeftButtonUp(taskListGrid, Canvas, e);
        }

        private void taskListGrid_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            (DataContext as MainViewModel).OnBeginningEdit(sender, e);
        }

     
        private void taskListGrid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            (DataContext as MainViewModel).OnCellEditEnding(sender, e);
        }
    }
}