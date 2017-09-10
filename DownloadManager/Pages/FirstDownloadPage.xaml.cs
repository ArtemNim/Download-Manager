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
using DownloadManager.Pages;
using DownloadManager;
using System.Windows.Forms;

namespace DownloadManager.Pages
{
    /// <summary>
    /// Interaction logic for FirstDownloadPage.xaml
    /// </summary>
    public partial class FirstDownloadPage : Page
    {
        public static Download download;
        public FirstDownloadPage()
        {
            InitializeComponent();

            download = new DownloadManager.Download();
        }
       
        private void OkDownloadButton_Click(object sender, RoutedEventArgs e)
        {
            var adddownload = System.Windows.Application.Current.Windows.Cast<Window>().FirstOrDefault(window => window is AddDownload) as AddDownload;
            if (LinkTextBox.Text != string.Empty)
            {
                download.Name = LinkTextBox.Text;
                adddownload.DownloadFrame.Content = new SecondDownloadPage();
                adddownload.Height = 200;
            }

        }

        private void BrowseTorrent_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd1 = new OpenFileDialog();
            if (ofd1.ShowDialog() == DialogResult.OK)
            {
                LinkTextBox.Text = ofd1.FileName;
            }
        }
    }
}
