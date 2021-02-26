using Microsoft.AspNetCore.Mvc;
using StackExchange_Redis_NetCore.Cache;
using StackExchange_Redis_NetCore.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace StackExchange_Redis_NetCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private List<Book> books=new List<Book>();
        ICacheService _cacheService;
        public BookController(ICacheService cacheService)
        {
            _cacheService = cacheService;
            books.Add(new Book { Id = 0, name = "abc" });
            books.Add(new Book { Id = 1, name = "dfc" });
            books.Add(new Book { Id = 2, name = "qwe" });
        }

        [HttpGet]
        public List<Book> Get()
        {
            string key = "books";
            if (!_cacheService.Any(key))
            {
                _cacheService.add(key, books);
            }
            return _cacheService.get<List<Book>>(key);

        }
        // GET api/<BookController>/5
        [HttpGet("{id}")]
        public Book Get(int id)
        {
            string key = id.ToString();
            if (!_cacheService.Any(key))
            {
                _cacheService.add(key, books[id]);
            }
            return _cacheService.get<Book>(key); ;
            
        }
        // POST api/<BookController>
        [HttpPost]
        public void Post([FromBody] Book value)
        {
            books.Add(value);
            _cacheService.add(value.Id.ToString(), value);
        }

        // PUT api/<BookController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Book value)
        {
            books[value.Id] = value;
        }

        // DELETE api/<BookController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            string key = id.ToString();
            if (!_cacheService.Any(key))
            {
                _cacheService.remove(key);
            }
            books.Remove(books[id]);

        }
    }
}
