using Microsoft.VisualStudio.TestTools.UnitTesting;
using D.Spider.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using D.Util.Logger.Console;

namespace D.Spider.Core.Tests
{
    [TestClass()]
    public class SsEngineTests
    {
        [TestMethod()]
        public void RunTest()
        {
            var html = "<!DOCTYPEhtml><html><head></head><body><divclass=\"volume - wrap\"><divclass=\"volume\"><divclass=\"cover\"></div><h3><aclass=\"subscri\"href=\"//read.qidian.com/BookReader/vol,3602691,10119581.aspx\"target=\"_blank\"><emclass=\"btn\"><bclass=\"iconfont\">&#xe636;</b>分卷阅读</em></a>九洲一号群<i>&#183;</i>共25章<spanclass=free>免费</span><emclass=\"count\">本卷共<cite>64022</cite>字</em></h3><ulclass=\"cf\"><lidata-rid=\"1\"><ahref=\"//read.qidian.com/chapter/iHBVJ0Mlhkw1/hMNLY_o2zHYex0RJOkJclQ2\"target=\"_blank\"data-eid=\"qd_G55\"data-cid=\"//read.qidian.com/chapter/iHBVJ0Mlhkw1/hMNLY_o2zHYex0RJOkJclQ2\"title=\"首发时间：2015-09-1119:14:11章节字数：3276\">第一章黄山真君和九洲一号群</a></li></ul></div><divclass=\"volume\"><divclass=\"cover\"></div><h3><aclass=\"subscrij_subscribe\"href=\"javascript:\"data-eid=\"qd_G56\"target=\"_blank\"data-volumeid=\"10256926\"data-volumenum=\"10256926\"><emclass=\"btn\"><bclass=\"iconfont\">&#xe636;</b>订阅本卷</em></a>仙农宗<i>&#183;</i>共61章<spanclass=vip>VIP</span><emclass=\"count\">本卷共<cite>188147</cite>字</em></h3><ulclass=\"cf\"><lidata-rid=\"1\"><ahref=\"//vipreader.qidian.com/chapter/3602691/89835187\"target=\"_blank\"data-eid=\"qd_G55\"data-cid=\"//vipreader.qidian.com/chapter/3602691/89835187\"title=\"首发时间：2015-11-0612:06:57章节字数：3399\">第一百三十三章各谋所需</a><emclass=\"iconfont\">&#xe63c;</em></li><lidata-rid=\"2\"><ahref=\"//vipreader.qidian.com/chapter/3602691/89839336\"target=\"_blank\"data-eid=\"qd_G55\"data-cid=\"//vipreader.qidian.com/chapter/3602691/89839336\"title=\"首发时间：2015-11-0615:35:24章节字数：3130\">第一百三十四章苏氏阿七怎么在这里？</a><emclass=\"iconfont\">&#xe63c;</em></li></ul></div></div></div></body></html>";
            var spiderscriptCode =
                @"var Volumes:array
                    foreach $('div.volume')
                        var v:object
                        v.Name = $('h3').inner
                        foreach $('li')
                            var c:object
                            c.Name = $('a').attr('title')
                    return Volumes";

            var engine = new SsEngine(new ConsoleLoggerFactory());

            var data = engine.Run(html, spiderscriptCode);

            Assert.Fail();
        }

        [TestMethod()]
        public void RunTestReturn()
        {
            var engine = new SsEngine(new ConsoleLoggerFactory());

            var data = engine.Run("", "return a");

            Assert.IsNull(data);
        }
    }
}