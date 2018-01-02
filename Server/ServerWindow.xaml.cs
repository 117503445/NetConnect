using System;
using System.Collections.Generic;
using System.IO;
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
using NetConnect;
namespace Server
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        TxtConnext_Server connect;
        public MainWindow()
        {
            InitializeComponent();

        }
        private void Info()
        {
            Console.WriteLine("Get");
        }
        string path = @"\\CLASSV\FolderShare";
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            connect = new TxtConnext_Server(path);
            connect.ShowInfo += (list) => { Console.WriteLine("Get!"); };
            connect.GetMethod += Connect_GetMethod;
            connect.Watcher_Changed(new object(),null);
        }

        private void Connect_GetMethod(string in_methodName, string in_methodParameters, out string out_methodName, out string out_methodParameters)
        {
            out_methodName = "in";
            Type type = typeof(MethodCollection);
            object[] parameters = in_methodParameters.Split(',');
            out_methodParameters = (string)type.GetMethod(in_methodName).Invoke(null, parameters);
        }
        public static class MethodCollection
        {
            /// <summary>
            /// 获取文件夹下的目录
            /// </summary>
            /// <param name="path">文件夹路径</param>
            /// <returns></returns>
            public static string GetDirectories(string path)
            {
                var temp = Directory.GetDirectories(path);
                string result = "";
                foreach (var item in temp)
                {
                    result += item + ",";
                }
                return result;
            }
        }

        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                TextBox t = ((TextBox)sender);
                string message = t.Text;
                //List<string> tasks = new List<string>
                //{
                //    message
                //};
                //tasks[0] = string.Format("{0};{1};{2}", netName, false, tasks[0]);
                string s0 = message.Split(' ')[0];
                string s1 = message.Split(' ')[1];
                //File.AppendAllLines(connectPath, tasks);
                connect.SendInfo(s0, s1,"client");
                t.Text = "";
            }
        }
    }
}
