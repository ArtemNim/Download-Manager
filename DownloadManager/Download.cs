using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.IO;
using DownloadManager.Pages;

namespace DownloadManager
{
   public class Download: INotifyPropertyChanged
    {
        bool ischecked;
        string name;
        object state;
        string speed;
        string size;
        string added;

        BackgroundWorker worker;
        ProgressBar prbar;

        public string destination;
        public bool IsChecked
        {
            get { return ischecked; }
            set
            {
                ischecked = value;
                OnPropertyChanged("IsChecked");
            }
        }
        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged("Name");
            }
        }
        public object State
        {
            get { return state; }
            set
            {
                state = value;
                OnPropertyChanged("State");
            }
        }
        public string Speed
        {
            get { return speed; }
            set
            {
                speed = value;
                OnPropertyChanged("Speed");
            }
        }
        public string Size
        {
            get { return size; }
            set
            {
                size = value;
                OnPropertyChanged("Size");
            }
        }
        public string Added
        {
            get { return added; }
            set
            {
                added = value;
                OnPropertyChanged("Added");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
        public static void ShowDownload(Download NewDownload)
        {
            Download download = NewDownload;
            MainWindow.Download_list.Add(download);
            ((MainWindow)System.Windows.Application.Current.MainWindow).dataGrid.Items.Refresh();
        }
        public void StartDownload(Download NewDownload)
        {
            worker = new BackgroundWorker();

            worker.WorkerReportsProgress = true;
            worker.DoWork += worker_DoWork;
            worker.ProgressChanged += worker_ProgressChanged;
            worker.RunWorkerAsync();
        }
        void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            byte[] buffer = new byte[1024 * 1024];
            using (FileStream source = new FileStream(Name, FileMode.Open, FileAccess.Read))
            {
                long fileLength = source.Length;
                using (FileStream dest = new FileStream(destination, FileMode.CreateNew, FileAccess.Write))
                {
                    long totalBytes = 0;
                    int currentBlockSize = 0;
                    while ((currentBlockSize = source.Read(buffer,0,buffer.Length))>0)
                    {
                        totalBytes += currentBlockSize;
                        dest.Write(buffer,0,currentBlockSize);
                        (sender as BackgroundWorker).ReportProgress(Convert.ToInt32(totalBytes * 100.0/ fileLength));
                    }
                }
            }
        }
        void worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            prbar.Value = e.ProgressPercentage;
        }
    }
}
