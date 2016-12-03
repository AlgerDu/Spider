using D.Util.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace D.Util.Test
{
    public class TestEvent : IEvent
    {
        public Guid GUID { get; set; }

        public DateTime TimeStamp { get; set; }
    }

    public class TestHandler : IEventHandler<TestEvent>
    {
        public void Handle(TestEvent e)
        {
            Thread.Sleep(1000);
        }
    }
}
