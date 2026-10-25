
using RunGroop.Data;
using RunGroop.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace RunGroop.Interfaces
{
    public interface IClubRepository
    {
        
        Task<IEnumerable<Club>> GetAllClubs();
        Task<Club> GetClubByIdAsnc(int id);

        Task<IEnumerable<Club>> GetClubsByCity(string City);

        Task<Club> GetByIdAsyncNoTracking(int id);
        bool AddClub(Club club);
        bool UpdateClub(Club club);
        bool DeleteClub(Club club);
        bool Save();

    }
}
