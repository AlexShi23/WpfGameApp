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
using WpfGameApp.Entities;

namespace WpfGameApp
{
    /// <summary>
    /// Логика взаимодействия для CardControl.xaml
    /// </summary>
    public partial class CardControl : UserControl
    {
        private bool isRackCard = false;

        public bool IsRackCard
        {
            get { return isRackCard; }
            set
            {
                isRackCard = value;

                if (isRackCard)
                    card.Background = Brushes.Aquamarine;
            }
        }
        private static bool isRackCardOnHold;

        private double dx;
        private double dy;
        public CardControl()
        {
            InitializeComponent();
        }

        public CardControl(bool isRackCard) : this()
        {
            IsRackCard = isRackCard;
        }

        private void grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            dx = Canvas.GetLeft((UIElement)sender) - e.GetPosition(this).X;
            dy = Canvas.GetLeft((UIElement)sender) - e.GetPosition(this).Y;

            // Cохранение данных для перетаскивания
            var data = new DataObject();
            // Сохранение контекста данных
            if (DataContext != null)
            {
                data.SetData("Card", DataContext);
                isRackCardOnHold = IsRackCard;
            }
            // Начало операци перетаскивания
            DragDrop.DoDragDrop((DependencyObject)sender, data, DragDropEffects.Copy);
        }

        private void card_Drop(object sender, DragEventArgs e)
        {
            IDataObject data = e.Data;

            DataContext = data.GetData("Card");
        }

        private void card_DragOver(object sender, DragEventArgs e)
        {
            e.Effects = DragDropEffects.Copy;
            if(IsRackCard && !isRackCardOnHold)
            {
                e.Effects = DragDropEffects.None;
            }
            else if (!IsRackCard && isRackCardOnHold)
            {
                e.Effects = DragDropEffects.None;
            }
        }

        private void grid_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                double x = e.GetPosition(this).X + dx;
                double y = e.GetPosition(this).Y + dy;
                Canvas.SetLeft((UIElement)sender, x);
                Canvas.SetTop((UIElement)sender, y);
            }
        }

        private void CardControl_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if(e.NewValue is Server)
            {
                Server server = e.NewValue as Server;
                card.Background = Brushes.LightCoral;

                property1.Text = server.Name;
                property2.Text = $"{server.CPUs} CPU";
                property3.Text = $"{server.Size}U";
                property4.Text = $"{server.Weight} кг";
                property5.Text = $"{server.Count} шт";
                property6.Text = $"{server.Price} USD";
            }
            else if (e.NewValue is KvmConsole)
            {
                KvmConsole kvmConsole = e.NewValue as KvmConsole;
                card.Background = Brushes.Bisque;

                property1.Text = kvmConsole.Name;
                property2.Text = $"{kvmConsole.Weight} кг";
                property3.Text = $"{kvmConsole.Count} шт";
                property4.Text = $"{kvmConsole.Price} USD";
                property5.Text = "";
                property6.Text = "";
            }
            else if (e.NewValue is NetworkSwitch)
            {
                NetworkSwitch networkSwitch = e.NewValue as NetworkSwitch;
                card.Background = Brushes.DarkKhaki;

                property1.Text = networkSwitch.Name;
                property2.Text = $"{networkSwitch.Weight} кг";
                property3.Text = $"{networkSwitch.Count} шт";
                property4.Text = $"{networkSwitch.Price} USD";
                property5.Text = "";
                property6.Text = "";
            }
            else if (e.NewValue is Storage)
            {
                Storage storage = e.NewValue as Storage;
                card.Background = Brushes.Tan;

                property1.Text = storage.Name;
                property2.Text = $"{storage.Weight} кг";
                property3.Text = $"{storage.Size} ТБ";
                property4.Text = $"{storage.Price} USD";
                property5.Text = "";
                property6.Text = "";
            }
            else if (e.NewValue is Rack)
            {
                Rack rack = e.NewValue as Rack;
                card.Background = Brushes.DimGray;

                property1.Text = rack.Name;
                property2.Text = $"{rack.Capacity} кг";
                property3.Text = $"{rack.Count} шт";
                property4.Text = $"{rack.Price} USD";
                property5.Text = "";
                property6.Text = "";
            }
        }
    }
}
