using D.NovelCrawl.Core.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using D.NovelCrawl.Core.Models.DTO;

namespace D.NovelCrawl.Core
{
    /// <summary>
    /// 虚拟的 IWebsitProxy 实现
    /// </summary>
    public class VirtualWebsetProxy : IWebsitProxy
    {
        public IEnumerable<NovelVolumeModel> NovelCatalog(Guid guid)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<NovelCrawlUrlModel> NovelCrawlUrls(Guid guid)
        {
            throw new NotImplementedException();
        }

        public ListResult<NovelListModel> NovelList(PageModel page)
        {
            throw new NotImplementedException();
        }

        public bool UploadNovelChapter(NovelChapterDetailModel chapter)
        {
            throw new NotImplementedException();
        }
    }
}
