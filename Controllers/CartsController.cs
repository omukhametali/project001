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
    public class CartsController : Controller{
        private readonly ApplicationDbContext _context;

        public CartsController(ApplicationDbContext context){
            _context = context;
        }

        public async Task<IActionResult> Index(){
            return View(await _context.Cart.ToListAsync());
        }

        public async Task<IActionResult> Details(string id){
            if (id == null){
                return NotFound();
            }
            var cart = await _context.Cart.FirstOrDefaultAsync(m => m.Id == id);
            if (cart == null){
                return NotFound();
            }
            return View(cart);
        }

        public IActionResult Create(){
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id")] Cart cart){
            if (ModelState.IsValid){
                _context.Add(cart);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(cart);
        }

        public async Task<IActionResult> Edit(string id){
            if (id == null){
                return NotFound();
            }
            var cart = await _context.Cart.FindAsync(id);
            if (cart == null){
                return NotFound();
            }
            return View(cart);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id")] Cart cart){
            if (id != cart.Id){
                return NotFound();
            }
            if (ModelState.IsValid){
                try{
                    _context.Update(cart);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException){
                    if (!CartExists(cart.Id)){
                        return NotFound();
                    }
                    else{
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(cart);
        }

        public async Task<IActionResult> Delete(string id){
            if (id == null){
                return NotFound();
            }
            var cart = await _context.Cart.FirstOrDefaultAsync(m => m.Id == id);
            if (cart == null){
                return NotFound();
            }
            return View(cart);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id){
            var cart = await _context.Cart.FindAsync(id);
            _context.Cart.Remove(cart);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CartExists(string id){
            return _context.Cart.Any(e => e.Id == id);
        }
    }
}
