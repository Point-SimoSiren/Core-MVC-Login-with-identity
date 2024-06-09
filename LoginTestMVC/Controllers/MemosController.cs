using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LoginTestMVC.Models;

namespace LoginTestMVC.Controllers
{
    public class MemosController : Controller
    {
        private readonly MemoDBContext _context = new MemoDBContext();


        // GET: Memos
        public async Task<IActionResult> Index()
        {
            if (User.Identity?.Name! == null)
            {
                return RedirectToAction("NotLoggedIn");
            }
              return _context.Memos != null ? 
                          View(await _context.Memos.ToListAsync()) :
                          Problem("Entity set 'MemoDBContext.Memos'  is null.");
        }

        // View for user that is not logged in and tries to enter Memos
        public async Task<IActionResult> NotLoggedIn()
        {
            return View();
           
        }

        // GET: Memos/Details/5
        public async Task<IActionResult> Details(int? id)
        {

            if (User.Identity?.Name! == null)
            {
                return RedirectToAction("NotLoggedIn");
            }

            if (id == null || _context.Memos == null)
            {
                return NotFound();
            }

            var memo = await _context.Memos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (memo == null)
            {
                return NotFound();
            }

            return View(memo);
        }

        // GET: Memos/Create
        public IActionResult Create()
        {
            if (User.Identity?.Name! == null)
            {
                return RedirectToAction("NotLoggedIn");
            }
            return View();
        }

        // POST: Memos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Note,Important,CreatedDateTime")] Memo memo)
        {
            if (User.Identity?.Name! == null)
            {
                return RedirectToAction("NotLoggedIn");
            }


            if (ModelState.IsValid)
            {
                _context.Add(memo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(memo);
        }

        // GET: Memos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {

            if (User.Identity?.Name! == null)
            {
                return RedirectToAction("NotLoggedIn");
            }

            if (id == null || _context.Memos == null)
            {
                return NotFound();
            }

            var memo = await _context.Memos.FindAsync(id);
            if (memo == null)
            {
                return NotFound();
            }
            return View(memo);
        }

        // POST: Memos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Note,Important,CreatedDateTime")] Memo memo)
        {

            if (User.Identity?.Name! == null)
            {
                return RedirectToAction("NotLoggedIn");
            }


            if (id != memo.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(memo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MemoExists(memo.Id))
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
            return View(memo);
        }

        // GET: Memos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {

            if (User.Identity?.Name! == null)
            {
                return RedirectToAction("NotLoggedIn");
            }


            if (id == null || _context.Memos == null)
            {
                return NotFound();
            }

            var memo = await _context.Memos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (memo == null)
            {
                return NotFound();
            }

            return View(memo);
        }

        // POST: Memos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {

            if (User.Identity?.Name! == null)
            {
                return RedirectToAction("NotLoggedIn");
            }


            if (_context.Memos == null)
            {
                return Problem("Entity set 'MemoDBContext.Memos'  is null.");
            }
            var memo = await _context.Memos.FindAsync(id);
            if (memo != null)
            {
                _context.Memos.Remove(memo);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MemoExists(int id)
        {
          return (_context.Memos?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
