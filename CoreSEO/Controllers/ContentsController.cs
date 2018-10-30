using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CoreSEO.Data;
using CoreSEO.Models;

namespace CoreSEO.Controllers
{
    public class ContentsController : Controller
    {
        private readonly CoreSEOContext _context;

        public ContentsController(CoreSEOContext context)
        {
            _context = context;
        }

        // GET: Contents
        public async Task<IActionResult> Index(string sortOrder,string SearchString, string currentFilter, int? page)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["TitleSort"] = String.IsNullOrEmpty(sortOrder) ? "title_desc" : "";
            ViewData["PublishedDateSort"] = sortOrder == "publisheddate" ? "publisheddate_desc" : "publisheddate";
            

            if (SearchString != null)
            {
                page = 1;
            }
            else
            {
                SearchString = currentFilter;
            }

            ViewData["CurrentFilter"] = SearchString;

            var contents = from s in _context.Contents
                           select s;
            if (!String.IsNullOrEmpty(SearchString))
            {
                contents = contents.Where(s => s.Title.Contains(SearchString));
            }

           

            switch (sortOrder)
            {
                case "title_desc":
                    contents = contents.OrderByDescending(s => s.Title);
                    break;
                case "publisheddate":
                    contents = contents.OrderBy(s => s.PublishedDate);
                    break;
                case "publisheddate_desc":
                    contents = contents.OrderByDescending(s => s.PublishedDate);
                    break;
                default:
                    contents = contents.OrderBy(s => s.Title);
                    break;
            }
            //return View(await contents.AsNoTracking().ToListAsync());
            int pageSize = 3;
            return View(await PaginatedList<Content>.CreateAsync(contents.AsNoTracking(), page ?? 1, pageSize));
        }

        // GET: Contents/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var content = await _context.Contents
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.ID == id);
            if (content == null)
            {
                return NotFound();
            }

            return View(content);
        }

        // GET: Contents/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Contents/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Title,Desciption,Keywords,PublishedDate")] Content content)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(content);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception ex)
            {
                 //Log exception here
                 
            }
            return View(content);
        }

        // GET: Contents/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var content = await _context.Contents.FindAsync(id);
            if (content == null)
            {
                return NotFound();
            }
            return View(content);
        }

        // POST: Contents/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("ID,Title,Desciption,Keywords,PublishedDate")] Content content)
        public async Task<IActionResult> EditContentPost(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var contentToUpdate = await _context.Contents.SingleOrDefaultAsync(c => c.ID == id);
            if (await TryUpdateModelAsync<Content>(
                contentToUpdate,
                "",
                  c => c.Title, c => c.Desciption, c => c.Keywords, c => c.PublishedDate))
            {
                try
                {
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    //Log the error  
               
                }
            }
            return View(contentToUpdate);
        }

        // GET: Contents/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var content = await _context.Contents
                .FirstOrDefaultAsync(m => m.ID == id);
            if (content == null)
            {
                return NotFound();
            }

            return View(content);
        }

        // POST: Contents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
         

            var content = await _context.Contents
        .AsNoTracking()
        .SingleOrDefaultAsync(m => m.ID == id);
            if (content == null)
            {
                return RedirectToAction(nameof(Index));
            }

            try
            {
                _context.Contents.Remove(content);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                //Log the error 
                return RedirectToAction(nameof(Delete), new { id = id, saveChangesError = true });
            }
        }

        private bool ContentExists(int id)
        {
            return _context.Contents.Any(e => e.ID == id);
        }
    }
}
