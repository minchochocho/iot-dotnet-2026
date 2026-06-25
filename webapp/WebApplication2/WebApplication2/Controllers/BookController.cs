
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication2.Models;

public class BookController : Controller {
    private readonly MySqlDbContext _context;

    public BookController(MySqlDbContext context)
    {
        _context = context;
    }

    // GET: Book
    public async Task<IActionResult> Index()
    {
        return View(await _context.Books.ToListAsync());
    }

    // GET: Book/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var book = await _context.Books
            .FirstOrDefaultAsync(m => m.Id == id);
        if (book == null)
        {
            return NotFound();
        }

        return View(book);
    }

    // GET: Book/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: Book/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Author,DivCode,BookName,ReleaseDt,ISBN,Price")] Book book)
    {
        if (ModelState.IsValid)
        {
            _context.Add(book);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(book);
    }

    // GET: Book/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var book = await _context.Books.FindAsync(id);
        if (book == null)
        {
            return NotFound();
        }
        return View(book);
    }

    // POST: Book/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? id, [Bind("Id,Author,DivCode,BookName,ReleaseDt,ISBN,Price")] Book book)
    {
        if (id != book.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(book);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookExists(book.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }
        return View(book);
    }

    // GET: Book/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var book = await _context.Books
            .FirstOrDefaultAsync(m => m.Id == id);
        if (book == null)
        {
            return NotFound();
        }

        return View(book);
    }

    // POST: Book/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? id)
    {
        var book = await _context.Books.FindAsync(id);
        if (book != null)
        {
            _context.Books.Remove(book);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool BookExists(int? id)
    {
        return _context.Books.Any(e => e.Id == id);
    }
}
