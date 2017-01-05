using D.NovelCrawl.Core.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using D.NovelCrawl.Core.Models.DTO;
using D.Util.Interface;
using D.NovelCrawl.Core.Models;

namespace D.NovelCrawl.Core
{
    /// <summary>
    /// 虚拟的 IWebsitProxy 实现
    /// </summary>
    public class VirtualWebsetProxy : IWebsitProxy
    {
        ILogger _logger;

        public VirtualWebsetProxy(
            ILoggerFactory loggerFactory
            )
        {
            _logger = loggerFactory.CreateLogger<VirtualWebsetProxy>();
        }

        public IEnumerable<NovelVolumeModel> NovelCatalog(Guid guid)
        {
            return new NovelVolumeModel[0];
        }

        public IEnumerable<NovelCrawlUrlModel> NovelCrawlUrls(Guid guid)
        {
            return new NovelCrawlUrlModel[]
            {
                new NovelCrawlUrlModel
                {
                    Url = "http://book.qidian.com/info/3602691#Catalog",
                    Official = true
                }
            };
        }

        public ListResult<NovelListModel> NovelList(PageModel page = null)
        {
            return new ListResult<NovelListModel>
            {
                RecordCount = 1,
                PageNumber = 1,
                PageSize = -1,
                CurrPageData = new NovelListModel[]
                {
                    new NovelListModel {
                        Guid = Guid.NewGuid(),
                        Name = "修真聊天群",
                        ChapterCount = 0,
                        LastChapterName = string.Empty,
                        LastChapterNumber = 0 }
                }
            };
        }

        public bool UploadNovelChapter(NovelChapterDetailModel chapter)
        {
            _logger.LogInformation("上传爬取到的章节信息\r\n" + chapter.ToString());
            return true;
        }

        public PageProcessStep UrlPageProcess(string host, UrlTypes type)
        {
            return new PageProcessStep
            {
                Type = PageProcessStepTypes.Html,
                DataNames = "Volumes",
                IsArray = true,
                ProcessStr = "div.volume",
                NextProcessStep = new PageProcessStep
                {
                    Type = PageProcessStepTypes.RegExp,
                    DataNames = "Name",
                    IsArray = false,
                    ProcessStr = @"<h3>[\s\S]*?</a>(?<Name>[\s\S]*?)<i>",
                    NextProcessStep = new PageProcessStep
                    {
                        Type = PageProcessStepTypes.Html,
                        DataNames = "Chapters",
                        IsArray = true,
                        ProcessStr = "ul.cf li a",
                        NextProcessStep = new PageProcessStep
                        {
                            Type = PageProcessStepTypes.RegExp,
                            DataNames = "Name",
                            IsArray = false,
                            ProcessStr = @"<a[^>]*>(?<Name>[\s\S]*?)</a>",
                            NextProcessStep = null
                        }
                    }
                }
            };
        }
    }
}
