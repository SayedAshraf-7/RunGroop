using RunGroop.Data;
using RunGroop.Interfaces;
using RunGroop.Models;
using Microsoft.EntityFrameworkCore;

namespace RunGroop.Repository
{
    public class ClubRepository : IClubRepository
    {
        private readonly ApplicationDbContext _context;

        public ClubRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public bool AddClub(Club club)
        {
            _context.Add(club);
            return Save();
        }

        public bool DeleteClub(Club club)
        {
            _context.Remove(club);
            return Save();
        }

        public async Task<IEnumerable<Club>> GetAllClubs()
        {
            return await _context.Clubs.ToListAsync();
        }

        public async Task<Club> GetClubByIdAsnc(int id)
        {
            return await _context.Clubs.Include(c => c.Address).FirstOrDefaultAsync((c) => c.Id == id);
        }

        public async Task<IEnumerable<Club>> GetClubsByCity(string City)
        {
            return await _context.Clubs.Include(c => c.Address).Where((c) => c.Address.City.Contains(City)).ToListAsync();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0;
        }

        public bool UpdateClub(Club club)
        {
            _context.Update(club);
            return Save();
        }
    }
}
