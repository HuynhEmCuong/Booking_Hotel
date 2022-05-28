using Booking_Hotel.Application.ViewModels;
using Booking_Hotel.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using X.PagedList;

namespace Booking_Hotel.Web.Controllers
{
    public class ArticleController : Controller
    {

        #region Fields
        private readonly ILogger<ArticleController> _logger;
        private readonly IOptions<Config> _config;
        private readonly HttpClient client;
        #endregion

        #region Ctor
        public ArticleController(ILogger<ArticleController> logger, IHttpClientFactory clientFactory, IOptions<Config> config)
        {
            client = clientFactory.CreateClient("default");
            _logger = logger;
            _config = config;
        }
        #endregion

        [Route("/tin-tuc/{page?}", Name = "article-default")]
        [Route("/tin-tuc/{page?}/{catid?}/{title?}", Name = "article")]
        public async Task<IActionResult> Index(int? page, int catID = 0, string title = "")
        {  //Khai báo pageSize dùng chung
            int pageNumper = page ?? 1;
            //Khai báo url đến api cần gọi
            string apiUrl = $"api/article/getall";
            if (catID > 0)
            {
                apiUrl = $"api/article/getallbycatid?catId={catID}";
            }

            var response = await client.GetAsync(apiUrl);

            if (response == null)
                return View();

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
                return View();

            string json = response.Content.ReadAsStringAsync().Result;
            List<ArticleViewModel> dataTemp = JsonConvert.DeserializeObject<List<ArticleViewModel>>(json);
            IPagedList<ArticleViewModel> data = dataTemp.ToPagedList(pageNumper, 12);
            ViewBag.Url = _config.Value.ApiUrl;
            if (catID > 0)
            {
                ViewBag.CatID = catID;
                var responseArticleCategory = await client.GetAsync($"api/articlecategory/getbyid?id={catID}");

                if (responseArticleCategory == null)
                    return View();

                if (responseArticleCategory.StatusCode != System.Net.HttpStatusCode.OK)
                    return View();

                string jsonArticleCategory = responseArticleCategory.Content.ReadAsStringAsync().Result;
                ArticleCategoryViewModel dataArticleCategory = JsonConvert.DeserializeObject<ArticleCategoryViewModel>(jsonArticleCategory);
                ViewBag.ArticleCategory = dataArticleCategory;
                ViewBag.Article = dataTemp;
            }


            return View();
        }
        [Route("/tin-tuc/chi-tiet/{id?}/{catid?}/{title?}", Name = "article-detail")]
        public async Task<IActionResult> Detail(int ID = 0, int catID = 1, string title = "")
        {
            var response = await client.GetAsync($"api/article/getbyid?id={ID}");

            if (response == null)
                return View();

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
                return View();

            string json = response.Content.ReadAsStringAsync().Result;
            ArticleViewModel data = JsonConvert.DeserializeObject<ArticleViewModel>(json);
            ViewBag.Url = _config.Value.ApiUrl;
            ViewBag.ID = ID;
            ViewBag.CatID = catID;
            return View();
        }

    }
}
