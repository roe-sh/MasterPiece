using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LoveSeedM.DTOs;
using LoveSeedM.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoveSeedM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly MyDbContext _db;

        public CategoriesController(MyDbContext db)
        {
            _db = db;
        }

        // GET: api/Categories/getallcategories
        [HttpGet("getallcategories")]
        public IActionResult GetAllCategories()
        {
            var categories = _db.Categories.Select(category => new CategoryDTO
            {
                Id = category.Id,
                CategoryName = category.CategoryName,
                Description = category.Description,
                Image = category.Image
            }).ToList();

            return Ok(categories);
        }

        // GET: api/Categories/getcategorybyid/{id}
        [HttpGet("getcategorybyid/{id}")]
        public IActionResult GetCategoryById(int id)
        {
            var category = _db.Categories.FirstOrDefault(x => x.Id == id);
            if (category == null)
            {
                return NotFound("Category not found.");
            }

            var categoryDTO = new CategoryDTO
            {
                Id = category.Id,
                CategoryName = category.CategoryName,
                Description = category.Description,
                Image = category.Image
            };

            return Ok(categoryDTO);
        }

        [HttpPost("addcategory")]
        [Consumes("multipart/form-data")]  // Specifies that the action consumes multipart/form-data
        public async Task<IActionResult> AddCategory([FromForm] AddCategoryDTO newCategory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Save the image file to a folder
            var imagesFolder = Path.Combine(Directory.GetCurrentDirectory(), "Uploads");
            if (!Directory.Exists(imagesFolder))
            {
                Directory.CreateDirectory(imagesFolder);
            }

            // Generate a unique name for the file to avoid conflicts
            var imageFileName = Guid.NewGuid() + Path.GetExtension(newCategory.Image.FileName);
            var imageFilePath = Path.Combine(imagesFolder, imageFileName);

            // Save the image to the folder
            using (var stream = new FileStream(imageFilePath, FileMode.Create))
            {
                await newCategory.Image.CopyToAsync(stream);
            }

            // Create the Category object
            var category = new Category
            {
                CategoryName = newCategory.CategoryName,
                Description = newCategory.Description,
                Image = imageFileName  // Store the file name or path in the database
            };

            // Add the new category to the database
            _db.Categories.Add(category);
            await _db.SaveChangesAsync();

            // Return the created category
            return Ok(category);
        }

        // PUT: api/Categories/updatecategory/{id}
        [HttpPut("updatecategory/{id}")]
        public IActionResult UpdateCategory(int id, [FromBody] UpdateCategoryDTO updatedCategory)
        {
            var category = _db.Categories.FirstOrDefault(x => x.Id == id);
            if (category == null)
            {
                return NotFound("Category not found.");
            }

            category.CategoryName = updatedCategory.CategoryName;
            category.Description = updatedCategory.Description;
            category.Image = updatedCategory.Image;

            _db.Categories.Update(category);
            _db.SaveChanges();
            return Ok(category);
        }

        // DELETE: api/Categories/deletecategory/{id}
        [HttpDelete("deletecategory/{id}")]
        public IActionResult DeleteCategory(int id)
        {
            var category = _db.Categories.FirstOrDefault(x => x.Id == id);
            if (category == null)
            {
                return NotFound("Category not found.");
            }

            _db.Categories.Remove(category);
            _db.SaveChanges();
            return Ok();
        }
    }
}
