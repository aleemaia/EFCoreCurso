using EFCore.Domain;
using EFCore.Repo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace EFCore.WebAPI.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class HeroiController : ControllerBase {

        private readonly HeroiContext _context;
        public HeroiController(HeroiContext context) {
            _context = context;
        }

        // GET: api/<HeroiController>
        [HttpGet]
        public ActionResult Get() {

            try {
                return Ok(new Heroi());
            }
            catch (Exception e) {

                return BadRequest($"Erro: {e.Message}");
            }
        }

        // GET api/<HeroiController>/5
        [HttpGet("{id}")]
        public ActionResult Get(int id) {
            return Ok("value");
        }

        // POST api/<HeroiController>
        [HttpPost]
        public ActionResult Post(Heroi model) {
            try {
                _context.Herois.Add(model);
                _context.SaveChanges();

                return Ok("BAZINGA");
            }
            catch (Exception e) {

                return BadRequest($"Erro: {e.Message}");
            }
        }

        // PUT api/<HeroiController>/5
        [HttpPut("{id}")]
        public ActionResult Put(int id, Heroi model) {
            try {
                // if (_context.Herois.Find(id) != null) -> Find - trava o objeto e não deixa alterar
                
                if (_context.Herois.AsNoTracking().FirstOrDefault(h => h.Id == id) != null) {
                    
                    _context.Herois.Update(model);
                    _context.SaveChanges();

                    return Ok("BAZINGA");
                }

                return Ok("Não Encontrado!");

            }
            catch (Exception e) {

                return BadRequest($"Erro: {e.Message}");
            }
        }

        // DELETE api/<HeroiController>/5
        [HttpDelete("{id}")]
        public void Delete(int id) {
        }
    }
}
