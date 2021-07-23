using CoursatyApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NewsAPI;
using NewsAPI.Constants;
using NewsAPI.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace CoursatyApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            //Response.Headers.Add("Refresh", "5");

            //https://newsapi.org/v2/everything?q=technology&from=2021-07-23&sortBy=popularity&apiKey=8ea678f7eb5143a28296fe145e491e53
            var newsApiClient = new NewsApiClient("8ea678f7eb5143a28296fe145e491e53");
            var articlesResponse = newsApiClient.GetEverything(new EverythingRequest
            {
                Q = "technology",
                SortBy = SortBys.Popularity,
                Language = Languages.EN,
                From = new DateTime(2021, 7, 01)
            });
            if (articlesResponse.Status == Statuses.Ok)
            {
                // total results found
                Console.WriteLine(articlesResponse.TotalResults);
                // here's the first 5
                ViewBag.News = articlesResponse.Articles.Take(5);
                //foreach (var article in articlesResponse.Articles)
                //{
                //    // title
                //    Console.WriteLine(article.Title);
                //    // author
                //    Console.WriteLine(article.Author);
                //    // description
                //    Console.WriteLine(article.Description);
                //    // url
                //    Console.WriteLine(article.Url);
                //    // published at
                //    Console.WriteLine(article.PublishedAt);
                //}
            }
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
