using Microsoft.VisualStudio.TestTools.UnitTesting;
using D.Spider.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using D.Util.Logger;

namespace D.Spider.Core.Tests
{
    [TestClass()]
    public class SsEngineTests
    {
        [TestMethod()]
        public void RunTest()
        {
            var html = "<!DOCTYPE html>      <html>      <head>      </head>      <body>                  <div class=\"volume-wrap\">                      <div class=\"volume\">                          <div class=\"cover\"></div>                          <h3>                              <a class=\"subscri\" href=\"//read.qidian.com/BookReader/vol,3602691,10119581.aspx\" target=\"_blank\"><em class=\"btn\"><b class=\"iconfont\">&#xe636;</b>分卷阅读</em></a>                              九洲一号群<i>&#183;</i>共25章<span class=free> 免费</span><em class=\"count\">本卷共<cite>64022</cite>字</em></h3>                          <ul class=\"cf\">                              <li data-rid=\"1\"><a href=\"//read.qidian.com/chapter/iHBVJ0Mlhkw1/hMNLY_o2zHYex0RJOkJclQ2\" target=\"_blank\" data-eid=\"qd_G55\" data-cid=\"//read.qidian.com/chapter/iHBVJ0Mlhkw1/hMNLY_o2zHYex0RJOkJclQ2\" title=\"首发 时间：2015-09-11 19:14:11 章节字数：3276\">第一章 黄山真君和九洲一号群</a>                              </li>                          </ul>                      </div>                      <div class=\"volume\">                          <div class=\"cover\"></div>                          <h3>                              <a class=\"subscri j_subscribe\" href=\"javascript:\" data-eid=\"qd_G56\" target=\"_blank\" data-volumeid=\"10256926\"  data-volumenum=\"10256926\"><em class=\"btn\"><b class=\"iconfont\">&#xe636;</b>订阅本卷</em></a>                              仙农宗<i>&#183;</i>共61章<span class=vip> VIP</span><em class=\"count\">本卷共<cite>188147</cite>字</em></h3>                          <ul class=\"cf\">                              <li data-rid=\"1\"><a href=\"//vipreader.qidian.com/chapter/3602691/89835187\" target=\"_blank\" data-eid=\"qd_G55\" data-cid=\"//vipreader.qidian.com/chapter/3602691/89835187\" title=\"首发时间：2015-11-06 12:06:57 章节 字数：3399\">第一百三十三章 各谋所需</a>                                  <em class=\"iconfont \">&#xe63c;</em>                              </li>                              <li data-rid=\"2\"><a href=\"//vipreader.qidian.com/chapter/3602691/89839336\" target=\"_blank\" data-eid=\"qd_G55\" data-cid=\"//vipreader.qidian.com/chapter/3602691/89839336\" title=\"首发时间：2015-11-06 15:35:24 章节 字数：3130\">第一百三十四章 苏氏阿七怎么在这里？</a>                                  <em class=\"iconfont \">&#xe63c;</em>                              </li>                          </ul>                      </div>                  </div>              </div>      </body>      </html>";
            var spiderscriptCode = @"
                    var vs:array
                    foreach $('div.volume')
                        var v:object
                        v.Name = $('h3').text
                        var cs:array
                        foreach $('li')
                            var c:object
                            c.Time = $('a').attr('title')
                            c.Name = $('a').text
                            cs[] = c
                        v.Chapters = cs
                        vs[] = v
                    return vs";

            var engine = new SsEngine(new NullLogFactory());

            var data = engine.Run(html, spiderscriptCode);
            Assert.AreEqual(data.GetType(), typeof(JArray));
        }

        [TestMethod()]
        public void RunTestReturn()
        {
            var engine = new SsEngine(new NullLogFactory());

            var data = engine.Run("", "var a:object\r\nreturn a");

            Assert.IsNotNull(data);
        }
    }
}