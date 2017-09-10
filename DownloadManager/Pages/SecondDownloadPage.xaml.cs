using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace DownloadManager.Pages
{
    public partial class SecondDownloadPage : Page
    {
        string dest = ""; 
        static Download NewDownload;
        public SecondDownloadPage()
        {
            InitializeComponent();
            SaveTextBox.Text = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),"Downloads");

            string path = FirstDownloadPage.download.Name;
            if (Uri.IsWellFormedUriString(path, UriKind.RelativeOrAbsolute))
            {

                var url = new Uri(path);

                var fullname = url.LocalPath.TrimStart('/');
                fullname = fullname.TrimEnd('?');

                var namewithoutextention = System.IO.Path.GetFileNameWithoutExtension(fullname);
                var ext = System.IO.Path.GetExtension(fullname);

                if (ext == string.Empty)
                    ext = ".html";
                if (namewithoutextention == string.Empty)
                    namewithoutextention = "Index";
                string name = namewithoutextention + ext;
                FilenameTextBox.Text = name;
                dest = FilenameTextBox.Text;
            }
            else
            {
                FilenameTextBox.Text = path.Split('\\')[(path.Split('\\').Length) - 1];
                dest = FilenameTextBox.Text;
            }
        }

        private void CancelDownloadButton1_Click(object sender, RoutedEventArgs e)
        {
            var adddownload = System.Windows.Application.Current.Windows.Cast<Window>().FirstOrDefault(window => window is AddDownload) as AddDownload;
            FirstDownloadPage dp = new FirstDownloadPage();
            dp.LinkTextBox.Text = FirstDownloadPage.download.Name;

            adddownload.DownloadFrame.Content = dp;

            adddownload.Height = 140;
        }

        private void BrowseButton2_Click(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog ofd2 = new FolderBrowserDialog();
            if (ofd2.ShowDialog() == DialogResult.OK)
            {
                SaveTextBox.Text = ofd2.SelectedPath;
            }
        }

        private void OkDownloadButton_Click(object sender, RoutedEventArgs e)
        {
            if (Uri.IsWellFormedUriString(FirstDownloadPage.download.Name, UriKind.Absolute))
            {
                NewDownload = new DownloadManager.Download();
                NewDownload.IsChecked = false;
                NewDownload.Name = FilenameTextBox.Text;
                NewDownload.Size = "0 MB";
                NewDownload.Speed = "0 MB/S";
                NewDownload.State = new object();
                NewDownload.Added = DateTime.Now.ToShortDateString();
                NewDownload.destination = dest;
            }
            MainWindow.download = NewDownload;
            MainWindow.Flag = true;
            Window.GetWindow(this).Close();
        }
    }
}
