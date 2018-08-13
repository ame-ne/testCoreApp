using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Moq;
using testCoreApp.Controllers;
using testCoreApp.Models;
using Xunit;

namespace testCoreApp.Tests
{
    public class BookControllerTests
    {
        [Fact]
        public void Can_Paginate()
        {
            Mock<IBookRepository> mock = new Mock<IBookRepository>();
            mock.Setup(x => x.Books).Returns((new Book[] {
                new Book { BookId = Guid.NewGuid(), Title = "B1"},
                new Book { BookId = Guid.NewGuid(), Title = "B2"},
                new Book { BookId = Guid.NewGuid(), Title = "B3"},
                new Book { BookId = Guid.NewGuid(), Title = "B4"},
                new Book { BookId = Guid.NewGuid(), Title = "B5"},
            }).AsQueryable<Book>());

            BookController controller = new BookController(mock.Object);
            controller.PageSize = 2;

            IEnumerable<Book> result = controller.List("", 3).ViewData.Model as IEnumerable<Book>;

            Book[] booksArr = result.ToArray();
            Assert.True(booksArr.Length == 1);
            Assert.Equal("B5", booksArr[0].Title);
        }
    }
}
