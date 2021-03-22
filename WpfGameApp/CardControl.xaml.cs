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

namespace WpfGameApp
{
    /// <summary>
    /// Логика взаимодействия для CardControl.xaml
    /// </summary>
    public partial class CardControl : UserControl
    {
        private double dx;
        private double dy;
        public CardControl()
        {
            InitializeComponent();
        }
        private void grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            dx = Canvas.GetLeft((UIElement)sender) - e.GetPosition(this).X;
            dy = Canvas.GetLeft((UIElement)sender) - e.GetPosition(this).Y;

            // Cохранение данных для перетаскивания
            var data = new DataObject();
            // Сохранение контекста данных
            data.SetData("Card", DataContext);
            // Начало операци перетаскивания
            DragDrop.DoDragDrop((DependencyObject)sender, data, DragDropEffects.Copy);
        }

        private void grid_MouseMove(object sender, MouseEventArgs e)
        {
            if(e.LeftButton == MouseButtonState.Pressed)
            {
                double x = e.GetPosition(this).X + dx;
                double y = e.GetPosition(this).Y + dy;
                Canvas.SetLeft((UIElement)sender, x);
                Canvas.SetTop((UIElement)sender, y);
            }
        }
    }
}
