using Autofac;
using D.Spider.Core.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Spider.Core.Extension
{
    public static class SpiderCoreExtensions
    {
        /// <summary>
        /// 添加 spider 的核心服务
        /// </summary>
        /// <param name="builder"></param>
        public static void AddSpiderCoreService(this ContainerBuilder builder)
        {
            builder.RegisterType<DSpider>()
                .As<ISpider>()
                .SingleInstance();
        }
    }
}
