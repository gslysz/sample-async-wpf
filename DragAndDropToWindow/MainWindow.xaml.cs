using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using DragAndDropToWindow.ViewModel;
using DragAndDropToWindow.Views;
using GwsUtils;

namespace DragAndDropToWindow
{
    /// <summary>
    ///   Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool _isWindowOpen;
        private PersonVm _personVm;
        private DraggingWindow _draggingWindow;

        #region Constructors

        public MainWindow()
        {
            InitializeComponent();



            var mainViewModel = new MainViewModel();

            

            DataContext = mainViewModel;
        }

        #endregion

      

        private void WindowOnClosing(object sender, CancelEventArgs cancelEventArgs)
        {
            _isWindowOpen = false;
        }

        private void MainWindow_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DebugUtils.Write("MainWindow_MouseLeft_DOWN");
            
        }

        private void MainWindow_OnMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            DebugUtils.Write("MainWindow_OnMouseUP!");

            if (_isWindowOpen)
                return;

            _draggingWindow.Close();


            PersonVm personVm = _personVm;
            if (personVm == null)
                return;

            var window = new LayoutWindow();
            window.WindowStartupLocation = WindowStartupLocation.Manual;

            var point = PointToScreen(Mouse.GetPosition(null));

            window.Width = 500;
            window.Height = 500;
            window.Left = point.X;
            window.Top = point.Y;

            var anotherVm = new MainViewModel();
            anotherVm.Persons.Clear();
            anotherVm.Persons.Add(personVm);

            window.DataContext = anotherVm;

            _isWindowOpen = true;
            window.Show();
            window.Closing += WindowOnClosing;

            this.ReleaseMouseCapture();
            
        }

        private void OnListBoxPreviewMouseButtonDown(object sender, MouseButtonEventArgs e)
        {
            DebugUtils.Write("ListBox_MouseLeft_DOWN");


            if (_isWindowOpen)
                return;
            else
                this.CaptureMouse();
            
            var item = ItemsControl.ContainerFromElement(sender as ListBox, e.OriginalSource as DependencyObject) as ListBoxItem;
            if (item != null)
            {
                DebugUtils.Write($"ListBox_item - {item}");
                _personVm = item.DataContext as PersonVm;

            }

            _draggingWindow = new DraggingWindow();
            _draggingWindow.WindowStartupLocation = WindowStartupLocation.Manual;

            var point = PointToScreen(Mouse.GetPosition(null));

            _draggingWindow.Width = 500;
            _draggingWindow.Height = 500;
            _draggingWindow.Left = point.X;
            _draggingWindow.Top = point.Y;
            _draggingWindow.Opacity = 0.5;
            _draggingWindow.AllowsTransparency = true;
            _draggingWindow.WindowStyle = WindowStyle.None;
            _draggingWindow.Background = new SolidColorBrush(Colors.Transparent);

            _draggingWindow.Show();



        }

        private void OnListBoxMouseUp(object sender, MouseButtonEventArgs e)
        {
            DebugUtils.Write("ListBox_MouseLeft_UP");


        }

        private void MainWindow_OnPreviewMouseMove(object sender, MouseEventArgs e)
        {
            var pointToScreen = PointToScreen(e.GetPosition(null));
            if (_draggingWindow == null)
                return;
            _draggingWindow.Left = pointToScreen.X;
            _draggingWindow.Top = pointToScreen.Y;


        }
    }
}