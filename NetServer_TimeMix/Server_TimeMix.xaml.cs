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
using System.Windows.Shapes;
using NetConnect;
namespace NetServer_TimeMix
{
    /// <summary>
    /// Server_TimeMix.xaml 的交互逻辑
    /// </summary>
    public partial class Server_TimeMix : Window
    {
        /// <summary>
        /// 网名
        /// </summary>
        public static string netName = "Server";
        /// <summary>
        /// @"\\192.168.2.233\FolderShare"
        /// </summary>
        public static string path = @"\\DESKTOP-ASUS\FolderShare";
        TxtConnect_Client connect = new TxtConnect_Client(netName,path);
        public Server_TimeMix()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            connect.ShowInfo += Connect_ShowInfo;
            connect.GetMethod += Connect_GetMethod;
            connect.DisplayInfo();
        }

        private void Connect_GetMethod(string in_methodName, string in_methodParameters, out string out_methodName, out string out_methodParameters)
        {
            out_methodName = "return";
            Type type = typeof(MethodCollection);
            object[] parameters = in_methodParameters.Split(',');
            out_methodParameters = (string)type.GetMethod(in_methodName).Invoke(null, parameters);
        }

        private void Connect_ShowInfo(List<string> Info)
        {
            TbInfo.Dispatcher.Invoke(new Action(delegate
            {
                TbInfo.Text = "";
                foreach (var item in Info)
                {
                    TbInfo.Text += item + "\n";
                }
            }));
        }
        private void TextBox_KeyDown(object sender, KeyEventArgs e)
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
            if (e.Key == Key.Enter)
            {
                //File.AppendAllLines(connectPath, tasks);
                connect.SendInfo(s0, s1);
                t.Text = "";
            }
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
            public static void CopyFile(string sourceFileName, string destFileName)
            {
                destFileName =path + "\\" + netName + "\\" + destFileName;
                File.Copy(sourceFileName, destFileName);
            }
        }
    }
}
