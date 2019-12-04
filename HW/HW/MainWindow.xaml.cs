using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
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
using System.Windows.Threading;

namespace HW
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string savePath = @"C:\Downloads\";
        private string url;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void DownloadBtnClick(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(urlTB.Text) || urlTB.Text == "Введите URL")
            {
                MessageBox.Show("Пшлнх");
                return;
            }
            url = urlTB.Text;
            progressBar.Visibility = Visibility.Visible;
            var thread = new Thread(Download);
            thread.IsBackground = true;
            thread.Start();
        }

        private void UrlTBGotFocus(object sender, RoutedEventArgs e)
        {
            urlTB.Text = string.Empty;
        }

        private void Download()
        {
            try
            {
                var fileName = url.Substring(url.Length - 10);
                using (var client = new WebClient())
                {
                    client.DownloadFile(url, savePath + fileName);
                }
                Dispatcher.BeginInvoke(DispatcherPriority.Normal,
                     (ThreadStart)delegate ()
                     {
                         progressBar.Visibility = Visibility.Hidden;
                     }
                 );
                MessageBox.Show("Закончил.");
            }
            catch
            {
                MessageBox.Show("Что то пошло не так");
            }
        }
    }
}
