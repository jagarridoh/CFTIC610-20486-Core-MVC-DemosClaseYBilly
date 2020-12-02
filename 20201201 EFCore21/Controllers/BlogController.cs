using _20202101_EFCore21.Controllers;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using _20202101_EFCore21.Models;
using _20202101_EFCore21.Data;


namespace _20202101_EFCore21.Controllers
{
    public class BlogController : Controller
    {
        private readonly BlogContext _context;
        public BlogController(BlogContext context)  //Inyección de la clase de contexto de datos en el constructor del controlador
        {
            _context = context;
        }
        // ACciones dentro del controlador CRUD
        // (C)reación de registros en la base de datos

        // Cuando no pones la decoración del verbo HTTP, se usa GET.
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Blog blog)
        {
            if (ModelState.IsValid)
            {
                _context.Add(blog);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(blog);
        }

        // (R)ead -
        public IActionResult Index()
        {
            return View(_context.Blogs.ToList());
        }
        public IActionResult Details(int? id)
        {
            if (id == null)
                return NotFound();
            var blog = _context.Blogs.FirstOrDefault(d => d.BlogId == id);  //LINQ
            if (blog == null)
                return NotFound();
            return View(blog);
        }


        // (U)pdate
        [HttpPost]
        public IActionResult Edit(int? id, Blog blog)  // este sí que guarda los datos recibidos en el server
        {
            if (id == null)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(blog);
                    _context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BlogExists(blog.BlogId))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(blog);
        }

        public async Task<IActionResult> Edit(int? id)  // Abrir un elemento en modo edicion: por eso es GET: no se han enviado datos aun
        {
            if (id == null)
                return NotFound();
            var blog = await _context.Blogs.FindAsync(id);
            if (blog == null)
                return NotFound();
            return View(blog);
        }

        // (D)elete
        public IActionResult Delete(int? id)
        {
            if (id == null)
                return NotFound();
            var blog = _context.Blogs.FirstOrDefault(b => b.BlogId == id);
            if (blog == null)
                return NotFound();
            return View(blog);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var blog = _context.Blogs.Find(id);
            _context.Blogs.Remove(blog);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public bool BlogExists(int id)
        {
            return _context.Blogs.Any(e => e.BlogId == id);
        }

        
    }

}