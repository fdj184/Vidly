using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Security;
using Vidly.Dtos;
using Vidly.Models;

namespace Vidly.Controllers.Api
{
    public class RentalController : ApiController
    {
        private ApplicationDbContext _context;

        public RentalController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        // GET /api/rental
        [HttpGet]
        public IHttpActionResult GetRentals()
        {
            var movieIds = _context.Movies.Select(m => m.Id).ToList();
            var rentalDto = new RentalDto
            {
                CustomerId = 1,
                MovieIds = movieIds
            };

            return Ok(rentalDto);
        }

        // POST /api/rental
        [HttpPost]
        public IHttpActionResult AddRental(RentalDto rentalDto)
        {
            var customer = _context.Customers.SingleOrDefault(c => c.Id == rentalDto.CustomerId);

            if (customer == null)
                return BadRequest("CustomerId is not found.");

            foreach (var movieId in rentalDto.MovieIds)
            {
                var movie = _context.Movies.SingleOrDefault(m => m.Id == movieId);

                if (movie == null)
                    return BadRequest("MovieId is not found.");

                if (movie.NumberAvalible <= 0)
                    return BadRequest($"Not enough amount of {movie.Name}.");

                movie.NumberAvalible--;

                var rental = new Rental
                {
                    Customer = customer,
                    Movie = movie,
                    DateRented = DateTime.Now
                };

                _context.Rentals.Add(rental);
            }

            _context.SaveChanges();

            return Ok();
        }
    }
}
