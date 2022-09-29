using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using my_books.Data.Models.Services;
using my_books.Data.ViewModels;

namespace my_books.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublisherController : ControllerBase
    {
        private readonly PublisherService publisherDb;
        public PublisherController(PublisherService publisherService)
        {
            this.publisherDb = publisherService;
        }


        [HttpPost("add-publisher")]
        public IActionResult CreatePublisher(PublisherVM publisherVM)
        {
            publisherDb.AddPublisher(publisherVM);
            return Ok();
        }


    }
}
