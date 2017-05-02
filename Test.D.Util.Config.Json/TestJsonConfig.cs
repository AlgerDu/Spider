using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using D.Util.Interface;
using D.Util.Config;

namespace Test.D.Util.Config.Json
{
    [TestClass]
    public class TestJsonConfig
    {
        /// <summary>
        /// 测试自动生成配置文件
        /// </summary>
        [TestMethod]
        public void TestAutoCreateConfigFile()
        {
            const string filePath = "TestAutoCreateConfigFile.json";

            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }

            IConfig config = new JsonConfig();

            config.LoadFile(filePath);

            config.Save("1.0.0", "TestAutoCreateConfigFile");

            Assert.IsTrue(File.Exists(filePath));
        }

        /// <summary>
        /// 测试读取配置文件
        /// </summary>
        [TestMethod]
        public void TestLoadConfigFile()
        {
            const string filePath = "config.example.json";

            IConfig config = new JsonConfig();
            config.LoadFile(filePath);

            var item = config.GetItem<NovelCrawlConfig>();

            Assert.AreEqual(item.Server, "www.duzhiwei.online");
        }
    }
}
