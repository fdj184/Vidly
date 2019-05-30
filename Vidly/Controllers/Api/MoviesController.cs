using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using Microsoft.Ajax.Utilities;
using Vidly.Dtos;
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
        }

        // GET /api/movies
        public IHttpActionResult GetMovies()
        {
            var moviesDto = _context.Movies.ToList().Select(Mapper.Map<Movie, MovieDto>);

            return Ok(moviesDto);
        }

        // GET /api/movies/{id}
        public IHttpActionResult GetMovie(int id)
        {
            var movieInDB = _context.Movies.SingleOrDefault(m => m.Id == id);

            if (movieInDB == null)
                return NotFound();

            var movieDto = Mapper.Map<Movie, MovieDto>(movieInDB);

            return Ok(movieDto);
        }

        // POST /api/movies
        [HttpPost]
        public IHttpActionResult CreateMovie(MovieDto movieDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var movie = Mapper.Map<MovieDto, Movie>(movieDto);

            _context.Movies.Add(movie);
            _context.SaveChanges();

            // update movieDto.Id after movie.Id auto generated
            movieDto.Id = movie.Id;

            return Created(new Uri($@"{Request.RequestUri}/{movieDto.Id}"), movieDto);
        }

        // PUT /api/movies/{id}
        [HttpPut]
        public IHttpActionResult UpdateMovie(int id, MovieDto movieDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var movieInDB = _context.Movies.SingleOrDefault(m => m.Id == id);

            if (movieInDB == null)
                return NotFound();

            Mapper.Map(movieDto, movieInDB);
            _context.SaveChanges();

            return Ok();
        }

        // DELETE /api/movies/{id}
        [HttpDelete]
        public IHttpActionResult DeleteMovie(int id)
        {
            var movieInDB = _context.Movies.SingleOrDefault(m => m.Id == id);

            if (movieInDB == null)
                return NotFound();

            _context.Movies.Remove(movieInDB);
            _context.SaveChanges();

            return Ok();
        }
    }
}
