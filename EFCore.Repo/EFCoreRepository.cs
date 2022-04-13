using EFCore.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCore.Repo {
    public class EFCoreRepository : IEFCoreRepository {
        public readonly HeroiContext _context;
        public EFCoreRepository(HeroiContext context) {
            _context = context;
        }

        public void Add<T>(T entity) where T : class {
            _context.Add(entity);
        }

        public void Update<T>(T entity) where T : class {
            _context.Update(entity);
        }

        public void Delete<T>(T entity) where T : class {
            _context.Remove(entity);
        }

        public async Task<bool> SaveChangeAsync() {
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<Heroi[]> GetAllHerois(bool incluirBatalha = false) {
            // Buscando o Heroi + Armas + Indentidade 
            IQueryable<Heroi> querry = _context.Herois
                .Include(h => h.Identidade)
                .Include(h => h.Armas);

            // Adicionando: Batalha
            if (incluirBatalha) {
                querry = querry.Include(h => h.HeroisBatalhas)
                               .ThenInclude(heroib => heroib.Batalha);
            }
            //Ordenando por ID
            querry = querry.AsNoTracking().OrderBy(h => h.Id);

            return await querry.ToArrayAsync();
        }

        public async Task<Heroi> GetHeroiById(int id, bool incluirBatalha = false) {
            IQueryable<Heroi> querry = _context.Herois
                .Include(h => h.Identidade)
                .Include(h => h.Armas);

            if (incluirBatalha) {
                querry = querry.Include(h => h.HeroisBatalhas)
                               .ThenInclude(heroib => heroib.Batalha);
            }

            querry = querry.AsNoTracking().OrderBy(h => h.Id);

            return await querry.FirstOrDefaultAsync(h => h.Id == id);
        }

        public async Task<Heroi[]> GetAllHeroisByNome(string nome, bool incluirBatalha = false) {
            IQueryable<Heroi> querry = _context.Herois
                .Include(h => h.Identidade)
                .Include(h => h.Armas);


            if (incluirBatalha) {
                querry = querry.Include(h => h.HeroisBatalhas)
                               .ThenInclude(heroib => heroib.Batalha);
            }

            querry = querry.AsNoTracking()
                           .Where(h => h.Nome.Contains(nome))
                           .OrderBy(h => h.Id);

            return await querry.ToArrayAsync();
        }

        public async Task<Batalha[]> GetAllBatalhas(bool incluirHeroi = false) {
            IQueryable<Batalha> querry = _context.Batalhas;

            if (incluirHeroi) {
                querry = querry.Include(h => h.HeroisBatalhas)
                               .ThenInclude(heroib => heroib.Heroi);
            }
            querry = querry.AsNoTracking().OrderBy(h => h.Id);

            return await querry.ToArrayAsync();
        }


        public async Task<Batalha> GetBatalhaById(int id, bool incluirHeroi = false) {
            IQueryable<Batalha> querry = _context.Batalhas;

            if (incluirHeroi) {
                querry = querry.Include(h => h.HeroisBatalhas)
                               .ThenInclude(heroib => heroib.Heroi);
            }
            querry = querry.AsNoTracking().OrderBy(h => h.Id);

            return await querry.FirstOrDefaultAsync(h => h.Id == id);
        }
    }
}
    