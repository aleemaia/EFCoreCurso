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
    public class BatalhaController : ControllerBase {
        private readonly IEFCoreRepository _repo;

        public BatalhaController(IEFCoreRepository repo) {
            _repo = repo;
        }

        // GET: api/<BatalhaController>
        [HttpGet]
        public async Task<IActionResult> Get() {
            try {
                var batalhas = await _repo.GetAllBatalhas();
                return Ok(batalhas);
            }
            catch (Exception e) {

                return BadRequest($"Erro: {e.Message}");
            }
        }

        // GET api/<BatalhaController>/5
        [HttpGet("{id}", Name = "GetBatalha")]
        public async Task<IActionResult> Get(int id) {
            try {
                var batalha = await _repo.GetBatalhaById(id, true);
                return Ok(batalha);
            }
            catch (Exception e) {

                return BadRequest($"Erro: {e.Message}");
            }
        }

        // POST api/<BatalhaController>
        [HttpPost]
        public async Task<IActionResult> Post(Batalha model) {
            try {
                _repo.Add(model);

                if (await _repo.SaveChangeAsync()) {
                    return Ok("Sucess!");
                }
            }
            catch (Exception e) {

                return BadRequest($"Erro: {e.Message}");
            }

            return BadRequest("Denied!");
        }

        // PUT api/<BatalhaController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Batalha model) {
            try {
                var batalha = await _repo.GetBatalhaById(id);

                if (batalha != null) {
                    _repo.Update(model);

                    if (await _repo.SaveChangeAsync()) {
                        return Ok("Sucess!");
                    }
                }
            }
            catch (Exception e) {

                return BadRequest($"Erro: {e.Message}");
            }

            return BadRequest("Não Encontrado!");
        }
    
        // DELETE api/<BatalhaController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id) {
            try {
                var batalha = await _repo.GetBatalhaById(id);

                if (batalha != null) {
                    _repo.Delete(batalha);
                    
                    if(await _repo.SaveChangeAsync()) {
                        return Ok("Sucess!");
                    }
                }
            }
            catch (Exception e) {

                return BadRequest($"Erro: {e.Message}");
            }

            return BadRequest("Denied!");
        }
    }
}
