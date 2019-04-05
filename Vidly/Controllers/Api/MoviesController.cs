using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Vidly.DTO;
using Vidly.Models;

namespace Vidly.Controllers.Api
{
    public class MoviesController : ApiController
    {
        private ApplicationDbContext _context;

        public MoviesController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
            base.Dispose(disposing);
        }

        /* @Route    POST api/movies/
         * @Desc     Creates a new Movie
         */
         [HttpPost]
         public IHttpActionResult CreateMovie(MovieDTO movieDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            movieDTO.Added = DateTime.Now;
            Movie movie = Mapper.Map<MovieDTO, Movie>(movieDTO);
            _context.Movies.Add(movie);
            _context.SaveChanges();
            return Created(new Uri(Request.RequestUri+"/"+movie.Id),Mapper.Map<Movie, MovieDTO>(movie));
        }

        /* @Route    GET api/movies/
         * @Desc     Gets all movies registered.
         */
        public IHttpActionResult GetMovies()
        {
            IEnumerable<MovieDTO> movies = _context.Movies.ToList().Select(Mapper.Map<Movie,MovieDTO>);
            return Ok(movies);
        }

        /* @Route    PUT api/movies/:id
        * @Desc      Updates a movie
        */
        [HttpPut]
        public IHttpActionResult UpdateMovie(int id, MovieDTO movieDTO)
        {
            if (id == 0 || !ModelState.IsValid)
                return BadRequest();
            Movie movie = _context.Movies.SingleOrDefault(m => m.Id == id);
            if (movie == null)
                return NotFound();
            Mapper.Map(movieDTO,movie);
            _context.SaveChanges();
            return Ok();
        }

       /* @Route    DELETE api/movies/:id
       * @Desc      Delete a movie
       */
       [HttpDelete]
       public IHttpActionResult DeleteMovie(int id)
        {
            if (id == 0)
                return BadRequest();
            Movie movie = _context.Movies.SingleOrDefault(m => m.Id == id);
            if (movie == null)
                return NotFound();
            _context.Movies.Remove(movie);
            _context.SaveChanges();
            return Ok();
        }

    }
}
