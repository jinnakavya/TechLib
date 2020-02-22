using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TechLibrary.Contracts.Responses;
using TechLibrary.Domain;
using TechLibrary.Models;
using TechLibrary.Services;

namespace TechLibrary.Controllers.Tests
{
    [TestFixture()]
    [Category("ControllerTests")]
    public class BooksControllerTests
    {

        private Mock<ILogger<BooksController>> _mockLogger;
        private Mock<IBookService> _mockBookService;
        private Mock<IMapper> _mockMapper;
        private NullReferenceException _expectedException;

        [OneTimeSetUp]
        public void TestSetup()
        {
            _expectedException = new NullReferenceException("Test Failed...");
            _mockLogger = new Mock<ILogger<BooksController>>();
            _mockBookService = new Mock<IBookService>();
            _mockMapper = new Mock<IMapper>();
        }

        #region GetAllBooks
        [Test()]
        public async Task GetAllTest_OnlyCallsOnce()
        {
            //  Arrange
            _mockBookService.Setup(b => b.GetBooksAsync(It.IsAny<PaginationandFilterParameters>())).Returns(Task.FromResult(It.IsAny<PagedResponse<Domain.Book>>()));
            var sut = new BooksController(_mockLogger.Object, _mockBookService.Object, _mockMapper.Object);

            //  Act
            var result = await sut.GetAll(It.IsAny<PaginationandFilterParameters>());

            //  Assert
            _mockBookService.Verify(s => s.GetBooksAsync(It.IsAny<PaginationandFilterParameters>()), Times.Once, "Expected GetBooksAsync to have been called once");
        }

        [Test()]
        public async Task GetAllTest_returnsListOfBooks_WhenBookRecordsAreAvailable()
        {
            //setup
            var books = CreateBooks(40);
            PagedResponse<Book> mockData = new PagedResponse<Book>();
            mockData.Results = books;

            //  Arrange
            _mockBookService.Setup(b => b.GetBooksAsync(It.IsAny<PaginationandFilterParameters>())).Returns(Task.FromResult(mockData));

            var sut = new BooksController(_mockLogger.Object, _mockBookService.Object, _mockMapper.Object);
            //  Act
            var result = (OkObjectResult)await sut.GetAll(It.IsAny<PaginationandFilterParameters>());
            var objectValue = (PagedResponse<Book>)result.Value;
            //  Assert
            Assert.AreEqual(objectValue.Results.Count, mockData.Results.Count);
        }
        [Test()]
        public async Task GetAllTest_returnsRequestedNumberOfResultsPerPage()
        {
            //setup
            var books = CreateBooks(30);
            var mockParameters = new PaginationandFilterParameters();
            mockParameters.PageSize = 7;
            mockParameters.PageNumber = 1;
           
            PagedResponse<Book> mockData = new PagedResponse<Book>();
            mockData.Results = books;
            mockData.PageSize = 7;
            mockData.CurrentPage = 1;
            
            //  Arrange
            _mockBookService.Setup(b => b.GetBooksAsync(mockParameters)).Returns(Task.FromResult(mockData));
            var sut = new BooksController(_mockLogger.Object, _mockBookService.Object, _mockMapper.Object);
            
            //  Act
            var result = (OkObjectResult)await sut.GetAll(mockParameters);
            var objectValue = (PagedResponse<Book>)result.Value;
            
            //  Assert
            Assert.AreEqual(mockParameters.PageSize, objectValue.PageSize);
        }

        #region Helpers
        private List<Book> CreateBooks(int noOfBooks)
        {
            List<Book> booksList = new List<Book>();
            for (int i = 0; i < noOfBooks; i++)
            {
                var book = new Book
                {
                    BookId = TestContext.CurrentContext.Random.Next(),
                    Title = TestContext.CurrentContext.Random.GetString(5),
                    ShortDescr = TestContext.CurrentContext.Random.GetString(15),
                    LongDescr = TestContext.CurrentContext.Random.GetString(70),
                    ISBN = TestContext.CurrentContext.Random.Next().ToString(),
                    PublishedDate = RandomDateTime(new DateTime(2000, 1, 1), new DateTime(2100, 12, 31)).ToShortDateString(),
                };
                booksList.Add(book);
            }
            return booksList;
        }
        private static DateTime RandomDateTime(DateTime start, DateTime end)
        {
            var _random = new Random();
            if (start >= end)
                throw new Exception("start date must be less than end date!");
            TimeSpan timeSpan = end - start;
            byte[] bytes = new byte[8];
            _random.NextBytes(bytes);
            long int64 = Math.Abs(BitConverter.ToInt64(bytes, 0)) % timeSpan.Ticks;
            TimeSpan newSpan = new TimeSpan(int64);
            return start + newSpan;
        }
        #endregion
    }
    #endregion
}
