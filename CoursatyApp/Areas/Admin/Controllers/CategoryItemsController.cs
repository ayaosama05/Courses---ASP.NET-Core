﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CoursatyApp.Data;
using CoursatyApp.Entitities;
using CoursatyApp.Extensions;

namespace CoursatyApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryItemsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CategoryItemsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Admin/CategoryItems
        public async Task<IActionResult> Index(int categoryId)
        {
            List<CategoryItem> list = await (from categoryItem in _context.CategoryItems
                                             join contentItem in _context.Contents
                                             on categoryItem.Id equals contentItem.CategoryItem.Id
                                             into getContent
                                             from subContent in getContent.DefaultIfEmpty()
                                             where categoryItem.CategoryId == categoryId
                                             select new CategoryItem
                                             {
                                                 Id = categoryItem.Id,
                                                 Title = categoryItem.Title,
                                                 Description = categoryItem.Description,
                                                 ReleaseDate = categoryItem.ReleaseDate,
                                                 MediaTypeId = categoryItem.MediaTypeId,
                                                 CategoryId = categoryId,
                                                 ContentId = (subContent != null) ? subContent.Id : 0
                                             }).ToListAsync();
            ViewBag.CategoryId = categoryId;
            return View(list);
        }

        // GET: Admin/CategoryItems/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categoryItem = await _context.CategoryItems
                .FirstOrDefaultAsync(m => m.Id == id);
            if (categoryItem == null)
            {
                return NotFound();
            }

            return View(categoryItem);
        }

        // GET: Admin/CategoryItems/Create
        public async Task<IActionResult> Create(int categoryId)
        {
            List<MediaType> mediaTypes = await _context.MediaTypes.ToListAsync();
            CategoryItem categoryItem = new CategoryItem
            {
                CategoryId = categoryId,
                MediaTypes = mediaTypes.ConvertToSelectList(0)
            };
            return View(categoryItem);
        }

        // POST: Admin/CategoryItems/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,ReleaseDate,Description,CategoryId,MediaTypeId")] CategoryItem categoryItem)
        {
            if (ModelState.IsValid)
            {
                _context.Add(categoryItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index),new { categoryId = categoryItem.CategoryId});
            }
            return View(categoryItem);
        }

        // GET: Admin/CategoryItems/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categoryItem = await _context.CategoryItems.FindAsync(id);
            if (categoryItem == null)
            {
                return NotFound();
            }
            List<MediaType> mediaTypes = await _context.MediaTypes.ToListAsync();
            categoryItem.MediaTypes = mediaTypes.ConvertToSelectList(categoryItem.MediaTypeId);
            return View(categoryItem);
        }

        // POST: Admin/CategoryItems/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,ReleaseDate,Description,CategoryId,MediaTypeId")] CategoryItem categoryItem)
        {
            if (id != categoryItem.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(categoryItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoryItemExists(categoryItem.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index) , new { categoryId = categoryItem.CategoryId});
            }
            return View(categoryItem);
        }

        // GET: Admin/CategoryItems/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categoryItem = await _context.CategoryItems
                .FirstOrDefaultAsync(m => m.Id == id);
            if (categoryItem == null)
            {
                return NotFound();
            }

            return View(categoryItem);
        }

        // POST: Admin/CategoryItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var categoryItem = await _context.CategoryItems.FindAsync(id);
            _context.CategoryItems.Remove(categoryItem);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index) , new { categoryId= categoryItem.CategoryId});
        }

        private bool CategoryItemExists(int id)
        {
            return _context.CategoryItems.Any(e => e.Id == id);
        }
    }
}
