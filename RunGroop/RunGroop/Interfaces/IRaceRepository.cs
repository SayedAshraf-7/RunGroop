using RunGroop.Models;

namespace RunGroop.Interfaces
{
    public interface IRaceRepository
    {
        Task<IEnumerable<Race>> GetAllRaces();
        Task<Race> GetRaceByIdAsnc(int id);
        Task<IEnumerable<Race>> GetRacesByCity(string City);
        bool AddRace(Race club);
        bool UpdateClub(Race club);
        bool DeleteClub(Race club);
        bool Save();
    }
}
