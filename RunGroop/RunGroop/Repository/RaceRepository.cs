using RunGroop.Data;
using RunGroop.Interfaces;
using RunGroop.Models;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace RunGroop.Repository
{
    public class RaceRepository : IRaceRepository
    {
        private readonly ApplicationDbContext _context;

        public RaceRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public bool AddRace(Race race)
        {
            _context.Races.Add(race);
            return Save();
        }

        public bool DeleteClub(Race race)
        {
            _context.Races.Remove(race);
            return Save();
        }

        public async Task<IEnumerable<Race>> GetAllRaces()
        {
            return await _context.Races.ToListAsync();
        }

        public async Task<Race> GetByIdAsyncNoTracking(int id)
        {
            return await _context.Races.AsNoTracking().FirstOrDefaultAsync(Race => Race.Id == id);
        }

        public async Task<Race> GetRaceByIdAsnc(int id)
        {
            return await _context.Races.FirstOrDefaultAsync(Race => Race.Id == id);
        }

        public async Task<IEnumerable<Race>> GetRacesByCity(string City)
        {
            return await _context.Races.Where(Race => Race.Address.City.Contains(City)).ToListAsync();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0;
        }

        public bool UpdateClub(Race Race)
        {
            _context.Update(Race);
            return Save();
        }
    }
}
