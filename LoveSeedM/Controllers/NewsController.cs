//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using LoveSeedM.DTOs;
//using LoveSeedM.Models;
//using System.Linq;

//namespace LoveSeedM.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class NewsController : ControllerBase
//    {
//        private readonly MyDbContext _context;

//        public NewsController(MyDbContext context)
//        {
//            _context = context;
//        }

//        // GET: api/News
//        [HttpGet]
//        public IActionResult GetNews()
//        {
//            var newsList = _context.News
//                .Select(n => new NewsDTO
//                {
//                    Id = n.Id,
//                    Title = n.Title,
//                    Content = n.Content,
//                    Image = n.Image,
//                    PublishedDate = n.PublishedDate,
//                    Status = n.Status,
//                    CreatedBy = n.CreatedBy != null ? n.CreatedBy.Username : null
//                }).ToList();

//            return Ok(newsList);
//        }

//        // GET: api/News/5
//        [HttpGet("{id}")]
//        public IActionResult GetNews(int id)
//        {
//            var news = _context.News
//                .Where(n => n.Id == id)
//                .Select(n => new NewsDTO
//                {
//                    Id = n.Id,
//                    Title = n.Title,
//                    Content = n.Content,
//                    Image = n.Image,
//                    PublishedDate = n.PublishedDate,
//                    Status = n.Status,
//                    CreatedBy = n.CreatedBy != null ? n.CreatedBy.Username : null
//                }).FirstOrDefault();

//            if (news == null)
//            {
//                return NotFound("News not found.");
//            }

//            return Ok(news);
//        }

//        // PUT: api/News/5
//        [HttpPut("{id}")]
//        public IActionResult PutNews(int id, [FromBody] UpdateNewsDTO newsDto)
//        {
//            var news = _context.News.Find(id);

//            if (news == null)
//            {
//                return NotFound("News not found.");
//            }

//            news.Title = newsDto.Title;
//            news.Content = newsDto.Content;
//            news.Status = newsDto.Status;

//            _context.Entry(news).State = EntityState.Modified;

//            try
//            {
//                _context.SaveChanges();
//            }
//            catch (DbUpdateConcurrencyException)
//            {
//                if (!NewsExists(id))
//                {
//                    return NotFound("News not found.");
//                }
//                else
//                {
//                    throw;
//                }
//            }

//            return NoContent();
//        }

//        // POST: api/News
//        [HttpPost]
//        public IActionResult PostNews([FromForm] CreateNewsDTO newsDto)
//        {
//            var news = new News
//            {
//                Title = newsDto.Title,
//                Content = newsDto.Content,
//                Status = newsDto.Status,
//                PublishedDate = DateTime.Now,
//                CreatedById = newsDto.CreatedById
//            };

//            if (newsDto.Image != null)
//            {
//                var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "Uploads", newsDto.Image.FileName);
//                using (var stream = new FileStream(imagePath, FileMode.Create))
//                {
//                    newsDto.Image.CopyTo(stream);
//                }
//                news.Image = newsDto.Image.FileName;
//            }

//            _context.News.Add(news);
//            _context.SaveChanges();

//            return CreatedAtAction(nameof(GetNews), new { id = news.Id }, news);
//        }

//        // DELETE: api/News/5
//        [HttpDelete("{id}")]
//        public IActionResult DeleteNews(int id)
//        {
//            var news = _context.News.Find(id);

//            if (news == null)
//            {
//                return NotFound("News not found.");
//            }

//            _context.News.Remove(news);
//            _context.SaveChanges();

//            return NoContent();
//        }

//        private bool NewsExists(int id)
//        {
//            return _context.News.Any(e => e.Id == id);
//        }
//    }
//}
