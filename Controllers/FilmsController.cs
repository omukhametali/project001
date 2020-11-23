using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using project001.Models;
using project001.Data;

namespace project001.Controllers
{
    public class FilmsController : Controller{
        private readonly ApplicationDbContext _context;

        public FilmsController(ApplicationDbContext context){
            _context = context;
        }

        public async Task<IActionResult> Index(){
            return View(await _context.Film.ToListAsync());
        }

        public async Task<IActionResult> Details(string id){
            if (id == null){
                return NotFound();
            }
            var film = await _context.Film.FirstOrDefaultAsync(m => m.Id == id);
            if (film == null){
                return NotFound();
            }
            return View(film);
        }

        public IActionResult Create(){
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id, Name, Genre, Features, Duration, Image, Year, Director")] Film film){
            if (ModelState.IsValid){
                _context.Add(film);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(film);
        }

        public async Task<IActionResult> Edit(string id){
            if (id == null){
                return NotFound();
            }
            var film = await _context.Film.FindAsync(id);
            if (film == null){
                return NotFound();
            }
            return View(film);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id, Name, Genre, Features, Duration, Image, Year, Director")] Film film){
            if (id != film.Id){
                return NotFound();
            }

            if (ModelState.IsValid){
                try{
                    _context.Update(film);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException){
                    if (!ItemExists(film.Id)){
                        return NotFound();
                    }
                    else{
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(film);
        }

        public async Task<IActionResult> Delete(string id){
            if (id == null){
                return NotFound();
            }
            var film = await _context.Film.FirstOrDefaultAsync(m => m.Id == id);
            if (film == null){
                return NotFound();
            }
            return View(film);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id){
            var film = await _context.Film.FindAsync(id);
            _context.Film.Remove(film);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ItemExists(string id){
            return _context.Film.Any(e => e.Id == id);
        }
    }
}