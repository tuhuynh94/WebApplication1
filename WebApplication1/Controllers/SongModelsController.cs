using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;
using WebApplication1.Models.ViewModels;

namespace WebApplication1.Controllers
{
    public class SongModelsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private ISongRepository repository;
        public int PageSize = 15;

        public SongModelsController(ISongRepository repo, ApplicationDbContext context)
        {
            repository = repo;
            _context = context;
        }

        // GET: SongModels
        //public async Task<ViewResult> Index(string category, int page = 1)
    //=> View(new SongListViewModel
    //{
    //    Songs = await repository.SongsAsync(category, PageSize, page),
    //    PagingInfo = new PagingInfo
    //    {
    //        CurrentPage = page,
    //        ItemsPerPage = PageSize,
    //        TotalItems = category == null ? await repository.SongCountAsync() :
    //            await repository.SongCountAsync(category)
    //    },
    //    CurrentCategory = category
    //});
        public async Task<IActionResult> Index(string category, string searchText, int page = 1, string sortOrder = "name_asc")
        {


            IEnumerable<SongModel> songs = await repository.SongsAsync();

            if (!String.IsNullOrEmpty(searchText))
            {
                songs = repository.SearchText(searchText);
            }
            ViewData["searchText"] = searchText;    //?? ViewData["searchString"]
            ViewData["sortOrder"] = sortOrder;

            int pageSize = 5;
            //return View(await PaginatedList<Student>.CreateAsync(students.AsNoTracking(), page ?? 1, pageSize));
            return View(new SongListViewModel
            {

                Songs = songs.Skip((page - 1) * pageSize)

                     .Take(pageSize),

            PagingInfo = new PagingInfo
                {

                    CurrentPage = page,

                    ItemsPerPage = pageSize,

                    TotalItems = songs.Count()

                }

            });
        }
        // GET: SongModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var songModel = await _context.Songs
                .SingleOrDefaultAsync(m => m.id == id);
            if (songModel == null)
            {
                return NotFound();
            }

            return View(songModel);
        }

        // GET: SongModels/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: SongModels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,Category,Name,Description,Price")] SongModel songModel, string Category)
        {
            if (ModelState.IsValid)
            {
                songModel.Category = _context.Categories.FirstOrDefault(c => c.Name == Category);
                _context.Add(songModel);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(songModel);
        }

        // GET: SongModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var songModel = await _context.Songs.SingleOrDefaultAsync(m => m.id == id);
            if (songModel == null)
            {
                return NotFound();
            }
            return View(songModel);
        }

        // POST: SongModels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,Name,Description,Price")] SongModel songModel)
        {
            if (id != songModel.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(songModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SongModelExists(songModel.id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            return View(songModel);
        }

        // GET: SongModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var songModel = await _context.Songs
                .SingleOrDefaultAsync(m => m.id == id);
            if (songModel == null)
            {
                return NotFound();
            }

            return View(songModel);
        }

        // POST: SongModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var songModel = await _context.Songs.SingleOrDefaultAsync(m => m.id == id);
            _context.Songs.Remove(songModel);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool SongModelExists(int id)
        {
            return _context.Songs.Any(e => e.id == id);
        }
    }
}
