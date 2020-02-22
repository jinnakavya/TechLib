using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TechLibrary.Contracts.Responses;
using TechLibrary.Data;
using TechLibrary.Domain;
using TechLibrary.Helpers;
using TechLibrary.Models;

namespace TechLibrary.Services
{
    public interface IBookService
    {
        Task<PagedResponse<Book>> GetBooksAsync(PaginationandFilterParameters paginationParameters = null);
        Task<Book> GetBookByIdAsync(int bookid);
        Book Update(Book bookChanges);
    }

    public class BookService : IBookService
    {
        private readonly DataContext _dataContext;

        public BookService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<PagedResponse<Book>> GetBooksAsync(PaginationandFilterParameters paginationParameters = null)
        {
            PagedResponse<Book> result = new PagedResponse<Book>();
            try
            {
                var queryable = _dataContext.Books.AsQueryable();
                if (!String.IsNullOrEmpty(paginationParameters.SearchText))
                    queryable = SearchForValue(queryable, paginationParameters);
                result = await PaginateBookRecords(queryable, paginationParameters);
            }
            catch (Exception ex)
            {
                throw new Exception("Unable to retrieve records" + ex.ToString());
            }
            return result;
        }

        public async Task<Book> GetBookByIdAsync(int bookid)
        {
            return await _dataContext.Books.SingleOrDefaultAsync(x => x.BookId == bookid);
        }
        public Book Update(Book bookChanges)
        {
            _dataContext.Books.Update(bookChanges);
            _dataContext.Entry(bookChanges).State = EntityState.Modified;
            _dataContext.SaveChanges();
            return bookChanges;
        }
        private async Task<PagedResponse<Book>> PaginateBookRecords(IQueryable<Book> queryable, PaginationandFilterParameters paginationParameters = null)
        {
            PagedResponse<Book> result = new PagedResponse<Book>();
            if (paginationParameters != null)
            {
                result = await Utilities.GetPaged<Book>(queryable, paginationParameters.PageNumber, paginationParameters.PageSize);
            }
            else
            {
                //if pagination parameters are null return all records on page 1
                result = await Utilities.GetPaged<Book>(queryable, 1, queryable.Count());

            }
            return result;
        }

        private IQueryable<Book> SearchForValue(IQueryable<Book> queryable, PaginationandFilterParameters filterParameters)
        {
            //return queryable.Where(x => (x.ShortDescr.ToLower().Contains(filterParameters.Term) || x.Title.ToLower().Contains(filterParameters.Term)));
            return queryable.Where(o => (o.Title.ToLower().Contains(filterParameters.SearchText.Trim().ToLower())
                                || o.ShortDescr.ToLower().Contains(filterParameters.SearchText.Trim().ToLower())
                                || o.ISBN.ToLower().Contains(filterParameters.SearchText.Trim().ToLower())
                                || o.LongDescr.ToLower().Contains(filterParameters.SearchText.Trim().ToLower())));
        }
    }
}
