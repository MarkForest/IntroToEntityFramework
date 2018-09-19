using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseFirest2
{
    class Program
    {
        static void GetAllBooks()
        {
            using (BookStoreEntities db = new BookStoreEntities())
            {
                var books = db.Book.OrderBy(s => s.Title).ToList();
                foreach (var book in books)
                {
                    Console.WriteLine($"Book:{book.Title}, price:{book.Price}, author:{book.Author.FirstName} {book.Author.LastName}");
                }
            }
        }
        static void Init()
        {
            Author author = new Author { FirstName = "Ray", LastName = "Bradbury" };
            AddAuthor(author);
            author = new Author { FirstName = "Harry", LastName = "Harrison" };
            AddAuthor(author);
            author = new Author { FirstName = "Clifford", LastName = "Simak" };
            AddAuthor(author);
            Publisher publisher = new Publisher { PublisherName = "Rainbow", Address = "Kyiv" };
            AddPublisher(publisher);
            publisher = new Publisher { PublisherName = "Exlibris", Address = "Kyiv" };
            AddPublisher(publisher);
            Book book = new Book { Title = "Way Station", IdAuthor = 1, IdPublisher = 2
            , Pages = 234, Price = 234};
            AddBook(book);
            book = new Book
            {
                Title = "Ring Around the Sun",
                IdAuthor = 7,
                IdPublisher = 1,
                Pages = 234,
                Price = 234
            };
            AddBook(book);
            book = new Book
            {
                Title = "The Martian Chronicles",
                IdAuthor = 2,
                IdPublisher = 2,
                Pages = 234,
                Price = 234
            };
            AddBook(book);


        }
        static void AddBook(Book book)
        {
            using (BookStoreEntities db = new BookStoreEntities())
            {
                Book p = db.Book
                    .Where(x => x.Title == book.Title)
                    .FirstOrDefault();
                if (p == null)
                {
                    db.Book.Add(book);
                    db.SaveChanges();
                    Console.WriteLine("New publisher added: " + book.Title);
                }
            }
        }
        static void AddPublisher(Publisher publisher)
        {
            using (BookStoreEntities db = new BookStoreEntities())
            {
                Publisher p = db.Publisher
                    .Where(x => x.PublisherName == publisher.PublisherName)
                    .FirstOrDefault();
                if(p == null)
                {
                    db.Publisher.Add(publisher);
                    db.SaveChanges();
                    Console.WriteLine("New publisher added: "+publisher.PublisherName);
                }
            }
        }
        static Author GetAuthorById(int id)
        {
            using (BookStoreEntities db = new BookStoreEntities())
            {
                var au = db.Author.Find(id);
                return au;
            }
        }
        static Author GetAuthorByName(string fname)
        {
            using (BookStoreEntities db = new BookStoreEntities())
            {
                return (from a in db.Author
                              where a.FirstName == fname
                              select a).FirstOrDefault<Author>();
            }
        }
        static void GetAllAuthors()
        {
            using (BookStoreEntities db = new BookStoreEntities())
            {
                var authors = db.Author.OrderBy(x=>x.LastName).ToList();
                authors = (from a in db.Author 
                           orderby a.LastName ascending select a).ToList();
                foreach (var author in authors)
                {
                    Console.WriteLine($"{author.FirstName} {author.LastName}");
                }
            }
        }
        static void AddAuthor(Author author)
        {
            using (BookStoreEntities db = new BookStoreEntities())
            {
                db.Author.Add(author);
                db.SaveChanges();
                Console.WriteLine($"New author added: {author.LastName}");
            }
        }
        static void Main(string[] args)
        {
            //Author author = new Author { FirstName = "Whillim", LastName="Sheckspaer"};
            //AddAuthor(author);
            //GetAllAuthors();
            //Author author = GetAuthorByName("Савин");
            //Author a = GetAuthorById(7);
            //Console.WriteLine(a.FirstName + " " + a.LastName);
            //Init();
            GetAllBooks();
            Console.ReadKey();

        }
    }
}
