using InfoTree.Contexts;
using InfoTree.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InfoTree.Controllers
{
    public class LessonFileController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public LessonFileController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var files = await _context.LessonFiles.ToListAsync();
            return Ok(files);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var file = await _context.LessonFiles.FindAsync(id);
            if (file == null) return NotFound();

            return Ok(file);
        }


        [HttpPost]
        public async Task<IActionResult> Upload([FromForm] IFormFile file, [FromForm] int lessonId)
        {
            if (file == null || file.Length == 0)
                return BadRequest("No file uploaded");

            var uploadsFolder = Path.Combine(_env.WebRootPath, "uploads");
            if (!Directory.Exists(uploadsFolder))
                Directory.CreateDirectory(uploadsFolder);

            var uniqueFileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
            var filePath = Path.Combine(uploadsFolder, uniqueFileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            var lessonFile = new LessonFile
            {
                FileName = file.FileName,
                FilePath = "/uploads/" + uniqueFileName,
                LessonId = lessonId
            };

            _context.LessonFiles.Add(lessonFile);
            await _context.SaveChangesAsync();

            return Ok(lessonFile);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var file = await _context.LessonFiles.FindAsync(id);
            if (file == null) return NotFound();

            var fullPath = Path.Combine(_env.WebRootPath, file.FilePath.TrimStart('/'));
            if (System.IO.File.Exists(fullPath))
                System.IO.File.Delete(fullPath);

            _context.LessonFiles.Remove(file);
            await _context.SaveChangesAsync();

            return Ok("File deleted");
        }
    }
}