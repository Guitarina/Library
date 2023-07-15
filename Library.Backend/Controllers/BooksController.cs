using Library.Backend.Data;
using Library.Backend.Models.Domain;
using Library.Backend.Models.Dto;
using Library.BLL.Services;
using Library.DAL.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Configuration;

namespace Library.Backend.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class BooksController : ControllerBase
	{
		private readonly IBooksService _booksService;
		private readonly IConfiguration _configuration;

		public BooksController(IBooksService booksService, IConfiguration configuration)
		{
			_booksService = booksService;
			_configuration = configuration;
		}
		// GET: api/Books
		[HttpGet]
		public async Task<IEnumerable<Book>> GetAllBooks()
		{
			var books = await _booksService;
			return books;
		}

		// GET: api/Books/5
		[HttpGet("{id:int}")]
		public async Task<IActionResult> GetBook(int id)
		{
			var book = _libraryDbContext.Books
				.Include(x => x.Reviews)
				.Include(x => x.Ratings)
				.FirstOrDefault(x => x.Id == id);

			if (book == null)
				return NotFound();

			var bookDetailsDto = new BookDetailsDto()
			{
				Id = book.Id,
				Title = book.Title,
				Author = book.Author,
				Cover = book.Cover,
				Content = book.Content,
				Reviews = book.Reviews
			};
			if (book.Ratings.Any())
				bookDetailsDto.Rating = book.Ratings.Average(x => x.Score);

			return Ok(bookDetailsDto);
		}

		// POST: api/Books/Save
		[HttpPost("save")]
		public async Task<IActionResult> CreateBook(Models.Dto.SaveBookDto book)
		{
			if (book.Id != null)
			{
				var bookFromDb = _libraryDbContext.Books.Find(book.Id);

				if (bookFromDb == null) return BadRequest("Book with provided Id doesn't exist");

				bookFromDb.Title = book.Title;
				bookFromDb.Author = book.Author;
				bookFromDb.Cover = book.Cover;
				bookFromDb.Content = book.Content;
				bookFromDb.Genre = book.Genre;

				_libraryDbContext.Books.Update(bookFromDb);
				_libraryDbContext.SaveChanges();

				return Ok(new { bookFromDb.Id });

			}
			else
			{
				Models.Domain.Book domainBook = new()
				{
					Author = book.Author,
					Content = book.Content,
					Cover = book.Cover,
					Genre = book.Genre,
					Title = book.Title

				};

				await _libraryDbContext.Books.AddAsync(domainBook);
				await _libraryDbContext.SaveChangesAsync();
				return Ok(new { domainBook.Id });

			}



		}

		// PUT: api/Books/5/review
		[HttpPost("{id:int}/review")]
		public async Task<IActionResult> ReviewBook(int id, Models.Dto.ReviewBookDto review)
		{
			var reviewDomain = new Models.Domain.Review
			{
				Message = review.Message,
				Reviewer = review.Reviewer,
				BookId = id
			};


			_libraryDbContext.Reviews.Add(reviewDomain);
			_libraryDbContext.SaveChanges();

			return Ok(new { reviewDomain.Id });
		}

		//PUT: api/Books/5/Rate
		[HttpPut("{id:int}/rate")]
		public async Task<IActionResult> RateBook(int id, Models.Dto.RateBookDto rating)
		{
			var ratingDomain = new Rating
			{
				BookId = id,
				Score = rating.Score,

			};
			await _libraryDbContext.Ratings.AddAsync(ratingDomain);
			await _libraryDbContext.SaveChangesAsync();

			return Ok();
		}
		[HttpDelete("{id:int}")]
		public async Task<IActionResult> DeleteBook(int id, [FromQuery] string secret)
		{
			if (secret == _configuration["SecretKey"])
			{
				var existingBook = _libraryDbContext.Books.Find(id);

				if (existingBook == null) return BadRequest("Book with provided Id doesn't exist");
				_libraryDbContext.Books.Remove(existingBook);
				await _libraryDbContext.SaveChangesAsync();
				return Ok();
			}
			else
			{
				return BadRequest("Wrong secret key");
			}
		}

	}
}
