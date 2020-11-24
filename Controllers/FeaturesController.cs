using System;
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
    public class FeaturesController : Controller{
        private readonly ApplicationDbContext _context;

        public FeaturesController(ApplicationDbContext context){
            _context = context;
        }

        public async Task<IActionResult> Index(){
            return View(await _context.Features.ToListAsync());
        }

        public async Task<IActionResult> Details(string id){
            if (id == null){
                return NotFound();
            }

            var features = await _context.Features.FirstOrDefaultAsync(m => m.Id == id);
            if (features == null){
                return NotFound();
            }
            return View(features);
        }

        public IActionResult Create(){
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id, Description, Size, Quality, Actors, Country")] Features features){
            if (ModelState.IsValid){
                _context.Add(features);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(features);
        }

        public async Task<IActionResult> Edit(string id){
            if (id == null){
                return NotFound();
            }
            var features = await _context.Features.FindAsync(id);
            if (features == null){
                return NotFound();
            }
            return View(features);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id, Description, Size, Quality, Actors, Country")] Features features){
            if (id != features.Id){
                return NotFound();
            }

            if (ModelState.IsValid){
                try{
                    _context.Update(features);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException){
                    if (!FeaturesExists(features.Id)){
                        return NotFound();
                    }
                    else{
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(features);
        }
        public async Task<IActionResult> Delete(string id){
            if (id == null){
                return NotFound();
            }

            var features = await _context.Features.FirstOrDefaultAsync(m => m.Id == id);
            if (features == null){
                return NotFound();
            }
            return View(features);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id){
            var features = await _context.Features.FindAsync(id);
            _context.Features.Remove(features);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FeaturesExists(string id){
            return _context.Features.Any(e => e.Id == id);
        }
    }
}
