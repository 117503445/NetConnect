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
using System.Threading;
using System.Windows.Threading;

namespace EasyNotePad
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            dispatcherTimer.Tick += DispatcherTimer_Tick;
            dispatcherTimer.Start();
        }
        DispatcherTimer dispatcherTimer = new DispatcherTimer {
            Interval = TimeSpan.FromSeconds(1)
        };
        string path = @"\\DESKTOP-ASUS\FolderShare\client\Connect.txt";
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new Microsoft.Win32.OpenFileDialog();

            var result = openFileDialog.ShowDialog();
            if (result == true)
            {
                path = openFileDialog.FileName;

            }
        }

        private void DispatcherTimer_Tick(object sender, EventArgs e)
        {
            Tb.Text =System.IO.File.ReadAllText(path);
        }
    }
}
