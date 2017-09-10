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

namespace DownloadManager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static List<Download> Download_list;
        public static Download download;
        public static bool Flag;
        void CheckBool(bool flag)
        {
            

        }
        public MainWindow()
        {
            InitializeComponent();

            Download_list = new List<Download>();

            Download dwnl = new DownloadManager.Download();
            dwnl.Name = "Name";
            dwnl.Size = "0";
            Download_list.Add(dwnl);

          

            Task.Run(() =>
            {
                while (!Flag) { };
                Download.ShowDownload(download);
                download.StartDownload(download);
            }
            );
        }
        private void Add_MouseDown(object sender, MouseButtonEventArgs e)
        {
            AddDownload adddownload = new DownloadManager.AddDownload();
            adddownload.Show();

            
        }
        
    }
}
