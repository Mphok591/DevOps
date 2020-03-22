using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProductApp.Models;

namespace ProductApp.Controllers
{
    public class CommentController : Controller
    {
        private readonly ProductDB _context;

        public CommentController(ProductDB context)
        {
            _context = context;
        }

        // GET: Comment
        public async Task<IActionResult> Index()
        {
            var productDB = _context.comments.Include(c => c.Product);
            return View(await productDB.ToListAsync());
        }

        // GET: Comment/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comment = await _context.comments
                .Include(c => c.Product)
                .FirstOrDefaultAsync(m => m.CommentId == id);
            if (comment == null)
            {
                return NotFound();
            }

            return View(comment);
        }

        // GET: Comment/Create
        public IActionResult Create()
        {
            ViewData["ProductId"] = new SelectList(_context.products, "ProductId", "ProductId");
            return View();
        }

        // POST: Comment/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CommentId,CommentName,Email,DateOfComment,ProductId")] Comment comment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(comment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProductId"] = new SelectList(_context.products, "ProductId", "ProductId", comment.Product);
            return View(comment);
        }

        // GET: Comment/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comment = await _context.comments.FindAsync(id);
            if (comment == null)
            {
                return NotFound();
            }
            ViewData["ProductId"] = new SelectList(_context.products, "ProductId", "ProductId", comment.Product);
            return View(comment);
        }

        // POST: Comment/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CommentId,CommentName,Email,DateOfComment,ProductId")] Comment comment)
        {
            if (id != comment.CommentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(comment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CommentExists(comment.CommentId))
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
            ViewData["ProductId"] = new SelectList(_context.products, "ProductId", "ProductId", comment.Product);
            return View(comment);
        }

        // GET: Comment/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comment = await _context.comments
                .Include(c => c.Product)
                .FirstOrDefaultAsync(m => m.CommentId == id);
            if (comment == null)
            {
                return NotFound();
            }

            return View(comment);
        }

        // POST: Comment/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var comment = await _context.comments.FindAsync(id);
            _context.comments.Remove(comment);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CommentExists(int id)
        {
            return _context.comments.Any(e => e.CommentId == id);
        }
    }
}
