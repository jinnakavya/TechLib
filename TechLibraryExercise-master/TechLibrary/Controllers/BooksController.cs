using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using TechLibrary.Domain;
using TechLibrary.Models;
using TechLibrary.Services;
using System;

namespace TechLibrary.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BooksController : Controller
    {
        private readonly ILogger<BooksController> _logger;
        private readonly IBookService _bookService;
        private readonly IMapper _mapper;

        public BooksController(ILogger<BooksController> logger, IBookService bookService, IMapper mapper)
        {
            _logger = logger;
            _bookService = bookService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery]PaginationandFilterParameters paginationParameters = null)
        {
            _logger.LogInformation("Get all books");

            var books = await _bookService.GetBooksAsync(paginationParameters);

            return Ok(books);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            _logger.LogInformation($"Get book by id {id}");

            var book = await _bookService.GetBookByIdAsync(id);

            var bookResponse = _mapper.Map<BookResponse>(book);

            return Ok(bookResponse);
        }
        [HttpPut("{id}")]
        public IActionResult UpdateBook(int id, [FromBody]Book book)
        {
            Book bookResponse = null;
            try
            {
                _logger.LogInformation($"Update book");

                var response = _bookService.Update(book);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error while updating book", ex.Message);
            }
            return Ok(bookResponse);
        }
    }
}
