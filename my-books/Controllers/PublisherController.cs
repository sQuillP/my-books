using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using my_books.ActionResults;
using my_books.Data.Models;
using my_books.Data.Models.Services;
using my_books.Data.ViewModels;
using my_books.Exceptions;

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
            try
            {
                var newPublisher = publisherDb.AddPublisher(publisherVM);
                return Created(nameof(CreatePublisher), newPublisher);
            }
            catch(PublisherNameException ex)
            {
                return BadRequest($"{ex.Message}, Publisher name: {ex.PublisherName}");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("get-publisher-by-id/{id}")]
        public ActionResult<CustomActionResult> GetPublisherById(int id)
        {
            //throw new Exception("This is an exception that will be handled by middleware");
            var _response = publisherDb.GetPublisherById(id);
            if(_response != null)
            {
                var _responseObj = new CustomActionResultVM()
                {
                    Publisher = _response
                };

                return new CustomActionResult(_responseObj);
               //return _response;
            }
            return (NotFound());
        }

       
        [HttpGet("get-publisher-books-with-authors/{id}")]
        public IActionResult GetPublisherData(int id)
        {
            var _response = publisherDb.GetPublisherData(id);
            return Ok(_response);
        }


        [HttpDelete("delete-publisher-by-id/{id}")]
        public IActionResult DeletePublisherById(int id)
        {
            try
            {
              
                publisherDb.deletePublisherById(id);
                return Ok();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
