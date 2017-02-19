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

namespace D.Spider
{
    /// <summary>
    /// 封装的 cef 浏览器主窗口
    /// </summary>
    public partial class CefBrowserMainForm : Form
    {
        /// <summary>
        /// 静态变量 cef 是否初始化，不需要暴露给外部
        /// </summary>
        static bool _cefInited = false;

        ChromiumWebBrowser _wb;

        public CefBrowserMainForm()
        {
            InitializeComponent();

            InitializeCef();

            //实例化控件
            _wb = new ChromiumWebBrowser("http://www.baidu.com");
            //设置停靠方式
            _wb.Dock = DockStyle.Fill;

            _wb.FrameLoadEnd += new EventHandler<FrameLoadEndEventArgs>(CefWbLoadEnd);

            //加入到当前窗体中
            Controls.Add(_wb);
        }

        /// <summary>
        /// 加载 address 对应的页面
        /// </summary>
        /// <param name="address"></param>
        public void LoadHtml(string address)
        {

        }

        /// <summary>
        /// 初始化 cef 相关
        /// </summary>
        private static void InitializeCef()
        {
            if (_cefInited)
            {
                var setting = new CefSettings();
                setting.Locale = "zh-CN"; //设置默认的本地语言
                //setting.p

                Cef.Initialize(setting);
                _cefInited = true;
            }
        }

        /// <summary>
        /// cef 浏览器页面加载完毕
        /// </summary>
        /// <param name="e"></param>
        private async void CefWbLoadEnd(object sender, FrameLoadEndEventArgs e)
        {
            //MessageBox.Show("end");
            var html = await _wb.GetSourceAsync();
            MessageBox.Show(html.Substring(0, 20));
        }

        private void CefBrowserMainForm_Shown(object sender, EventArgs e)
        {
            //this.Visible = false;
        }

        private void CefBrowserMainForm_Load(object sender, EventArgs e)
        {
            //this.Visible = false;
        }
    }
}
