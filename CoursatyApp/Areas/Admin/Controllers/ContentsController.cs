using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CoursatyApp.Data;
using CoursatyApp.Entitities;

namespace CoursatyApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ContentsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ContentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Admin/Contents/Create
        public IActionResult Create(int categoryItemId,int categoryId)
        {
            Content content = new Content
            {
                CategoryId = categoryId,
                CatItemId = categoryItemId
            };
            return View(content);
        }

        // POST: Admin/Contents/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,HTMLContent,VideoLink,CatItemId,CategoryId")] Content content)
        {
            if (ModelState.IsValid)
            {
                content.CategoryItem = await _context.CategoryItems.FindAsync(content.CatItemId);
                _context.Add(content);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index), "CategoryItems", new { categoryId = content.CategoryId });
            }
            return View(content);
        }

        // GET: Admin/Contents/Edit/5
        public async Task<IActionResult> Edit(int categoryItemId, int categoryId)
        {

            var content = await _context.Contents.SingleOrDefaultAsync(item => item.CategoryItem.Id == categoryItemId);
            content.CategoryId = categoryId;
            if (content == null)
            {
                return NotFound();
            }
            return View(content);
        }

        // POST: Admin/Contents/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,HTMLContent,VideoLink,CatItemId,CategoryId")] Content content)
        {
            if (id != content.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(content);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ContentExists(content.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index),"CategoryItems",new { categoryId = content.CategoryId});
            }
            return View(content);
        }

      
        private bool ContentExists(int id)
        {
            return _context.Contents.Any(e => e.Id == id);
        }
    }
}
