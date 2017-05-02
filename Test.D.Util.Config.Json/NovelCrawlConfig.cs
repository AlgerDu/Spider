using D.Util.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.D.Util.Config.Json
{
    internal class NovelCrawlConfig : IConfigItem
    {
        public string Path
        {
            get
            {
                return "core.novelCrawl";
            }
        }

        public string Server { get; set; }
    }
}
