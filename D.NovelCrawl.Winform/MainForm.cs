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
using CefSharp;

namespace D.NovelCrawl.Winform
{
    /// <summary>
    /// 程序主窗口，同时也承担承载 cef 浏览器的责任
    /// </summary>
    public partial class MainForm : Form
        , IDownloader
    {
        ChromiumWebBrowser _wb;

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
            _wb = new ChromiumWebBrowser("");
            //设置停靠方式
            _wb.Dock = DockStyle.Fill;
            _wb.FrameLoadEnd += new EventHandler<CefSharp.FrameLoadEndEventArgs>(CefWbLoadEnd);

            //加入到当前窗体中
            P_Cef.Controls.Add(_wb);
        }

        /// <summary>
        /// cef 浏览器页面加载完毕
        /// </summary>
        /// <param name="e"></param>
        private async void CefWbLoadEnd(object sender, FrameLoadEndEventArgs e)
        {
            //MessageBox.Show("end");
            var html = await _wb.GetSourceAsync();
            MessageBox.Show(html.Substring(0, 50));
        }

        private void MainForm_Shown(object sender, EventArgs e)
        {
            //P_Cef.Visible = false;
            //string t = string.Empty;
            _wb.Load("http://www.baidu.com");
            _wb.FrameLoadEnd += new EventHandler<CefSharp.FrameLoadEndEventArgs>(CefWbLoadEnd);
            P_Cef.Invalidate();
            //MessageBox.Show(t);
        }
    }
}
