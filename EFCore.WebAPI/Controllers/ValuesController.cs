using System.Linq;
using EFCore.Domain;
using EFCore.Repo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EFCore.WebAPI.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase {

        public readonly HeroiContext _context;
        public ValuesController(HeroiContext context) {
            _context = context;
        }
        // GET api/values
        [HttpGet ("Filtro/{nome}")]
        public ActionResult GetFiltro(string nome) {
            
            // var listaHeroi = (from heroi in _context.Herois
            //                   where heroi.Nome.Contains(nome)
            //                   select heroi).ToList();

            var listaHeroi = (from heroi in _context.Herois
                              where EF.Functions.Like(heroi.Nome, $"%{nome}%")
                              orderby heroi.Id
                              select heroi).FirstOrDefault();

            return Ok(listaHeroi);
        }

        // GET api/values/5
        [HttpGet("Atualizar/{nameHero}")]
        public ActionResult Get(string nameHero) {

            var heroi = _context.Herois
                        .Where (h => h.Id == 3)
                        .FirstOrDefault();

            heroi.Nome = "Homem Aranha";

            // _context.Herois.Add(heroi);
            _context.SaveChanges();

            return Ok();
        }

        // GET api/values/5
        [HttpGet("AddRange")]
        public ActionResult GetAddRange() {

            _context.AddRange(
                new Heroi { Nome = "Capitão América" },
                new Heroi { Nome = "Doutor Estranho" },
                new Heroi { Nome = "Pantera Negra" },
                new Heroi { Nome = "Viúva Negra" },
                new Heroi { Nome = "Hulk" },
                new Heroi { Nome = "Gavião Arqueiro" },
                new Heroi { Nome = "Capitã Marvel" }
                );
            _context.SaveChanges();

            return Ok();
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value) {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value) {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id) {
        }
    }
}