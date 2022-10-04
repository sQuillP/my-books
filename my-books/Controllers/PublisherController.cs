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

        [HttpGet("get-all-publishers")]
        public IActionResult GetAllPublishers(string? sortBy, string? searchString, int?pageNumber)
        {
            try
            {
                var allPublishers = publisherDb.GetAllPublishers(sortBy,searchString,pageNumber);
                return Ok(allPublishers);
            }
            catch (Exception e)
            {
                return BadRequest("Sorry, we could not load the publishers");
            }
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
        //ActionResult<CustomActionResult>
        public IActionResult GetPublisherById(int id)
        {
            //throw new Exception("This is an exception that will be handled by middleware");
            var _response = publisherDb.GetPublisherById(id);
            if(_response != null)
            {
                //var _responseObj = new CustomActionResultVM()
                //{
                //    Publisher = _response
                //};

                return Ok(_response);
                //return new CustomActionResult(_responseObj);
               //return _response;
            }
            else
            {
                //var _responseObj = new CustomActionResultVM()
                //{
                //    Exception = new Exception("This is coming from publishers controller.")
                //};

                //return new CustomActionResult(_responseObj);
                return NotFound();
            }
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
