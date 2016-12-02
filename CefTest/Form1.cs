using CefSharp;
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

namespace CefTest
{
    public partial class Form1 : Form
    {
        public IWinFormsWebBrowser Browser { get; private set; }

        public Form1()
        {
            InitializeComponent();

            var setting = new CefSharp.CefSettings();

            // 设置语言
            setting.Locale = "zh-CN";
            CefSharp.Cef.Initialize(setting);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            var url = "www.baidu.com";

            var browser = new ChromiumWebBrowser(url)
            {
                Dock = DockStyle.Fill
            };

            Browser = browser;

            browser.BrowserSettings.ImageLoading = CefState.Disabled;

            browser.FrameLoadEnd += new EventHandler<FrameLoadEndEventArgs>(End);
            
            P_Web.Controls.Add(browser);
            browser.Load(url);
        }

        public void End(object sender, FrameLoadEndEventArgs ar)
        {

        }
    }
}
