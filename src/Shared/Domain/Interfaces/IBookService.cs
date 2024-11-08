﻿using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services
{
	public interface IBookService
	{
		void AddBook(Book book);
		void DeleteBook(int id);
		void EditBook(int id, Book newBook);
		Book GetBookById(int id);
	}
}
