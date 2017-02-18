using CefSharp.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace D.Spider
{
    /// <summary>
    /// 封装的 cef 浏览器主窗口
    /// </summary>
    public partial class CefBrowserMainForm : Form
    {
        public CefBrowserMainForm()
        {
            InitializeComponent();


            CefSharp.Cef.Initialize();
            
            //实例化控件
            ChromiumWebBrowser wb = new ChromiumWebBrowser("http://www.baidu.com");
            //设置停靠方式
            wb.Dock = DockStyle.Fill;

            //加入到当前窗体中
            Controls.Add(wb);
        }
    }
}
