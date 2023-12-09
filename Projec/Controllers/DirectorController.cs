﻿using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.Metrics;
using Microsoft.EntityFrameworkCore;
using Projec.Dtos;
using Microsoft.AspNetCore.Authorization;
using MandD;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Projec.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DirectorController : ControllerBase
    {
        // GET: api/<DirecController>
        private readonly MoviesandDirectorsContext _db;

        public DirectorController(MoviesandDirectorsContext db)
        {
            _db = db;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            /*List<Director> directors = await _db.Directors.Include(d => d.Movies).ToListAsync();

            List<DirectorDto> directorDtos = directors.Select(d => new DirectorDto
            {
                DirectorId = d.DirectorId,
                Name = d.Name,
                Nationality = d.Nationality,
                Email = d.Email,
                Age = d.Age,
                Movies = d.Movies.Select(m => new MovieDto
                {
                    Title = m.Title
                }).ToList()
            }).ToList();

            return Ok(directorDtos);*/

            /* List<Director> directors = await _db.Directors.Include(d => d.Movies).ToListAsync();

             List<string> directorMovieTitles = directors.SelectMany(d => d.Movies.Select(m => m.Title)).ToList();

             return Ok(directorMovieTitles);*/

            List<Director> directors = await _db.Directors.Include(d => d.Movies).ToListAsync();

            List<DirectorDto> directorDtos = directors.Select(d => new DirectorDto
            {
                DirectorId = d.DirectorId,
                Name = d.Name,
                Movies = d.Movies.Select(m => new MovieDto { Title = m.Title }).ToList()
            }).ToList();

            return Ok(directorDtos);
        }


        // GET api/<DirecController>/5
        
        [HttpGet("{directorId}")]
        public async Task<IActionResult> GetDirectorWithMovies(int directorId)
        {
            Director director = await _db.Directors.Include(d => d.Movies).FirstOrDefaultAsync(d => d.DirectorId == directorId);

            if (director == null)
            {
                return NotFound($"Director not found with ID: {directorId}");
            }

            List<MovieDto> movieDtos = director.Movies.Select(m => new MovieDto
            {
                MovieId = m.MovieId,
                Title = m.Title,
                ReleaseDate = m.ReleaseDate
            }).ToList();

            DirectorDto directorDto = new DirectorDto
            {
                DirectorId = director.DirectorId,
                Name = director.Name,
                Nationality = director.Nationality,
                Email = director.Email,
                Age = director.Age,
                Movies = movieDtos
            };

            return Ok(directorDto);
        }


        // POST api/<DirecController>
        [HttpPost("{directorId}/movies")]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddMovieToDirector(int directorId, [FromBody] MovieDto newMovieDto)
        {
            Director director = await _db.Directors.Include(d => d.Movies).FirstOrDefaultAsync(d => d.DirectorId == directorId);

            /*if (!User.IsInRole("Director") && !User.IsInRole("Admin"))
            {
                Console.WriteLine("Unauthorized access: User does not have required role (Director or Admin)");
                return Forbid(); // Return 403 Forbidden status
            }*/

            if (director == null)
            {
                return NotFound($"Director not found with ID: {directorId}");
            }

            // Check if the movie with the same name already exists for the director
            bool movieExists = director.Movies.Any(m => m.Title == newMovieDto.Title);

            if (movieExists)
            {
                return BadRequest($"Movie with the name '{newMovieDto.Title}' already exists for this director.");
            }

            // Create a new Movie entity from the MovieDto
            Movie movie = new Movie
            {
                MovieId = newMovieDto.MovieId,
                Title = newMovieDto.Title,
                ReleaseDate = newMovieDto.ReleaseDate,
                Budget = newMovieDto.Budget
                
            };

            // Add the new movie to the director's list of movies
            director.Movies.Add(movie);

            // Save changes to the database
            await _db.SaveChangesAsync();

            // Return the updated director information
            List<MovieDto> movieDtos = director.Movies.Select(m => new MovieDto
            {
                MovieId = m.MovieId,
                Title = m.Title,
                Description = m.Description,
                ReleaseDate = m.ReleaseDate,
                Budget = m.Budget,
                Genre = m.Genre,
                Collections = m.Collections
            }).ToList();

            DirectorDto directorDto = new DirectorDto
            {
                DirectorId = director.DirectorId,
                Name = director.Name,
                Nationality = director.Nationality,
                Email = director.Email,
                Age = director.Age,
                Movies = movieDtos
            };

            return Ok(directorDto);
        }


        // PUT api/<DirecController>/5
        [HttpPut("UpdateMovieInDirector/{directorId}/{movieTitle}")]
        public async Task<IActionResult> UpdateMovieInDirector(int directorId, string Title, [FromBody] MovieDto updatedMovieDto)
        {
            Director director = await _db.Directors.Include(d => d.Movies).FirstOrDefaultAsync(d => d.DirectorId == directorId);

            if (director == null)
            {
                return NotFound($"Director not found with ID: {directorId}");
            }

            // Find the movie with the specified Name
            Movie movieToUpdate = director.Movies.FirstOrDefault(m => m.Title == Title);

            if (movieToUpdate == null)
            {
                return NotFound($"Movie not found with Title: {Title} for Director ID: {directorId}");
            }

            // Update the movie details
            movieToUpdate.Title = updatedMovieDto.Title;
            movieToUpdate.ReleaseDate = updatedMovieDto.ReleaseDate;
            movieToUpdate.Budget = updatedMovieDto.Budget;
            movieToUpdate.Collections = updatedMovieDto.Collections;
            movieToUpdate.Genre = updatedMovieDto.Genre;

            // Save changes to the database
            await _db.SaveChangesAsync();

            // Return the updated movie information
            MovieDto updatedMovieDtoResponse = new MovieDto
            {
                Title = movieToUpdate.Title,
                ReleaseDate = movieToUpdate.ReleaseDate,
                Budget = movieToUpdate.Budget,
                Collections = movieToUpdate.Collections,
                Genre = movieToUpdate.Genre,
            };

            return Ok(updatedMovieDtoResponse);
        }


        // DELETE api/<DirectorController>/5
        [HttpDelete("DeleteMovieFromDirector/{directorId}/{movieTitle}")]
        public async Task<IActionResult> DeleteMovieFromDirector(int directorId, string Title)
        {
            Director director = await _db.Directors.Include(d => d.Movies).FirstOrDefaultAsync(d => d.DirectorId == directorId);

            if (director == null)
            {
                return NotFound($"Director not found with ID: {directorId}");
            }

            // Find the movie with the specified ID
            Movie movieToDelete = director.Movies.FirstOrDefault(m => m.Title == Title);

            if (movieToDelete == null)
            {
                return NotFound($"Movie not found with Name: {Title} for Director ID: {directorId}");
            }

            // Remove the movie from the director's list of movies
            director.Movies.Remove(movieToDelete);

            // Save changes to the database
            await _db.SaveChangesAsync();

            // Return a success message or any other relevant information
            return Ok($"Movie with Name: {Title} deleted from Director ID: {directorId}");
        }

    }
}
