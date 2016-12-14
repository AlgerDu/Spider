using D.Util.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Util.Event
{
    public class BaseEvent : IEvent
    {
        Guid _guid = Guid.NewGuid();
        DateTime _timeStamp = DateTime.Now;

        public Guid GUID
        {
            get
            {
                return _guid;
            }
        }

        public DateTime TimeStamp
        {
            get
            {
                return _timeStamp;
            }
        }
    }
}
