using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

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
        }
    }
}
