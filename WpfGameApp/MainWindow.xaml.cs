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
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const double height = 70;

        private const double spanY = 20;

        private List<CardControl> cells = new List<CardControl>();

        private Entities.GameState state;

        public MainWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Завершение перетаскивания
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void card_Drop(object sender, DragEventArgs e)
        {
            // Принятые данные
            IDataObject data = e.Data;
            var source = data.GetData("Card") as Entities.Server;
            // Карточка приёмник данных
            var dest = sender as CardControl;
            dest.DataContext = source;
        }

        /// <summary>
        /// Загрузка окна
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // Формирование шкафа
            for (int i = 0; i < 7; i++)
            {
                var item = new CardControl()
                {
                    AllowDrop = true
                };
                Canvas.SetLeft(item, 30);
                Canvas.SetTop(item, 30 + i * (height + spanY));
                item.Drop += card_Drop;

                cells.Add(item);

                canvas.Children.Add(item);
            }
            DataObject dataObject = new DataObject(DataFormats.Bitmap, Properties.Resources.server1);
            var server = new Entities.Server()
            {
                Name = "IBM 5300",
                CPUs = 2,
                Size = 1,
                Weight = 10,
                Price = 1100,
            };

            var card = new CardControl()
            {
                DataContext = server
            };
            Canvas.SetLeft(card, 500);
            Canvas.SetTop(card, 80);
            canvas.Children.Add(card);
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void save_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var dialog = new System.Windows.Forms.SaveFileDialog()
                {
                    Filter = "Файлы (*.xml)|*.xml|Все файлы (*.*)|*.*"
                };
                // Выбор файла для сохранения
                if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    var servers = new List<Entities.Server>();
                    foreach (CardControl cell in cells)
                    {
                        if (cell.DataContext is Entities.Server)
                        {
                            servers.Add(cell.DataContext as Entities.Server);
                        }
                        else
                        {
                            servers.Add(new Entities.Server());
                        }
                    }

                    state = new Entities.GameState()
                    {
                        Servers = servers.ToArray()
                    };

                    state.Save(dialog.FileName);
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
        }

        private void load_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var dialog = new System.Windows.Forms.OpenFileDialog()
                {
                    Filter = "Файлы (*.xml)|*.xml|Все файлы (*.*)|*.*"
                };
                if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    state = Entities.GameState.Load(dialog.FileName);
                    for (int i = 0; i < 7; i++)
                    {
                        cells[i].DataContext = state.Servers[i];
                    }
                }
            }
            catch (Exception ex)
            {
                string message = "";
                do
                {
                    message += ex.Message + Environment.NewLine;
                    ex = ex.InnerException;
                }
                while (ex != null);
                System.Windows.Forms.MessageBox.Show(message);
            }
        }
    }
}
