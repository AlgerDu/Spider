using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace D.Spider.CefDownloader.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            Form form = new CefBrowserMainForm();
            form.ShowDialog();
            Console.ReadKey();
        }
    }
}
