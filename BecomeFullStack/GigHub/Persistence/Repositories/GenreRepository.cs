using GigHub.Interfaces;
using GigHub.Models;
using System.Collections.Generic;
using System.Linq;

namespace GigHub.Repositories
{
    public class GenreRepository : IGenreRepository
    {
        private readonly ApplicationDbContext _dataBase;
        public GenreRepository(ApplicationDbContext database)
        {
            _dataBase = database;
        }

        public IEnumerable<Genre> GetAllGenres()
        {
            return _dataBase.Genres.ToList();
        }
    }
}