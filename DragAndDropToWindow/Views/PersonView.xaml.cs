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
using System.Windows.Navigation;
using System.Windows.Shapes;
using DragAndDropToWindow.ViewModel;
using GwsUtils;

namespace DragAndDropToWindow.Views
{
  /// <summary>
  /// Interaction logic for PersonView.xaml
  /// </summary>
  public partial class PersonView : UserControl
  {
    private Point _startPoint;

    public PersonView()
    {
      InitializeComponent();
    }

    private void PersonView_OnPreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
      _startPoint = e.GetPosition(null);

      


      DebugUtils.Write($"MouseDown - Startpoint = {_startPoint}");
    }

    private void PersonView_OnPreviewMouseMove(object sender, MouseEventArgs e)
    {
      DebugUtils.Write($"MouseMove = {e.GetPosition(null)}");





      //var vm = DataContext as PersonVm;
      //if (vm == null)
      //  return;

      //var handler = vm.NotifyPersonMoveHandler;

      //handler?.Invoke(this, vm);


    }

    private void PersonView_OnPreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
    {
      //DebugUtils.Write($"MouseUp = {e.GetPosition(null)}");

      //var vm = (PersonVm)DataContext;
      //var handler = vm.NotifyPersonMoveHandler;

      //handler?.Invoke(this, vm);

    }

    private void OnMouseMove(object sender, MouseEventArgs e)
    {
      
    }
  }
}
