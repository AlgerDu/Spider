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
    }
}
