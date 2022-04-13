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
        private readonly IEFCoreRepository _context;

        public HeroiController(IEFCoreRepository context) {
            _context = context;
        }

        // GET: api/<HeroiController>
        [HttpGet]
        public async Task<IActionResult> Get() {
            try {
                var herois =  await _context.GetAllHerois();
                return Ok(herois);
            }
            catch (Exception e) {

                return BadRequest($"Erro: {e.Message}");
            }
        }

        // GET api/<HeroiController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id) {
            try {
                var heroi = await _context.GetHeroiById(id, true);
                return Ok(heroi);
            }
            catch (Exception e) {

                return BadRequest($"Erro: {e.Message}");
            }
        }

        // POST api/<HeroiController>
        [HttpPost]
        public async Task<IActionResult> Post(Heroi model) {
            try {
                _context.Add(model);

                if(await _context.SaveChangeAsync()){
                    return Ok("Sucess!");
                }
            }
            catch (Exception e) {

                return BadRequest($"Erro: {e.Message}");
            }

            return BadRequest("Denied!");
        }

        // PUT api/<HeroiController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Heroi model) {
            try {
                var heroi = await _context.GetBatalhaById(id);

                if (heroi != null) {
                    _context.Update(model);

                    if (await _context.SaveChangeAsync()) {
                        return Ok("Sucess!");
                    }
                }
            }
            catch (Exception e) {

                return BadRequest($"Erro: {e.Message}");
            }

            return BadRequest("Não Encontrado!");
        }

        // DELETE api/<HeroiController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id) {
            try {
                var heroi = await _context.GetHeroiById(id);

                if (heroi != null) {
                    _context.Delete(heroi);

                    if (await _context.SaveChangeAsync()) {
                        return Ok("Sucess!");
                    }
                }
            }
            catch (Exception e) {

                return BadRequest($"Erro: {e.Message}");
            }

            return BadRequest("Não Encontrado!");
        }
    }
}
