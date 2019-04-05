using System.Collections.Generic;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels;
using System.Data.Entity;
using System.Linq;
using System;

namespace Vidly.Controllers
{
    public class MoviesController : Controller
    {
        private ApplicationDbContext _context;


        public MoviesController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            _context.Dispose();
        }

        public ActionResult New()
        {
            IEnumerable<Genre> genres = _context.Genres.ToList();
            MovieFormViewModel viewModel = new MovieFormViewModel { Genres = genres, Movie = new Movie() };
            return View("MovieForm",viewModel);
        }

        public ActionResult Edit(int id)
        {
            IEnumerable<Genre> genres = _context.Genres.ToList();
            Movie movie = _context.Movies.SingleOrDefault(m => m.Id == id);
            if (movie == null)
                return HttpNotFound();
            MovieFormViewModel viewModel = new MovieFormViewModel { Genres = genres, Movie = movie };
            return View("MovieForm",viewModel);
        }

        // POST: Movies/New
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Movie movie)
        {
            if (!ModelState.IsValid)
            {
                IEnumerable<Genre> genres = _context.Genres.ToList();
                MovieFormViewModel viewModel = new MovieFormViewModel { Genres = genres, Movie = movie };
                return View("MovieForm", viewModel);
            }
            if (movie.Id == 0)// Create   
            {
                movie.Added = DateTime.Now;
                _context.Movies.Add(movie);
            }
            else //update
            {
                var movieInDb = _context.Movies.SingleOrDefault(m => m.Id == movie.Id);
                if (movieInDb == null)
                    return HttpNotFound();
                movieInDb.Name = movie.Name;
                movieInDb.Stock = movie.Stock;
                movieInDb.GenreId = movie.GenreId;
                movieInDb.ReleaseDate = movie.ReleaseDate;

            }
            _context.SaveChanges();
            return RedirectToAction("Index", "Movies");
        }

        public ActionResult Detail(int id)
        {
            Movie movie = _context.Movies.Include(m=>m.Genre).SingleOrDefault(m=>m.Id==id);
            if (movie == null)
                return HttpNotFound();
            return View(movie);
        }

        public ActionResult Index()
        {
            MoviesViewModel _MoviesViewModel = new MoviesViewModel { Movies = _context.Movies.Include(m => m.Genre).ToList() };
            return View(_MoviesViewModel);
        }

    }
}