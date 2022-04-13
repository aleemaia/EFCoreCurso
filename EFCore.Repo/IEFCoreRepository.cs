using EFCore.Domain;
using System.Threading.Tasks;

namespace EFCore.Repo {
    public interface IEFCoreRepository {
        void Add<T>(T entity) where T : class;
        void Update<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        
        Task<bool> SaveChangeAsync();
        Task<Heroi[]> GetAllHerois(bool incluirBatalha = false); // Outra opção Task<IEnumerable<Heroi>> GetAllHerois();
        Task<Heroi> GetHeroiById(int id, bool incluirBatalha = false);
        Task<Heroi[]> GetAllHeroisByNome(string nome, bool incluirBatalha = false);
        Task<Batalha[]> GetAllBatalhas(bool incluirHeroi = false); 
        Task<Batalha> GetBatalhaById(int id, bool incluirHeroi = false);
    }
}
