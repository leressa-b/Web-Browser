using Browser.Core.Interfaces;
using Browser.Core.Models;
using Browser.Core.Services;
using Browser.DataAccess;
using System.Collections.Generic;

namespace Browser.UI
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();

            // 1. Setup Infrastructure
            IHttpRequester httpRequester = new HttpRequester();
            IHtmlParser htmlParser = new HtmlParser();

            // 2. Setup Data Access (Repository)
            IDataRepository<HomePageSetting> homeRepo = new JsonRepository<HomePageSetting>("homepage.json");
            IDataRepository<List<Bookmark>> bookmarkRepo = new JsonRepository<List<Bookmark>>("bookmarks.json");
            IDataRepository<List<History>> historyRepo = new JsonRepository<List<History>>("history.json");

            // 3. Setup Manager (Business Logic)
            IBrowserService browserService = new BrowserService(httpRequester, htmlParser);
            IHomePageManager homePageManager = new HomePageManager(homeRepo);
            IBookmarkManager bookmarkManager = new BookmarkManager(bookmarkRepo);
            IHistoryManager historyManager = new HistoryManager(historyRepo);

            Application.Run(new Forms.MainForm(browserService, homePageManager, bookmarkManager, historyManager));
        }
    }
}