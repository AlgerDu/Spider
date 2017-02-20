using D.Spider.Core.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using D.Spider.Core.Events;
using D.Util.Interface;
using CefSharp.WinForms;

namespace D.NovelCrawl.Winform
{
    /// <summary>
    /// 程序主窗口，同时也承担承载 cef 浏览器的责任
    /// </summary>
    public partial class MainForm : Form
        , IDownloader
    {
        public MainForm()
        {
            InitializeComponent();
        }

        #region IDownloader 实现
        public void Run()
        {
            throw new NotImplementedException();
        }

        void IEventHandler<UrlWaitingEvent>.Handle(UrlWaitingEvent e)
        {
            throw new NotImplementedException();
        }
        #endregion

        private void MainForm_Load(object sender, EventArgs e)
        {
            CefSharp.Cef.Initialize();

            //实例化控件
            ChromiumWebBrowser wb = new ChromiumWebBrowser("http://www.baidu.com");
            //设置停靠方式
            wb.Dock = DockStyle.Fill;

            //加入到当前窗体中
            P_Cef.Controls.Add(wb);
        }
    }
}
