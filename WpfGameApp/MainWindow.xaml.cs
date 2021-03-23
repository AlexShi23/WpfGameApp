using Microsoft.Win32;
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
        private const int cellsInRack = 8;

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
            CreateShelf();
            CreateCards();

            RegistryKey currentUser = Registry.CurrentUser;
            RegistryKey applicationKey = currentUser?.OpenSubKey("CreateYourDC");
            string fileName = applicationKey?.GetValue("SaveFileName")?.ToString();

            if(fileName != null)
            {
                state = Entities.GameState.Load(fileName);
                SetDataContextToCells();
            }
        }

        private void SetDataContextToCells()
        {
            for (int i = 0; i < state.Entities.Count; i++)
            {
                cells[i].DataContext = state.Entities[i];
            }
        }

        private void CreateShelf()
        {
            for(int i = 0; i < cellsInRack; i++)
            {
                var item = new CardControl()
                {
                    AllowDrop = true
                };
                Canvas.SetLeft(item, 30);
                Canvas.SetTop(item, 30 + i * (height + spanY));

                cells.Add(item);

                canvas.Children.Add(item);
            }

            cells[cells.Count - 1].IsRackCard = true;
        }
        
        private void CreateCards()
        {
            var db = new Database();

            int y = 80;
            foreach (var server in db.GetServers())
            {
                var serverCard = new CardControl()
                {
                    DataContext = server
                };
                Canvas.SetLeft(serverCard, 1000);
                Canvas.SetTop(serverCard, y);
                canvas.Children.Add(serverCard);
                y += 100;
            }

            

            var rack = new Entities.Rack()
            {
                Name = "Rack",
                Capacity = 200,
                Count = 10,
                Price = 2100
            };

            var rackCard = new CardControl(isRackCard : true)
            {
                DataContext = rack
            };
            Canvas.SetLeft(rackCard, 500);
            Canvas.SetTop(rackCard, 180);
            canvas.Children.Add(rackCard);

            var kvm = new Entities.KvmConsole()
            {
                Name = "KVM Console",
                Weight = 10,
                Count = 5,
                Price = 1500
            };

            var kvmCard = new CardControl()
            {
                DataContext = kvm
            };
            Canvas.SetLeft(kvmCard, 500);
            Canvas.SetTop(kvmCard, 280);
            canvas.Children.Add(kvmCard);

            var networkSwitch = new Entities.NetworkSwitch()
            {
                Name = "Network switch",
                Weight = 5,
                Count = 25,
                Price = 800
            };

            var networkSwitchCard = new CardControl()
            {
                DataContext = networkSwitch
            };
            Canvas.SetLeft(networkSwitchCard, 500);
            Canvas.SetTop(networkSwitchCard, 380);
            canvas.Children.Add(networkSwitchCard);

            var storage = new Entities.Storage()
            {
                Name = "Storage",
                Weight = 15,
                Size = 25,
                Price = 800
            };

            var storageCard = new CardControl()
            {
                DataContext = storage
            };
            Canvas.SetLeft(storageCard, 500);
            Canvas.SetTop(storageCard, 480);
            canvas.Children.Add(storageCard);
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
                    Filter = "Файлы (*.xml)|*.xml|Файлы (*.json)|*.json|Все файлы (*.*)|*.*"
                };
                // Выбор файла для сохранения
                if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    state = new Entities.GameState();
                    foreach (CardControl cell in cells)
                    {
                        if (cell.DataContext is Entities.Server)
                        {
                            state.Entities.Add(cell.DataContext as Entities.Server);
                        }
                        else if (cell.DataContext is Entities.Rack)
                        {
                            state.Entities.Add(cell.DataContext as Entities.Rack);
                        }
                        else if (cell.DataContext is Entities.KvmConsole)
                        {
                            state.Entities.Add(cell.DataContext as Entities.KvmConsole);
                        }
                        else if (cell.DataContext is Entities.NetworkSwitch)
                        {
                            state.Entities.Add(cell.DataContext as Entities.NetworkSwitch);
                        }
                        else if (cell.DataContext is Entities.Storage)
                        {
                            state.Entities.Add(cell.DataContext as Entities.Storage);
                        }
                        else
                        {
                            state.Entities.Add(new Entities.Server());
                        }
                    }
                    switch (dialog.FilterIndex)
                    {
                        case 1:
                            // XML
                            state.Save(dialog.FileName);
                            break;
                        case 2:
                            // JSON
                            //string json = System.Text.Json.JsonSerializer.Serialize(state);
                            //System.IO.Flie.WriteAllText(dialog.FileName, json);
                            break;
                        default:
                            break;
                    }

                    // Запись в реестр
                    RegistryKey currentUserKey = Registry.CurrentUser;
                    RegistryKey applicationKey = currentUserKey.CreateSubKey("CreateYourDC");
                    applicationKey.SetValue("SaveFileName", dialog.FileName);
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
                    SetDataContextToCells();
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
