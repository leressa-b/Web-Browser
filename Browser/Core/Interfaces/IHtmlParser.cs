using Browser.Core.Models;

namespace Browser.Core.Interfaces
{
    public interface IHtmlParser
    {
        void ParsePage(PageContent pageContent);
    }
}
