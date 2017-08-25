using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace DownloadManager
{
    public class Download
    {
        public CheckBox IsChecked { get; set; }
        public string Name { get; set; }
        public string State { get; set; }
        public string Speed { get; set; }
        public string Size { get; set; }
        public string Added { get; set; }
    }
}
