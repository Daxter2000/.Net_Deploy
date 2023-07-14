using InternationalAPI.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace InternationalAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {

        private readonly IStringLocalizer<PostsController> _stringLocalizer;
        private readonly IStringLocalizer<SharedResource> _setResourceLocalizer;

        public PostsController(IStringLocalizer<PostsController> stringLocalizer, IStringLocalizer<SharedResource> setResourceLocalizer)

        {
            _stringLocalizer = stringLocalizer;
            _setResourceLocalizer = setResourceLocalizer;
        }

        [HttpGet]
        [Route("PostsControllerResource")]
        public IActionResult GetUsingPostControllerResource() 
        {
            //find text
            var article = _stringLocalizer["Article"];
            var postName = _stringLocalizer.GetString("welcome").Value ?? String.Empty;

            return Ok(new
            {
                PostType = article.Value,
                PostName = postName
            });
        }

        [HttpGet]
        [Route("SharedResource")]
        public IActionResult GetUsingSharedResources()
        {
            //find text
            var article = _stringLocalizer["Article"];
            var postName = _stringLocalizer.GetString("welcome").Value ?? String.Empty;
            var todayIs = string.Format(_setResourceLocalizer.GetString("TodayIs"),DateTime.Now.ToLongDateString());


            return Ok(new
            {
                PostType = article.Value,
                PostName = postName,
                TodayIs = todayIs
            });
        }


    }
}
