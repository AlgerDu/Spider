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
            //form.Activate();
            form.ShowDialog();
            //form.();
            //form.Show();
            //form.Hide();
            //Console.ReadKey();
            //Environment.Exit(0);
        }
    }
}
