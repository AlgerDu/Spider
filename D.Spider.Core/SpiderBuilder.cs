using D.Spider.Core.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Spider.Core
{
    public class SpiderBuilder : ISpiderBuilder
    {
        public ISpider Build()
        {
            throw new NotImplementedException();
        }

        public ISpiderBuilder UseStartup<T>() where T : IStartup, new()
        {
            throw new NotImplementedException();
        }
    }
}
