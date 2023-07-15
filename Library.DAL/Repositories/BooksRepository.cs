using Library.DAL.Contracts;
using Library.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.DAL.Repositories
{
	internal class BooksRepository : IRepository<Book>
	{
		public Task<Book> Create(Book obj)
		{
			throw new NotImplementedException();
		}

		public void Delete(int id)
		{
			throw new NotImplementedException();
		}

		public Task<IEnumerable<Book>> GetAll()
		{
			throw new NotImplementedException();
		}

		public Task<Book> GetById(int id)
		{
			throw new NotImplementedException();
		}

		public void Update(Book obj)
		{
			throw new NotImplementedException();
		}
	}
}
