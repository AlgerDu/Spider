using Microsoft.VisualStudio.TestTools.UnitTesting;
using D.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using D.Util.Interface;
using D.Util.Test;

namespace D.Util.Tests
{
    [TestClass()]
    public class EventBusTests
    {
        /// <summary>
        /// 发布事件测试
        /// </summary>
        [TestMethod()]
        public void PublishTest()
        {
            IEventBus eventBus = new EventBus();

            var t = eventBus.Publish(new TestEvent());
            t.Wait();
            var i = t.Result;

            Assert.AreEqual(i, 0);
        }

        /// <summary>
        /// 同步发布事件
        /// </summary>
        [TestMethod()]
        public void SubscribeTest()
        {
            IEventBus eventBus = new EventBus();
            eventBus.Subscribe(new TestHandler());
            eventBus.Subscribe(new TestHandler());

            var begin = DateTime.Now;

            var t = eventBus.Publish(new TestEvent());
            t.Wait();
            var i = t.Result;
            var stamp = DateTime.Now - begin;

            Assert.AreEqual(i, 2);
            Assert.AreEqual(stamp > new TimeSpan(0, 0, 2), true);
        }

        /// <summary>
        /// 同步发布事件
        /// </summary>
        [TestMethod()]
        public void SubscribeTest_Sync()
        {
            IEventBus eventBus = new EventBus();
            eventBus.Subscribe(new TestHandler());

            var begin = DateTime.Now;

            eventBus.Publish(new TestEvent());

            var stamp = DateTime.Now - begin;

            Assert.AreEqual(stamp < new TimeSpan(0, 0, 1), true);
        }
    }
}