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

        /// <summary>
        /// 测试向配置文件中添加配置
        /// </summary>
        [TestMethod]
        public void TestAddItem()
        {
            const string filePath = "TestAddItem.json";

            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }

            IConfig config1 = new JsonConfig();
            config1.LoadFile(filePath);

            var item = config1.GetItem<NovelCrawlConfig>();

            config1.Save();

            IConfig config2 = new JsonConfig();
            config2.LoadFile(filePath);

            var item2 = config2.GetItem<NovelCrawlConfig>();

            Assert.AreEqual(item2.Server, new NovelCrawlConfig().Server);
        }
    }
}
