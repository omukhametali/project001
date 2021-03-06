﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using project001.Models;
using project001.Data;

namespace project001.Controllers
{
    public class CommentsController : Controller{
        private readonly ApplicationDbContext _context;

        public CommentsController(ApplicationDbContext context){
            _context = context;
        }

        public async Task<IActionResult> Index(){
            return View(await _context.Comments.ToListAsync());
        }
        
        public async Task<IActionResult> Details(string id){
            if (id == null){
                return NotFound();
            }
            var comments = await _context.Comments.FirstOrDefaultAsync(m => m.Id == id);
            if (comments == null){
                return NotFound();
            }
            return View(comments);
        }

        public IActionResult Create(){
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Content")] Comments comments){
            if (ModelState.IsValid){
                _context.Add(comments);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(comments);
        }
        
        public async Task<IActionResult> Edit(string id){
            if (id == null){
                return NotFound();
            }
            var comments = await _context.Comments.FindAsync(id);
            if (comments == null){
                return NotFound();
            }
            return View(comments);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,Content")] Comments comments){
            if (id != comments.Id){
                return NotFound();
            }
            if (ModelState.IsValid){
                try{
                    _context.Update(comments);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException){
                    if (!CommentsExists(comments.Id)){
                        return NotFound();
                    }
                    else{
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(comments);
        }
        
        public async Task<IActionResult> Delete(string id){
            if (id == null){
                return NotFound();
            }
            var comments = await _context.Comments.FirstOrDefaultAsync(m => m.Id == id);
            if (comments == null){
                return NotFound();
            }
            return View(comments);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id){
            var comments = await _context.Comments.FindAsync(id);
            _context.Comments.Remove(comments);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CommentsExists(string id){
            return _context.Comments.Any(e => e.Id == id);
        }
    }
}
